namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAstVisitor
    {
        // TODO: Is there a better pattern for this?
        void LeaveCallExpression(AstNode node);

        void VisitCallExpression(AstNode node);

        void VisitNumberLiteral(AstNode node);
    }
}
