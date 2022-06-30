namespace Farsica.Framework.Test.Core;

public interface IFactory<out T>
{
    T Create();
}