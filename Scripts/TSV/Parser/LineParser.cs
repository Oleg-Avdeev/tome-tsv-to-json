using TSV.Entities;
using TSV.Data;

namespace TSV.Parser
{
    public sealed class LineParser : AbstractParser<Data.Line>
    {
        private static readonly ActionParser actionParser = new ActionParser();
        private static readonly ConditionParser conditionParser = new ConditionParser();

        protected override bool Validate(Matrix matrix)
        {
            if (matrix == null) return ValidationError($"matrix can't be null");
            if (matrix.Lines.Count == 0) return ValidationError($"matrix lines array can't be length 0");
            return true;
        }

        protected override Data.Line ParseMatrix(Matrix matrix)
        {
            var line = new Data.Line();

            line.Character = matrix[0][1];
            line.Text = matrix[0][2];

            line.Actions = actionParser.Parse(new Matrix(matrix[0][4]));
            
			line.State = matrix[0][6];

            return line;
        }
    }
}