using Farsica.Framework.Test.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Farsica.Framework.Test.Scenario
{
    [TestFixture]
    public class ScenarioBase<T>
        where T : Action.ActionBase, new()
    {
        protected IWebDriver? Driver { get; private set; }
        protected T? Action { get; private set; }

        [SetUp]
        public void Setup()
        {
            UiTestSession.Current.Start();
            Driver = UiTestSession.Current.Resolve<IWebDriver>();
            var url = UiTestSession.Current.Settings.ApplicationUrl;
            Driver?.Navigate().GoToUrl(url);
            Driver?.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(60));

            Action = new T
            {
                Driver = Driver
            };
        }

        [TearDown]
        public void CleanUp()
        {
            UiTestSession.Current.CleanUp();
            Driver?.Close();
            Driver?.Quit();
            
            //var processes = Process.GetProcessesByName("chromedriver");
            //if (processes is not null)
            //{
            //    foreach (var process in processes)
            //    {
            //        process.Kill();
            //    }
            //}
        }
    }
}
