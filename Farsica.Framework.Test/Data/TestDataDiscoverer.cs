using Xunit.Abstractions;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	public class TestDataDiscoverer : DataDiscoverer
	{
		public override bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
		{
			return !dataAttribute.GetNamedArgument<bool>("DisableDiscoveryEnumeration");
		}
	}
}
