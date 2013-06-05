namespace GomokuUI.UI
{
    partial class PlayerInfoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playerTypeCombo = new System.Windows.Forms.ComboBox();
            this.depthCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // playerTypeCombo
            // 
            this.playerTypeCombo.FormattingEnabled = true;
            this.playerTypeCombo.Items.AddRange(new object[] {
            "Human",
            "Computer"});
            this.playerTypeCombo.Location = new System.Drawing.Point(0, 3);
            this.playerTypeCombo.Name = "playerTypeCombo";
            this.playerTypeCombo.Size = new System.Drawing.Size(149, 21);
            this.playerTypeCombo.TabIndex = 0;
            this.playerTypeCombo.Text = "Player Type";
            this.playerTypeCombo.SelectedIndexChanged += new System.EventHandler(this.playerTypeCombo_SelectedIndexChanged);
            // 
            // depthCombo
            // 
            this.depthCombo.Enabled = false;
            this.depthCombo.FormattingEnabled = true;
            this.depthCombo.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.depthCombo.Location = new System.Drawing.Point(190, 2);
            this.depthCombo.Name = "depthCombo";
            this.depthCombo.Size = new System.Drawing.Size(78, 21);
            this.depthCombo.TabIndex = 1;
            this.depthCombo.Text = "Depth";
            // 
            // PlayerInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.depthCombo);
            this.Controls.Add(this.playerTypeCombo);
            this.Name = "PlayerInfoControl";
            this.Size = new System.Drawing.Size(274, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox playerTypeCombo;
        private System.Windows.Forms.ComboBox depthCombo;
    }
}
