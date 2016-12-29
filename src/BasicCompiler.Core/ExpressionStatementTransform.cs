using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    /// <summary>
    /// An AST transform that adds a top-level ExpressionStatement node.
    /// </summary>
    public class ExpressionStatementTransform : IAstTransform
    {
        public IAstTransformer CreateTransformer() => new Transformer(this);

        /// <summary>
        /// The transformer used to apply this transform operation.
        /// </summary>
        private sealed class Transformer : IAstTransformer
        {
            private readonly ExpressionStatementTransform _transform;
            private readonly AstNode _root;
            private AstNode _current;

            internal Transformer(ExpressionStatementTransform transform)
            {
                _transform = transform ?? throw new ArgumentNullException(nameof(transform));
                _root = _current = AstNode.ExpressionStatement();
            }

            public Ast NewAst => new Ast(_root);

            public void LeaveCallExpression(AstNode node)
            {
                _current = _current.Parent;
            }

            public void LeaveExpressionStatement(AstNode node)
            {
                throw new NotSupportedException($"{nameof(node)} should not have ExpressionStatements yet.");
            }

            public void VisitCallExpression(AstNode node)
            {
                var newNode = AstNode.CallExpression(node.Value);
                _current.Add(newNode);
                _current = newNode;
            }

            public void VisitExpressionStatement(AstNode node)
            {
                throw new NotSupportedException($"{nameof(node)} should not have ExpressionStatements yet.");
            }

            public void VisitNumberLiteral(AstNode node)
            {
                var newNode = AstNode.NumberLiteral(node.Value);
                _current.Add(newNode);
            }
        }
    }
}
