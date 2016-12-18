using BasicCompiler.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Tests
{
    public class ExpectedResult
    {
        public string Input { get; set; }
        public IEnumerable<Token> Tokens { get; set; }
        public Ast Ast { get; set; }
    }
}
