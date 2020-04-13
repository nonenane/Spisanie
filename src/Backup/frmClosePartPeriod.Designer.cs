namespace Spisanie
{
    partial class frmClosePartPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClosePartPeriod));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClosePeriod = new System.Windows.Forms.Button();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::Spisanie.Properties.Resources.pict_close;
            this.btnExit.Location = new System.Drawing.Point(349, 82);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClosePeriod
            // 
            this.btnClosePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePeriod.Image = ((System.Drawing.Image)(resources.GetObject("btnClosePeriod.Image")));
            this.btnClosePeriod.Location = new System.Drawing.Point(313, 82);
            this.btnClosePeriod.Name = "btnClosePeriod";
            this.btnClosePeriod.Size = new System.Drawing.Size(30, 30);
            this.btnClosePeriod.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnClosePeriod, "Закрыть часть периода");
            this.btnClosePeriod.UseVisualStyleBackColor = true;
            this.btnClosePeriod.Click += new System.EventHandler(this.btnClosePeriod_Click);
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(12, 15);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(41, 13);
            this.departmentLabel.TabIndex = 20;
            this.departmentLabel.Text = "Отдел:";
            // 
            // cbDepartment
            // 
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Location = new System.Drawing.Point(71, 12);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Size = new System.Drawing.Size(212, 21);
            this.cbDepartment.TabIndex = 19;
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.Enabled = false;
            this.dtpDateStart.Location = new System.Drawing.Point(71, 50);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(133, 20);
            this.dtpDateStart.TabIndex = 22;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 54);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(225, 13);
            this.lblPeriod.TabIndex = 21;
            this.lblPeriod.Text = "Период с                                                     по";
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.Enabled = false;
            this.dtpDateEnd.Location = new System.Drawing.Point(243, 50);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(133, 20);
            this.dtpDateEnd.TabIndex = 23;
            // 
            // frmClosePartPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 124);
            this.ControlBox = false;
            this.Controls.Add(this.dtpDateEnd);
            this.Controls.Add(this.dtpDateStart);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.btnClosePeriod);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClosePartPeriod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Закрытие части периода";
            this.Load += new System.EventHandler(this.frmClosePartPeriod_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClosePeriod;
        private System.Windows.Forms.Label departmentLabel;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}