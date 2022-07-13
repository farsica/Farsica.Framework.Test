using Farsica.Framework.Test.Core;
using Farsica.Framework.Test.Data;
using OpenQA.Selenium;
using Xunit;

namespace Farsica.Framework.Test.Scenario
{
	[TestCaseOrderer("Farsica.Framework.Test.Data.TestPriorityOrderer", "Farsica.Framework.Test")]
	public abstract class ScenarioBase<TScenario, TAction> : IDisposable
		where TScenario : ScenarioBase<TScenario, TAction>, new()
		where TAction : Action.ActionBase, new()
	{
		protected IWebDriver? Driver { get; private set; }
		protected TAction? Action { get; private set; }
		//protected ILogger<TScenario>? Logger { get; private set; }

		protected bool IsDisposed { get; set; }

		//public ScenarioBase(ITestOutputHelper testOutputHelper)
		protected ScenarioBase()
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

			//var loggerFactory = LoggerFactory.Create(l =>
			//{
			//	l.AddProvider(new XunitLoggerProvider(testOutputHelper));
			//});
			//Logger = loggerFactory.CreateLogger<TScenario>();
		}

		public TNestedAction GetNestedAction<TNestedAction>()
			where TNestedAction : Action.ActionBase, new()
		{
			return new TNestedAction { Driver = Driver };
		}

		public TData? GetNestedData<TGenerator, TData>()
			where TGenerator : ITestDataGenerator<TData>, new()
			where TData : IData
		{
			return new TGenerator().GetData().FirstOrDefault();
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
				Driver.Close();
				Driver.Quit();
				Driver.Dispose();
			}

			IsDisposed = true;
		}

		#endregion
	}
}
