namespace Spisanie
{
    partial class mainForm
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.ToolStripDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPrihod = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPereoc = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOtgruz = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemVozvrat = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSpis = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOrganizations = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPaths = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFirms = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripReports = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBuhOst = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInvRezults = new System.Windows.Forms.ToolStripMenuItem();
            this.упарвлениеПериодамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClosePartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripDocs,
            this.ToolStripDictionary,
            this.ToolStripSettings,
            this.ToolStripReports,
            this.упарвлениеПериодамиToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1016, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // ToolStripDocs
            // 
            this.ToolStripDocs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLoad,
            this.MenuItemPrihod,
            this.MenuItemPereoc,
            this.MenuItemOtgruz,
            this.MenuItemVozvrat,
            this.MenuItemSpis,
            this.MenuItemExit});
            this.ToolStripDocs.Name = "ToolStripDocs";
            this.ToolStripDocs.Size = new System.Drawing.Size(77, 20);
            this.ToolStripDocs.Text = "Документы";
            // 
            // MenuItemLoad
            // 
            this.MenuItemLoad.Name = "MenuItemLoad";
            this.MenuItemLoad.Size = new System.Drawing.Size(204, 22);
            this.MenuItemLoad.Text = "Загрузить";
            this.MenuItemLoad.Click += new System.EventHandler(this.MenuItemLoad_Click);
            // 
            // MenuItemPrihod
            // 
            this.MenuItemPrihod.Name = "MenuItemPrihod";
            this.MenuItemPrihod.Size = new System.Drawing.Size(204, 22);
            this.MenuItemPrihod.Text = "Приход от поставщика";
            this.MenuItemPrihod.Click += new System.EventHandler(this.MenuItemPrihod_Click);
            // 
            // MenuItemPereoc
            // 
            this.MenuItemPereoc.Name = "MenuItemPereoc";
            this.MenuItemPereoc.Size = new System.Drawing.Size(204, 22);
            this.MenuItemPereoc.Text = "Акты переоценки";
            this.MenuItemPereoc.Click += new System.EventHandler(this.MenuItemPereoc_Click);
            // 
            // MenuItemOtgruz
            // 
            this.MenuItemOtgruz.Name = "MenuItemOtgruz";
            this.MenuItemOtgruz.Size = new System.Drawing.Size(204, 22);
            this.MenuItemOtgruz.Text = "Отгрузка покупателю";
            this.MenuItemOtgruz.Click += new System.EventHandler(this.MenuItemOtgruz_Click);
            // 
            // MenuItemVozvrat
            // 
            this.MenuItemVozvrat.Name = "MenuItemVozvrat";
            this.MenuItemVozvrat.Size = new System.Drawing.Size(204, 22);
            this.MenuItemVozvrat.Text = "Возврат от покупателя";
            this.MenuItemVozvrat.Click += new System.EventHandler(this.MenuItemVozvrat_Click);
            // 
            // MenuItemSpis
            // 
            this.MenuItemSpis.Name = "MenuItemSpis";
            this.MenuItemSpis.Size = new System.Drawing.Size(204, 22);
            this.MenuItemSpis.Text = "Акты списания";
            this.MenuItemSpis.Click += new System.EventHandler(this.MenuItemSpis_Click);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(204, 22);
            this.MenuItemExit.Text = "Выход";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // ToolStripDictionary
            // 
            this.ToolStripDictionary.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemOrganizations});
            this.ToolStripDictionary.Name = "ToolStripDictionary";
            this.ToolStripDictionary.Size = new System.Drawing.Size(86, 20);
            this.ToolStripDictionary.Text = "Справочники";
            // 
            // MenuItemOrganizations
            // 
            this.MenuItemOrganizations.Name = "MenuItemOrganizations";
            this.MenuItemOrganizations.Size = new System.Drawing.Size(213, 22);
            this.MenuItemOrganizations.Text = "Справочник организаций";
            this.MenuItemOrganizations.Click += new System.EventHandler(this.MenuItemOrganizations_Click);
            // 
            // ToolStripSettings
            // 
            this.ToolStripSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPaths,
            this.MenuItemFirms,
            this.ClosePartToolStripMenuItem});
            this.ToolStripSettings.Name = "ToolStripSettings";
            this.ToolStripSettings.Size = new System.Drawing.Size(73, 20);
            this.ToolStripSettings.Text = "Настройки";
            // 
            // MenuItemPaths
            // 
            this.MenuItemPaths.Name = "MenuItemPaths";
            this.MenuItemPaths.Size = new System.Drawing.Size(206, 22);
            this.MenuItemPaths.Text = "Настройка путей";
            this.MenuItemPaths.Visible = false;
            // 
            // MenuItemFirms
            // 
            this.MenuItemFirms.Name = "MenuItemFirms";
            this.MenuItemFirms.Size = new System.Drawing.Size(206, 22);
            this.MenuItemFirms.Text = "Настройка организаций";
            this.MenuItemFirms.Click += new System.EventHandler(this.MenuItemFirms_Click);
            // 
            // ToolStripReports
            // 
            this.ToolStripReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBuhOst,
            this.MenuItemInvRezults});
            this.ToolStripReports.Name = "ToolStripReports";
            this.ToolStripReports.Size = new System.Drawing.Size(59, 20);
            this.ToolStripReports.Text = "Отчеты";
            // 
            // MenuItemBuhOst
            // 
            this.MenuItemBuhOst.Name = "MenuItemBuhOst";
            this.MenuItemBuhOst.Size = new System.Drawing.Size(232, 22);
            this.MenuItemBuhOst.Text = "Бухгалтерские остатки";
            this.MenuItemBuhOst.Click += new System.EventHandler(this.MenuItemBuhOst_Click);
            // 
            // MenuItemInvRezults
            // 
            this.MenuItemInvRezults.Name = "MenuItemInvRezults";
            this.MenuItemInvRezults.Size = new System.Drawing.Size(232, 22);
            this.MenuItemInvRezults.Text = "Результаты инвентаризации";
            this.MenuItemInvRezults.Click += new System.EventHandler(this.MenuItemInvRezults_Click);
            // 
            // упарвлениеПериодамиToolStripMenuItem
            // 
            this.упарвлениеПериодамиToolStripMenuItem.Name = "упарвлениеПериодамиToolStripMenuItem";
            this.упарвлениеПериодамиToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.упарвлениеПериодамиToolStripMenuItem.Text = "Управление периодами";
            this.упарвлениеПериодамиToolStripMenuItem.Click += new System.EventHandler(this.упарвлениеПериодамиToolStripMenuItem_Click);
            // 
            // ClosePartToolStripMenuItem
            // 
            this.ClosePartToolStripMenuItem.Name = "ClosePartToolStripMenuItem";
            this.ClosePartToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ClosePartToolStripMenuItem.Text = "Закрытие части периода";
            this.ClosePartToolStripMenuItem.Click += new System.EventHandler(this.ClosePartToolStripMenuItem_Click);
            // 
            // mainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1016, 666);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Инвентаризационное списание.Бухгалтер.";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDocs;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoad;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPrihod;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPereoc;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOtgruz;
        private System.Windows.Forms.ToolStripMenuItem MenuItemVozvrat;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSpis;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDictionary;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOrganizations;
        private System.Windows.Forms.ToolStripMenuItem ToolStripSettings;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPaths;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFirms;
        private System.Windows.Forms.ToolStripMenuItem ToolStripReports;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBuhOst;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInvRezults;
        private System.Windows.Forms.ToolStripMenuItem упарвлениеПериодамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClosePartToolStripMenuItem;
    }
}

