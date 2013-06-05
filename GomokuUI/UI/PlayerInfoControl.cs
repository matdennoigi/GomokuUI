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
    public enum PlayerType
    {
        PlayerTypeHuman,
        PlayerTypeComputer,
    }

    public class PlayerInfo
    {
        public PlayerType Type
        {
            set;
            get;
        }

        public int Depth
        {
            set;
            get;
        }
    }

    public partial class PlayerInfoControl : UserControl
    {
        public PlayerInfoControl()
        {
            InitializeComponent();
        }

        private void playerTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerTypeCombo.SelectedIndex == 1)
                depthCombo.Enabled = true;
            else
                depthCombo.Enabled = false;
        }

        public PlayerInfo PlayerInfo
        {
            get
            {
                if (playerTypeCombo.SelectedIndex < 0)
                    return null;

                if (playerTypeCombo.SelectedIndex == 0)
                {
                    return new PlayerInfo { Type = PlayerType.PlayerTypeHuman };
                }
                else
                {
                    if (depthCombo.SelectedIndex < 0)
                        return null;

                    return new PlayerInfo { Type = PlayerType.PlayerTypeComputer, Depth = depthCombo.SelectedIndex + 2 };
                }
            }
        }
    }
}
