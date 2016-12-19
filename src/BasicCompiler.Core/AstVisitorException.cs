using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class AstVisitorException : Exception
    {
        public AstVisitorException()
        {
        }

        public AstVisitorException(string message) : base(message)
        {
        }

        public AstVisitorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
