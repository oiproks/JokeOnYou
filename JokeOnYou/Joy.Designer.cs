namespace JokeOnYou
{
    partial class Joy
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.log.Location = new System.Drawing.Point(0, 1);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(117, 13);
            this.log.TabIndex = 0;
            this.log.Text = "Moving in 10s. 10 steps";
            this.log.MouseHover += new System.EventHandler(this.StopIt);
            // 
            // Joy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(117, 15);
            this.Controls.Add(this.log);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Joy";
            this.Opacity = 0.5D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.Joy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox log;
    }
}

