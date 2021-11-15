namespace CodeGenerator.Generator
{
    public class Context
    {
        public static string StartDelimeter { get; set; } = "{";

        public static string EndingDelimiter { get; set; } = "}";

        public string Input { get; set; }

        public string Output { get; set; }

        internal object Extra { get; set; }
    }
}