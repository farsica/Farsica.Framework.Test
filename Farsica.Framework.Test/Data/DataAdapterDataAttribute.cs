using System.Data;
using System.Reflection;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	[DataDiscoverer("Farsica.Framework.Test.Data.DataAdapterDataAttributeDiscoverer", "Farsica.Framework.Test")]
	public abstract class DataAdapterDataAttribute : DataAttribute
	{
		protected abstract IDataAdapter DataAdapter { get; }

		public bool EnableDiscoveryEnumeration { get; set; }

		public override IEnumerable<object?[]> GetData(MethodInfo methodUnderTest)
		{
			using DataSet dataSet = new();
			IDataAdapter adapter = DataAdapter;
			try
			{
				adapter.Fill(dataSet);

				foreach (DataRow row in dataSet.Tables[0].Rows)
					yield return ConvertParameters(row.ItemArray);
			}
			finally
			{
				if (adapter is IDisposable disposable)
					disposable.Dispose();
			}
		}

		object?[] ConvertParameters(object?[] values)
		{
			object?[] result = new object[values.Length];

			for (int idx = 0; idx < values.Length; idx++)
			{
				result[idx] = ConvertParameter(values[idx]);
			}

			return result;
		}

		protected virtual object? ConvertParameter(object? parameter)
		{
			if (parameter is DBNull)
				return null;

			return parameter;
		}
	}
}
