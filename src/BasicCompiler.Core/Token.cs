using System;

namespace BasicCompiler.Core
{
    public enum TokenType
    {
        CloseParenthesis,
        Identifier,
        Number,
        OpenParenthesis,
    }

    public class Token : IEquatable<Token>
    {
        private Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        public static Token CloseParenthesis(string value) => new Token(value, TokenType.CloseParenthesis);

        public static Token Identifier(string value) => new Token(value, TokenType.Identifier);

        public static Token Number(string value) => new Token(value, TokenType.Number);

        public static Token OpenParenthesis(string value) => new Token(value, TokenType.OpenParenthesis);

        public string Value { get; set; }
        public TokenType Type { get; set; }

        public override bool Equals(object obj) => obj is Token t && Equals(t);

        public bool Equals(Token other) =>
            other != null &&
            Value == other.Value &&
            Type == other.Type;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString() => $"{{ {nameof(Value)}: {Value}, {nameof(Type)}: {Type} }}";
    }
}