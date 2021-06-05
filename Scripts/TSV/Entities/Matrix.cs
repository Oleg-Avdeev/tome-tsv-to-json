using System.Collections.Generic;
using System.Linq;

namespace TSV.Entities
{
	public sealed class Matrix
	{
		public List<Line> Lines = new List<Line>();

		public Matrix() {}
		
		public Matrix(List<Line> lines) 
			=> Lines = lines;

		public Matrix(Line line) 
			=> Lines = new List<Line>() { line };

		public Matrix(Cell cell)
			=> Lines = new List<Line>() { new Line() { Cells = new List<Cell>() { cell } } };

		public void Add(Line line) 
			=> Lines.Add(line);

		public Line this[int index]
		{
			get => Lines[index];
		}

		public void RemoveComments()
		{
			Lines.RemoveAll(line => !line.Cells.Skip(1)
											   .Any(c => !string.IsNullOrWhiteSpace(c.Value)));
		}

		public void UnwrapRow(int rowIndex)
		{
			var lastValue = string.Empty;
			
			foreach(var line in Lines)
			{
				var cell = line.Cells[rowIndex];
				if (!string.IsNullOrWhiteSpace(cell.Value))
					lastValue = cell.Value;
				
				cell.Value = lastValue;
			}			
		}

		public void SkipLines(int count)
		{
			Lines = Lines.Skip(count).ToList();
		}
	}
}