namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A mutable AST transformer that visits an AST and builds a new one.
    /// </summary>
    public interface IAstTransformer : IAstVisitor
    {
        /// <summary>
        /// Gets the new AST that was built.
        /// </summary>
        Ast NewAst { get; }
    }
}
