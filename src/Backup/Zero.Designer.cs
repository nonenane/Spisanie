namespace Spisanie
{
    partial class frmZero
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbZcena = new System.Windows.Forms.TextBox();
            this.tbRcena = new System.Windows.Forms.TextBox();
            this.tbNds = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сформированы документы с отсутствием у товаров цен:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "закупочной";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "продажной";
            // 
            // tbZcena
            // 
            this.tbZcena.BackColor = System.Drawing.Color.White;
            this.tbZcena.Location = new System.Drawing.Point(15, 57);
            this.tbZcena.Multiline = true;
            this.tbZcena.Name = "tbZcena";
            this.tbZcena.ReadOnly = true;
            this.tbZcena.Size = new System.Drawing.Size(222, 132);
            this.tbZcena.TabIndex = 3;
            // 
            // tbRcena
            // 
            this.tbRcena.BackColor = System.Drawing.Color.White;
            this.tbRcena.Location = new System.Drawing.Point(243, 57);
            this.tbRcena.Multiline = true;
            this.tbRcena.Name = "tbRcena";
            this.tbRcena.ReadOnly = true;
            this.tbRcena.Size = new System.Drawing.Size(222, 132);
            this.tbRcena.TabIndex = 4;
            // 
            // tbNds
            // 
            this.tbNds.BackColor = System.Drawing.Color.White;
            this.tbNds.Location = new System.Drawing.Point(471, 57);
            this.tbNds.Multiline = true;
            this.tbNds.Name = "tbNds";
            this.tbNds.ReadOnly = true;
            this.tbNds.Size = new System.Drawing.Size(222, 132);
            this.tbNds.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(473, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "с отсутствием у товаров ставки НДС";
            // 
            // frmZero
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(707, 201);
            this.Controls.Add(this.tbNds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRcena);
            this.Controls.Add(this.tbZcena);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отсутствие цен";
            this.Load += new System.EventHandler(this.frmZero_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbZcena;
        private System.Windows.Forms.TextBox tbRcena;
        private System.Windows.Forms.TextBox tbNds;
        private System.Windows.Forms.Label label4;
    }
}