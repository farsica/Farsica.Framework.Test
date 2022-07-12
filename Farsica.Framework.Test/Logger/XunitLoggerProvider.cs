using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Farsica.Framework.Test.Logger
{
	public class XunitLoggerProvider : ILoggerProvider
	{
		private readonly ITestOutputHelper testOutputHelper;

		protected bool IsDisposed { get; set; }

		public XunitLoggerProvider(ITestOutputHelper testOutputHelper)
		{
			this.testOutputHelper = testOutputHelper;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new XunitLogger(testOutputHelper, categoryName);
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
			if (!IsDisposed && disposing)
			{
			}

			IsDisposed = true;
		}

		#endregion
	}
}
