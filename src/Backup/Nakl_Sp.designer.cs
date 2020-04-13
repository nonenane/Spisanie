namespace Spisanie
{
    partial class frmNakl_Sp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTovars = new System.Windows.Forms.DataGridView();
            this.tvEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvNetto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvZcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvRcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvNds = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tvSumR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTTN = new System.Windows.Forms.TextBox();
            this.tbVnudok = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDateEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbEditor = new System.Windows.Forms.TextBox();
            this.tbSumR = new System.Windows.Forms.TextBox();
            this.tbSumNetto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.wait = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbUL = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTovars)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTovars
            // 
            this.dgvTovars.AllowUserToAddRows = false;
            this.dgvTovars.AllowUserToDeleteRows = false;
            this.dgvTovars.AllowUserToResizeRows = false;
            this.dgvTovars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTovars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTovars.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTovars.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvTovars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTovars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tvEan,
            this.tvName,
            this.tvNetto,
            this.tvZcena,
            this.tvRcena,
            this.tvNds,
            this.tvSumR});
            this.dgvTovars.Location = new System.Drawing.Point(12, 38);
            this.dgvTovars.MultiSelect = false;
            this.dgvTovars.Name = "dgvTovars";
            this.dgvTovars.RowHeadersVisible = false;
            this.dgvTovars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTovars.Size = new System.Drawing.Size(757, 369);
            this.dgvTovars.TabIndex = 14;
            this.dgvTovars.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTovars_CellValidating);
            this.dgvTovars.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTovars_CellEndEdit);
            this.dgvTovars.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvTovars_Paint);
            this.dgvTovars.Resize += new System.EventHandler(this.dgvTovars_Resize);
            this.dgvTovars.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvTovars_ColumnWidthChanged);
            // 
            // tvEan
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.tvEan.DefaultCellStyle = dataGridViewCellStyle10;
            this.tvEan.HeaderText = "EAN";
            this.tvEan.Name = "tvEan";
            this.tvEan.ReadOnly = true;
            // 
            // tvName
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.tvName.DefaultCellStyle = dataGridViewCellStyle11;
            this.tvName.FillWeight = 215F;
            this.tvName.HeaderText = "Наименование";
            this.tvName.Name = "tvName";
            this.tvName.ReadOnly = true;
            // 
            // tvNetto
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N3";
            dataGridViewCellStyle12.NullValue = null;
            this.tvNetto.DefaultCellStyle = dataGridViewCellStyle12;
            this.tvNetto.FillWeight = 95F;
            this.tvNetto.HeaderText = "Количество";
            this.tvNetto.Name = "tvNetto";
            this.tvNetto.ReadOnly = true;
            // 
            // tvZcena
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            this.tvZcena.DefaultCellStyle = dataGridViewCellStyle13;
            this.tvZcena.FillWeight = 95F;
            this.tvZcena.HeaderText = "Цена закупки";
            this.tvZcena.Name = "tvZcena";
            this.tvZcena.ReadOnly = true;
            // 
            // tvRcena
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.tvRcena.DefaultCellStyle = dataGridViewCellStyle14;
            this.tvRcena.FillWeight = 95F;
            this.tvRcena.HeaderText = "Цена реал.";
            this.tvRcena.Name = "tvRcena";
            // 
            // tvNds
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.tvNds.DefaultCellStyle = dataGridViewCellStyle15;
            this.tvNds.FillWeight = 50F;
            this.tvNds.HeaderText = "НДС";
            this.tvNds.Name = "tvNds";
            this.tvNds.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tvNds.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tvSumR
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "N2";
            dataGridViewCellStyle16.NullValue = null;
            this.tvSumR.DefaultCellStyle = dataGridViewCellStyle16;
            this.tvSumR.HeaderText = "Сумма";
            this.tvSumR.Name = "tvSumR";
            this.tvSumR.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Дата:";
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(54, 11);
            this.tbDate.Name = "tbDate";
            this.tbDate.ReadOnly = true;
            this.tbDate.Size = new System.Drawing.Size(64, 20);
            this.tbDate.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "ТТН:";
            // 
            // tbTTN
            // 
            this.tbTTN.Location = new System.Drawing.Point(159, 11);
            this.tbTTN.MaxLength = 11;
            this.tbTTN.Name = "tbTTN";
            this.tbTTN.Size = new System.Drawing.Size(68, 20);
            this.tbTTN.TabIndex = 18;
            // 
            // tbVnudok
            // 
            this.tbVnudok.Location = new System.Drawing.Point(309, 11);
            this.tbVnudok.Name = "tbVnudok";
            this.tbVnudok.ReadOnly = true;
            this.tbVnudok.Size = new System.Drawing.Size(82, 20);
            this.tbVnudok.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "№ внут. док:";
            // 
            // tbDateEdit
            // 
            this.tbDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateEdit.Location = new System.Drawing.Point(241, 439);
            this.tbDateEdit.Name = "tbDateEdit";
            this.tbDateEdit.ReadOnly = true;
            this.tbDateEdit.Size = new System.Drawing.Size(124, 20);
            this.tbDateEdit.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 442);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Дата:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Завел:";
            // 
            // tbEditor
            // 
            this.tbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEditor.Location = new System.Drawing.Point(57, 439);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.ReadOnly = true;
            this.tbEditor.Size = new System.Drawing.Size(121, 20);
            this.tbEditor.TabIndex = 24;
            // 
            // tbSumR
            // 
            this.tbSumR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbSumR.Location = new System.Drawing.Point(676, 407);
            this.tbSumR.Name = "tbSumR";
            this.tbSumR.ReadOnly = true;
            this.tbSumR.Size = new System.Drawing.Size(92, 20);
            this.tbSumR.TabIndex = 28;
            this.tbSumR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSumNetto
            // 
            this.tbSumNetto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbSumNetto.Location = new System.Drawing.Point(416, 407);
            this.tbSumNetto.Name = "tbSumNetto";
            this.tbSumNetto.ReadOnly = true;
            this.tbSumNetto.Size = new System.Drawing.Size(88, 20);
            this.tbSumNetto.TabIndex = 29;
            this.tbSumNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(370, 410);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Итого:";
            // 
            // wait
            // 
            this.wait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wait.AutoSize = true;
            this.wait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wait.Location = new System.Drawing.Point(707, 3);
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(69, 16);
            this.wait.TabIndex = 31;
            this.wait.Text = "Ждите...";
            this.wait.Visible = false;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::Spisanie.Properties.Resources.pict_save;
            this.btSave.Location = new System.Drawing.Point(701, 433);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(30, 30);
            this.btSave.TabIndex = 13;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btClose.Location = new System.Drawing.Point(737, 433);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 13;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(481, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Тип накладной:";
            // 
            // cbUL
            // 
            this.cbUL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUL.FormattingEnabled = true;
            this.cbUL.Location = new System.Drawing.Point(659, 11);
            this.cbUL.Name = "cbUL";
            this.cbUL.Size = new System.Drawing.Size(72, 21);
            this.cbUL.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(626, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "ЮЛ:";
            // 
            // frmNakl_Sp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(779, 475);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbUL);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wait);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbSumNetto);
            this.Controls.Add(this.tbSumR);
            this.Controls.Add(this.tbDateEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbEditor);
            this.Controls.Add(this.tbVnudok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTovars);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNakl_Sp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Акт списания";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmNakl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTovars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridView dgvTovars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTTN;
        private System.Windows.Forms.TextBox tbVnudok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbDateEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbEditor;
        private System.Windows.Forms.TextBox tbSumR;
        private System.Windows.Forms.TextBox tbSumNetto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label wait;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvEan;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvNetto;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvZcena;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvRcena;
        private System.Windows.Forms.DataGridViewComboBoxColumn tvNds;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvSumR;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUL;
        private System.Windows.Forms.Label label8;
    }
}