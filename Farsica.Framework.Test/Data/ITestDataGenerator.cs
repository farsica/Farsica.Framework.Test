namespace Farsica.Framework.Test.Data
{
	public interface ITestDataGenerator<T>
		where T : ITestDataGenerator<T>, new()
	{
		IEnumerable<T[]> GetData();
	}
}
