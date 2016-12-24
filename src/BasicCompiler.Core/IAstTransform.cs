namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a transform operation to be applied on an AST.
    /// </summary>
    public interface IAstTransform
    {
        /// <summary>
        /// Creates a new, mutable transformer used to inspect the AST.
        /// </summary>
        /// <returns>The transformer used to apply this transform.</returns>
        IAstTransformer CreateTransformer();
    }

    public static class AstTransformExtensions
    {
        public static Ast Apply(this IAstTransform transform, Ast ast)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (ast == null)
            {
                throw new ArgumentNullException(nameof(ast));
            }

            var transformer = transform.CreateTransformer();
            ast.Accept(transformer);
            return transformer.NewAst;
        }
    }
}
