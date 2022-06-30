using Microsoft.Extensions.DependencyInjection;

namespace Farsica.Framework.Test.Core.DI;

public static class  ServiceRegistry
{
    public static IServiceProvider Register()
    {
        var collection = new ServiceCollection();
        collection.RegisterContainers();
        return collection.BuildServiceProvider();
    }
    
}