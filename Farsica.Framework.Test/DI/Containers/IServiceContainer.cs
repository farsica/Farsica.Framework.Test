using Microsoft.Extensions.DependencyInjection;

namespace Farsica.Framework.Test.Core.DI.Containers;

public interface IServiceContainer
{
    void Register(IServiceCollection collection);
}