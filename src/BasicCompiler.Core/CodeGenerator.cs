using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public class CodeGenerator
    {
        public static string Stringify(Ast ast)
        {
            if (ast == null)
            {
                throw new ArgumentNullException(nameof(ast));
            }

            var visitor = new CodeGenVisitor();
            ast.Accept(visitor);
            return visitor.ToString();
        }
    }
}
