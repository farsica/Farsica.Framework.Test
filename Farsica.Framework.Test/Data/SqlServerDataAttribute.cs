namespace Farsica.Framework.Test.Data
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class SqlServerDataAttribute : OleDbDataAttribute
	{
		public SqlServerDataAttribute(string selectStatement)
			: base(selectStatement)
		{
		}
	}
}