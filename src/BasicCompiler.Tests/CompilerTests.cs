using BasicCompiler.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace BasicCompiler.Tests
{
    public class CompilerTests
    {
        [Theory]
        [ClassData(typeof(SampleInputs))]
        public void Lex(ExpectedResult er)
        {
            Assert.Equal(er.Tokens, Lexer.Lex(er.Input));
        }
    }
}
