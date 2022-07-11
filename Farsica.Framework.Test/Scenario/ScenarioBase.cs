using Farsica.Framework.Test.Core;
using Farsica.Framework.Test.Logger;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace Farsica.Framework.Test.Scenario
{
	[TestCaseOrderer("Farsica.Framework.Test.Data.TestPriorityOrderer", "Farsica.Framework.Test")]
	public abstract class ScenarioBase<TScenario, TAction> : IDisposable
		where TAction : Action.ActionBase, new()
	{
		protected readonly IWebDriver? Driver;
		protected readonly TAction? Action;
		protected readonly ILogger<TScenario>? Logger;

		protected bool IsDisposed { get; set; }

		public ScenarioBase(ITestOutputHelper testOutputHelper)
		{
			UiTestSession.Current.Start();
			Driver = UiTestSession.Current.Resolve<IWebDriver>();
			var url = UiTestSession.Current.Settings.ApplicationUrl;
			Driver?.Navigate().GoToUrl(url);
			Driver?.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(60));

			Action = new TAction
			{
				Driver = Driver
			};

			var loggerFactory = LoggerFactory.Create(l =>
			{
				l.AddProvider(new XunitLoggerProvider(testOutputHelper));
			});
			Logger = loggerFactory.CreateLogger<TScenario>();
		}

		~ScenarioBase()
		{
			Dispose(false);
		}

		#region IDisposable Implementation

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void CheckDisposed()
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException("Driver is already disposed and cannot be used anymore.");
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing && Driver != null)
			{
				UiTestSession.Current.CleanUp();
				Driver?.Close();
				Driver?.Quit();
				Driver?.Dispose();
			}

			IsDisposed = true;
		}

		#endregion
	}
}
