using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class AddTransform : IAstTransform
    {
        // `bool rhs`?
        public AddTransform(int addend)
        {
            if (addend < 0)
            {
                // Negative numbers are not supported yet.
                throw new ArgumentOutOfRangeException(nameof(addend));
            }

            Addend = addend;
        }

        internal int Addend { get; }

        public IAstTransformer CreateTransformer() => new AddTransformer(this);
    }

    internal class AddTransformer : IAstTransformer
    {
        private readonly AddTransform _transform;
        private AstNode _root;
        private AstNode _parent;

        public Ast NewAst => new Ast(_root);

        internal AddTransformer(AddTransform transform)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void LeaveCallExpression(AstNode node)
        {
            var addExpr = _parent.Parent;

            if (addExpr.Value != "add")
            {
                throw new AstVisitorException("Expected injected node to be 'add'.");
            }

            _parent = addExpr.Parent;
        }

        public void VisitCallExpression(AstNode node)
        {
            var addExpr = InjectAddExpression();
            var newNode = AstNode.CallExpression(node.Value);

            addExpr.Add(newNode);
            _parent = newNode; // When we leave this CallExpression, _parent will be set to
                               // its previous value in LeaveCallExpression
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
