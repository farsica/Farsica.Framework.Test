using System.Runtime.Serialization;

namespace Farsica.Framework.Test.Common;

[Serializable]
public class ConventionException : Exception
{
    public ConventionException()
    {
    }

    public ConventionException(string message)
        : base(message)
    {
    }

    public ConventionException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected ConventionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}