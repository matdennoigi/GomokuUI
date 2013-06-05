using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    interface AI
    {

        int[,] Think(int[,] pBoard);
        void SetGenerateBehavior(GenerateBehavior generateBehavior);
        void SetEvaluateBehavior(EvaluateBehavior evaluateBehavior);
    }
}
