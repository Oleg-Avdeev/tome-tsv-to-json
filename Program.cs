using System.IO;
using TSV.Parser;
using System;
using System.Text.Json;
using TSV.Data;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace tome_tsv_to_json
{
	class Program
	{
		public static int Main(string[] args)
		{
			if (args.Length == 0) return Error(0, "Provide filepath as the argument");

			var lines = File.ReadAllLines(args[0]);
			Info($"Successfuly read {lines.Length} lines");

			var tsvParser = new TSVParser();
			var matrix = tsvParser.Parse(lines);

			matrix.RemoveComments();
			matrix.SkipLines(1);
			matrix.UnwrapRow(0);
			matrix.UnwrapRow(1);

			var documentParser = new DocumentParser();
			var document = documentParser.Parse(matrix);
			
			var serializationOptions = new JsonSerializerOptions() {
				 WriteIndented = true,
				 Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			Export(document, args[0]+".json", true);
			Export(document, args[0]+"-compact.json", false);
			
			Info($"Parsed and Saved to {args[0]+".json".Green()}");

			return 0;
		}

		private static int Error(int code, string message)
		{
			Console.WriteLine($"Error: {message}".Red());
			return code;
		}

		private static void Info(string message)
		{
			Console.WriteLine($"{message}".Green());
		}

		private static void Export(Document document, string path, bool pretty = false)
		{
			var serializationOptions = new JsonSerializerOptions() {
				 WriteIndented = pretty,
				 IgnoreNullValues = true,
				 Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var json = JsonSerializer.Serialize<Document>(document, serializationOptions);
			json = json.Replace("\\\"", "\\\\\\\"");

			File.WriteAllText(path, json);
		}
	}
}
