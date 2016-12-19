using BasicCompiler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BasicCompiler.Tests
{
    public class TransformTests
    {
        [Theory, ClassData(typeof(SampleInputs))]
        public void TransformAst(ExpectedResult er)
        {
            var transforms = er.Transforms.ToArray();
            var newAsts = er.NewAsts.ToArray();

            Assert.Equal(transforms.Length, newAsts.Length);

            Ast currentAst = er.Ast;

            for (int i = 0; i < transforms.Length; i++)
            {
                var transformer = transforms[i].CreateTransformer();
                currentAst.Accept(transformer);
                Ast newAst = transformer.NewAst;

                Assert.Equal(newAsts[i], newAst);
            }
        }
    }
}
