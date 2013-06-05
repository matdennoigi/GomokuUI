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

                GameController.Current.CreateGameSession(rowCount, columnCount, 3, 
                    new Player { PlayerType = PlayerType.ComputerPlayer, IsMax = true }, 
                    new Player { PlayerType = PlayerType.ComputerPlayer, IsMax = false });
            }
        }

        public void ShowThinking()
        {
            ThinkingDialog.Current.ShowDialog();
        }

        private void gomokuBoard_CellClickInvalid(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid!");
        }

        private void gomokuBoard_CellClicked(object sender, EventArgs e)
        {
        }
    }
}
