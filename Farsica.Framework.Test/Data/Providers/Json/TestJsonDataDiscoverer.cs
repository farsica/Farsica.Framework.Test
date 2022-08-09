using System.Diagnostics.CodeAnalysis;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data.Providers.Json
{
	public class TestJsonDataDiscoverer : DataDiscoverer
	{
		public override bool SupportsDiscoveryEnumeration([NotNull] IAttributeInfo dataAttribute, IMethodInfo testMethod)
			=> !dataAttribute.GetNamedArgument<bool>(nameof(MemberDataAttributeBase.DisableDiscoveryEnumeration));
	}
}
