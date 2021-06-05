using Pastel;

public static class StringExtensions
{
	public static string Red(this string str) => str.Pastel("#fc7303");
	public static string Green(this string str) => str.Pastel("#a5ed09");
	public static string Blue(this string str) => str.Pastel("#139ded");
}