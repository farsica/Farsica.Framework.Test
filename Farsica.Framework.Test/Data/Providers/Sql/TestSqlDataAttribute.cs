namespace Farsica.Framework.Test.Data.Providers.Sql
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class TestSqlDataAttribute : OleDbDataAttribute
	{
#pragma warning disable CA1019 // Define accessors for attribute arguments
		public TestSqlDataAttribute(string selectStatement)
#pragma warning restore CA1019 // Define accessors for attribute arguments
			: base(selectStatement)
		{
		}
	}
}