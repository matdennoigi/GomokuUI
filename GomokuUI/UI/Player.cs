using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GomokuConsole;

namespace GomokuUI.UI
{
    #region Game Event
    public class ThinkCompletedEventArgs : EventArgs
    {
        public int[,] BoardMatrix
        {
            set;
            get;
        }
    }

    public delegate void ThinkCompletedEventHandler(object sender, ThinkCompletedEventArgs e);

    #endregion

    public abstract class Player
    {
        public abstract void GetTurn(int[,] boardMatrix);

        public void FireThinkCompletedEvent(int[,] boardMatrix)
        {
            if (ThinkCompleted != null)
                ThinkCompleted(this, new ThinkCompletedEventArgs { BoardMatrix = boardMatrix });
        }

        public event ThinkCompletedEventHandler ThinkCompleted;
    }

    public class HumanPlayer : Player
    {
        public override void GetTurn(int[,] boardMatrix)
        {
        }
    }

    public class ComputerPlayer : Player
    {
        private bool isMax;
        private int algorithmDepth;
        private Minimax ai;

        private GenerateBehavior generator;
        private EvaluateBehavior evaluator;

        public ComputerPlayer(bool isMax, int algorithmDepth, int row, int col)
        {
            this.isMax = isMax;
            this.algorithmDepth = algorithmDepth;

            //AI 
            this.generator = new MyGenerate();
            this.evaluator = new MyEvaluateV2(new int[row, col]);

            this.ai = new Minimax(generator, evaluator);
        }

        public bool IsMax
        {
            get
            {
                return this.isMax;
            }
        }

        public int AlgorithmDepth
        {
            get
            {
                return this.algorithmDepth;
            }
        }

        public override void GetTurn(int[,] boardMatrix)
        {
            BackgroundWorker thinkingThread = new BackgroundWorker();
            thinkingThread.DoWork += OnThinking;
            thinkingThread.RunWorkerCompleted += OnThinkingCompleted;
            
            thinkingThread.RunWorkerAsync(boardMatrix);
        }

        private void OnThinking(object sender, DoWorkEventArgs e)
        {
            int[,] boardMatrix = e.Argument as int[,];

            if (isMax)
                ai.Max_AlphaBeta(boardMatrix, algorithmDepth, int.MaxValue);
            else
                ai.Min_AlphaBeta(boardMatrix, algorithmDepth, int.MinValue);

        }

        private void OnThinkingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int[,] thinkResult = ai.getBestState();
            FireThinkCompletedEvent(thinkResult);
        }
    }
}
