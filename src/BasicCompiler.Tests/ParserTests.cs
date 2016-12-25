namespace BasicCompiler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BasicCompiler.Core;
    using Xunit;

    public class ParserTests
    {
        [Theory]
        [ClassData(typeof(SampleInputs))]
        public void Parse(ExpectedResult er)
        {
            Assert.Equal(er.Ast, Parser.Parse(er.Tokens));
        }

        [Theory]
        [MemberData(nameof(PropertiesData))]
        public void Properties(AstNode node, string value, NodeType type)
        {
            Assert.Equal(value, node.Value);
            Assert.Equal(type, node.Type);
        }

        public static TheoryData<AstNode, string, NodeType> PropertiesData()
        {
            return new TheoryData<AstNode, string, NodeType>
            {
                { AstNode.CallExpression("foo"), "foo", NodeType.CallExpression },
                { AstNode.NumberLiteral("100"), "100", NodeType.NumberLiteral }
            };
        }
    }
}
