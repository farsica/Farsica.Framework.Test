namespace Farsica.Framework.Test.Data
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class SqlServerDataAttribute : OleDbDataAttribute
	{
#pragma warning disable CA1019 // Define accessors for attribute arguments
		public SqlServerDataAttribute(string selectStatement)
#pragma warning restore CA1019 // Define accessors for attribute arguments
			: base(selectStatement)
		{
		}
	}
}