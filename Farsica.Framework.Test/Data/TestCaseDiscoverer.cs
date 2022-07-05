using Xunit.Abstractions;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	public class TestCaseDiscoverer : ITraitDiscoverer
	{
		private const string Key = "TestCase";

		public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
		{
			string? testCase;
			var attributeInfo = traitAttribute as ReflectionAttributeInfo;
			if (attributeInfo?.Attribute is TestCaseAttribute testCaseAttribute)
			{
				testCase = testCaseAttribute.TestCase;
			}
			else
			{
				var constructorArguments = traitAttribute.GetConstructorArguments().ToArray();
				testCase = constructorArguments[0]?.ToString();
			}
			if (!string.IsNullOrEmpty(testCase))
			{
				yield return new KeyValuePair<string, string>(Key, testCase);
			}
		}
	}
}
