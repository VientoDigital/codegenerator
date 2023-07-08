using Humanizer;

namespace CodeGenerator.Extensions;

public static class HumanizerExtensions
{
    public static string KebaberizeNoCaseChange(this string input)
    {
        return input.UnderscoreNoCaseChange().Dasherize();
    }

    public static string UnderscoreNoCaseChange(this string input)
    {
        return Regex.Replace(
            Regex.Replace(
                Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_");
    }
}