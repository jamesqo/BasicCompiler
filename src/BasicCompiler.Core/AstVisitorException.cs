namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An exception thrown when an operation by an <see cref="IAstVisitor"/> fails.
    /// </summary>
    public class AstVisitorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AstVisitorException"/> class.
        /// </summary>
        public AstVisitorException()
        {
        }

        public AstVisitorException(string message)
            : base(message)
        {
        }

        public AstVisitorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
