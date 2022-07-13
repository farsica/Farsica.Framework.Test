using System.Diagnostics.CodeAnalysis;

namespace Farsica.Framework.Test.Common;

public static class Conventions
{
	public static void Enforce<T>(T target, [NotNull] Predicate<T> condition, string? message)
	{
		if (target is not null && !condition.Invoke(target))
		{
			throw new ConventionException($"[{target.GetFriendlyTypeName()}] Convention Error :{message}");
		}
	}
}