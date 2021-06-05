using System;

namespace TSV.Data
{
	[Serializable]
	public sealed class Document
	{
		public Chapter[] Scenes { get; set; }
	}
}
