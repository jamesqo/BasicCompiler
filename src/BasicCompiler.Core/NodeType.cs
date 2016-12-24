namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The semantic type of an AST node.
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// Indicates a call expression.
        /// </summary>
        CallExpression,

        /// <summary>
        /// Indicates a number literal.
        /// </summary>
        NumberLiteral
    }
}
