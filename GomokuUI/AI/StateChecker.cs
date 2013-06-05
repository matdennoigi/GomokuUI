using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuUI.AI
{
    class StateChecker
    {
        public static int[] CheckState(int[,] oldState, int[,] newState)
        {
            int[] a = new int[2];
            for (int i = 0; i < oldState.GetLength(0); i++ )
            {
                for (int j = 0; j < oldState.GetLength(1); j++)
                {
                    if (oldState[i,j] != newState[i,j])
                    {
                        a[0] = i;
                        a[1] = j;
                        return a;
                    }
                }
            }
            return a;
        }
    }
}
