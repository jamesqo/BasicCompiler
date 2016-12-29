namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An AST transform that injects an "add" call around each node with a constant addend.
    /// </summary>
    public class AddTransform : IAstTransform
    {
        // TODO: Should we support making positioning the addend on the rhs of the call?
        public AddTransform(int addend)
        {
            if (addend < 0)
            {
                // Negative numbers are not supported yet.
                throw new ArgumentOutOfRangeException(nameof(addend));
            }

            Addend = addend;
        }

        /// <summary>
        /// The constant addend that will be supplied on each "add" invocation.
        /// </summary>
        internal int Addend { get; }

        public IAstTransformer CreateTransformer() => new Transformer(this);

        /// <summary>
        /// The transformer used to apply this transform operation.
        /// </summary>
        private sealed class Transformer : IAstTransformer
        {
            private readonly AddTransform _transform;
            private AstNode _root;
            private AstNode _parent;

            internal Transformer(AddTransform transform)
            {
                _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            }

            public Ast NewAst => new Ast(_root);

            public void LeaveCallExpression(AstNode node)
            {
                var addExpr = _parent.Parent;

                if (addExpr.Value != "add")
                {
                    throw new AstVisitorException("Expected injected node to be 'add'.");
                }

                _parent = addExpr.Parent;
            }

            public void LeaveExpressionStatement(AstNode node)
            {
                // TODO
                throw new NotImplementedException();
            }

            public void VisitCallExpression(AstNode node)
            {
                var addExpr = InjectAddExpression();
                var newNode = AstNode.CallExpression(node.Value);

                addExpr.Add(newNode);
                _parent = newNode; // When we leave this CallExpression, _parent will be set to
                                   // its previous value in LeaveCallExpression
            }

            public void VisitExpressionStatement(AstNode node)
            {
                // TODO
                throw new NotImplementedException();
            }

            public void VisitNumberLiteral(AstNode node)
            {
                var addExpr = InjectAddExpression();

                // Because AST nodes currently contain a link to their parents that
                // is mutated during AddChild{ren}, NumberLiteral nodes are not immutable
                // despite having no children. So we have to make a defensive copy.
                var newNode = AstNode.NumberLiteral(node.Value);

                addExpr.Add(newNode);
                // _parent stays the same when visiting childless nodes.
            }

            private AstNode InjectAddExpression()
            {
                var addExpr = AstNode.CallExpression("add");
                if (_root == null)
                {
                    _root = addExpr;
                }
                else
                {
                    _parent.Add(addExpr);
                }

                string addendString = _transform.Addend.ToString();
                addExpr.Add(AstNode.NumberLiteral(addendString));

                return addExpr;
            }
        }
    }
}
