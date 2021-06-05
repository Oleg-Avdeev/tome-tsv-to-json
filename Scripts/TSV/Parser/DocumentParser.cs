using System.Linq;
using TSV.Entities;
using TSV.Data;

namespace TSV.Parser
{
	public sealed class DocumentParser : AbstractParser<Document>
	{
		private static readonly ChapterParser chapterParser = new ChapterParser();

		protected override bool Validate(Matrix matrix)
		{
			if (matrix == null) return ValidationError($"matrix can't be null");
			if (matrix.Lines.Count == 0) return ValidationError($"matrix lines array can't be length 0");
			return true;
		}

		protected override Document ParseMatrix(Matrix matrix)
		{
			var document = new Document();

			document.Scenes = matrix.Lines
						.GroupBy(line => line[0].Value)
						.Select(group => chapterParser.Parse(new Matrix(group.ToList())))
						.ToArray();
			
			for (int chapter = 0; chapter < document.Scenes.Count(); chapter++)
				for (int line = 0; line < document.Scenes[chapter].Lines.Length; line++)
				{
					document.Scenes[chapter].Lines[line].ChapterIndex = chapter;
					document.Scenes[chapter].Lines[line].NodeIndex = line;
				}

			return document;
		}

	}
}