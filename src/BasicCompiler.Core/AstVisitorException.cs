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

        /// <summary>
        /// Initializes a new instance of the <see cref="AstVisitorException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public AstVisitorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AstVisitorException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AstVisitorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
