using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	[DataDiscoverer("Farsica.Framework.Test.Data.TestDataDiscoverer", "Farsica.Framework.Test")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class TestDataAttribute<T> : MemberDataAttributeBase
		where T : ITestDataGenerator<T>, new()
	{
		private const string methodName = nameof(ITestDataGenerator<T>.GetData);

		public TestDataAttribute(params object[] parameters)
			: base(methodName, parameters)
		{
			MemberType = typeof(T);
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
			var data = new T().GetData();
			return data.OfType<object[]>();

			//var type = MemberType ?? testMethod.DeclaringType;
			//Func<object?>? func = GetPropertyAccessor(type) ?? GetFieldAccessor(type) ?? GetMethodAccessor(type);

			//if (func is null)
			//{
			//	object[] parameters = Parameters;
			//	string text = (parameters != null && parameters.Length != 0) ? (" with parameter types: " + string.Join(", ", Parameters.Select((object p) => p?.GetType().FullName ?? "(null)"))) : "";
			//	throw new ArgumentException("Could not find public static member (property, field, or method) named '" + MemberName + "' on " + type?.FullName + text);
			//}

			//object? obj = func();
			//if (obj is null)
			//{
			//	return null;
			//}

			//if (obj is not IEnumerable enumerable)
			//{
			//	throw new ArgumentException("Property " + MemberName + " on " + type?.FullName + " did not return IEnumerable");
			//}

			//return from object item in enumerable
			//	   select ConvertDataItem(testMethod, item);
		}

		private Func<object?>? GetFieldAccessor(Type? type)
		{
			FieldInfo? fieldInfo = null;
			Type? type2 = type;
			while ((object?)type2 is not null)
			{
				fieldInfo = type2.GetRuntimeField(MemberName);
				if ((object?)fieldInfo is not null)
				{
					break;
				}

				type2 = type2.GetTypeInfo().BaseType;
			}

			if ((object?)fieldInfo is null || !fieldInfo.IsStatic)
			{
				return null;
			}

			return () => fieldInfo.GetValue(null);
		}

		private Func<object?>? GetMethodAccessor(Type? type)
		{
			MethodInfo? methodInfo = null;
			Type[]? parameterTypes = (Parameters is null) ? Array.Empty<Type>() : Parameters.Select((object p) => p?.GetType()).ToArray();
			Type? type2 = type;
			while ((object?)type2 is not null)
			{
				methodInfo = type2.GetRuntimeMethods().FirstOrDefault((MethodInfo m) => m.Name == MemberName && ParameterTypesCompatible(m.GetParameters(), parameterTypes));
				if ((object?)methodInfo is not null)
				{
					break;
				}

				type2 = type2.GetTypeInfo().BaseType;
			}

			if ((object?)methodInfo is null)
			//if ((object?)methodInfo is null || !methodInfo.IsStatic)
			{
				return null;
			}

			return () => methodInfo.Invoke(null, Parameters);
		}

		private Func<object?>? GetPropertyAccessor(Type? type)
		{
			PropertyInfo? propInfo = null;
			Type? type2 = type;
			while ((object?)type2 is not null)
			{
				propInfo = type2.GetRuntimeProperty(MemberName);
				if ((object?)propInfo is not null)
				{
					break;
				}

				type2 = type2.GetTypeInfo().BaseType;
			}

			if ((object?)propInfo is null || (object?)propInfo.GetMethod is null || !propInfo.GetMethod!.IsStatic)
			{
				return null;
			}

			return () => propInfo?.GetValue(null, null);
		}

		private static bool ParameterTypesCompatible(ParameterInfo[] parameters, Type[] parameterTypes)
		{
			if (parameters?.Length != parameterTypes.Length)
			{
				return false;
			}

			for (int i = 0; i < parameters.Length; i++)
			{
				if ((object?)parameterTypes[i] is not null && !parameters[i].ParameterType.GetTypeInfo().IsAssignableFrom(parameterTypes[i].GetTypeInfo()))
				{
					return false;
				}
			}

			return true;
		}
	}
}
