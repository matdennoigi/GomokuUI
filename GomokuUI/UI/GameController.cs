using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GomokuConsole;
using System.ComponentModel;
using System.Threading;

namespace GomokuUI.UI
{
    public class GameController
    {
        public static GameController Current
        {
            set;get;
        }

        private GomokuBoard board;

        public GameController(GomokuBoard board)
        {
            if (board == null)
                throw new ArgumentNullException();

            this.board = board;
        }

        private Player[] players = new Player[2];
        private int currentPlayerIndex = -1;

        public void CreateGameSession(int rowCount, int columnCount, Player player1, Player player2)
        {
            this.board.InitializeBoard(rowCount, columnCount);

            this.players[0] = player1;
            this.players[1] = player2;

            players[0].ThinkCompleted += OnPlayerThinkCompleted;
            players[1].ThinkCompleted += OnPlayerThinkCompleted;

            NextTurn();
        }

        private void NextTurn()
        {
            if (currentPlayerIndex != 0)
                currentPlayerIndex = 0;
            else
                currentPlayerIndex = 1;

            Player currentPlayer = players[currentPlayerIndex];

            if (currentPlayer is HumanPlayer)
                board.AllowClick = true;
            else
                board.AllowClick = false;

            currentPlayer.GetTurn(board.Matrix);
        }

        public Player CurrentTurn
        {
            get
            {
                return players[currentPlayerIndex];
            }
        }

        private void OnPlayerThinkCompleted(object sender, ThinkCompletedEventArgs e)
        {
            int[,] matrix = e.BoardMatrix;
            board.ApplyMatrix(matrix);
            NextTurn();
        }

        
    }
}
