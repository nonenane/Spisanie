namespace Spisanie
{
    partial class frmLoad
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cbDeps = new System.Windows.Forms.ComboBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.gbIn = new System.Windows.Forms.GroupBox();
            this.rbVozvr = new System.Windows.Forms.RadioButton();
            this.rbPrih = new System.Windows.Forms.RadioButton();
            this.gbOut = new System.Windows.Forms.GroupBox();
            this.rbSpis = new System.Windows.Forms.RadioButton();
            this.rbOtgruz = new System.Windows.Forms.RadioButton();
            this.wait = new System.Windows.Forms.Label();
            this.gbIn.SuspendLayout();
            this.gbOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отдел:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(65, 49);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(121, 20);
            this.dtpDate.TabIndex = 2;
            // 
            // cbDeps
            // 
            this.cbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeps.FormattingEnabled = true;
            this.cbDeps.Location = new System.Drawing.Point(65, 22);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(121, 21);
            this.cbDeps.TabIndex = 3;
            this.cbDeps.SelectionChangeCommitted += new System.EventHandler(this.cbDeps_SelectionChangeCommitted);
            // 
            // btClose
            // 
            this.btClose.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btClose.Location = new System.Drawing.Point(412, 91);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 4;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSave
            // 
            this.btSave.Image = global::Spisanie.Properties.Resources.pict_save;
            this.btSave.Location = new System.Drawing.Point(376, 91);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(30, 30);
            this.btSave.TabIndex = 7;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click_1);
            // 
            // gbIn
            // 
            this.gbIn.Controls.Add(this.rbVozvr);
            this.gbIn.Controls.Add(this.rbPrih);
            this.gbIn.Location = new System.Drawing.Point(201, 7);
            this.gbIn.Name = "gbIn";
            this.gbIn.Size = new System.Drawing.Size(117, 77);
            this.gbIn.TabIndex = 8;
            this.gbIn.TabStop = false;
            this.gbIn.Text = "Для прихода";
            // 
            // rbVozvr
            // 
            this.rbVozvr.Location = new System.Drawing.Point(6, 37);
            this.rbVozvr.Name = "rbVozvr";
            this.rbVozvr.Size = new System.Drawing.Size(105, 35);
            this.rbVozvr.TabIndex = 1;
            this.rbVozvr.Text = "\"Возврат от покупателя\"";
            this.rbVozvr.UseVisualStyleBackColor = true;
            // 
            // rbPrih
            // 
            this.rbPrih.AutoSize = true;
            this.rbPrih.Checked = true;
            this.rbPrih.Location = new System.Drawing.Point(6, 19);
            this.rbPrih.Name = "rbPrih";
            this.rbPrih.Size = new System.Drawing.Size(72, 17);
            this.rbPrih.TabIndex = 0;
            this.rbPrih.TabStop = true;
            this.rbPrih.Text = "\"Приход\"";
            this.rbPrih.UseVisualStyleBackColor = true;
            this.rbPrih.CheckedChanged += new System.EventHandler(this.rbPrih_CheckedChanged);
            // 
            // gbOut
            // 
            this.gbOut.Controls.Add(this.rbSpis);
            this.gbOut.Controls.Add(this.rbOtgruz);
            this.gbOut.Location = new System.Drawing.Point(325, 7);
            this.gbOut.Name = "gbOut";
            this.gbOut.Size = new System.Drawing.Size(117, 77);
            this.gbOut.TabIndex = 9;
            this.gbOut.TabStop = false;
            this.gbOut.Text = "Для расхода";
            // 
            // rbSpis
            // 
            this.rbSpis.AutoSize = true;
            this.rbSpis.Location = new System.Drawing.Point(6, 50);
            this.rbSpis.Name = "rbSpis";
            this.rbSpis.Size = new System.Drawing.Size(104, 17);
            this.rbSpis.TabIndex = 1;
            this.rbSpis.Text = "\"Акт списания\"";
            this.rbSpis.UseVisualStyleBackColor = true;
            // 
            // rbOtgruz
            // 
            this.rbOtgruz.AutoSize = true;
            this.rbOtgruz.Checked = true;
            this.rbOtgruz.Location = new System.Drawing.Point(6, 19);
            this.rbOtgruz.Name = "rbOtgruz";
            this.rbOtgruz.Size = new System.Drawing.Size(82, 17);
            this.rbOtgruz.TabIndex = 0;
            this.rbOtgruz.TabStop = true;
            this.rbOtgruz.Text = "\"Отгрузка\"";
            this.rbOtgruz.UseVisualStyleBackColor = true;
            this.rbOtgruz.CheckedChanged += new System.EventHandler(this.rbOtgruz_CheckedChanged);
            // 
            // wait
            // 
            this.wait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wait.AutoSize = true;
            this.wait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wait.Location = new System.Drawing.Point(381, 3);
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(69, 16);
            this.wait.TabIndex = 10;
            this.wait.Text = "Ждите...";
            this.wait.Visible = false;
            // 
            // frmLoad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(452, 133);
            this.ControlBox = false;
            this.Controls.Add(this.wait);
            this.Controls.Add(this.gbOut);
            this.Controls.Add(this.gbIn);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.cbDeps);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoad";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузить данные";
            this.Load += new System.EventHandler(this.frmLoad_Load);
            this.gbIn.ResumeLayout(false);
            this.gbIn.PerformLayout();
            this.gbOut.ResumeLayout(false);
            this.gbOut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cbDeps;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.GroupBox gbIn;
        private System.Windows.Forms.RadioButton rbVozvr;
        private System.Windows.Forms.RadioButton rbPrih;
        private System.Windows.Forms.GroupBox gbOut;
        private System.Windows.Forms.RadioButton rbSpis;
        private System.Windows.Forms.RadioButton rbOtgruz;
        private System.Windows.Forms.Label wait;
    }
}