using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GomokuUI.UI
{
    public partial class MainForm : Form
    {
        public static MainForm Current
        {
            private set;
            get;
        }

        public MainForm()
        {
            InitializeComponent();
            GameController.Current = new GameController(gomokuBoard);

            MainForm.Current = this;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameConfigurationDialog newGameDialog = new GameConfigurationDialog();
            DialogResult result = newGameDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                int rowCount = newGameDialog.RowCount;
                int columnCount = newGameDialog.ColumnCount;

                PlayerInfo playerInfo1 = newGameDialog.Player1Info;
                PlayerInfo playerInfo2 = newGameDialog.Player2Info;

                Player player1 = null;
                Player player2 = null;

                if (playerInfo1.Type == PlayerType.PlayerTypeHuman)
                    player1 = new HumanPlayer();
                else
                    player1 = new ComputerPlayer(true, playerInfo1.Depth, rowCount, columnCount);


                if (playerInfo2.Type == PlayerType.PlayerTypeHuman)
                    player2 = new HumanPlayer();
                else
                    player2 = new ComputerPlayer(false, playerInfo2.Depth, rowCount, columnCount);


                GameController.Current.CreateGameSession(rowCount, columnCount, player1, player2);
            }
        }

        private void gomokuBoard_CellClickInvalid(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid!");
        }

        private void gomokuBoard_CellClicked(object sender, EventArgs e)
        {
            if (GameController.Current.CurrentTurn is HumanPlayer)
            {
                HumanPlayer player = GameController.Current.CurrentTurn as HumanPlayer;
                player.FireThinkCompletedEvent(gomokuBoard.Matrix);
            }
        }
    }
}
