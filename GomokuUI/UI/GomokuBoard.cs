using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GomokuUI.UI
{
    public partial class GomokuBoard : UserControl
    {
        private const int EMPTY_VALUE = 0;
        private const int X_VALUE = 1;
        private const int O_VALUE = -1;

        private class CellInformation
        {
            public int Row { set;get; }
            public int Column { set;get; }
        }

        public const int CELL_WIDTH = 20;
        public const int CELL_HEIGHT = 20;

        public GomokuBoard()
        {
            InitializeComponent();
            PlayerValue = X_VALUE;
            AllowClick = true;
        }

        private Button[,] _cells = null;
        private int[,] _matrix = null;
        private int rowCount = 0;
        private int columnCount = 0;
        public void InitializeBoard(int rowCount, int columnCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;

            _cells = new Button[rowCount, columnCount];
            _matrix = new int[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
                for (int column = 0; column < columnCount; column++)
                {
                    Button cell = new Button();
                    cell.Width = CELL_WIDTH;
                    cell.Height = CELL_HEIGHT;
                    cell.Left = column * CELL_WIDTH;
                    cell.Top = row * CELL_HEIGHT;

                    CellInformation cellInfomation = new CellInformation { Row = row, Column = column };
                    cell.Tag = cellInfomation;

                    cell.Click += OnBoardCellClick;

                    _cells[row, column] = cell;
                    _matrix[row, column] = 0;

                    this.Controls.Add(cell);
                }
        }

        private void ApplyCell(int row, int column, int cellValue)
        {
            if (cellValue == EMPTY_VALUE)
            {
                _cells[row, column].Text = string.Empty;
            }
            else if (cellValue == O_VALUE)
            {
                _cells[row, column].Text = "O";
            }
            else if (cellValue == X_VALUE)
            {
                _cells[row, column].Text = "X";
            }

            _matrix[row, column] = cellValue;
        }

        public int[,] Matrix
        {
            get
            {
                return _matrix;
            }
        }

        public void ApplyMatrix(int[,] matrix)
        {
            for (int row = 0; row < rowCount; row++)
                for (int column = 0; column < columnCount; column++)
                {
                    int cellValue = matrix[row, column];

                    _matrix[row, column] = cellValue;
                    ApplyCell(row, column, cellValue);
                }
        }

        public bool AllowClick
        {
            set;get;
        }

        public int PlayerValue
        {
            set;get;
        }

        private void OnBoardCellClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                CellInformation cellInfo = (sender as Button).Tag as CellInformation;

                if (cellInfo != null)
                {
                    if (AllowClick)
                    {
                        if (Matrix[cellInfo.Row, cellInfo.Column] == 0)
                        {
                            ApplyCell(cellInfo.Row, cellInfo.Column, PlayerValue);

                            if (CellClicked != null)
                                CellClicked(this, new EventArgs());
                        }
                        else
                        {
                            if (CellClickInvalid != null)
                                CellClickInvalid(this, new EventArgs());
                        }
                    }
                }
            }
        }

        public event EventHandler CellClicked;
        public event EventHandler CellClickInvalid;
    }
}
