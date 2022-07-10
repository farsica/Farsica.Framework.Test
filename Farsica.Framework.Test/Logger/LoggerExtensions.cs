using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Farsica.Framework.Test.Logger
{
	public static class LoggerExtensions
	{
		public static void LogError(this ILogger logger, Exception? exception, [CallerMemberName] string? memberName = "")
		{
			logger.LogError(exception, memberName);
		}
	}
}
