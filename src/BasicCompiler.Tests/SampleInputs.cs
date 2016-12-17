using BasicCompiler.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static BasicCompiler.Core.Token;
using System.Collections;
using System.Linq;

namespace BasicCompiler.Tests
{
    public class SampleInputs : IEnumerable<object[]>
    {
        private static ExpectedResult[] ExpectedResults { get; } =
        {
            new ExpectedResult
            {
                Input = "(add 4 9)",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("add"),
                    Number("4"),
                    Number("9"),
                    CloseParenthesis(")")
                }
            },
            new ExpectedResult
            {
                Input = "(add (subtract 4 1) 2)",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("add"),
                    OpenParenthesis("("),
                    Identifier("subtract"),
                    Number("4"),
                    Number("1"),
                    CloseParenthesis(")"),
                    Number("2"),
                    CloseParenthesis(")")
                }
            },
            new ExpectedResult
            {
                Input = "(multiply (divide 9 1) 0)",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("multiply"),
                    OpenParenthesis("("),
                    Identifier("divide"),
                    Number("9"),
                    Number("1"),
                    CloseParenthesis(")"),
                    Number("0"),
                    CloseParenthesis(")")
                }
            },
            new ExpectedResult
            {
                Input = "(exp 9 (exp 9 1))",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("exp"),
                    Number("9"),
                    OpenParenthesis("("),
                    Identifier("exp"),
                    Number("9"),
                    Number("1"),
                    CloseParenthesis(")"),
                    CloseParenthesis(")")
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return ExpectedResults.Select(res => new object[] { res }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
