namespace CMS
{
    partial class cashierpaycs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cashierpaycs));
            this.panel3 = new System.Windows.Forms.Panel();
            this.discount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.orderdetail = new System.Windows.Forms.DataGridView();
            this.receipt = new System.Windows.Forms.Button();
            this.pay = new System.Windows.Forms.Button();
            this.changepay = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.addordergrid = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pay_button = new System.Windows.Forms.Button();
            this.cus_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.button7 = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdetail)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addordergrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.discount);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.orderdetail);
            this.panel3.Controls.Add(this.receipt);
            this.panel3.Controls.Add(this.pay);
            this.panel3.Controls.Add(this.changepay);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.amount);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.price);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(846, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(407, 633);
            this.panel3.TabIndex = 5;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // discount
            // 
            this.discount.AutoSize = true;
            this.discount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.discount.Location = new System.Drawing.Point(277, 459);
            this.discount.Name = "discount";
            this.discount.Size = new System.Drawing.Size(34, 20);
            this.discount.TabIndex = 106;
            this.discount.Text = "20$";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 460);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 23);
            this.label4.TabIndex = 105;
            this.label4.Text = "Discount Amount($):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(142, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 31);
            this.label3.TabIndex = 81;
            this.label3.Text = "Order Detail";
            // 
            // orderdetail
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.orderdetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.orderdetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.orderdetail.Location = new System.Drawing.Point(0, 68);
            this.orderdetail.Name = "orderdetail";
            this.orderdetail.RowHeadersWidth = 51;
            this.orderdetail.RowTemplate.Height = 24;
            this.orderdetail.Size = new System.Drawing.Size(407, 315);
            this.orderdetail.TabIndex = 2;
            this.orderdetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdetail_CellContentClick);
            // 
            // receipt
            // 
            this.receipt.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.receipt.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receipt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.receipt.Location = new System.Drawing.Point(222, 580);
            this.receipt.Name = "receipt";
            this.receipt.Size = new System.Drawing.Size(140, 38);
            this.receipt.TabIndex = 103;
            this.receipt.Text = "Receipt";
            this.receipt.UseVisualStyleBackColor = false;
            this.receipt.Click += new System.EventHandler(this.receipt_Click);
            // 
            // pay
            // 
            this.pay.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pay.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pay.Location = new System.Drawing.Point(56, 580);
            this.pay.Name = "pay";
            this.pay.Size = new System.Drawing.Size(140, 38);
            this.pay.TabIndex = 100;
            this.pay.Text = "Pay";
            this.pay.UseVisualStyleBackColor = false;
            this.pay.Click += new System.EventHandler(this.pay_Click);
            // 
            // changepay
            // 
            this.changepay.AutoSize = true;
            this.changepay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.changepay.Location = new System.Drawing.Point(285, 536);
            this.changepay.Name = "changepay";
            this.changepay.Size = new System.Drawing.Size(26, 20);
            this.changepay.TabIndex = 102;
            this.changepay.Text = "0$";
            this.changepay.Click += new System.EventHandler(this.change_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(90, 536);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 23);
            this.label13.TabIndex = 101;
            this.label13.Text = "Change($):";
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(252, 495);
            this.amount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.amount.Multiline = true;
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(110, 28);
            this.amount.TabIndex = 94;
            this.amount.TextChanged += new System.EventHandler(this.amount_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(90, 500);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 23);
            this.label11.TabIndex = 100;
            this.label11.Text = "Amount($):";
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.price.Location = new System.Drawing.Point(277, 418);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(34, 20);
            this.price.TabIndex = 100;
            this.price.Text = "20$";
            this.price.Click += new System.EventHandler(this.price_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(89, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 23);
            this.label7.TabIndex = 100;
            this.label7.Text = "Price($):";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.addordergrid);
            this.panel1.Location = new System.Drawing.Point(12, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 374);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 80;
            this.label1.Text = "Orders";
            // 
            // addordergrid
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.addordergrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.addordergrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.addordergrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.addordergrid.Location = new System.Drawing.Point(0, 51);
            this.addordergrid.Name = "addordergrid";
            this.addordergrid.RowHeadersWidth = 51;
            this.addordergrid.RowTemplate.Height = 24;
            this.addordergrid.Size = new System.Drawing.Size(812, 322);
            this.addordergrid.TabIndex = 3;
            this.addordergrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.addordergrid_CellContentClick);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.textBox2.Location = new System.Drawing.Point(731, 29);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(448, 10);
            this.textBox2.TabIndex = 106;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.textBox3.Location = new System.Drawing.Point(89, 29);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(453, 10);
            this.textBox3.TabIndex = 105;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(548, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(177, 31);
            this.label15.TabIndex = 104;
            this.label15.Text = "Order Payment";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pay_button);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.cus_id);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 469);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(812, 212);
            this.panel2.TabIndex = 107;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pay_button
            // 
            this.pay_button.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pay_button.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pay_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pay_button.Location = new System.Drawing.Point(102, 115);
            this.pay_button.Name = "pay_button";
            this.pay_button.Size = new System.Drawing.Size(555, 38);
            this.pay_button.TabIndex = 114;
            this.pay_button.Text = "Payment";
            this.pay_button.UseVisualStyleBackColor = false;
            this.pay_button.Click += new System.EventHandler(this.remove_Click);
            // 
            // cus_id
            // 
            this.cus_id.Location = new System.Drawing.Point(258, 64);
            this.cus_id.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cus_id.Multiline = true;
            this.cus_id.Name = "cus_id";
            this.cus_id.Size = new System.Drawing.Size(220, 29);
            this.cus_id.TabIndex = 113;
            this.cus_id.TextChanged += new System.EventHandler(this.order_id_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(323, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 112;
            this.label2.Text = "CustomerID:";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::CMS.Properties.Resources.back;
            this.pictureBox4.Location = new System.Drawing.Point(20, 159);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(39, 38);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 137;
            this.pictureBox4.TabStop = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.button7.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.Location = new System.Drawing.Point(53, 159);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(64, 38);
            this.button7.TabIndex = 136;
            this.button7.Text = "Back";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // cashierpaycs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 693);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label15);
            this.Name = "cashierpaycs";
            this.Text = "cashierpaycs";
            this.Load += new System.EventHandler(this.cashierpaycs_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addordergrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView orderdetail;
        private System.Windows.Forms.Button receipt;
        private System.Windows.Forms.Button pay;
        private System.Windows.Forms.Label changepay;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label price;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView addordergrid;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button pay_button;
        private System.Windows.Forms.TextBox cus_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label discount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button7;
    }
}