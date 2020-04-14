namespace Spisanie
{
    partial class frmNaklsP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDeps = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvNakls = new System.Windows.Forms.DataGridView();
            this.nkdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkttn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkdep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkpost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkUL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nksumnakl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nksumtov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nkvnudok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbEditor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDateEdit = new System.Windows.Forms.TextBox();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.wait = new System.Windows.Forms.Label();
            this.tbSumTov = new System.Windows.Forms.TextBox();
            this.tbSumNak = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNakls)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отдел:";
            // 
            // cbDeps
            // 
            this.cbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeps.FormattingEnabled = true;
            this.cbDeps.Location = new System.Drawing.Point(59, 6);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(121, 21);
            this.cbDeps.TabIndex = 5;
            this.cbDeps.SelectionChangeCommitted += new System.EventHandler(this.cbDeps_SelectionChangeCommitted);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(282, 6);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(85, 20);
            this.dtpStartDate.TabIndex = 4;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Период с:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(398, 6);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(84, 20);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "по:";
            // 
            // dgvNakls
            // 
            this.dgvNakls.AllowUserToAddRows = false;
            this.dgvNakls.AllowUserToDeleteRows = false;
            this.dgvNakls.AllowUserToResizeRows = false;
            this.dgvNakls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNakls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNakls.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNakls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvNakls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNakls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nkdate,
            this.nkttn,
            this.nkSF,
            this.nkdep,
            this.nkpost,
            this.nkUL,
            this.nksumnakl,
            this.nksumtov,
            this.nkvnudok});
            this.dgvNakls.Location = new System.Drawing.Point(12, 42);
            this.dgvNakls.MultiSelect = false;
            this.dgvNakls.Name = "dgvNakls";
            this.dgvNakls.ReadOnly = true;
            this.dgvNakls.RowHeadersVisible = false;
            this.dgvNakls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNakls.Size = new System.Drawing.Size(757, 367);
            this.dgvNakls.TabIndex = 7;
            this.dgvNakls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNakls_CellContentClick);
            this.dgvNakls.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNakls_CellMouseDoubleClick);
            this.dgvNakls.CurrentCellChanged += new System.EventHandler(this.dgvNakls_CurrentCellChanged);
            this.dgvNakls.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvNakls_Paint);
            // 
            // nkdate
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.nkdate.DefaultCellStyle = dataGridViewCellStyle11;
            this.nkdate.FillWeight = 70F;
            this.nkdate.HeaderText = "Дата";
            this.nkdate.Name = "nkdate";
            this.nkdate.ReadOnly = true;
            // 
            // nkttn
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.nkttn.DefaultCellStyle = dataGridViewCellStyle12;
            this.nkttn.FillWeight = 70F;
            this.nkttn.HeaderText = "ТТН";
            this.nkttn.Name = "nkttn";
            this.nkttn.ReadOnly = true;
            // 
            // nkSF
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.nkSF.DefaultCellStyle = dataGridViewCellStyle13;
            this.nkSF.FillWeight = 80F;
            this.nkSF.HeaderText = "№ счет фактуры";
            this.nkSF.Name = "nkSF";
            this.nkSF.ReadOnly = true;
            // 
            // nkdep
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nkdep.DefaultCellStyle = dataGridViewCellStyle14;
            this.nkdep.FillWeight = 80F;
            this.nkdep.HeaderText = "Отдел";
            this.nkdep.Name = "nkdep";
            this.nkdep.ReadOnly = true;
            // 
            // nkpost
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nkpost.DefaultCellStyle = dataGridViewCellStyle15;
            this.nkpost.HeaderText = "Поставщик";
            this.nkpost.Name = "nkpost";
            this.nkpost.ReadOnly = true;
            // 
            // nkUL
            // 
            this.nkUL.HeaderText = "Юр. лицо";
            this.nkUL.Name = "nkUL";
            this.nkUL.ReadOnly = true;
            // 
            // nksumnakl
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "N2";
            dataGridViewCellStyle16.NullValue = null;
            this.nksumnakl.DefaultCellStyle = dataGridViewCellStyle16;
            this.nksumnakl.FillWeight = 90F;
            this.nksumnakl.HeaderText = "Сумма накл.";
            this.nksumnakl.Name = "nksumnakl";
            this.nksumnakl.ReadOnly = true;
            this.nksumnakl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nksumtov
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N2";
            dataGridViewCellStyle17.NullValue = null;
            this.nksumtov.DefaultCellStyle = dataGridViewCellStyle17;
            this.nksumtov.FillWeight = 90F;
            this.nksumtov.HeaderText = "Сумма по товару";
            this.nksumtov.Name = "nksumtov";
            this.nksumtov.ReadOnly = true;
            this.nksumtov.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nkvnudok
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nkvnudok.DefaultCellStyle = dataGridViewCellStyle18;
            this.nkvnudok.FillWeight = 70F;
            this.nkvnudok.HeaderText = "№ внут. док";
            this.nkvnudok.Name = "nkvnudok";
            this.nkvnudok.ReadOnly = true;
            // 
            // tbEditor
            // 
            this.tbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEditor.Location = new System.Drawing.Point(59, 416);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.ReadOnly = true;
            this.tbEditor.Size = new System.Drawing.Size(121, 20);
            this.tbEditor.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 419);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Завел:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 419);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Дата:";
            // 
            // tbDateEdit
            // 
            this.tbDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateEdit.Location = new System.Drawing.Point(243, 416);
            this.tbDateEdit.Name = "tbDateEdit";
            this.tbDateEdit.ReadOnly = true;
            this.tbDateEdit.Size = new System.Drawing.Size(124, 20);
            this.tbDateEdit.TabIndex = 11;
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.Image = global::Spisanie.Properties.Resources.pict_edit;
            this.btEdit.Location = new System.Drawing.Point(631, 439);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(30, 30);
            this.btEdit.TabIndex = 15;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Image = global::Spisanie.Properties.Resources.pict_delete;
            this.btDelete.Location = new System.Drawing.Point(667, 439);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(30, 30);
            this.btDelete.TabIndex = 14;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::Spisanie.Properties.Resources.WZPRINT;
            this.btPrint.Location = new System.Drawing.Point(703, 439);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(30, 30);
            this.btPrint.TabIndex = 13;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btClose.Location = new System.Drawing.Point(739, 439);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 12;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // wait
            // 
            this.wait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wait.AutoSize = true;
            this.wait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wait.Location = new System.Drawing.Point(710, 3);
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(69, 16);
            this.wait.TabIndex = 16;
            this.wait.Text = "Ждите...";
            this.wait.Visible = false;
            // 
            // tbSumTov
            // 
            this.tbSumTov.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSumTov.BackColor = System.Drawing.Color.White;
            this.tbSumTov.Location = new System.Drawing.Point(589, 416);
            this.tbSumTov.Name = "tbSumTov";
            this.tbSumTov.ReadOnly = true;
            this.tbSumTov.Size = new System.Drawing.Size(108, 20);
            this.tbSumTov.TabIndex = 18;
            this.tbSumTov.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSumNak
            // 
            this.tbSumNak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSumNak.BackColor = System.Drawing.Color.White;
            this.tbSumNak.Location = new System.Drawing.Point(478, 416);
            this.tbSumNak.Name = "tbSumNak";
            this.tbSumNak.ReadOnly = true;
            this.tbSumNak.Size = new System.Drawing.Size(108, 20);
            this.tbSumNak.TabIndex = 19;
            this.tbSumNak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmNaklsP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(781, 477);
            this.Controls.Add(this.tbSumNak);
            this.Controls.Add(this.tbSumTov);
            this.Controls.Add(this.wait);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tbDateEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbEditor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDeps);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvNakls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNaklsP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Накладные прихода от поставщика";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmNakls_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNakls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDeps;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvNakls;
        private System.Windows.Forms.TextBox tbEditor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDateEdit;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Label wait;
        private System.Windows.Forms.TextBox tbSumTov;
        private System.Windows.Forms.TextBox tbSumNak;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkttn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkSF;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkdep;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkpost;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkUL;
        private System.Windows.Forms.DataGridViewTextBoxColumn nksumnakl;
        private System.Windows.Forms.DataGridViewTextBoxColumn nksumtov;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkvnudok;
    }
}