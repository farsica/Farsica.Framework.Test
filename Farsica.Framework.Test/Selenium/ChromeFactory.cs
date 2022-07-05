using Farsica.Framework.Test.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Farsica.Framework.Test.Core.Selenium;

public class ChromeFactory : INamedBrowserFactory
{
    private readonly SessionSettings options;
    public ChromeFactory(SessionSettings options)
    {
        this.options = options;
    }

    public IWebDriver Create()
    {
        var driverService = ChromeDriverService.CreateDefaultService($"{Environment.CurrentDirectory}\\Drivers");
        var options = new ChromeOptions();
        if (this.options.Headless)
        {
            options.AddArgument("headless");
        }
        options.AddArgument("--no-sandbox");
        options.AddArgument("--start-maximized");
        options.AddUserProfilePreference("download.default_directory", this.options.DownloadDirectory);
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
        options.SetLoggingPreference(LogType.Browser, LogLevel.All);

		return new ChromeDriver(driverService, options, TimeSpan.FromSeconds(this.options.DefaultTimeoutSeconds));
    }

    public BrowserType BrowserType => BrowserType.Chrome;
}