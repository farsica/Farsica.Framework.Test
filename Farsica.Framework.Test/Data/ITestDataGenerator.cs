namespace Farsica.Framework.Test.Data
{
	public interface ITestDataGenerator<T>
		where T : IData, new()
	{
		IEnumerable<T> GetData();
	}
}
