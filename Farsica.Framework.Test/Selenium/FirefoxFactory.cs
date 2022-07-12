using Farsica.Framework.Test.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Farsica.Framework.Test.Core.Selenium;

public class FirefoxFactory : INamedBrowserFactory
{
	private readonly SessionSettings options;
	private readonly string[] implicitlyDownloadedFileTypes =
		new[] {
			"text/csv"
			,"text/plain"
		};

	public FirefoxFactory(SessionSettings options)
	{
		this.options = options;
	}

	public IWebDriver Create()
	{
#pragma warning disable CA2000 // Dispose objects before losing scope
		var driverService = FirefoxDriverService.CreateDefaultService($"{Environment.CurrentDirectory}\\Drivers");
#pragma warning restore CA2000 // Dispose objects before losing scope
		var options = new FirefoxOptions();
		if (this.options.Headless)
		{
			options.AddArgument("-headless");
		}
		options.AddArgument("-private");
		options.SetPreference("browser.download.folderList", 2);
		options.SetPreference("browser.download.dir", this.options.DownloadDirectory);
		options.SetPreference("network.cookie.cookieBehavior", 0);
		options.SetPreference("browser.helperApps.neverAsk.saveToDisk", string.Join(",", implicitlyDownloadedFileTypes));
		var driver = new FirefoxDriver(driverService, options, TimeSpan.FromSeconds(this.options.DefaultTimeoutSeconds));
		driver.Manage().Window.Maximize();
		return driver;
	}

	public BrowserType BrowserType => BrowserType.Firefox;
}