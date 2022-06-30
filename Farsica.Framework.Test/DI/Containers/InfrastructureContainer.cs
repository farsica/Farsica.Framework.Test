using System.Diagnostics.CodeAnalysis;
using Farsica.Framework.Test.Common;
using Farsica.Framework.Test.Core.Selenium;
using Microsoft.Extensions.DependencyInjection;

namespace Farsica.Framework.Test.Core.DI.Containers;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
public class InfrastructureContainer : IServiceContainer
{
    public void Register(IServiceCollection collection)
    {
        collection
            .AddOptions()
            .UseTestConfiguration<SessionSettings>()
            .AddSingleton<INamedBrowserFactory, ChromeFactory>()
            .AddSingleton<INamedBrowserFactory, FirefoxFactory>()
            .AddTransient(s => new WebDriverFactory(s).Create());
    }
}