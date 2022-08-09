using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data.Providers.Class
{
	[DataDiscoverer("Farsica.Framework.Test.Data.Providers.Class.TestDataDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class TestDataAttribute : MemberDataAttributeBase
	{
		public TestDataAttribute(Type memberType)
			: base(nameof(ITestDataGenerator<IData>.GetData), null)
		{
			MemberType = memberType;
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
			var instance = Activator.CreateInstance(MemberType);
			return (instance?.GetType().GetMethod(MemberName)?.Invoke(instance, null) as IEnumerable<object>)?.Select(t => new object[] { t });
		}
	}
}
