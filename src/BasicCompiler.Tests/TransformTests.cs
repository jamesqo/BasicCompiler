namespace BasicCompiler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BasicCompiler.Core;
    using Xunit;

    public class TransformTests
    {
        [Theory]
        [ClassData(typeof(SampleInputs))]
        public void TransformAst(ExpectedResult er)
        {
            var transforms = er.Transforms.ToArray();
            var newAsts = er.NewAsts.ToArray();

            Assert.Equal(transforms.Length, newAsts.Length);

            Ast baseAst = er.Ast;

            for (int i = 0; i < transforms.Length; i++)
            {
                Ast newAst = transforms[i].Apply(baseAst);

                Assert.Equal(newAsts[i], newAst);
            }
        }
    }
}
