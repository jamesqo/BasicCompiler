namespace BasicCompiler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BasicCompiler.Core;

    public class ExpectedResult
    {
        public string Input { get; set; }

        public IEnumerable<Token> Tokens { get; set; }

        public Ast Ast { get; set; }

        public IEnumerable<IAstTransform> Transforms { get; set; }

        public IEnumerable<Ast> NewAsts { get; set; }
    }
}
