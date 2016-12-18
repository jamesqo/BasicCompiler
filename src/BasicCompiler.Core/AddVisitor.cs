using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class AddVisitor : IAstVisitor
    {
        private readonly string _addendString;
        private AstNode _root;
        private AstNode _parent;

        // `bool rhs`?
        public AddVisitor(int addend)
        {
            if (addend < 0)
            {
                // Negative numbers are not supported yet.
                throw new ArgumentOutOfRangeException(nameof(addend));
            }

            _addendString = addend.ToString();
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

            addExpr.AddChild(newNode);
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

            addExpr.AddChild(newNode);
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
                _parent.AddChild(addExpr);
            }

            addExpr.AddChild(AstNode.NumberLiteral(_addendString));

            return addExpr;
        }
    }
}
