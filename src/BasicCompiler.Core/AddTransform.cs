namespace BasicCompiler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddTransform : IAstTransform
    {
        // `bool rhs`?
        public AddTransform(int addend)
        {
            if (addend < 0)
            {
                // Negative numbers are not supported yet.
                throw new ArgumentOutOfRangeException(nameof(addend));
            }

            Addend = addend;
        }

        internal int Addend { get; }

        public IAstTransformer CreateTransformer() => new AddTransformer(this);
    }
}
