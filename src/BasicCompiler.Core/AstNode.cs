using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class AstNode : IEquatable<AstNode>
    {
        private readonly List<AstNode> _children;

        private AstNode(string value, NodeType type)
        {
            Value = value;
            Type = type;
            _children = new List<AstNode>();
        }

        public static AstNode CallExpression(string value) => new AstNode(value, NodeType.CallExpression);

        public static AstNode NumberLiteral(string value) => new AstNode(value, NodeType.NumberLiteral);

        public string Value { get; }
        public IReadOnlyList<AstNode> Children => _children.AsReadOnly();
        public NodeType Type { get; }

        public AstNode AddChild(AstNode child)
        {
            _children.Add(child);
            return this;
        }

        public AstNode AddChildren(params AstNode[] children)
        {
            _children.AddRange(children);
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
    }
}
