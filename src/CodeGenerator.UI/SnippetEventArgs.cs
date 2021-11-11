using System;

namespace CodeGenerator.CodeGenerator.UI
{
    public class SnippetEventArgs : EventArgs
    {
        public string Snippet { get; set; }

        public SnippetEventArgs(string snippet)
        {
            Snippet = snippet;
        }
    }
}