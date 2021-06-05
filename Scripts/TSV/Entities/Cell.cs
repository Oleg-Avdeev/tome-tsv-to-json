using System.Collections.Generic;
using System.Linq;

namespace TSV.Entities
{
	public sealed class Cell
	{
		public Cell(string value) => Value = value;
		
		public string Value;

		public static implicit operator string(Cell c)
			=> c.Value;
	}
}