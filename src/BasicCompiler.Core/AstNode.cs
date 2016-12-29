namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An mutable AST node that has access to its parent and its children.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [DebuggerTypeProxy(typeof(DebuggerProxy))]
    public partial class AstNode : IEquatable<AstNode>
    {
        /// <summary>
        /// The children of this node.
        /// </summary>
        private readonly List<AstNode> _children;

        /// <summary>
        /// The parent of this node.
        /// </summary>
        private AstNode _parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AstNode"/> class.
        /// </summary>
        /// <param name="value">The value of this node.</param>
        /// <param name="type">The semantic type of this node.</param>
        private AstNode(string value, NodeType type)
        {
            Value = value;
            Type = type;
            _children = new List<AstNode>();
        }

        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        public IReadOnlyList<AstNode> Children => _children.AsReadOnly();

        // TODO: More performant implementation.

        /// <summary>
        /// Gets the depth of this node.
        /// </summary>
        public int Depth => IsLeaf ? 0 : _children.Max(c => c.Depth) + 1;

        /// <summary>
        /// Gets a value indicating whether this node is childless.
        /// </summary>
        public bool IsLeaf => _children.Count == 0;

        /// <summary>
        /// Gets a value indicating whether this node is a root node.
        /// </summary>
        public bool IsRoot => _parent == null;

        /// <summary>
        /// Gets the parent of this node, or <c>null</c> if it has none.
        /// </summary>
        public AstNode Parent => _parent;

        /// <summary>
        /// Gets the semantic type of this node.
        /// </summary>
        public NodeType Type { get; }

        /// <summary>
        /// Gets the value of this node.
        /// </summary>
        public string Value { get; }

        internal string DebuggerDisplay
        {
            get
            {
                return $"{nameof(Type)}: {Type}, "
                    + $"{nameof(Value)}: {Value}, "
                    + $"{nameof(_children.Count)}: {_children.Count}, "
                    + $"{nameof(Depth)}: {Depth}";
            }
        }

        /// <summary>
        /// Creates a node with type <see cref="NodeType.CallExpression"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new node.</returns>
        public static AstNode CallExpression(string value) => new AstNode(value, NodeType.CallExpression);

        /// <summary>
        /// Creates a node with type <see cref="NodeType.ExpressionStatement"/>.
        /// </summary>
        /// <returns>The new node.</returns>
        public static AstNode ExpressionStatement() => new AstNode(null, NodeType.ExpressionStatement);

        /// <summary>
        /// Creates a node with type <see cref="NodeType.NumberLiteral"/>.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        /// <returns>The new node.</returns>
        public static AstNode NumberLiteral(string value) => new AstNode(value, NodeType.NumberLiteral);

        /// <summary>
        /// Does a depth-first traversal of this node and its descendants.
        /// </summary>
        /// <param name="visitor">The AST visitor used to traverse this node.</param>
        public void Accept(IAstVisitor visitor)
        {
            switch (Type)
            {
                case NodeType.CallExpression:
                    visitor.VisitCallExpression(this);
                    break;
                case NodeType.ExpressionStatement:
                    visitor.VisitExpressionStatement(this);
                    break;
                case NodeType.NumberLiteral:
                    visitor.VisitNumberLiteral(this);
                    break;
                default:
                    throw new NotImplementedException("Did you forget to add support for a new node type?");
            }

            foreach (AstNode child in _children)
            {
                child.Accept(visitor);
            }

            // If we're a type of node that can have children, we'll want to notify the visitor
            // that we're leaving this node so it gets a chance to update its context accordingly.

            switch (Type)
            {
                case NodeType.CallExpression:
                    visitor.LeaveCallExpression(this);
                    break;
                case NodeType.ExpressionStatement:
                    visitor.LeaveExpressionStatement(this);
                    break;
            }
        }

        /// <summary>
        /// Adds a child node to this node.
        /// </summary>
        /// <param name="child">The child node.</param>
        /// <returns>This node.</returns>
        public AstNode Add(AstNode child)
        {
            // TODO: Validate we're not a childless node like a NumberLiteral.

            _children.Add(child);
            child._parent = this;
            return this;
        }

        /// <summary>
        /// Adds two child nodes to this node.
        /// </summary>
        /// <param name="child1">The first child.</param>
        /// <param name="child2">The second child.</param>
        /// <returns>This node.</returns>
        public AstNode AddTwo(AstNode child1, AstNode child2)
        {
            // TODO: Validate we're not a childless node like a NumberLiteral.

            _children.Add(child1);
            _children.Add(child2);
            child1._parent = this;
            child2._parent = this;
            return this;
        }

        public override bool Equals(object obj) => obj is AstNode astn && Equals(astn);

        public bool Equals(AstNode other)
        {
            if (other == null ||
                Type != other.Type ||
                Value != other.Value ||
                // (object)Parent != other.Parent || // TODO: What's the best way to handle this?
                _children.Count != other._children.Count)
            {
                return false;
            }

            for (int i = 0; i < _children.Count; i++)
            {
                if (!_children[i].Equals(other._children[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
