namespace CMS
{
    partial class colors
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
            this.colorl = new System.Windows.Forms.Label();
            this.datee = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colorl
            // 
            this.colorl.AutoSize = true;
            this.colorl.Location = new System.Drawing.Point(210, 210);
            this.colorl.Name = "colorl";
            this.colorl.Size = new System.Drawing.Size(44, 16);
            this.colorl.TabIndex = 137;
            this.colorl.Text = "label1";
            // 
            // datee
            // 
            this.datee.AutoSize = true;
            this.datee.Location = new System.Drawing.Point(437, 210);
            this.datee.Name = "datee";
            this.datee.Size = new System.Drawing.Size(44, 16);
            this.datee.TabIndex = 138;
            this.datee.Text = "label1";
            this.datee.Click += new System.EventHandler(this.datee_Click);
            // 
            // colors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.datee);
            this.Controls.Add(this.colorl);
            this.Name = "colors";
            this.Text = "colors";
            this.Load += new System.EventHandler(this.colors_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label colorl;
        private System.Windows.Forms.Label datee;
    }
}