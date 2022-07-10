namespace Farsica.Framework.Test.Data
{
	public abstract class TestDataGeneratorBase<T>
		where T : TestDataGeneratorBase<T>, new()
	{
		public abstract IEnumerable<T[]> GetData();
	}
}
