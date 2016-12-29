namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A visitor which an AST node accepts.
    /// </summary>
    public interface IAstVisitor
    {
        /// <summary>
        /// Invoked after all descendants of a CallExpression node have been visited.
        /// </summary>
        /// <param name="node">The node.</param>
        void LeaveCallExpression(AstNode node); // TODO: Is there a better pattern for this?

        /// <summary>
        /// Invoked after all descendants of an ExpressionStatement node have been visited.
        /// </summary>
        /// <param name="node">The node.</param>
        void LeaveExpressionStatement(AstNode node);

        /// <summary>
        /// Invoked when a CallExpression node is visited.
        /// </summary>
        /// <param name="node">The node.</param>
        void VisitCallExpression(AstNode node);

        /// <summary>
        /// Invoked when an ExpressionStatement node is visited.
        /// </summary>
        /// <param name="node">The node.</param>
        void VisitExpressionStatement(AstNode node);

        /// <summary>
        /// Invoked when a NumberLiteral node is visited.
        /// </summary>
        /// <param name="node">The node.</param>
        void VisitNumberLiteral(AstNode node);
    }
}
