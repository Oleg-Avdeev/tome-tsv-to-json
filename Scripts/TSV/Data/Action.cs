using System;

namespace TSV.Data
{
	[Serializable]
	public sealed class Action
	{
		public enum Type : int
		{
			None = 0,
			GoTo = 1,
			Command = 2,
			Operation = 3,
			Next = 4,
		}

		public enum Operator
		{
			Inrease,
			Decrease,
			Assign
		}

		public Type ActionType { get; set; }
		public string Value { get; set; }

		public Operator Operation { get; set; }
		public string Variable { get; set; }
	}
}