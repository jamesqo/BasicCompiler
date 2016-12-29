namespace BasicCompiler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BasicCompiler.Core;

    /// <summary>
    /// Describes the expected outcome of a compiler operation.
    /// </summary>
    public class ExpectedResult
    {
        /// <summary>
        /// Gets or sets the input text.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the expected token list.
        /// </summary>
        public IEnumerable<Token> Tokens { get; set; }

        /// <summary>
        /// Gets or sets the expected AST.
        /// </summary>
        public Ast Ast { get; set; }

        /// <summary>
        /// Gets or sets the cumulative transforms to apply to the AST.
        /// </summary>
        public IEnumerable<IAstTransform> Transforms { get; set; }

        /// <summary>
        /// Gets or sets the expected ASTs resulting from each transform.
        /// </summary>
        public IEnumerable<Ast> NewAsts { get; set; }
    }
}
