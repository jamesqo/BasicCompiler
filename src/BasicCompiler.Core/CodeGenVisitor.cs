using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    internal class CodeGenVisitor : IAstVisitor
    {
        private readonly StringBuilder _sb;
        private bool _visitingFirstArg;

        internal CodeGenVisitor()
        {
            _sb = new StringBuilder();
            _visitingFirstArg = true;
        }

        public void LeaveCallExpression(AstNode node)
        {
            if (_sb.Length == 0)
            {
                throw new AstVisitorException("Leaving CallExpression, but nothing has been appended to the StringBuilder yet.");
            }

            if (_visitingFirstArg ^ _sb[_sb.Length - 1] != '(')
            {
                throw new AstVisitorException("The last character should be an open parenthesis iff there are no arguments.");
            }

            _sb.Append(')');
            _visitingFirstArg = true;
        }

        public void LeaveExpressionStatement(AstNode node)
        {
            if (_sb.Length == 0 || _sb[_sb.Length - 1] != ')')
            {
                throw new AstVisitorException("Did not find closing parenthesis after leaving CallExpression.");
            }

            if (!_visitingFirstArg)
            {
                throw new AstVisitorException("Leaving an ExpressionStatement in the middle of appending arguments?");
            }

            _sb.Append(';');
        }

        public void VisitCallExpression(AstNode node)
        {
            if (!_visitingFirstArg)
            {
                throw new AstVisitorException("Visiting a CallExpression in the middle of appending arguments?");
            }

            _sb.Append(node.Value).Append('(');
        }

        public void VisitExpressionStatement(AstNode node)
        {
            // No-op. Appending the callee name will be taken care of by VisitCallExpression.
        }

        public void VisitNumberLiteral(AstNode node)
        {
            // TODO: Extend check to non-first arguments.
            if (_visitingFirstArg && (_sb.Length == 0 || _sb[_sb.Length - 1] != '('))
            {
                throw new AstVisitorException("Visiting a NumberLiteral node when not in a CallExpression?");
            }

            if (!_visitingFirstArg)
            {
                _sb.Append(", ");
            }
            else
            {
                _visitingFirstArg = false;
            }

            _sb.Append(node.Value);
        }

        public override string ToString() => _sb.ToString();
    }
}
