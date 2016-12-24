namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Parser
    {
        /// <summary>
        /// Parses a list of tokens into an AST.
        /// </summary>
        /// <param name="tokens">The tokens to parse.</param>
        /// <returns>The parsed AST.</returns>
        public static Ast Parse(IEnumerable<Token> tokens)
        {
            using (IEnumerator<Token> enumerator = tokens.GetEnumerator())
            {
                enumerator.MoveNext();
                AstNode rootNode = ParseNext(enumerator);
                return new Ast(rootNode);
            }
        }

        private static AstNode ParseNext(IEnumerator<Token> enumerator)
        {
            Token token = enumerator.Current;

            switch (token.Type)
            {
                case TokenType.CloseParenthesis:
                    throw null;
                case TokenType.OpenParenthesis:
                    if (!enumerator.MoveNext())
                    {
                        throw new ArgumentException("Unexpectedly reached EOF after opening parenthesis.");
                    }

                    token = enumerator.Current;

                    if (token.Type != TokenType.Identifier)
                    {
                        throw new ArgumentException("Expected an identifier after opening parenthesis.");
                    }

                    string identifierName = token.Value;
                    var callNode = AstNode.CallExpression(identifierName);

                    while (enumerator.MoveNext())
                    {
                        token = enumerator.Current;

                        switch (token.Type)
                        {
                            case TokenType.CloseParenthesis:
                                return callNode;
                            case TokenType.OpenParenthesis:
                                callNode.Add(ParseNext(enumerator));
                                break;
                            case TokenType.Identifier:
                                throw new ArgumentException("Unexpected identifier.");
                            case TokenType.Number:
                                callNode.Add(ParseNext(enumerator));
                                break;
                        }
                    }

                    throw new ArgumentException("Unexpectedly reached EOF after identifier.");
                case TokenType.Number:
                    return AstNode.NumberLiteral(token.Value);
                case TokenType.Identifier:
                    throw new ArgumentException("Expected an opening parenthesis before identifier.");
                default:
                    throw new ArgumentException("Invalid token type.");
            }
        }
    }
}
