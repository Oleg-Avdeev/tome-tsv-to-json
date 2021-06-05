using System.Collections.Generic;
using System.Text.RegularExpressions;
using TSV.Entities;
using TSV.Data;

namespace TSV.Parser
{
    public sealed class ConditionParser : AbstractParser<List<Condition>>
    {
        private static readonly Regex operationRegex = new Regex("\\[([^\\[\\]]*)\\]");

        private static readonly Regex _operationRE = new Regex("([a-zА-ЯA-Zа-я0-9]*)([\\=\\>\\<\\!])([\\-a-zА-ЯA-Zа-я0-9]*)");

        protected override bool Validate(Matrix matrix)
        {
            if (matrix == null) return ValidationError($"matrix can't be null");
            if (matrix.Lines.Count == 0) return ValidationError($"matrix lines array can't be length 0");
            return true;
        }

        protected override List<Condition> ParseMatrix(Matrix matrix)
        {
            var Conditions = new List<Condition>();

            Conditions.AddRange(ParseConditions(matrix[0][0], Condition.Type.Operation, operationRegex));

            return Conditions;
        }

        private List<Condition> ParseConditions(string line, Condition.Type type, Regex re)
        {
            var Conditions = new List<Condition>();
            var matches = re.Matches(line);
            foreach (Match match in matches)
            {
                var Condition = new Condition()
                {
                    ConditionType = type,
                    Value = match.Groups[1].Value
                };
                Conditions.Add(Condition);
            }
            return Conditions;
        }

        private void ParseLinkCondition(Condition condition, string conditionText)
        {
            conditionText = conditionText.Replace(" ", "");

            Match m = _operationRE.Match(conditionText);
            if (m != null && m.Success)
            {
                var op = Condition.Operator.Equal;

                switch (m.Groups[2].Value)
                {
                    case ">": op = Condition.Operator.More; break;
                    case "<": op = Condition.Operator.Less; break;
                    case "=": op = Condition.Operator.Equal; break;
                    case "!": op = Condition.Operator.NotEqual; break;
                    default:
                        Logger.Error($"Story.LinkParser: Couldn\'t parse condition operator {m.Groups[1].Value} in {conditionText}");
                        break;
                }

                condition.Operation = op;
                condition.Variable = m.Groups[1].Value;
                condition.Value = m.Groups[3].Value;
            }

            Logger.Error($"Story.LinkParser: Couldn\'t parse link condition {conditionText}");
        }
    }
}