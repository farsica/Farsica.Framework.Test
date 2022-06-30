namespace Farsica.Framework.Test.Common;

public static class Conventions
{
    public static void Enforce<T>(T target, Predicate<T> condition, string? message)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }
        
        if (!condition.Invoke(target))
        {
            throw new ConventionException($"[{target.GetFriendlyTypeName()}] Convention Error :{message}");
        }
    }
}