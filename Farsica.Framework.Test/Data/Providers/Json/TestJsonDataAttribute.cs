using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data.Providers.Json
{
	[DataDiscoverer("Farsica.Framework.Test.Data.Providers.Json.TestJsonDataDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class TestJsonDataAttribute : DataAttribute
	{
		private readonly string filePath;
		private readonly Type memberType;

#pragma warning disable CA1019 // Define accessors for attribute arguments
		public TestJsonDataAttribute(Type memberType, [NotNull] string filePath)
#pragma warning restore CA1019 // Define accessors for attribute arguments
		{
			this.filePath = filePath;
			this.memberType = memberType;
		}

		public override IEnumerable<object[]>? GetData(MethodInfo testMethod)
		{
			if (testMethod == null)
			{
				throw new ArgumentNullException(nameof(testMethod));
			}

			var path = Path.IsPathFullyQualified(filePath)
				? filePath
				: Path.Combine(Environment.CurrentDirectory, filePath);

			if (!File.Exists(path))
			{
				throw new ArgumentException($"Could not find file at path: {path}");
			}

			// Load the file
			var fileData = File.ReadAllText(filePath);

			return System.Text.Json.JsonSerializer.Deserialize<List<object>>(fileData)?.Select(t => new object[] { t });
		}
	}
}
