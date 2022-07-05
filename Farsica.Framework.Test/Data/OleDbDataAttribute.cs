using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.OleDb;

namespace Farsica.Framework.Test.Data
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class OleDbDataAttribute : DataAdapterDataAttribute
	{
		private readonly string connectionString;
		private readonly string selectStatement;

		public OleDbDataAttribute(string selectStatement)
		{
			var filePath = "appsettings.json";
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(filePath);
			}
			var config = new ConfigurationBuilder()
				.AddJsonFile(filePath, false, true)
				.Build();
			connectionString = config.GetConnectionString("DefaultConnection");

			this.selectStatement = selectStatement;
		}

		protected override IDataAdapter DataAdapter => new OleDbDataAdapter(selectStatement, connectionString);
	}
}
