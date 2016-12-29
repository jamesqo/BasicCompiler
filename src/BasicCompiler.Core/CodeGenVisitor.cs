namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An AST visitor used to generate code for an AST.
    /// </summary>
    internal class CodeGenVisitor : IAstVisitor
    {
        /// <summary>
        /// The <see cref="StringBuilder"/> used to build the output string.
        /// </summary>
        private readonly StringBuilder _sb;

        /// <summary>
        /// A stack of booleans indicating whether the current argument at each call site is the first.
        /// </summary>
        private readonly Stack<bool> _visitingFirstArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenVisitor"/> class.
        /// </summary>
        internal CodeGenVisitor()
        {
            _sb = new StringBuilder();
            _visitingFirstArgs = new Stack<bool>();
        }

        public void LeaveCallExpression(AstNode node)
        {
            if (_sb.Length == 0)
            {
                throw new AstVisitorException("Leaving CallExpression, but nothing has been appended to the StringBuilder yet.");
            }

            if (_visitingFirstArgs.Pop() ^ _sb[_sb.Length - 1] == '(')
            {
                throw new AstVisitorException("The last character should be an open parenthesis iff there are no arguments.");
            }

            _sb.Append(')');

            // If this CallExpression is the first argument to another CallExpression, e.g.
            // (multiply (divide 9 111) 0), we want to make sure that we update the status of
            // `_visitingFirstArgs` appropriately for the next level up in the stack.
            // Note: If we're the top-level CallExpression, the stack will be empty, in which
            // case we don't have to do anything.
            if (_visitingFirstArgs.Count > 0)
            {
                _visitingFirstArgs.Pop();
                _visitingFirstArgs.Push(false);
            }
        }

        public void LeaveExpressionStatement(AstNode node)
        {
            if (_sb.Length == 0 || _sb[_sb.Length - 1] != ')')
            {
                throw new AstVisitorException("Did not find closing parenthesis after leaving CallExpression.");
            }

            _sb.Append(';');
        }

        public void VisitCallExpression(AstNode node)
        {
            PrepareForNextArgument();
            _sb.Append(node.Value).Append('(');
            _visitingFirstArgs.Push(true);
        }

        public void VisitExpressionStatement(AstNode node)
        {
            // No-op. Everything will be taken care of by VisitCallExpression.
        }

        public void VisitNumberLiteral(AstNode node)
        {
            // TODO: Extend check to non-first arguments.
            if (_visitingFirstArgs.Peek() && (_sb.Length == 0 || _sb[_sb.Length - 1] != '('))
            {
                throw new AstVisitorException("Visiting a NumberLiteral node when not in a CallExpression?");
            }

            PrepareForNextArgument();
            _sb.Append(node.Value);
        }

        /// <summary>
        /// Gets the generated code as a string.
        /// </summary>
        /// <returns>The generated code, as a string.</returns>
        public override string ToString() => _sb.ToString();

        /// <summary>
        /// Prepares this visitor for an argument that will be subsequently appended.
        /// </summary>
        private void PrepareForNextArgument()
        {
            // The context stack can be empty if we're a top-level CallExpression. In that case,
            // there's nothing to do.
            if (_visitingFirstArgs.Count > 0)
            {
                if (!_visitingFirstArgs.Peek())
                {
                    // This is not the first argument at this call site.
                    _sb.Append(", ");
                }
                else
                {
                    // This is the first argument at this call site. After we insert this argument,
                    // the next argument will not be the first one, so update the context for this
                    // call site on the stack.
                    _visitingFirstArgs.Pop();
                    _visitingFirstArgs.Push(false);
                }
            }
        }
    }
}
