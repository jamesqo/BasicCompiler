using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    internal struct ParseState
    {
        private IEnumerator<Token> _enumerator;
        private int _parenthesisDepth;

        public IEnumerator<Token> Enumerator => _enumerator;
        public int ParenthesisDepth => _parenthesisDepth;

        public ParseState WithEnumerator(IEnumerator<Token> value)
        {
            var copy = this;
            copy._enumerator = value;
            return copy;
        }

        public ParseState WithParenthesisDepth(int value)
        {
            var copy = this;
            copy._parenthesisDepth = value;
            return copy;
        }
    }
}
