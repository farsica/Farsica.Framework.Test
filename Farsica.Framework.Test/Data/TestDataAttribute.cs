using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	[DataDiscoverer("Farsica.Framework.Test.Data.TestDataDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class TestDataAttribute<TGenerator, TData> : MemberDataAttributeBase
		where TGenerator : ITestDataGenerator<TData>, new()
		where TData : IData, new()
	{
		public TestDataAttribute(params object[] parameters)
			: base(nameof(ITestDataGenerator<TData>.GetData), parameters)
		{
			MemberType = typeof(TGenerator);
		}

		protected override object[]? ConvertDataItem(MethodInfo? testMethod, object? item)
		{
			if (item == null)
			{
				return null;
			}

			if (item is not object[] array)
			{
				throw new ArgumentException($"Property {MemberName} on {MemberType ?? testMethod?.DeclaringType} yielded an item that is not an object[]");
			}

			return array;
		}

		public override IEnumerable<object[]>? GetData(MethodInfo testMethod)
		{
			return new TGenerator().GetData().Select(t => new object[] { t });
		}
	}
}
