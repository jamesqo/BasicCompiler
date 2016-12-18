using BasicCompiler.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace BasicCompiler.Tests
{
    public class LexerTests
    {
        [Theory, ClassData(typeof(SampleInputs))]
        public void Lex(ExpectedResult er)
        {
            Assert.Equal(er.Tokens, Lexer.Lex(er.Input));
        }

        [Theory, MemberData(nameof(PropertiesData))]
        public void Properties(Token token, string value, TokenType type)
        {
            Assert.Equal(value, token.Value);
            Assert.Equal(type, token.Type);
        }

        public static TheoryData<Token, string, TokenType> PropertiesData()
        {
            return new TheoryData<Token, string, TokenType>
            {
                { Token.CloseParenthesis(")"), ")", TokenType.CloseParenthesis },
                { Token.Identifier("foo"), "foo", TokenType.Identifier },
                { Token.Number("9"), "9", TokenType.Number },
                { Token.OpenParenthesis("("), "(", TokenType.OpenParenthesis }
            };
        }
    }
}
