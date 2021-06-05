
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TSV.Parser
{
	public static class Extensions
	{
		public static List<T> ForEach<T>(this List<T> list, System.Action<T> action)
		{
			foreach (var u in list) action(u);
			return list;
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, System.Action<T> action)
		{
			foreach (var u in list) action(u);
			return list;
		}

	}
}