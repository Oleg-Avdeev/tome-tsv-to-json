using System;

namespace TSV.Data
{
    [Serializable]
    public sealed class Condition
    {
        public enum Type : int
        {
            None = 0,
            Operation = 1,
        }

        public enum Operator
        {
            More,
            Less,
            Equal,
            NotEqual
        }

        public Type ConditionType { get; set; }
        public string Value { get; set; }

        public Operator Operation { get; set; }
        public string Variable { get; set; }
    }
}
