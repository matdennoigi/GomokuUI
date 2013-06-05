using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuConsole
{
    class MyEvaluate : EvaluateBehavior
    {

        public static int FIVE_IN_A_LINE = 100000;
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
        //
        int[,] ScoreArray = new int[2, 5] { { LIVE_ONE, LIVE_TWO, LIVE_THREE, LIVE_FOUR, FIVE_IN_A_LINE }, 
                                            { SLEEP_ONE, SLEEP_TWO, SlEEP_THREE, SLEEP_FOUR, FIVE_IN_A_LINE } };
        private int[,] mBoard;
        // mang trace de luu vet,xem 1 o da dc duyet theo chieu nao!
        // 4 chieu theo thu tu la: ngang, doc, cheo trai (\), cheo phai (/)
        private bool[,][] mTrace;
        private int row;
        private int col;
        public MyEvaluate(int[,] pBoard)
        {
            this.mBoard = pBoard;
            this.row = this.mBoard.GetLength(0);
            this.col = this.mBoard.GetLength(1);
            this.mTrace = new bool[row, col][];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    mTrace[i, j] = new bool[4];
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
                    for (int k = 0; k < 4; k++)
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
            int countO = 1;
            int countX = 1;
            int j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int score = 0;
            mTrace[x, y][0] = true;

            // neu diem dang duyet la O
            if (mBoard[x, y] == O_VALUE)
            {
                // duyet ben phai
                j = y + 1;
                if (IsInOfIndex(x, j, row))
                {
                    // duyet tiep cho den khi gap Empty hoac gap X
                    while (IsInOfIndex(x, j, row) && (mBoard[x, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[x, j][0] = true;
                        j = j + 1;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == X_VALUE))
                    {
                        rightBound = X_VALUE;
                    }
                    if (!IsInOfIndex(x, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {// neu nhu x,y la diem mep cua ban co
                    rightBound = 5;
                }

                // duyet ben trai
                j = y - 1;
                if (IsInOfIndex(x, j, row))
                {
                    // duyet tiep cho den khi gap Empty hoac gap X
                    while (IsInOfIndex(x, j, row) && (mBoard[x, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[x, j][0] = true;
                        j = j - 1;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == X_VALUE))
                    {
                        leftBound = X_VALUE;
                    }
                    if (!IsInOfIndex(x, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {// x,y o mep trai ban co
                    leftBound = -5;
                }

            }

            // neu diem dang duyet la X
            if (mBoard[x, y] == X_VALUE)
            {
                j = y + 1;
                if (IsInOfIndex(x, j, row))
                {
                    // duyet tiep cho den khi gap Empty hoac gap X
                    while (IsInOfIndex(x, j, row) && (mBoard[x, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[x, j][0] = true;
                        j = j + 1;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == O_VALUE))
                    {
                        rightBound = O_VALUE;
                    }

                    if (!IsInOfIndex(x, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {// neu nhu x,y la diem mep cua ban co
                    rightBound = 5;
                }

                // duyet ben trai
                j = y - 1;
                if (IsInOfIndex(x, j, row))
                {
                    // duyet tiep cho den khi gap Empty hoac gap X
                    while (IsInOfIndex(x, j, row) && (mBoard[x, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[x, j][0] = true;
                        j = j - 1;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }

                    if (IsInOfIndex(x, j, row) && (mBoard[x, j] == O_VALUE))
                    {
                        leftBound = O_VALUE;
                    }
                    if (!IsInOfIndex(x, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {// x,y o mep trai ban co
                    leftBound = -5;
                }

            }
            if (mBoard[x, y] == O_VALUE)
                score = -EvalAfterTraverseCell(countO, rightBound, leftBound, x, y);
            if (mBoard[x, y] == X_VALUE)
                score = EvalAfterTraverseCell(countX, rightBound, leftBound, x, y);

            return score;
        }
        private int VerticalTraverse(int x, int y)
        {
            int countO = 1;
            int countX = 1;
            int j;
            int leftBound = 0;
            int rightBound = 0;
            int score = 0;
            mTrace[x, y][1] = true;
            // neu o dang xet la O
            if (mBoard[x, y] == O_VALUE)
            {
                //duyet phia duoi
                j = x + 1;
                if (IsInOfIndex(j, y, row))
                {
                    while (IsInOfIndex(j, y, row) && (mBoard[j, y] == O_VALUE))
                    {
                        countO++;
                        mTrace[j, y][1] = true;
                        j = j + 1;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == X_VALUE))
                    {
                        rightBound = X_VALUE;
                    }
                    if (!IsInOfIndex(j, y, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }
                //duyet phia tren
                j = x - 1;
                if (IsInOfIndex(j, y, row))
                {
                    while (IsInOfIndex(j, y, row) && (mBoard[j, y] == O_VALUE))
                    {
                        countO++;
                        mTrace[j, y][1] = true;
                        j = j - 1;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == X_VALUE))
                    {
                        leftBound = X_VALUE;
                    }
                    if (!IsInOfIndex(j, y, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }

            // neu o dang xet la X
            if (mBoard[x, y] == X_VALUE)
            {
                // duyet phia duoi
                j = x + 1;
                if (IsInOfIndex(j, y, row))
                {
                    while (IsInOfIndex(j, y, row) && (mBoard[j, y] == X_VALUE))
                    {
                        countX++;
                        mTrace[j, y][1] = true;
                        j = j + 1;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == O_VALUE))
                    {
                        rightBound = O_VALUE;
                    }
                    if (!IsInOfIndex(j, y, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }
                // duyet phia tren
                j = x - 1;
                if (IsInOfIndex(j, y, row))
                {
                    while (IsInOfIndex(j, y, row) && (mBoard[j, y] == X_VALUE))
                    {
                        countX++;
                        mTrace[j, y][1] = true;
                        j = j - 1;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(j, y, row) && (mBoard[j, y] == O_VALUE))
                    {
                        leftBound = O_VALUE;
                    }
                    if (!IsInOfIndex(j, y, row))
                    {
                        leftBound = 5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }
            if (mBoard[x, y] == O_VALUE)
                score = -EvalAfterTraverseCell(countO, rightBound, leftBound, x, y);
            if (mBoard[x, y] == X_VALUE)
                score = EvalAfterTraverseCell(countX, rightBound, leftBound, x, y);
            return score;
        }
        private int LeftDiagonalTraverse(int x, int y)
        {
            int countO = 1;
            int countX = 1;
            int i, j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int score = 0;
            mTrace[x, y][2] = true;
            // neu o dang xet la O
            if (mBoard[x, y] == O_VALUE)
            {
                // duyet xuong duoi, ben phai
                i = x + 1;
                j = y + 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[i, j][2] = true;
                        i = i + 1;
                        j = j + 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        rightBound = X_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }

                // duyet phia tren ben trai
                i = x - 1;
                j = y - 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[i, j][2] = true;
                        i = i - 1;
                        j = j - 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        leftBound = X_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }

            // neu o dang xet la X
            if (mBoard[x, y] == X_VALUE)
            {
                // duyet xuong duoi, ben phai
                i = x + 1;
                j = y + 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[i, j][2] = true;
                        i = i + 1;
                        j = j + 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        rightBound = O_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }

                // duyet phia tren ben trai
                i = x - 1;
                j = y - 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[i, j][2] = true;
                        i = i - 1;
                        j = j - 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        leftBound = O_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }

            if (mBoard[x, y] == O_VALUE)
                score = -EvalAfterTraverseCell(countO, rightBound, leftBound, x, y);
            if (mBoard[x, y] == X_VALUE)
                score = EvalAfterTraverseCell(countX, rightBound, leftBound, x, y);
            return score;
        }
        private int RightDiagonalTraverse(int x, int y)
        {
            int countO = 1;
            int countX = 1;
            int i, j;
            // can trai va can phai dung de tinh toan xem lien tuc co bi chan hay ko.
            int rightBound = 0;
            int leftBound = 0;
            int score = 0;
            mTrace[x, y][3] = true;
            // neu o dang xet la O
            if (mBoard[x, y] == O_VALUE)
            {
                // duyet phia tren, ben phai
                i = x + 1;
                j = y - 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[i, j][3] = true;
                        i = i + 1;
                        j = j - 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        rightBound = X_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }

                // duyet phia duoi ben trai
                i = x - 1;
                j = y + 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        countO++;
                        mTrace[i, j][3] = true;
                        i = i - 1;
                        j = j + 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        leftBound = X_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }

            // neu o dang xet la X
            if (mBoard[x, y] == X_VALUE)
            {
                // duyet phia tren, ben phai
                i = x - 1;
                j = y + 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[i, j][3] = true;
                        i = i - 1;
                        j = j + 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        rightBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        rightBound = O_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        rightBound = 5;
                    }
                }
                else
                {
                    rightBound = 5;
                }

                // duyet phia duoi ben trai
                i = x + 1;
                j = y - 1;
                if (IsInOfIndex(i, j, row))
                {
                    while (IsInOfIndex(i, j, row) && (mBoard[i, j] == X_VALUE))
                    {
                        countX++;
                        mTrace[i, j][3] = true;
                        i = i + 1;
                        j = j - 1;

                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == EMPTY_VALUE))
                    {
                        leftBound = EMPTY_VALUE;
                    }
                    if (IsInOfIndex(i, j, row) && (mBoard[i, j] == O_VALUE))
                    {
                        leftBound = O_VALUE;
                    }
                    if (!IsInOfIndex(i, j, row))
                    {
                        leftBound = -5;
                    }
                }
                else
                {
                    leftBound = -5;
                }
            }
            if (mBoard[x, y] == O_VALUE)
                score = -EvalAfterTraverseCell(countO, rightBound, leftBound, x, y);
            if (mBoard[x, y] == X_VALUE)
                score = EvalAfterTraverseCell(countX, rightBound, leftBound, x, y);

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

        private int EvalAfterTraverseCell(int pCount, int pRightBound, int pLeftBound, int x, int y)
        {
            int score = 0;
            if (pCount >= 5)
            {
                score = FIVE_IN_A_LINE;
            }
            else
            {
                if ((pRightBound == EMPTY_VALUE) && (pLeftBound == EMPTY_VALUE))
                {
                    score = ScoreArray[0, pCount - 1];
                }
                else
                {
                    if ((pRightBound != EMPTY_VALUE) && (pLeftBound != EMPTY_VALUE))
                    {
                        score = 0;
                    }
                    else
                    {
                        score = ScoreArray[1, pCount - 1];
                    }
                }
            }

            return score;
        }
    }
}
