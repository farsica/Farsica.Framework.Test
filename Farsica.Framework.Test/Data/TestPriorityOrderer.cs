﻿using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Farsica.Framework.Test.Data
{
	public class TestPriorityOrderer : ITestCaseOrderer
	{
		public IEnumerable<TTestCase> OrderTestCases<TTestCase>([NotNull] IEnumerable<TTestCase> testCases)
			where TTestCase : ITestCase
		{
			string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
			var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
			foreach (var testCase in testCases)
			{
				int priority = testCase.TestMethod.Method.GetCustomAttributes(assemblyName).FirstOrDefault()?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

				GetOrCreate(sortedMethods, priority).Add(testCase);
			}

			foreach (TTestCase testCase in sortedMethods.Keys.SelectMany(priority => sortedMethods[priority].OrderBy(testCase => testCase.TestMethod.Method.Name)))
			{
				yield return testCase;
			}
		}

		private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
			where TKey : struct
			where TValue : new()
		{
			return dictionary.TryGetValue(key, out var result)
				? result
				: (dictionary[key] = new TValue());
		}
	}
}
