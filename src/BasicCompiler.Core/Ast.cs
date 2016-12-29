namespace BasicCompiler.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// An AST tree.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Ast : IEquatable<Ast>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ast"/> class.
        /// </summary>
        /// <param name="root">The root of this tree.</param>
        public Ast(AstNode root)
        {
            // TODO: This is a temp workaround for the .NET CLI apparently not supporting
            // C# 7 yet. Once it does, write this as an arrow expression.
            Root = root;
        }

        /// <summary>
        /// Gets the root of this tree.
        /// </summary>
        public AstNode Root { get; }

        internal string DebuggerDisplay => $"Root: {{ {Root.DebuggerDisplay} }}";

        /// <summary>
        /// Does a depth-first traversal of this tree.
        /// </summary>
        /// <param name="visitor">The AST visitor used to traverse this tree.</param>
        public void Accept(IAstVisitor visitor) => Root.Accept(visitor);

        public override bool Equals(object obj) => obj is Ast ast && Equals(ast);

        // TODO: Add equality operators to both classes. (What classes?)
        public bool Equals(Ast other) => other?.Root != null && Root.Equals(other.Root);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
