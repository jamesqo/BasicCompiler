namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class Lexer
    {
        /// <summary>
        /// Converts a raw string into a list of tokens.
        /// </summary>
        /// <param name="input">The string to lex.</param>
        /// <returns>A list of tokens based on the string.</returns>
        public static IEnumerable<Token> Lex(string input)
        {
            var tokens = new List<Token>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (char.IsWhiteSpace(c))
                {
                    continue; // Ignore whitespace.
                }

                if (c == '(' || c == ')')
                {
                    bool openParenthesis = c == '(';
                    tokens.Add(openParenthesis ? Token.OpenParenthesis("(") : Token.CloseParenthesis(")"));
                    continue;
                }

                if (c >= '0' && c <= '9')
                {
                    // We found a digit. Read in the rest of the digits into a string until we hit whitespace.
                    // `end` is the index of the last digit before we 1) reach EOF or 2) reach a non-digit.

                    int end = i;
                    for (; end + 1 < input.Length; end++)
                    {
                        char next = input[end + 1];
                        if (next < '0' || next > '9')
                        {
                            break;
                        }
                    }

                    string value = input.Substring(i, end - i + 1);
                    tokens.Add(Token.Number(value));
                    i = end;
                    continue;
                }

                if (char.IsLetter(c))
                {
                    int end = i;
                    for (; end + 1 < input.Length; end++)
                    {
                        char next = input[end + 1];
                        if (!char.IsLetter(next))
                        {
                            break;
                        }
                    }

                    string value = input.Substring(i, end - i + 1);
                    tokens.Add(Token.Identifier(value));
                    i = end;
                    continue;
                }

                throw new ArgumentException("The input contained invalid tokens.", nameof(input));
            }

            return tokens;
        }
    }
}