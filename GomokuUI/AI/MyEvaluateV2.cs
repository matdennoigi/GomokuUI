using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    class MyEvaluateV2 : EvaluateBehavior
    {
        const int FIVE_IN_A_LINE = 100000;
        const int LIVE_FOUR = 10000;
        const int SLEEP_FOUR = 1500;
        const int LIVE_THREE = 1000;
        const int SlEEP_THREE = 150;
        const int LIVE_TWO = 100;
        const int SLEEP_TWO = 15;
        const int LIVE_ONE = 10;
        const int SLEEP_ONE = 1;

        const int X_VALUE = 1;
        const int O_VALUE = -1;
        const int EMPTY_VALUE = 0;
        const int BLOCK_VALUE = 13;
        //
        int[,] ScoreArray = new int[2, 5] { { LIVE_ONE, LIVE_TWO, LIVE_THREE, LIVE_FOUR, FIVE_IN_A_LINE }, 
                                            { SLEEP_ONE, SLEEP_TWO, SlEEP_THREE, SLEEP_FOUR, FIVE_IN_A_LINE } };
        private int[,] mBoard;
        // mang trace de luu vet,xem 1 o da dc duyet theo chieu nao!
        // 4 chieu theo thu tu la: ngang, doc, cheo trai (\), cheo phai (/)
        private bool[,][] mTrace;
        private int row;
        private int col;
        public MyEvaluateV2(int[,] pBoard)
        {
            this.mBoard = pBoard;
            this.row = this.mBoard.GetLength(0);
            this.col = this.mBoard.GetLength(1);
            this.mTrace = new bool[row, col][];
            for (int i = 0; i < row; i++ )
            {
                for (int j = 0; j < col; j++ )
                {
                    mTrace[i,j] = new bool[4];
                }
            }
        }
        
        public void init(int[,] board)
        {
            this.mBoard = board;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    for (int k = 0; k < 4; k++ )
                    {
                        mTrace[i, j][k] = false;
                    }
                    
                }
            }
            
        }

        public int Evaluate()
        {
            int horizontalScore = 0;
            int verticalScore = 0;
            int leftDiagonalScore = 0;
            int rightDiagonalScore = 0;
            int allScore = 0;
            int totalScore = 0;
            // co gang dem xem o cac hang ngang, hang doc, hang cheo chinh, va hang cheo phu co bao nhieu o lien tiep
            // moi lan duyet,neu gap o doi phuong dung luon
            // gap o co quan danh thi moi duyet, gap o trong thi ko duyet
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (mBoard[i, j] != EMPTY_VALUE)
                    {
                        if (!mTrace[i, j][0])
                        {
                            horizontalScore = HorizontalTraverse(i, j);
                        }
                        else
                        {
                            horizontalScore = 0;
                        }

                        if (!mTrace[i, j][1])
                        {
                            verticalScore = VerticalTraverse(i, j);
                        }
                        else
                        {
                            verticalScore = 0;
                        }

                        if (!mTrace[i, j][2])
                        {
                            leftDiagonalScore = LeftDiagonalTraverse(i, j);
                        }
                        else
                        {
                            leftDiagonalScore = 0;
                        }

                        if (!mTrace[i, j][3])
                        {
                            rightDiagonalScore = RightDiagonalTraverse(i, j);
                        }
                        else
                        {
                            rightDiagonalScore = 0;
                        }

                        allScore = horizontalScore + verticalScore + leftDiagonalScore + rightDiagonalScore;
                        totalScore = totalScore + allScore;

                    }
                }
            }

            return totalScore;
        }

        private int HorizontalTraverse(int x, int y)
        {
            int count = 0;
            int i,j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int countEmpty = 0; // countEmty chi dem o empty ve phia ben phai
            int tempCell = mBoard[x, y];
            int emptyPosition = 0; // dung de xem o empty o vi tri nao
            int score = 0;
            //duyet ben phai
            i = x;
            j = y + 1;
            count = 1;
            mTrace[x, y][0] = true;
            while( IsInOfIndex(i,j,row) && (countEmpty<2) && ( mBoard[i,j] != (-tempCell) ) )
            {
                mTrace[i,j][0] = true;
                if (mBoard[i,j] == tempCell)
                {
                    count++;
                }
                if (mBoard[i,j] == EMPTY_VALUE)
                {
                    if (countEmpty == 0)
                    {
                        emptyPosition = count;
                    }
                    countEmpty++;
                    if (countEmpty == 2)
                    {
                        break;
                    }
                }
                j++;
            }
            if ( (!IsInOfIndex(i,j,row)) || (mBoard[i,j] == - tempCell) )
            {
                rightBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                rightBound = EMPTY_VALUE;
            }

            //duyet ben trai
            // vi ta duyet theo chieu tu trai sang phai => cac diem ben trai da dc duyet roi
            // duyet ben trai chi nham muc dich: xem can ben trai no la empty hay Block
            i = x;
            j = y - 1;            
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                leftBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                leftBound = EMPTY_VALUE;
            }
            score = EvalAfterTraverseCell(count, rightBound, leftBound, countEmpty, emptyPosition) * tempCell;

            return score;
        }
        private int VerticalTraverse(int x, int y)
        {
            int count = 0;
            int i, j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int countEmpty = 0; // countEmty chi dem o empty ve phia ben phai
            int tempCell = mBoard[x, y];
            int emptyPosition = 0; // dung de xem o empty o vi tri nao
            int score = 0;
            //duyet ben phai
            i = x + 1;
            j = y;
            count = 1;
            mTrace[x, y][1] = true;
            while (IsInOfIndex(i, j, row) && (countEmpty < 2) && (mBoard[i, j] != (-tempCell)))
            {
                mTrace[i, j][1] = true;
                if (mBoard[i, j] == tempCell)
                {
                    count++;
                }
                if (mBoard[i, j] == EMPTY_VALUE)
                {
                    if (countEmpty == 0)
                    {
                        emptyPosition = count;
                    }
                    countEmpty++;
                    if (countEmpty == 2)
                    {
                        break;
                    }
                }
                i++;
            }
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                rightBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                rightBound = EMPTY_VALUE;
            }

            //duyet ben trai
            // vi ta duyet theo chieu tu trai sang phai => cac diem ben trai da dc duyet roi
            // duyet ben trai chi nham muc dich: xem can ben trai no la empty hay Block
            i = x - 1;
            j = y;
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                leftBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                leftBound = EMPTY_VALUE;
            }
            score = EvalAfterTraverseCell(count, rightBound, leftBound, countEmpty, emptyPosition) * tempCell;

            return score;
        }
        private int LeftDiagonalTraverse(int x, int y)
        {
            int count = 0;
            int i, j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int countEmpty = 0; // countEmty chi dem o empty ve phia ben phai
            int tempCell = mBoard[x, y];
            int emptyPosition = 0; // dung de xem o empty o vi tri nao
            int score = 0;
            //duyet ben phai
            i = x + 1;
            j = y + 1;
            count = 1;
            mTrace[x, y][2] = true;
            while (IsInOfIndex(i, j, row) && (countEmpty < 2) && (mBoard[i, j] != (-tempCell)))
            {
                mTrace[i, j][2] = true;
                if (mBoard[i, j] == tempCell)
                {
                    count++;
                }
                if (mBoard[i, j] == EMPTY_VALUE)
                {
                    if (countEmpty == 0)
                    {
                        emptyPosition = count;
                    }
                    countEmpty++;
                    if (countEmpty == 2)
                    {
                        break;
                    }
                }
                i++;
                j++;
            }
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                rightBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                rightBound = EMPTY_VALUE;
            }

            //duyet ben trai
            // vi ta duyet theo chieu tu trai sang phai => cac diem ben trai da dc duyet roi
            // duyet ben trai chi nham muc dich: xem can ben trai no la empty hay Block
            i = x - 1;
            j = y - 1;
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                leftBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                leftBound = EMPTY_VALUE;
            }
            score = EvalAfterTraverseCell(count, rightBound, leftBound, countEmpty, emptyPosition) * tempCell;

            return score;
        }
        private int RightDiagonalTraverse(int x, int y)
        {
            int count = 0;
            int i, j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int countEmpty = 0; // countEmty chi dem o empty ve phia ben phai
            int tempCell = mBoard[x, y];
            int emptyPosition = 0; // dung de xem o empty o vi tri nao
            int score = 0;
            //duyet ben phai
            i = x + 1;
            j = y - 1;
            count = 1;
            mTrace[x, y][3] = true;
            while (IsInOfIndex(i, j, row) && (countEmpty < 2) && (mBoard[i, j] != (-tempCell)))
            {
                mTrace[i, j][3] = true;
                if (mBoard[i, j] == tempCell)
                {
                    count++;
                }
                if (mBoard[i, j] == EMPTY_VALUE)
                {
                    if (countEmpty == 0)
                    {
                        emptyPosition = count;
                    }
                    countEmpty++;
                    if (countEmpty == 2)
                    {
                        break;
                    }
                }
                i++;
                j--;
            }
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                rightBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                rightBound = EMPTY_VALUE;
            }

            //duyet ben trai
            // vi ta duyet theo chieu tu trai sang phai => cac diem ben trai da dc duyet roi
            // duyet ben trai chi nham muc dich: xem can ben trai no la empty hay Block
            i = x - 1;
            j = y + 1;
            if ((!IsInOfIndex(i, j, row)) || (mBoard[i, j] == -tempCell))
            {
                leftBound = BLOCK_VALUE;
            }
            if ((IsInOfIndex(i, j, row)) && (mBoard[i, j] == EMPTY_VALUE))
            {
                leftBound = EMPTY_VALUE;
            }
            score = EvalAfterTraverseCell(count, rightBound, leftBound, countEmpty, emptyPosition) * tempCell;

            return score;
        }


        private bool IsInOfIndex(int x, int y, int n)
        {
            if ((x >= 0) && (y >= 0) && (x < n) && (y < n))
            {
                return true;
            }
            return false;
        }

        private int EvalAfterTraverseCell(int pCount, int pRightBound, int pLeftBound, int pCountEmpty, int pEmptyPosition)
        {
            int score = 0;
            if (pLeftBound == EMPTY_VALUE)
            {
                if (pRightBound == EMPTY_VALUE) // 2 dau deu ko bi chan
                {
                    if (pCount == pEmptyPosition)
                    {
                        if (pCount >= 5)
                        {
                            score = FIVE_IN_A_LINE;
                        } 
                        else
                        {
                            score = ScoreArray[0, pCount - 1];
                        }
                        
                    } 
                    else// vd: *OO*O* => count = 3, emptyposition = 2
                    {
                        if ((pEmptyPosition >= 5) || ((pCount - pEmptyPosition) >= 5))
                        {
                            score = FIVE_IN_A_LINE;
                        }
                        else
                        {
                            if (pCount <= 4)
                            {
                                score = (int)(0.8 * ScoreArray[0, pCount - 1]);
                            }
                            else
                            {
                                score = (int)(0.8 * ScoreArray[0, 3]);
                            }
                        }
                    }
                } 
                else// bi chan dau ben phai
                {
                    if (pCountEmpty == 0) // vd: *OOOX
                    {
                        if (pCount >= 5)
                        {
                            score = FIVE_IN_A_LINE;
                        } 
                        else
                        {
                            score = ScoreArray[1, pCount - 1];
                        }
                        
                    } 
                    else// emptycount = 1, vd: *OOO*X
                    {
                        if (pCount == pEmptyPosition)
                        {
                            if (pCount >= 5)
                            {
                                score = FIVE_IN_A_LINE;
                            }
                            else
                            {
                                score = ScoreArray[0, pCount - 1];
                            }
                        } 
                        else
                        {
                            if ((pEmptyPosition >= 5) || ((pCount - pEmptyPosition) >= 5))
                            {
                                score = FIVE_IN_A_LINE;
                            }
                            else
                            {
                                if (pCount <= 4)
                                {
                                    score = (int)(0.8 * ScoreArray[1, pCount - 1]);
                                }
                                else
                                {
                                    score = (int)(0.8 * ScoreArray[1, 3]);
                                }
                            }
                        }
                    }
                }
            }
            else // leftbound = block
            {
                if (pRightBound == BLOCK_VALUE)
                {
                    if (pCountEmpty == 0)
                    {
                        if (pCount >=5)
                        {
                            score = FIVE_IN_A_LINE;
                        } 
                        else
                        {
                            score = 0;
                        }
                    } 
                    else // pcountempty = 1 => XOO*OX || XOOO*X
                    {
                        if (pCount == pEmptyPosition) //XOOO*X
                        {
                            if (pCount >=5)
                            {
                                score = FIVE_IN_A_LINE;
                            }
                            if (pCount == 4)
                            {
                                score = (int)(0.8 * ScoreArray[1, 3]);
                            }
                            if (pCount < 4)
                            {
                                score = 0;
                            }
                        } 
                        else // XOO*OX
                        {
                            if ((pEmptyPosition >= 5) || ((pCount - pEmptyPosition) >= 5))
                            {
                                score = FIVE_IN_A_LINE;
                            }
                            else
                            {
                                if (pCount <= 4)
                                {
                                    score = 0;
                                }
                                else
                                {
                                    score = (int)(0.8 * ScoreArray[1, 3]);
                                }
                            }
                        }
                    }
                } 
                else// rightbound = empty
                {
                    if (pCount == pEmptyPosition)
                    {
                        if (pCount >= 5)
                        {
                            score = FIVE_IN_A_LINE;
                        } 
                        else
                        {
                            score = ScoreArray[1, pCount - 1];
                        }
                    } 
                    else
                    {
                        if ((pEmptyPosition >= 5) || ((pCount - pEmptyPosition) >= 5))
                        {
                            score = FIVE_IN_A_LINE;
                        }
                        else
                        {
                            if (pCount <= 4)
                            {
                                score = (int)(0.8 * ScoreArray[1, pCount - 1]);
                            }
                            else
                            {
                                score = (int)(0.8 * ScoreArray[1, 3]);
                            }
                        }
                    }
                }
            }
            return score;
        }
    }
    
}
