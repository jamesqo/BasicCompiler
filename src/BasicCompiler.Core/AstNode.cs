using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class AstNode : IEquatable<AstNode>
    {
        private readonly List<AstNode> _children;

        private AstNode _parent;

        private AstNode(string value, NodeType type)
        {
            Value = value;
            Type = type;
            _children = new List<AstNode>();
        }

        public static AstNode CallExpression(string value) => new AstNode(value, NodeType.CallExpression);

        public static AstNode NumberLiteral(string value) => new AstNode(value, NodeType.NumberLiteral);

        public bool IsRoot => _parent == null;

        public AstNode Parent => _parent;

        public string Value { get; }

        public IReadOnlyList<AstNode> Children => _children.AsReadOnly();

        public NodeType Type { get; }

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

        public AstNode AddChild(AstNode child)
        {
            _children.Add(child);
            child._parent = this;
            return this;
        }

        public AstNode AddChildren(params AstNode[] children)
        {
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
