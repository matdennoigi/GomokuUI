using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    class Minimax : AI
    {
        public int[,] Think(int[,] pBoard)
        {
            int[,] a = new int[1, 1];
            return a;
        }
        private int[,] mBestState;
        private GenerateBehavior mGenerateBehavior;
        private EvaluateBehavior mEvaluateBehavior;
        public void SetGenerateBehavior(GenerateBehavior generateBehavior)
        {
            this.mGenerateBehavior = generateBehavior;
        }

        public void SetEvaluateBehavior(EvaluateBehavior evaluateBehavior)
        {
            this.mEvaluateBehavior = evaluateBehavior;
        }

        public Minimax(GenerateBehavior mGenerateBehavior, EvaluateBehavior mEvaluateBehavior)
        {
            this.mEvaluateBehavior = mEvaluateBehavior;
            this.mGenerateBehavior = mGenerateBehavior;
        }

        public int Min(int[,] pBoard, int depth)
        {

            int[,] trace = null;
            int best;
            int k;
            mEvaluateBehavior.init(pBoard);
            int diem = mEvaluateBehavior.Evaluate();
            if ((diem > 800000) || (diem < -800000))
            {
                mBestState = pBoard;
                return diem;
            }

            if (depth == 0)
            {
                mEvaluateBehavior.init(pBoard);
                return mEvaluateBehavior.Evaluate();
            }
            else
            {
                best = 1000000;
                List<int[,]> listBoard = mGenerateBehavior.GenerateV3(pBoard, false);
                if ((listBoard != null) && (listBoard.Count > 0))
                {
                    for (int i = 0; i < listBoard.Count; i++)
                    {
                        k = Max(listBoard[i], depth - 1);
                        if (k < best)
                        {
                            best = k;
                            trace = listBoard[i];
                        }
                    }
                    mBestState = trace;
                }
            }

            return best;
        }

        public int Max(int[,] pBoard, int depth)
        {
            int[,] trace = null;
            int best;
            int k;
            int diem = mEvaluateBehavior.Evaluate();
            if ((diem > 800000) || (diem < -800000))
            {
                mBestState = pBoard;
                return diem;
            }

            if (depth == 0)
            {
                mEvaluateBehavior.init(pBoard);
                return mEvaluateBehavior.Evaluate();
            }
            else
            {
                best = -1000000;
                List<int[,]> listBoard = mGenerateBehavior.GenerateV3(pBoard, true);
                if ((listBoard != null) && (listBoard.Count > 0))
                {
                    for (int i = 0; i < listBoard.Count; i++)
                    {
                        k = Min(listBoard[i], depth - 1);
                        if (k > best)
                        {
                            best = k;
                            trace = listBoard[i];

                        }
                    }
                    mBestState = trace;
                }
            }
            return best;
        }

        public int Max_AlphaBeta(int[,] pBoard, int depth, int beta)
        {
            int[,] trace = null;
            int best;
            int k;
            mEvaluateBehavior.init(pBoard);
            int diem = mEvaluateBehavior.Evaluate();
            if ((diem > (0.8 * MyEvaluate.FIVE_IN_A_LINE)) || (diem < (-0.8 * MyEvaluate.FIVE_IN_A_LINE)))
            {
                mBestState = pBoard;
                return diem;
            }

            if (depth == 0)
            {
                mEvaluateBehavior.init(pBoard);
                return mEvaluateBehavior.Evaluate();
            }
            else
            {
                best = int.MinValue;
                List<int[,]> listBoard = mGenerateBehavior.GenerateV3(pBoard, true);
                if ((listBoard != null) && (listBoard.Count > 0))
                {
                    for (int i = 0; i < listBoard.Count; i++)
                    {
                        k = Min_AlphaBeta(listBoard[i], depth - 1, best);
                        if (k >= beta)
                        {
                            return k;
                        }
                        else
                        {
                            if (best < k)
                            {
                                best = k;
                                trace = listBoard[i];
                            }
                        }
                    }
                    mBestState = trace;
                }
            }
            return best;
        }

        public int Min_AlphaBeta(int[,] pBoard, int depth, int alpha)
        {

            int[,] trace = null;
            int best;
            int k;
            mEvaluateBehavior.init(pBoard);
            int diem = mEvaluateBehavior.Evaluate();
            if ((diem > 0.8 * MyEvaluate.FIVE_IN_A_LINE) || (diem < -0.8 * MyEvaluate.FIVE_IN_A_LINE))
            {
                mBestState = pBoard;
                return diem;
            }

            if (depth == 0)
            {
                mEvaluateBehavior.init(pBoard);
                return mEvaluateBehavior.Evaluate();
            }
            else
            {
                best = int.MaxValue;
                List<int[,]> listBoard = mGenerateBehavior.GenerateV3(pBoard, false);
                if ((listBoard != null) && (listBoard.Count > 0))
                {
                    for (int i = 0; i < listBoard.Count; i++)
                    {
                        k = Max_AlphaBeta(listBoard[i], depth - 1, best);
                        if (k <= alpha)
                        {
                            return k;
                        }
                        else
                        {
                            if (best > k)
                            {
                                best = k;
                                trace = listBoard[i];
                            }
                        }
                    }
                    mBestState = trace;
                }
            }

            return best;
        }

        public int[,] getBestState()
        {
            return this.mBestState;
        }


    }
}
