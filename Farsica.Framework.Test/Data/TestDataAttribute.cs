using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	[DataDiscoverer("Farsica.Framework.Test.Data.TestDataDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class TestDataAttribute : MemberDataAttributeBase
	//where TGenerator : ITestDataGenerator<TData>, new()
	//where TData : IData, new()
	{
		public TestDataAttribute(Type memberType)
			: base(nameof(ITestDataGenerator<BaseData>.GetData), null)
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
			var instance = CreateConstructor(MemberType);
			if (instance is null)
			{
				return null;
			}

			return (instance()?.GetType()?.GetMethod(MemberName)?.Invoke(instance, null) as IEnumerable<object>)?.Select(t => new object[] { t });
			//return (instance as ITestDataGenerator<BaseData>)?.GetData().Select(t => new object[] { t });
		}

		private delegate object ConstructorDelegate(params object[] args);

		private static ConstructorDelegate? CreateConstructor(Type type)
		{
			var constructorInfo = type.GetConstructor(Array.Empty<Type>());

			var paramExpr = Expression.Parameter(typeof(object[]));

			if (constructorInfo is null)
			{
				return null;
			}

			var body = Expression.New(constructorInfo);

			var constructor = Expression.Lambda<ConstructorDelegate>(body, paramExpr);
			return constructor.Compile();
		}
	}
}
