namespace BasicCompiler.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A token that results from lexing raw input text.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Token : IEquatable<Token>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <param name="type">The semantic type of this token.</param>
        private Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the value of this token.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the semantic type of this token.
        /// </summary>
        public TokenType Type { get; set; }

        internal string DebuggerDisplay => $"{{ {nameof(Value)}: {Value}, {nameof(Type)}: {Type} }}";

        /// <summary>
        /// Creates a token with type <see cref="TokenType.CloseParenthesis"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new token.</returns>
        public static Token CloseParenthesis(string value) => new Token(value, TokenType.CloseParenthesis);

        /// <summary>
        /// Creates a token with type <see cref="TokenType.Identifier"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new token.</returns>
        public static Token Identifier(string value) => new Token(value, TokenType.Identifier);

        /// <summary>
        /// Creates a token with type <see cref="TokenType.Number"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new token.</returns>
        public static Token Number(string value) => new Token(value, TokenType.Number);

        /// <summary>
        /// Creates a token with type <see cref="TokenType.OpenParenthesis"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new token.</returns>
        public static Token OpenParenthesis(string value) => new Token(value, TokenType.OpenParenthesis);

        public override bool Equals(object obj) => obj is Token t && Equals(t);

        public bool Equals(Token other) =>
            other != null &&
            Value == other.Value &&
            Type == other.Type;

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}