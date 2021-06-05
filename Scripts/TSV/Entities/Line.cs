using System.Collections.Generic;
using System.Linq;

namespace TSV.Entities
{
	public sealed class Line
	{
		public List<Cell> Cells = new List<Cell>();

		public void Add(Cell cell) => Cells.Add(cell);

		public Cell this[int index]
		{
			get => Cells[index];
		}
	}
}