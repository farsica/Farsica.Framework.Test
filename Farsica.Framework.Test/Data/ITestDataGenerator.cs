namespace Farsica.Framework.Test.Data
{
	public interface ITestDataGenerator<T>
		where T : BaseData
	{
		IEnumerable<T> GetData();
	}
}
