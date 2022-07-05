using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	[TraitDiscoverer("Farsica.Framework.Test.Data.TestCaseDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method)]
	public class TestCaseAttribute : Attribute, ITraitAttribute
	{
		public string TestCase { get; set; }

		public TestCaseAttribute(string testCase)
		{
			TestCase = testCase;
		}
	}
}
