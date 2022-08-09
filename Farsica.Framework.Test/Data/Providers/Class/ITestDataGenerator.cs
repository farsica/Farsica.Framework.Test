namespace Farsica.Framework.Test.Data.Providers.Class
{
	public interface ITestDataGenerator<T>
		where T : IData
	{
		IEnumerable<T> GetData();
	}
}
