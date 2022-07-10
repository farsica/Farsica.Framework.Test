using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Farsica.Framework.Test.Action
{
	public abstract class ActionBase
	{
		internal IWebDriver? Driver { get; set; }

		//protected IWebElement? WaitUntilElementVisible(By by, int waitSeconds = 60)
		//{
		//	//WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(waitSeconds));
		//	//wait.Until(ExpectedConditions.ElementExists(by));
		//	//return wait.Until(ExpectedConditions.ElementIsVisible(by));

		//	//var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds));
		//	//return wait.Until(t => t.FindElement(by));

		//	return Driver is null ? null :
		//		new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds))
		//		.Until(ExpectedConditions.ElementToBeClickable(by));
		//}

		//protected IWebElement? WaitUntilElement(By by, int waitSeconds = 5)
		//{
		//    return Driver is not null ? new DefaultWait<IWebDriver>(Driver)
		//    {
		//        Timeout = TimeSpan.FromSeconds(waitSeconds),
		//        PollingInterval = TimeSpan.FromMilliseconds(2000)
		//    }.Until(t => t.FindElement(by)) : null;
		//}

		protected IWebElement? FindElement(By by, int waitSeconds = 60)
		{
			if (Driver is null)
			{
				return null;
			}

			var wait = new DefaultWait<ISearchContext>(Driver)
			{
				Timeout = TimeSpan.FromSeconds(waitSeconds)
			};
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
			return wait.Until(ctx =>
			{
				var elem = ctx.FindElement(by);
				return !elem.Displayed ? null : elem;
			});
		}

		protected IReadOnlyCollection<IWebElement>? FindElements(By by, int waitSeconds = 60)
		{
			if (Driver is null)
			{
				return null;
			}

			var wait = new DefaultWait<ISearchContext>(Driver)
			{
				Timeout = TimeSpan.FromSeconds(waitSeconds)
			};
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
			return wait.Until(ctx =>
			{
				var elements = ctx.FindElements(by);
				return !elements.Last().Displayed ? null : elements;
			});
		}

		protected void Click(By by, int waitSeconds = 60)
		{
			var element = FindElement(by, waitSeconds);
			element?.Click();
		}

		protected void SendKeys(By by, string? text, int waitSeconds = 60)
		{
			var element = FindElement(by, waitSeconds);
			element?.SendKeys(text);
		}
	}
}
