namespace BasicCompiler.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BasicCompiler.Core;
    using static BasicCompiler.Core.AstNode;
    using static BasicCompiler.Core.Token;

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
                    .AddTwo(
                        NumberLiteral("4"),
                        NumberLiteral("90"))),
                Transforms = new IAstTransform[]
                {
                    new AddTransform(5),
                    new ExpressionStatementTransform()
                },
                NewAsts = new[]
                {
                    new Ast(
                        CallExpression("add") // Injected
                        .AddTwo(
                            NumberLiteral("5"), // Injected
                            CallExpression("add")
                            .AddTwo(
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("5"), // Injected
                                    NumberLiteral("4")),
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("5"), // Injected
                                    NumberLiteral("90"))))),
                    new Ast(
                        ExpressionStatement() // Injected
                        .Add(
                            CallExpression("add") // Injected
                            .AddTwo(
                                NumberLiteral("5"), // Injected
                                CallExpression("add")
                                .AddTwo(
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("5"), // Injected
                                        NumberLiteral("4")),
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("5"), // Injected
                                        NumberLiteral("90"))))))
                },
                Outputs = new[]
                {
                    "add(5, add(add(5, 4), add(5, 90)))",
                    "add(5, add(add(5, 4), add(5, 90)));"
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
                },
                Ast = new Ast(
                    CallExpression("add")
                    .AddTwo(
                        CallExpression("subtract")
                        .AddTwo(
                            NumberLiteral("4"),
                            NumberLiteral("1")),
                        NumberLiteral("2"))),
                Transforms = new IAstTransform[]
                {
                    new AddTransform(2),
                    new ExpressionStatementTransform()
                },
                NewAsts = new[]
                {
                    new Ast(
                        CallExpression("add") // Injected
                        .AddTwo(
                            NumberLiteral("2"), // Injected
                            CallExpression("add")
                            .AddTwo(
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("2"), // Injected
                                    CallExpression("subtract")
                                    .AddTwo(
                                        CallExpression("add") // Injected
                                        .AddTwo(
                                            NumberLiteral("2"), // Injected
                                            NumberLiteral("4")),
                                        CallExpression("add") // Injected
                                        .AddTwo(
                                            NumberLiteral("2"), // Injected
                                            NumberLiteral("1")))),
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("2"), // Injected
                                    NumberLiteral("2"))))),
                    new Ast(
                        ExpressionStatement() // Injected
                        .Add(
                            CallExpression("add") // Injected
                            .AddTwo(
                                NumberLiteral("2"), // Injected
                                CallExpression("add")
                                .AddTwo(
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("2"), // Injected
                                        CallExpression("subtract")
                                        .AddTwo(
                                            CallExpression("add") // Injected
                                            .AddTwo(
                                                NumberLiteral("2"), // Injected
                                                NumberLiteral("4")),
                                            CallExpression("add") // Injected
                                            .AddTwo(
                                                NumberLiteral("2"), // Injected
                                                NumberLiteral("1")))),
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("2"), // Injected
                                        NumberLiteral("2"))))))
                },
                Outputs = new[]
                {
                    "add(2, add(add(2, subtract(add(2, 4), add(2, 1))), add(2, 2)))",
                    "add(2, add(add(2, subtract(add(2, 4), add(2, 1))), add(2, 2)));"
                }
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
                    .AddTwo(
                        CallExpression("divide")
                        .AddTwo(
                            NumberLiteral("9"),
                            NumberLiteral("111")),
                        NumberLiteral("0"))),
                Transforms = new[]
                {
                    new ExpressionStatementTransform()
                },
                NewAsts = new[]
                {
                    new Ast(
                        ExpressionStatement() // Injected
                        .Add(
                            CallExpression("multiply")
                            .AddTwo(
                                CallExpression("divide")
                                .AddTwo(
                                    NumberLiteral("9"),
                                    NumberLiteral("111")),
                                NumberLiteral("0"))))
                },
                Outputs = new[]
                {
                    "multiply(divide(9, 111), 0);"
                }
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
                    .AddTwo(
                        NumberLiteral("16"),
                        CallExpression("exp")
                        .AddTwo(
                            NumberLiteral("9"),
                            NumberLiteral("1")))),
                Transforms = new IAstTransform[]
                {
                    new AddTransform(0),
                    new ExpressionStatementTransform()
                },
                NewAsts = new[]
                {
                    new Ast(
                        CallExpression("add") // Injected
                        .AddTwo(
                            NumberLiteral("0"), // Injected
                            CallExpression("exp")
                            .AddTwo(
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("0"), // Injected
                                    NumberLiteral("16")),
                                CallExpression("add") // Injected
                                .AddTwo(
                                    NumberLiteral("0"), // Injected
                                    CallExpression("exp")
                                    .AddTwo(
                                        CallExpression("add") // Injected
                                        .AddTwo(
                                            NumberLiteral("0"), // Injected
                                            NumberLiteral("9")),
                                        CallExpression("add") // Injected
                                        .AddTwo(
                                            NumberLiteral("0"), // Injected
                                            NumberLiteral("1"))))))),
                    new Ast(
                        ExpressionStatement() // Injected
                        .Add(
                            CallExpression("add") // Injected
                            .AddTwo(
                                NumberLiteral("0"), // Injected
                                CallExpression("exp")
                                .AddTwo(
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("0"), // Injected
                                        NumberLiteral("16")),
                                    CallExpression("add") // Injected
                                    .AddTwo(
                                        NumberLiteral("0"), // Injected
                                        CallExpression("exp")
                                        .AddTwo(
                                            CallExpression("add") // Injected
                                            .AddTwo(
                                                NumberLiteral("0"), // Injected
                                                NumberLiteral("9")),
                                            CallExpression("add") // Injected
                                            .AddTwo(
                                                NumberLiteral("0"), // Injected
                                                NumberLiteral("1"))))))))
                },
                Outputs = new[]
                {
                    "add(0, exp(add(0, 16), add(0, exp(add(0, 9), add(0, 1)))))",
                    "add(0, exp(add(0, 16), add(0, exp(add(0, 9), add(0, 1)))));"
                }
            }
        };

        public static IEnumerable<object[]> AsEnumerable()
        {
            return ExpectedResults.Select(res => new object[] { res });
        }

        public IEnumerator<object[]> GetEnumerator() => AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
