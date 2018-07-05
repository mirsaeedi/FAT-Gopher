namespace Gopher
{
    partial class frmMBR
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
            this.rtbMBR = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbMBR
            // 
            this.rtbMBR.Location = new System.Drawing.Point(2, 1);
            this.rtbMBR.Name = "rtbMBR";
            this.rtbMBR.Size = new System.Drawing.Size(721, 340);
            this.rtbMBR.TabIndex = 0;
            this.rtbMBR.Text = "";
            // 
            // frmMBR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 343);
            this.Controls.Add(this.rtbMBR);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMBR";
            this.Text = "Show MBR";
            this.Load += new System.EventHandler(this.frmMBR_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMBR;
    }
}