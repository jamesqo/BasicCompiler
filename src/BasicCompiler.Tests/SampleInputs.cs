using BasicCompiler.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static BasicCompiler.Core.Token;
using static BasicCompiler.Core.AstNode;
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
                Input = "(add 4 90)",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("add"),
                    Number("4"),
                    Number("90"),
                    CloseParenthesis(")")
                },
                Ast = new Ast(
                    CallExpression("add")
                    .AddChildren(
                        NumberLiteral("4"),
                        NumberLiteral("90")
                    )
                )
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
                },
                Ast = new Ast(
                    CallExpression("add")
                    .AddChildren(
                        CallExpression("subtract")
                        .AddChildren(
                            NumberLiteral("4"),
                            NumberLiteral("1")
                        ),
                        NumberLiteral("2")
                    )
                )
            },
            new ExpectedResult
            {
                Input = "(multiply (divide 9 111) 0)",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("multiply"),
                    OpenParenthesis("("),
                    Identifier("divide"),
                    Number("9"),
                    Number("111"),
                    CloseParenthesis(")"),
                    Number("0"),
                    CloseParenthesis(")")
                },
                Ast = new Ast(
                    CallExpression("multiply")
                    .AddChildren(
                        CallExpression("divide")
                        .AddChildren(
                            NumberLiteral("9"),
                            NumberLiteral("111")
                        ),
                        NumberLiteral("0")
                    )
                )
            },
            new ExpectedResult
            {
                Input = "(exp 16 (exp 9 1))",
                Tokens = new[]
                {
                    OpenParenthesis("("),
                    Identifier("exp"),
                    Number("16"),
                    OpenParenthesis("("),
                    Identifier("exp"),
                    Number("9"),
                    Number("1"),
                    CloseParenthesis(")"),
                    CloseParenthesis(")")
                },
                Ast = new Ast(
                    CallExpression("exp")
                    .AddChildren(
                        NumberLiteral("16"),
                        CallExpression("exp")
                        .AddChildren(
                            NumberLiteral("9"),
                            NumberLiteral("1")
                        )
                    )
                )
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return ExpectedResults.Select(res => new object[] { res }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
