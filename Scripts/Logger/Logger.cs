using System;

public sealed class Logger
{

	public static void Error(string message)
	{
		Console.WriteLine(message);
	}

	public static void Log(string message)
	{
		Console.WriteLine(message);
	}
}