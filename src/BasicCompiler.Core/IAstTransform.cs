using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCompiler.Core
{
    public interface IAstTransform
    {
        IAstTransformer CreateTransformer();
    }
}
