﻿using System.Reflection;
using Farsica.Framework.Test.Common;
using Farsica.Framework.Test.Core.DI.Containers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace Farsica.Framework.Test.Core.DI;

public static class ContainerExtensions
{
    public static IServiceCollection RegisterContainers(this IServiceCollection collection)
    {
        var type = typeof(IServiceContainer);
        foreach (var container in Assembly.GetAssembly(type)!.GetTypes().Where(t => t.IsClass && type.IsAssignableFrom(t)))
        {
            //Containers should be in Farsica.Framework.Test.Core.DI.Containers namespace
            Conventions.Enforce(container,
                c => c.Namespace!.StartsWith(type.Namespace!),
           $"{container.GetFriendlyTypeName()} is not in a valid namespace.");
            var constructors = container.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            //Containers should have only default constructors.
            Conventions.Enforce(constructors,
                c => c.Length == 1 && c[0].GetParameters().Length < 1,
                $"{container.GetFriendlyTypeName()} must only have a default constructor.");
            (Activator.CreateInstance(container) as IServiceContainer)!.Register(collection);
        }
        return collection;
    }

    public static IServiceCollection UseTestConfiguration<T>(this IServiceCollection container) where T : class, new()
    {
        var filePath = Environment.GetEnvironmentVariable("SessionSettings");
        filePath = string.IsNullOrEmpty(filePath) ? "appsettings.json" : filePath;
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(filePath);
        }
        var config = new ConfigurationBuilder()
            .AddJsonFile(filePath, false, true)
            .Build();
        var settings = new T();
        new ConfigureFromConfigurationOptions<T>(config.GetSection("SessionSettings")).Configure(settings);
        return container
            .AddSingleton(config)
            .AddSingleton(settings)
			.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }
}