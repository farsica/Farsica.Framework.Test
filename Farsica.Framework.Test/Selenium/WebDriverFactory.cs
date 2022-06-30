using Farsica.Framework.Test.Common;
using Farsica.Framework.Test.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace Farsica.Framework.Test.Core.Selenium;

public class WebDriverFactory
{
    private readonly SessionSettings driverOptions;
    private readonly IServiceProvider serviceProvider;

    public WebDriverFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        driverOptions = this.serviceProvider.GetRequiredService<SessionSettings>();
    }

    public IWebDriver Create()
    {
        var factory = serviceProvider.GetServices<INamedBrowserFactory>().FirstOrDefault(t => t.BrowserType == driverOptions.BrowserType);
        if (factory is null)
        {
            throw new ServiceNotRegisteredException($"No factory registered for {driverOptions.BrowserType} browser.");
        }

        return factory.Create();
    }
}
