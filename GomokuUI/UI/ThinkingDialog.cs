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
    public partial class ThinkingDialog : Form
    {
        private static ThinkingDialog _current = null;

        public static ThinkingDialog Current
        {
            get
            {
                if (_current == null)
                    return new ThinkingDialog();

                return _current;
            }
        }

        private ThinkingDialog()
        {
            InitializeComponent();
        }
    }
}
