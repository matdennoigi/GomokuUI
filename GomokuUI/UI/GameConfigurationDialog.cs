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
    public partial class GameConfigurationDialog : Form
    {
        public const int MAX_ROW_COUNT = 19;
        public const int MAX_COLUMN_COUNT = 19;

        public GameConfigurationDialog()
        {
            InitializeComponent();
        }

        public int RowCount
        {
            private set;
            get;
        }

        public int ColumnCount
        {
            private set;
            get;
        }

        private bool VerifyInput()
        {
            try
            {
                int rowCount = rowCountTextbox.IntValue;
                int columnCount = columnCountTextbox.IntValue;

                if (rowCount <= 0 || columnCount <= 0)
                    return false;

                RowCount = rowCount;
                ColumnCount = columnCount;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (VerifyInput())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please input Game Information");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void GameConfigurationDialog_Load(object sender, EventArgs e)
        {
            rowCountTextbox.Text = "19";
            columnCountTextbox.Text = "19";
        }
        
    }
}
