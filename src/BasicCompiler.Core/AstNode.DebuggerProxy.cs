using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BasicCompiler.Core
{
    public partial class AstNode
    {
        private class DebuggerProxy
        {
            private readonly AstNode _node;

            public DebuggerProxy(AstNode node)
            {
                _node = node ?? throw new ArgumentNullException(nameof(node));
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public AstNode[] Children => _node.Children.ToArray();
        }
    }
}
