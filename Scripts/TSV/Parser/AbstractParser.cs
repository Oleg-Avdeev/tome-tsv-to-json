using System.Collections.Generic;
using TSV.Entities;
using System.Linq;

namespace TSV.Parser
{
	public abstract class AbstractParser<T>
	{
		public T Parse(Matrix matrix)
		{
			if (!Validate(matrix)) return default(T);
			return ParseMatrix(matrix);
		}

		protected abstract bool Validate(Matrix matrix);

		protected abstract T ParseMatrix(Matrix matrix);

		protected bool ValidationError(string errorMessage)
		{
			Logger.Error($"{typeof(T)} Parsing error: {errorMessage}");
			return false;
		}
	}
}