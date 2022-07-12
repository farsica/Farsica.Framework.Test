﻿using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	public class DataAdapterDataAttributeDiscoverer: DataDiscoverer
	{
		public override bool SupportsDiscoveryEnumeration([NotNull] IAttributeInfo dataAttribute, IMethodInfo testMethod)
			=> dataAttribute.GetNamedArgument<bool>("EnableDiscoveryEnumeration");
	}
}
