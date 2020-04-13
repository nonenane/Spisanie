namespace Spisanie
{
    partial class frmPrintPreView
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crvReport2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.crvReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crvReport2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(704, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Счет-фактура";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // crvReport2
            // 
            this.crvReport2.ActiveViewIndex = -1;
            this.crvReport2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReport2.DisplayGroupTree = false;
            this.crvReport2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReport2.Location = new System.Drawing.Point(3, 3);
            this.crvReport2.Name = "crvReport2";
            this.crvReport2.SelectionFormula = "";
            this.crvReport2.ShowCloseButton = false;
            this.crvReport2.ShowGroupTreeButton = false;
            this.crvReport2.ShowRefreshButton = false;
            this.crvReport2.Size = new System.Drawing.Size(698, 380);
            this.crvReport2.TabIndex = 2;
            this.crvReport2.ViewTimeSelectionFormula = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.crvReport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(704, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Накладная";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // crvReport
            // 
            this.crvReport.ActiveViewIndex = -1;
            this.crvReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReport.DisplayGroupTree = false;
            this.crvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReport.Location = new System.Drawing.Point(3, 3);
            this.crvReport.Name = "crvReport";
            this.crvReport.SelectionFormula = "";
            this.crvReport.ShowCloseButton = false;
            this.crvReport.ShowGroupTreeButton = false;
            this.crvReport.ShowRefreshButton = false;
            this.crvReport.Size = new System.Drawing.Size(698, 380);
            this.crvReport.TabIndex = 1;
            this.crvReport.ViewTimeSelectionFormula = "";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(712, 412);
            this.tab.TabIndex = 1;
            // 
            // frmPrintPreView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(712, 412);
            this.Controls.Add(this.tab);
            this.Name = "frmPrintPreView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать документов";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrintPreView_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReport2;
        private System.Windows.Forms.TabPage tabPage1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReport;
        private System.Windows.Forms.TabControl tab;

    }
}