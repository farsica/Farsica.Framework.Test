using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.CodeAnalysis;

namespace Farsica.Framework.Test.Data
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class OleDbDataAttribute : DataAdapterDataAttribute
	{
		private readonly string connectionString;
		private readonly string selectStatement;

#pragma warning disable CA1019 // Define accessors for attribute arguments
		public OleDbDataAttribute([NotNull] string selectStatement)
#pragma warning restore CA1019 // Define accessors for attribute arguments
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

#pragma warning disable CA1416 // Validate platform compatibility
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
		protected override IDataAdapter DataAdapter => new OleDbDataAdapter(selectStatement, connectionString);
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
#pragma warning restore CA1416 // Validate platform compatibility
	}
}
