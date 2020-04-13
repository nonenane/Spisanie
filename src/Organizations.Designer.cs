namespace Spisanie
{
    partial class frmOrganizations
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
            this.btGetOrg2 = new System.Windows.Forms.Button();
            this.tbPost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBuyer = new System.Windows.Forms.TextBox();
            this.btGetOrg3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btGetOrg2
            // 
            this.btGetOrg2.Location = new System.Drawing.Point(443, 6);
            this.btGetOrg2.Name = "btGetOrg2";
            this.btGetOrg2.Size = new System.Drawing.Size(27, 23);
            this.btGetOrg2.TabIndex = 27;
            this.btGetOrg2.Text = "...";
            this.btGetOrg2.UseVisualStyleBackColor = true;
            this.btGetOrg2.Click += new System.EventHandler(this.btGetOrg2_Click);
            // 
            // tbPost
            // 
            this.tbPost.Location = new System.Drawing.Point(124, 8);
            this.tbPost.MaxLength = 64;
            this.tbPost.Name = "tbPost";
            this.tbPost.ReadOnly = true;
            this.tbPost.Size = new System.Drawing.Size(313, 20);
            this.tbPost.TabIndex = 24;
            this.tbPost.MouseEnter += new System.EventHandler(this.tbPost_MouseEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Поставщик:";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::Spisanie.Properties.Resources.pict_save;
            this.btSave.Location = new System.Drawing.Point(405, 87);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(30, 30);
            this.btSave.TabIndex = 21;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btClose.Location = new System.Drawing.Point(441, 87);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 20;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Покупатель:";
            // 
            // tbBuyer
            // 
            this.tbBuyer.Location = new System.Drawing.Point(124, 34);
            this.tbBuyer.MaxLength = 64;
            this.tbBuyer.Name = "tbBuyer";
            this.tbBuyer.ReadOnly = true;
            this.tbBuyer.Size = new System.Drawing.Size(313, 20);
            this.tbBuyer.TabIndex = 24;
            this.tbBuyer.MouseEnter += new System.EventHandler(this.tbBuyer_MouseEnter);
            // 
            // btGetOrg3
            // 
            this.btGetOrg3.Location = new System.Drawing.Point(443, 32);
            this.btGetOrg3.Name = "btGetOrg3";
            this.btGetOrg3.Size = new System.Drawing.Size(27, 23);
            this.btGetOrg3.TabIndex = 27;
            this.btGetOrg3.Text = "...";
            this.btGetOrg3.UseVisualStyleBackColor = true;
            this.btGetOrg3.Click += new System.EventHandler(this.btGetOrg3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Кол-во дней:";
            // 
            // nudDays
            // 
            this.nudDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudDays.Location = new System.Drawing.Point(124, 92);
            this.nudDays.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(42, 20);
            this.nudDays.TabIndex = 28;
            this.nudDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Процент наценки:";
            // 
            // tbPrc
            // 
            this.tbPrc.Location = new System.Drawing.Point(124, 60);
            this.tbPrc.MaxLength = 64;
            this.tbPrc.Name = "tbPrc";
            this.tbPrc.Size = new System.Drawing.Size(108, 20);
            this.tbPrc.TabIndex = 29;
            this.tbPrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPrc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrc_KeyPress);
            this.tbPrc.Validating += new System.ComponentModel.CancelEventHandler(this.tbPrc_Validating);
            // 
            // frmOrganizations
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(483, 129);
            this.ControlBox = false;
            this.Controls.Add(this.tbPrc);
            this.Controls.Add(this.nudDays);
            this.Controls.Add(this.btGetOrg3);
            this.Controls.Add(this.btGetOrg2);
            this.Controls.Add(this.tbBuyer);
            this.Controls.Add(this.tbPost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrganizations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка организаций";
            this.Load += new System.EventHandler(this.frmOrganizations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btGetOrg2;
        private System.Windows.Forms.TextBox tbPost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBuyer;
        private System.Windows.Forms.Button btGetOrg3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrc;
    }
}