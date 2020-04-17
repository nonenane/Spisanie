using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spisanie.percentData
{
    public partial class frmFindTovar : Form
    {
        public DataTable dtResult { set; private get; }
        public DateTime date { set; private get; }
        public frmFindTovar()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmFindTovar_Load(object sender, EventArgs e)
        {
            tbPrcn.Text = ((decimal)dtResult.Rows[0]["prnc"]).ToString("0.000");
            dgvData.DataSource = dtResult;

            Logging.StartFirstLevel(1526);
            Logging.Comment("По результатам выгрузка отчета по результатам инвентаризации обнаружены товары с превышением процента наценки");
            foreach (DataRow row in dtResult.Rows)
            {
                Logging.Comment($"EAN: {row["ean"].ToString()}; " +
                    $"Отдел (ID:{row["id_otdel"].ToString()};Наименование: {row["nameDeps"].ToString()});" +
                    $"Товар (ID:{row["id_tovar"].ToString()};Наименование: {row["nameTovar"].ToString()});" +
                    $"Цена закупки: {row["zcena"].ToString()};" +
                    $"Цена продажи: {row["realRcena"].ToString()};" +
                    $"ТТН: {row["ttn"].ToString()};" +
                    $"Тип накладной: {row["nameOperand"].ToString()}");
                
            }
            Logging.Comment($"Настройка процента наценки: {tbPrcn.Text}");
            Logging.StopFirstLevel();

        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(79);
            Logging.Comment("Произведена выгрузка отчета с превышения процента наценки");
            Logging.Comment($"Количество выгруженных записей: {dtResult.Rows.Count}");
            Logging.Comment($"Настройка процента наценки: {tbPrcn.Text}");
            Logging.StopFirstLevel();

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("list-1");

            int rIndex = 1;
            int maxMerge = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible)
                    maxMerge++;

            report.Merge(rIndex, 1, rIndex, maxMerge);
            report.AddSingleValue("Проверка превышения процента наценки", rIndex, 1);
            report.SetCellAlignmentToCenter(rIndex, 1, rIndex, 1);
            report.SetFontBold(rIndex, 1, rIndex, 1);
            report.SetFontSize(rIndex, 1, rIndex, 1, 14);
            rIndex++;

            report.Merge(rIndex, 1, rIndex, 2);
            report.Merge(rIndex, 5, rIndex, 7);
            report.AddSingleValue("Дата: " + date.ToShortDateString(), rIndex, 1);
            report.AddSingleValue("Выгрузил: " + UserSettings.User.FullUsername, rIndex, 3);
            report.SetCellAlignmentToRight(rIndex, 3, rIndex, 3);
            rIndex++;


            string shotName = "";
            if (Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer().ToLower().Contains("k21")) shotName = "Косыгина 21"; else shotName = "Хошимина 14";

            report.Merge(rIndex, 1, rIndex, 2);
            report.Merge(rIndex, 3, rIndex, 4);
            report.Merge(rIndex, 5, rIndex, 7);
            report.AddSingleValue("Процент наценки: " + tbPrcn.Text, rIndex, 1);
            report.AddSingleValue("Магазин: " + shotName, rIndex, 3);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), rIndex, 5);
            report.SetCellAlignmentToRight(rIndex, 3, rIndex, 3);
            rIndex++;
            rIndex++;

            int cIndex = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (col.Visible)
                {
                    cIndex++;
                    report.AddSingleValue(col.HeaderText, rIndex, cIndex);
                }
            }
            report.SetBorders(rIndex, 1, rIndex, cIndex);
            report.SetCellAlignmentToCenter(rIndex, 1, rIndex, cIndex);
            report.SetWrapText(rIndex, 1, rIndex, cIndex);
            rIndex++;

            foreach (DataRowView row in dtResult.DefaultView)
            {
                cIndex = 0;
                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible)
                    {
                        cIndex++;
                        report.AddSingleValue(row[col.DataPropertyName].ToString(), rIndex, cIndex);
                    }
                report.SetBorders(rIndex, 1, rIndex, cIndex);
                report.SetCellAlignmentToCenter(rIndex, 1, rIndex, cIndex);
                rIndex++;
            }
            
            report.SetColumnAutoSize(1, 1, rIndex, maxMerge);
            report.Show();
        }
    }
}
