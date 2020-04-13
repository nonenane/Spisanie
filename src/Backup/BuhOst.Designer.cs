namespace Spisanie
{
    partial class frmBuhOst
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSums = new System.Windows.Forms.DataGridView();
            this.nkdep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nksumnakl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbDeps = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btPrint = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wait = new System.Windows.Forms.Label();
            this.chbIs = new System.Windows.Forms.CheckBox();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSums)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSums
            // 
            this.dgvSums.AllowUserToAddRows = false;
            this.dgvSums.AllowUserToDeleteRows = false;
            this.dgvSums.AllowUserToResizeColumns = false;
            this.dgvSums.AllowUserToResizeRows = false;
            this.dgvSums.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSums.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSums.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSums.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSums.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nkdep,
            this.nksumnakl});
            this.dgvSums.Location = new System.Drawing.Point(12, 65);
            this.dgvSums.MultiSelect = false;
            this.dgvSums.Name = "dgvSums";
            this.dgvSums.ReadOnly = true;
            this.dgvSums.RowHeadersVisible = false;
            this.dgvSums.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSums.Size = new System.Drawing.Size(190, 191);
            this.dgvSums.TabIndex = 10;
            // 
            // nkdep
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nkdep.DefaultCellStyle = dataGridViewCellStyle2;
            this.nkdep.FillWeight = 109.7245F;
            this.nkdep.HeaderText = "Отдел";
            this.nkdep.Name = "nkdep";
            this.nkdep.ReadOnly = true;
            // 
            // nksumnakl
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.nksumnakl.DefaultCellStyle = dataGridViewCellStyle3;
            this.nksumnakl.FillWeight = 113.1181F;
            this.nksumnakl.HeaderText = "Сумма остатка";
            this.nksumnakl.Name = "nksumnakl";
            this.nksumnakl.ReadOnly = true;
            this.nksumnakl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbDeps
            // 
            this.cbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeps.FormattingEnabled = true;
            this.cbDeps.Location = new System.Drawing.Point(12, 38);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(97, 21);
            this.cbDeps.TabIndex = 9;
            this.cbDeps.SelectionChangeCommitted += new System.EventHandler(this.cbDeps_SelectionChangeCommitted);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(110, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(82, 20);
            this.dtpStartDate.TabIndex = 8;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::Spisanie.Properties.Resources.pict_excel;
            this.btPrint.Location = new System.Drawing.Point(136, 267);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(30, 30);
            this.btPrint.TabIndex = 15;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btClose.Location = new System.Drawing.Point(172, 267);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 14;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Остаток на утро:";
            // 
            // wait
            // 
            this.wait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wait.AutoSize = true;
            this.wait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wait.Location = new System.Drawing.Point(142, 3);
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(69, 16);
            this.wait.TabIndex = 17;
            this.wait.Text = "Ждите...";
            this.wait.Visible = false;
            // 
            // chbIs
            // 
            this.chbIs.AutoSize = true;
            this.chbIs.Location = new System.Drawing.Point(15, 274);
            this.chbIs.Name = "chbIs";
            this.chbIs.Size = new System.Drawing.Size(98, 17);
            this.chbIs.TabIndex = 18;
            this.chbIs.Text = "без учета и. с.";
            this.chbIs.UseVisualStyleBackColor = true;
            this.chbIs.CheckedChanged += new System.EventHandler(this.chbIs_CheckedChanged);
            // 
            // frmBuhOst
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(214, 309);
            this.ControlBox = false;
            this.Controls.Add(this.chbIs);
            this.Controls.Add(this.wait);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvSums);
            this.Controls.Add(this.cbDeps);
            this.Controls.Add(this.dtpStartDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuhOst";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Бухгалтерские остатки";
            this.Load += new System.EventHandler(this.frmBuhOst_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBuhOst_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSums)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSums;
        private System.Windows.Forms.ComboBox cbDeps;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn nkdep;
        private System.Windows.Forms.DataGridViewTextBoxColumn nksumnakl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label wait;
        private System.Windows.Forms.CheckBox chbIs;
        private System.Windows.Forms.BindingSource bds;
    }
}