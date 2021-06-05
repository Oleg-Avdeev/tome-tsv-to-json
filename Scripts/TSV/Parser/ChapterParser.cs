using TSV.Data;
using TSV.Entities;
using System.Linq;

namespace TSV.Parser
{
	public sealed class ChapterParser : AbstractParser<Chapter>
	{
		private static readonly LineParser lineParser = new LineParser();

		protected override bool Validate(Matrix matrix)
		{
			if (matrix == null) return ValidationError($"matrix can't be null");
			if (matrix.Lines.Count == 0) return ValidationError($"matrix lines array can't be length 0");
			return true;
		}

		protected override Chapter ParseMatrix(Matrix matrix)
		{
			var chapter = new Chapter();

			chapter.Id = matrix.Lines[0].Cells[0].Value.Trim();
			var lines = matrix.Lines.Select(line => lineParser.Parse(new Matrix(line))).ToList();
			
			lines.Where(l => l.Character.ToUpper().Contains("ВЫБОР")).ForEach(l => l.IsChoice = true );
			
			Logger.Log($"\nlines:");
			foreach(var line in lines)
			{
				line.IsChoice = line.Character.ToUpper().Contains("ВЫБОР");
				Logger.Log($"	{line.Character}: {line.Text}");
			}
			
			var lastLine = lines.LastOrDefault(l => !l.IsChoice) ?? lines.Last();
			lastLine.Choices = lines.Where(line => line.IsChoice).ToList();
			lastLine.IsLast = true;

			chapter.Lines = lines.Where(line => !line.IsChoice).ToArray();

			return chapter;
		}

	}
}