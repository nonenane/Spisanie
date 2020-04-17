using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.ToWord;


namespace Spisanie
{
    public partial class frmZero : Form
    {
        DataTable dtIn;

        public frmZero(DataTable dt)
        {
            dtIn = dt;
            InitializeComponent();
        }

        private void frmZero_Load(object sender, EventArgs e)
        {
            DataRow[] drs = dtIn.Select("type=1");
            foreach (DataRow dr in drs)
            {
                tbZcena.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }
            drs = dtIn.Select("type=2");
            foreach (DataRow dr in drs)
            {
                tbRcena.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }
            drs = dtIn.Select("type=3");
            foreach (DataRow dr in drs)
            {
                tbNds.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }

            Logging.Comment("Перечень данных по отсутствию цен (с формы 'Отсутствие цен'):");
            Logging.Comment($"Сформированы документы с отсутствием у товаров цен - закупочной: {tbZcena.Text}");
            Logging.Comment($"Сформированы документы с отсутствием у товаров цен - продажной: {tbRcena.Text}");
            Logging.Comment($"Сформированы документы с отсутствием у товаров ставки НДС: {tbNds.Text}");

            Logging.StopFirstLevel();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(79);
            Logging.Comment("Произведена выгрузка отчета с формы 'Отсутствие цен'");
            Logging.Comment("Перечень данных по отсутствию цен (с формы 'Отсутствие цен'):");
            Logging.Comment($"Сформированы документы с отсутствием у товаров цен - закупочной: {tbZcena.Text}");
            Logging.Comment($"Сформированы документы с отсутствием у товаров цен - продажной: {tbRcena.Text}");
            Logging.Comment($"Сформированы документы с отсутствием у товаров ставки НДС: {tbNds.Text}");
            Logging.StopFirstLevel();

            HandmadeReport report = new HandmadeReport();

            report.TypeText(false, "1. Отсутствует цена закупки в накладных:\x0B");
            report.TypeText(false, tbZcena.Text.Replace("\r\n", "\x0B") + "\x0B");

            report.TypeText(false, "2. Отсутствует цена продажи в накладных:\x0B");
            report.TypeText(false, tbRcena.Text.Replace("\r\n", "\x0B") + "\x0B");

            report.TypeText(false, "3. Не заполнена ставка НДС в накладных:\x0B");
            report.TypeText(false, tbNds.Text.Replace("\r\n", "\x0B") + "\x0B");

            report.Show();

            this.Close();
        }
    }
}
