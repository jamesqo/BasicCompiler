using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class Ast : IEquatable<Ast>
    {
        public Ast(AstNode root) => Root = root;

        public AstNode Root { get; }

        public override bool Equals(object obj) => obj is Ast ast && Equals(ast);

        // TODO: Add equality operators to both classes.
        public bool Equals(Ast other) => other?.Root != null && Root.Equals(other.Root);
    }
}
