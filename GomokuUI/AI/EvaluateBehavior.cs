using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    interface EvaluateBehavior
    {
        void init(int[,] board);
        int Evaluate();
    }
}
