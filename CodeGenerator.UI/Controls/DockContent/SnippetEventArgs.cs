using System;

namespace CodeGenerator.UI
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