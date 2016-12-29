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
        private readonly List<AstNode> _children;

        private AstNode _parent;

        private AstNode(string value, NodeType type)
        {
            Value = value;
            Type = type;
            _children = new List<AstNode>();
        }

        public IReadOnlyList<AstNode> Children => _children.AsReadOnly();

        // TODO: More performant implementation.
        public int Depth => IsLeaf ? 0 : _children.Max(c => c.Depth) + 1;

        public bool IsLeaf => _children.Count == 0;

        public bool IsRoot => _parent == null;

        public AstNode Parent => _parent;

        public NodeType Type { get; }

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

        public static AstNode CallExpression(string value) => new AstNode(value, NodeType.CallExpression);

        public static AstNode ExpressionStatement() => new AstNode(null, NodeType.ExpressionStatement);

        public static AstNode NumberLiteral(string value) => new AstNode(value, NodeType.NumberLiteral);

        public void Accept(IAstVisitor visitor)
        {
            // Do a depth-first traversal.

            switch (Type)
            {
                case NodeType.CallExpression:
                    visitor.VisitCallExpression(this);
                    break;
                case NodeType.NumberLiteral:
                    visitor.VisitNumberLiteral(this);
                    break;
            }

            foreach (AstNode child in _children)
            {
                child.Accept(visitor);
            }

            switch (Type)
            {
                case NodeType.CallExpression:
                    visitor.LeaveCallExpression(this);
                    break;
            }
        }

        public AstNode Add(AstNode child)
        {
            // TODO: Validate we're not a childless node like a NumberLiteral.

            _children.Add(child);
            child._parent = this;
            return this;
        }

        public AstNode AddTwo(AstNode child1, AstNode child2)
        {
            // TODO: Validate we're not a childless node like a NumberLiteral.

            _children.Add(child1);
            _children.Add(child2);
            child1._parent = this;
            child2._parent = this;
            return this;
        }

        public AstNode AddMany(params AstNode[] children)
        {
            // TODO: Validate we're not a childless node like a NumberLiteral.

            _children.AddRange(children);

            foreach (AstNode child in children)
            {
                child._parent = this;
            }

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
