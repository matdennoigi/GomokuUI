using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GomokuConsole;
using System.ComponentModel;
using System.Threading;

namespace GomokuUI.UI
{
    public enum PlayerType
    {
        HumanPlayer,
        ComputerPlayer,
    }

    public class Player
    {
        public PlayerType PlayerType
        {
            set;get;
        }

        public bool IsMax
        {
            set;get;
        }
    }
    
    public class GameController
    {
        private GomokuBoard board;
        private Minimax stragyController;
        private GenerateBehavior generateBehavior;
        private EvaluateBehavior evaluateBehavior;
        private int minimaxDepth;

        public Player Player1
        {
            private set;get;
        }

        public Player Player2
        {
            private set;get;
        }

        public static GameController Current
        {
            set;get;
        }

        public GameController(GomokuBoard board)
        {
            if (board == null)
                throw new ArgumentNullException();

            this.board = board;
        }

        public void CreateGameSession(int rowCount, int columnCount, int depth, Player player1, Player player2)
        {
            this.minimaxDepth = depth;

            this.board.InitializeBoard(rowCount, columnCount);
            generateBehavior = new MyGenerate();
            evaluateBehavior = new MyEvaluateV2(new int[rowCount, columnCount]);

            this.stragyController = new Minimax(generateBehavior, evaluateBehavior);

            this.Player1 = player1;
            this.Player2 = player2;

            SetCurrentPlayer(Player1);
        }

        private Player currentPlayer;
        private void SetCurrentPlayer(Player player)
        {
            this.currentPlayer = player;

            if (currentPlayer.PlayerType == PlayerType.HumanPlayer)
            {
                board.AllowClick = true;
            }
            else
            {
                board.AllowClick = false;
                
                BackgroundWorker aiWorker = new BackgroundWorker();

                aiWorker.DoWork += OnAIThinking;
                aiWorker.RunWorkerCompleted += OnAIThinkCompleted;
                aiWorker.RunWorkerAsync(currentPlayer);
            }
        }

        
        private void OnAIThinking(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            Player current = e.Argument as Player;

            if (current.IsMax)
                stragyController.Max_AlphaBeta(board.Matrix, 3, int.MaxValue);
            else
                stragyController.Min_AlphaBeta(board.Matrix, 2, int.MinValue);
        }

        private void OnAIThinkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int[,] newMatrix = stragyController.getBestState();
            board.ApplyMatrix(newMatrix);

            if (currentPlayer == Player1)
                SetCurrentPlayer(Player2);
            else
                SetCurrentPlayer(Player1);
        }
    }
}
