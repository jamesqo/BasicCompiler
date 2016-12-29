namespace BasicCompiler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BasicCompiler.Core;
    using Xunit;

    public class CodeGenTests
    {
        [Theory]
        [ClassData(typeof(SampleInputs))]
        public void GenerateCode(ExpectedResult er)
        {
            if (er.Outputs == null)
            {
                return;
            }

            Assert.Equal(er.NewAsts?.Count(), er.Outputs.Count());

            var newAsts = er.NewAsts.ToArray();
            var outputs = er.Outputs.ToArray();

            for (int i = 0; i < newAsts.Length; i++)
            {
                Assert.Equal(outputs[i], CodeGenerator.Stringify(newAsts[i]));
            }
        }
    }
}
