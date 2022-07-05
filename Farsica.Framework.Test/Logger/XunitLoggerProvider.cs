using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Farsica.Framework.Test.Logger
{
	public class XunitLoggerProvider : ILoggerProvider
	{
		private readonly ITestOutputHelper testOutputHelper;

		public XunitLoggerProvider(ITestOutputHelper testOutputHelper)
		{
			this.testOutputHelper = testOutputHelper;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new XunitLogger(testOutputHelper, categoryName);
		}

		public void Dispose()
		{
		}
	}
}
