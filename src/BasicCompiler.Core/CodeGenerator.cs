namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Responsible for generating code from an AST.
    /// </summary>
    public static class CodeGenerator
    {
        /// <summary>
        /// Stringifies the AST; function calls are represented with C-style syntax.
        /// </summary>
        /// <param name="ast">The AST.</param>
        /// <returns>The resulting code, as a string.</returns>
        public static string Stringify(Ast ast)
        {
            if (ast == null)
            {
                throw new ArgumentNullException(nameof(ast));
            }

            var visitor = new CodeGenVisitor();
            ast.Accept(visitor);
            return visitor.ToString();
        }
    }
}
