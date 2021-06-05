using System.Collections.Generic;
using System.Linq;
using TSV.Entities;

namespace TSV.Parser
{
	public sealed class TSVParser
	{
		public Matrix Parse(string[] lines)
		{
			var width = lines[0].Count(c => c == '\t') + 1;
			var matrix = new Matrix();

			for (int y = 0; y < lines.Length; y++)
			{
				var values = lines[y].Split('\t');
				var line = new Line();

				for (int x = 0; x < values.Length; x++)
					line.Add(new Cell(values[x]));

				matrix.Add(line);
			}

			return matrix;
		}
	}
}