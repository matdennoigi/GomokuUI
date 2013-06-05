using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    class MyGenerate : GenerateBehavior
    {
        const int DISTANCE = 1; // DISTANCE max < kich thuoc ban co chia 2
        const int Xs = 1;
        const int Os = -1;
        const int EMPTY = 0;
        float[][] dx = new float[2][];
        float[][] dy = new float[2][];

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBoard">la 1 ban co</param>
        /// <param name="isXPlaying">luot di cua ai, X: true, O: false</param>
        /// <returns></returns>
        //public List<int[,]> Generate(int[,] pBoard, bool isXPlaying)
        //{
        //    // tempBoard su dung de xac dinh khong gian tim kiem nc di
        //    // do se la vung bao ngoai cua nhung o da dc danh tren ban co voi khoang cach la DISTANCE o day la 2 o
        //    // tempBoard la 1 mang 2 chieu nhu  Board va cac o ma ta se tim kiem nc di dc danh dau voi chu 'y'
            
        //    int row = pBoard.GetLength(0);
        //    int col = pBoard.GetLength(1);
        //    char[,] tempBoard = new char[row, col];
        //    int[,] mBoard;
        //    List<int[,]> listBoard = new List<int[,]>();

        //    // duyet het mang pBoard va danh dau cac o se thu nuoc di o mang tempBoard
        //    for (int i = 0; i < row; i++ )
        //    {
        //        for (int j = 0; j < col;j++ )
        //        {
        //            // xet cac o trong, neu xung quanh no co 1 nc di, thi danh dau o do vao tempBoard la 'y'
        //            if ((pBoard[i,j] == EMPTY)&&(IsItAround(i,j,pBoard)))
        //            {
        //                tempBoard[i, j] = 'y';
                        
        //            }
        //        }
        //    }

        //    //test
        //    //for (int i = 0; i < row; i++)
        //    //{
        //    //    for (int j = 0; j < col; j++)
        //    //    { // xet cac o trong, neu xung quanh no co 1 nc di, thi danh dau o do vao tempBoard la 'y'
        //    //        Console.Write(tempBoard[i,j]);
        //    //        Console.Write(' ');
        //    //    }
        //    //    Console.WriteLine();
        //    //}

        //    // duyet tempboard, neu o nao la 'y' thi danh dau o do tuong ung ben pBoard: la nc co the di
        //    // voi moi 1 o ta se co 1 pBoard khac, add cac pBoard vao 1 list => do la ko gian tim kiem
        //    for (int i = 0; i < row;i++ )
        //    {
        //        for (int j = 0; j < col; j++ )
        //        {
        //            if (tempBoard[i,j].CompareTo('y') == 0)
        //            {
                        
        //                if (isXPlaying)
        //                {
        //                    pBoard[i, j] = 1; // neu la luot di cua X thi danh 1 vao o do
        //                }
        //                else pBoard[i, j] = -1;
        //                mBoard = (int[,])pBoard.Clone();
        //                listBoard.Add(mBoard); // add the co vao list

        //                pBoard[i, j] = 0; // tra lai the co ban dau de tinh nc di khac
        //            }
        //        }
        //    }
        //    return listBoard;
        //}

        public List<int[,]> GenerateV3(int[,] pBoard, bool isXPlaying)
        {
            if (IsBoardEmpty(pBoard))
            {
                int row = pBoard.GetLength(0);
                int col = pBoard.GetLength(1);
                int[,] mBoard;
                List<int[,]> listBoard = new List<int[,]>();
                pBoard[pBoard.GetLength(0) / 2, pBoard.GetLength(1) / 2] = 1;
                mBoard = (int[,])pBoard.Clone();
                listBoard.Add(mBoard);
                return listBoard;

            }
            else
            {
                int row = pBoard.GetLength(0);
                int col = pBoard.GetLength(1);
                int[,] tempBoard = (int[,])pBoard.Clone(); ;
                int[,] mBoard;
                List<int[,]> listBoard = new List<int[,]>();
                dx[0] = new float[] { -1, -1, -1, 0, 0, 1, 1, 1 };
                dy[0] = new float[] { -1, 0, 1, -1, 1, -1, 0, 1 };
                dx[1] = new float[] { -1, -1, -1, -1, -1, -0.5f, -0.5f, 0, 0, 0.5f, 0.5f, 1, 1, 1, 1, 1 };

                dy[1] = new float[] { -1, -0.5f, 0, 0.5f, 1, -1, 1, -1, 1, -1, 1, -1, -0.5f, 0, 0.5f, 1 };

                // xet cac o co nc di, neu xung quanh no co o trong thi danh dau vi tri o trong lai
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (pBoard[i, j] != EMPTY)
                        {
                            // xet cac o xung quanh o [i,j] voi ban kinh bang DISTANCE
                            for (int k = 1; k <= DISTANCE; k++)
                            {
                                for (int l = 0; l < dx[k - 1].Length; l++)
                                {
                                    float x1 = i + k * dx[k - 1][l];
                                    float y1 = j + k * dy[k - 1][l];
                                    if ((IsInOfIndex(x1, y1, row)) && (tempBoard[(int)x1, (int)y1] == EMPTY))
                                    {

                                        if (isXPlaying)
                                        {
                                            tempBoard[(int)x1, (int)y1] = 1;
                                            pBoard[(int)x1, (int)y1] = 1;
                                        }
                                        else
                                        {
                                            tempBoard[(int)x1, (int)y1] = -1;
                                            pBoard[(int)x1, (int)y1] = -1;
                                        }
                                        mBoard = (int[,])pBoard.Clone();
                                        listBoard.Add(mBoard); // add the co vao list

                                        pBoard[(int)x1, (int)y1] = 0; // tra lai the co ban dau de tinh nc di khac
                                    }
                                }
                            }
                        }
                    }
                }

                return listBoard;
            }
        }

        /// <summary>
        /// kiem tra xem xung quanh 1 o [x,y] co o nao dc danh chua
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pTempBoard"></param>
        /// <returns></returns>
        private bool IsItAround(int x, int y, int[,] pBoard)
        {
            
            int n = pBoard.GetLength(0);
            dx[0] = new float[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            dy[0] = new float[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            dx[1] = new float[] { -1, -1,   -1, -1,  -1, -0.5f, -0.5f,   0, 0, 0.5f, 0.5f, 1, 1, 1, 1, 1 };
            
            dy[1] = new float[] { -1, -0.5f, 0, 0.5f, 1, -1    ,     1, -1, 1,   -1,    1, -1, -0.5f, 0, 0.5f, 1 };
            for (int i = 1; i <= DISTANCE;i++ ) // dung nham i=0
            {
                for (int j = 0; j < dx[i-1].Length; j++ )
                {
                    // xet cac o xung quanh o can xet
                    float x1 = x + i * dx[i - 1][j];
                    float y1 = y + i * dy[i - 1][j];
                    if (IsInOfIndex(x1, y1, n))
                    {
                        if ( pBoard[(int)x1, (int)y1] != 0 )
                        {
                            return true;
                        }
                        
                    }
                }
                
            }

            return false;
        }

        private bool IsInOfIndex(float x, float y, int n)
        {
            if ((x>=0)&&(y>=0)&&(x<n)&&(y<n))
            {
                return true;
            }
            return false;
        }

        private bool IsBoardEmpty(int[,] pBoard)
        {
            bool flag = true;
            for (int i = 0; i < pBoard.GetLength(0); i++)
            {
                for (int j = 0; j < pBoard.GetLength(1); j++)
                {
                    if (pBoard[i, j] != 0)
                    {
                        flag = false;
                        return flag;
                    }
                }
            }

            return flag;
        }
        

        
    }
}
