namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The semantic type of a token.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Indicates a close-parenthesis token.
        /// </summary>
        CloseParenthesis,

        /// <summary>
        /// Indicates an identifier token.
        /// </summary>
        Identifier,

        /// <summary>
        /// Indicates a number token.
        /// </summary>
        Number,

        /// <summary>
        /// Indicates an open-parenthesis token.
        /// </summary>
        OpenParenthesis
    }
}
