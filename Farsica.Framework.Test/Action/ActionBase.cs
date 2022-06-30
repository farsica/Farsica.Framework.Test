using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Farsica.Framework.Test.Action
{
    public class ActionBase
    {
        internal IWebDriver? Driver { get; set; }

        protected IWebElement? WaitUntilElementVisible(By by, int waitSeconds = 60)
        {
            return Driver is null ? null :
                new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds))
                .Until(ExpectedConditions.ElementToBeClickable(by));
        }
        
        //protected IWebElement? WaitUntilElement(By by, int waitSeconds = 5)
        //{
        //    return Driver is not null ? new DefaultWait<IWebDriver>(Driver)
        //    {
        //        Timeout = TimeSpan.FromSeconds(waitSeconds),
        //        PollingInterval = TimeSpan.FromMilliseconds(2000)
        //    }.Until(t => t.FindElement(by)) : null;
        //}

        protected IWebElement? FindElement(By by)
        {
            var asdasd = WaitUntilElementVisible(by);
            var jghjgh = asdasd?.FindElement(by);
            return jghjgh;
        }

        protected void Click(By by)
        {
            FindElement(by)?.Click();
        }

        protected void SendKeys(By by, string? text)
        {
            FindElement(by)?.SendKeys(text);
        }
    }
}
