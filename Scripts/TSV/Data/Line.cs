using System;
using System.Collections.Generic;

namespace TSV.Data
{
    [Serializable]
    public sealed class Line
    {
        public string Character { get; set; }
        public string Text { get; set; }
        public string State { get; set; }
		
		public List<Action> Actions { get; set; }
		public List<Line> Choices { get; set; }

		// Baked data
		public bool IsLast { get; set; }
		public bool IsChoice { get; set; }
		public string DocumentId { get; set; }
		public int ChapterIndex { get; set; }
		public int NodeIndex { get; set; }


    }
}
