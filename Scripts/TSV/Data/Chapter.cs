using System;

namespace TSV.Data
{
	[Serializable]
	public sealed class Chapter
	{
		public string Id { get; set; }
		public string[] Comments { get; set; }
		public Line[] Lines { get; set; }
	}
}
