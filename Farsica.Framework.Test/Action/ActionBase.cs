using Farsica.Framework.Test.Common;
using Farsica.Framework.Test.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics.CodeAnalysis;
using static Farsica.Framework.Test.Common.Constants;

namespace Farsica.Framework.Test.Action
{
	public abstract class ActionBase
	{
		private const int DefaultWaitSeconds = 60;
		private readonly By BodyElement = By.TagName("body");

		internal WebDriver? Driver { get; set; }
		internal SessionSettings? Settings { get; set; }

		protected bool WaitForElementReady(By by, int waitSeconds = DefaultWaitSeconds)
		{
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds));
			return wait.Until((d) => d.FindElement(by)?.Displayed ?? false);
		}

		protected IWebElement? FindElement(By by, int waitSeconds = DefaultWaitSeconds)
		{
			if (Driver is null)
			{
				return null;
			}

			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds));
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
			return wait.Until(driver =>
			{
				var element = driver.FindElement(by);
				return element.Displayed ? element : null;
			});
		}

		protected IReadOnlyCollection<IWebElement>? FindElements(By by, int waitSeconds = DefaultWaitSeconds)
		{
			if (Driver is null)
			{
				return null;
			}

			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds));
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
			return wait.Until(driver =>
			{
				var elements = driver.FindElements(by);
				return elements.Last().Displayed ? elements : null;
			});
		}

		protected void Click(By by, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			element?.Click();
		}

		protected bool SendKeys(By by, string? text, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			element.SendKeys(text);
			return true;
		}

		protected string? TakeScreenshot()
		{
			var now = DateTime.Now;
			var filePath = $"{Settings?.ScreenshotsDirectory}{now:yyyy-MM-dd-HH-mm-}{now.Ticks}.jpg";
			Driver?.GetScreenshot()?.SaveAsFile(filePath, ScreenshotImageFormat.Jpeg);

			return filePath;
		}

		protected string[]? GetCssClasses(By by, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			return element?.GetAttribute("class")?.Split(" ");
		}

		protected bool HasCssClass(By by, string? className, int waitSeconds = DefaultWaitSeconds)
		{
			var lst = GetCssClasses(by, waitSeconds);
			return lst?.Contains(className) ?? false;
		}

		protected bool SetAttribute(By by, string? attribute, string? value, int waitSeconds = DefaultWaitSeconds)
		{
			string script = "arguments[0].setAttribute(arguments[1], arguments[2])";
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			Driver?.ExecuteScript(script, element, attribute, value);
			return true;
		}

		protected string? GetAttribute(By by, string? attribute, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return null;
			}

			return element.GetAttribute(attribute);
		}

		protected bool GoTo([NotNull] Uri uri)
		{
			if (Driver is null)
			{
				return false;
			}

			Driver.Navigate().GoToUrl(uri);
			return Driver.Url == uri.ToString();
		}

		protected bool SetInputValue(By by, string? value, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			element.Click();
			Thread.Sleep(250);
			element.Clear();
			Thread.Sleep(250);
			element.SendKeys(value);

			return true;
		}

		protected string? GetInputValue(By by, int waitSeconds = DefaultWaitSeconds)
		{
			return GetAttribute(by, "value", waitSeconds);
		}

		protected bool SetDropDownByValue(By by, string? value, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			var dropdown = new SelectElement(element);
			dropdown.SelectByValue(value);

			return true;
		}

		protected bool SetDropDownByText(By by, string? text, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			var dropdown = new SelectElement(element);
			dropdown.SelectByText(text);

			return true;
		}

		protected bool FocusOnElement(By by, int waitSeconds = DefaultWaitSeconds)
		{
			string script = "arguments[0].click()";
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			(Driver as IJavaScriptExecutor)?.ExecuteScript(script, element);
			return true;
		}

		protected bool ToggleCheckbox(By by, int waitSeconds = DefaultWaitSeconds)
		{
			string script = "arguments[0].click()";
			var element = FindElement(by, waitSeconds);
			if (element is null)
			{
				return false;
			}

			(Driver as IJavaScriptExecutor)?.ExecuteScript(script, element);
			return true;
		}

		protected bool SendKeys(Key key, By? by = null, int waitSeconds = DefaultWaitSeconds)
		{
			var element = FindElement(by ?? BodyElement, waitSeconds);
			if (element is null)
			{
				return false;
			}

			element.SendKeys(Converter.Convert(key));
			return true;
		}
	}
}
