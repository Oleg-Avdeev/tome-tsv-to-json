using System.Collections.Generic;
using System.Text.RegularExpressions;
using TSV.Entities;
using TSV.Data;

namespace TSV.Parser
{
	public sealed class ActionParser : AbstractParser<List<Action>>
	{
		private static readonly Regex gotoRegex = new Regex("\\[([^\\[\\]]*)\\]"); // [goto]
		private static readonly Regex commandRegex = new Regex("\\(([^\\{\\}]*)\\)"); // (command)
		private static readonly Regex operationRegex = new Regex("\\{([^\\(\\)]*)\\}"); // {operation}

		// Operations
		private static readonly Regex _plusActionRE = new Regex("([\\+])([a-zА-ЯA-Zа-я0-9]*)");
		private static readonly Regex _minusActionRE = new Regex("([\\-])([a-zА-ЯA-Zа-я0-9]*)");
		private static readonly Regex _assignActionRE = new Regex("([a-zА-ЯA-Zа-я0-9]*)\\=([\\-a-zА-ЯA-Zа-я0-9]*)");

		protected override bool Validate(Matrix matrix)
		{
			if (matrix == null) return ValidationError($"lines array can't be null");
			if (matrix.Lines.Count > 1) return ValidationError($"lines array can't be longer than 1!");
			return true;
		}

		protected override List<Action> ParseMatrix(Matrix matrix)
		{
			var actions = new List<Action>();

			actions.AddRange(ParseActions(matrix[0][0], Action.Type.GoTo, gotoRegex));
			actions.AddRange(ParseActions(matrix[0][0], Action.Type.Command, commandRegex));
			actions.AddRange(ParseActions(matrix[0][0], Action.Type.Operation, operationRegex));

			return actions;
		}

		private List<Action> ParseActions(string line, Action.Type type, Regex re)
		{
			var actions = new List<Action>();
			var matches = re.Matches(line);

			foreach (Match match in matches)
			{
				var action = new Action()
				{
					ActionType = type,
					Value = match.Groups[1].Value
				};

				if (type == Action.Type.Operation)
					ParseOperation(action, match.Groups[1].Value);

				actions.Add(action);
			}

			return actions;
		}

		private void ParseOperation(Action action, string actionText)
		{
			actionText = actionText.Replace(" ", "");

			Match m = _plusActionRE.Match(actionText);
			if (m != null && m.Success)
			{
				action.Operation = Action.Operator.Inrease;
				action.Variable = m.Groups[2].Value;
				return;
			}

			m = _minusActionRE.Match(actionText);
			if (m != null && m.Success)
			{
				action.Operation = Action.Operator.Decrease;
				action.Variable = m.Groups[2].Value;
				return;
			}

			m = _assignActionRE.Match(actionText);
			if (m != null && m.Success)
			{
				action.Operation = Action.Operator.Assign;
				action.Variable = m.Groups[1].Value;
				action.Value = m.Groups[2].Value;
				return;
			}

			Logger.Error($"Story.LinkParser: Couldn\'t parse link action {actionText}");
		}
	}
}