using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    interface GenerateBehavior
    {
        //List<int[,]> Generate(int[,] pBoard, bool isXPlaying);
        //List<int[,]> GenerateV2(int[,] pBoard, bool isXPlaying);
        List<int[,]> GenerateV3(int[,] pBoard, bool isXPlaying);
    }
}
