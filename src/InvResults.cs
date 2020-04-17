using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
using Nwuram.Framework.ToExcel;

namespace Spisanie
{
    public partial class frmInvResults : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public frmInvResults()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmInvResults_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть форму");
            t.SetToolTip(btPrint, "Результаты инвентаризации");
            t.SetToolTip(dtpInvDate, "Введите дату основной инвентаризации");
            DataTable dt = proc.GetCloseDate();
            if (dt == null)
            {
                MessageBox.Show("Не удалось получить дату последней инвентаризации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dtpInvDate.Value = DateTime.Parse(dt.Rows[0]["dinv"].ToString());
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            DataTable dtResult = proc.getTovarOutPrcnPrice(dtpInvDate.Value.Date);

            if (dtResult == null) {
                MessageBox.Show("Не удалось получить данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                return;
            }

            if (dtResult.Rows.Count > 0)
            {
                new percentData.frmFindTovar() { Text = $"Проверка превышения процента наценки за {dtpInvDate.Value.ToShortDateString()}", dtResult = dtResult,date = dtpInvDate.Value }.ShowDialog();
            }


            DataTable dt = proc.GetInvRezults(dtpInvDate.Value.Date);
            if (dt == null)
            {
                MessageBox.Show("Не удалось получить данные инвентаризации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                return;
            }
            if (dt.Rows.Count > 0 && HandmadeReport.ExcelAvailable)
            {
                try
                {
                    HandmadeReport temp = new HandmadeReport();

                    string shopName = "";
                    if (Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer().ToLower().Contains("k21")) shopName = "Косышгина 21"; else shopName = "Хошимина 14";

                    temp.AddSingleValue($"Инвентаризация на {dtpInvDate.Value.ToShortDateString()} с возвратами по магазину {shopName}", 1, 2);
                    temp.Merge(1, 2, 1, 9);
                    temp.AddSingleValue($"Выгрузил {Nwuram.Framework.Settings.User.UserSettings.User.FullUsername}; Дата выгрузки: {DateTime.Now.ToString()}", 1, 2);
                    temp.Merge(2, 2, 2, 9);
                    temp.SetCellAlignmentToRight(2, 2, 2, 9);

                    temp.AddSingleValue("Отдел", 3, 2);
                    temp.AddSingleValue("Бухг. (тов. отчет)", 3, 3);
                    temp.AddSingleValue("Инвентар. (факт)", 3, 4);
                    temp.AddSingleValue("Дельта", 3, 5);
                    temp.AddSingleValue("Недостача", 3, 6);
                    temp.AddSingleValue("Разница", 3, 7);
                    temp.AddSingleValue("Пробито", 3, 8);
                    temp.AddSingleValue("Возвраты", 3, 9);
                    temp.SetCellAlignmentToCenter(3, 2, 3, 9);
                    temp.SetFontBold(1, 2, 3, 9);

                    int i = 4;
                    foreach (DataRow dr in dt.Rows)
                    {
                        temp.AddSingleValue(dr["name"].ToString(), i, 2);
                        temp.AddSingleValue(dr["buh_ost_without_spis"].ToString(), i, 3);
                        temp.AddSingleValue(dr["inv_fact"].ToString(), i, 4);
                        temp.AddSingleValue(dr["delta"].ToString(), i, 5);
                        temp.AddSingleValue(dr["nedost"].ToString(), i, 6);
                        temp.AddSingleValue("=E" + i.ToString() + "-F" + i.ToString(), i, 7);

                        temp.AddSingleValue(dr["vozvkass"].ToString(), i, 9);
                        i++;
                    }


                    temp.AddSingleValue("Общая сумма", i, 2);
                    temp.AddSingleValue("=СУММ(C4:C" + (i - 1).ToString() + ")", i, 3);
                    temp.AddSingleValue("=СУММ(D4:D" + (i - 1).ToString() + ")", i, 4);
                    temp.AddSingleValue("=СУММ(E4:E" + (i - 1).ToString() + ")", i, 5);
                    temp.AddSingleValue("=СУММ(F4:F" + (i - 1).ToString() + ")", i, 6);
                    temp.AddSingleValue("=СУММ(G4:G" + (i - 1).ToString() + ")", i, 7);
                    temp.AddSingleValue("=СУММ(H4:H" + (i - 1).ToString() + ")", i, 8);
                    temp.AddSingleValue("=СУММ(I4:I" + (i - 1).ToString() + ")", i, 9);
                    temp.SetFontBold(i, 2, i, 9);
                    temp.SetCellAlignmentToRight(4, 3, i, 9);

                    temp.SetBorders(3, 2, i, 9);
                    temp.SetFormat(3, 3, i, 9, "### ##0,00");
                    //temp.SetColumnAutoSize(3, 2, i, 2);

                    temp.AddSingleValue("Списание по результатам инвентаризации", i + 3, 2);
                    temp.Merge(1, 2, 1, 9);

                    int startPos = i + 5;
                    temp.AddSingleValue("Отдел", startPos, 2);
                    temp.Merge(startPos, 2, startPos + 1, 2);
                    temp.AddSingleValue("Бухг. (тов. отчет)", startPos, 3);
                    temp.Merge(startPos, 3, startPos + 1, 3);
                    temp.AddSingleValue("списание", startPos, 4);
                    temp.Merge(startPos, 4, startPos, 5);
                    temp.AddSingleValue("закупка", startPos + 1, 4);
                    temp.AddSingleValue("продажа", startPos + 1, 5);
                    temp.AddSingleValue("переоценка", startPos, 6);
                    temp.Merge(startPos, 6, startPos + 1, 6);
                    temp.AddSingleValue("Итого", startPos, 7);
                    temp.Merge(startPos, 7, startPos + 1, 7);
                    temp.AddSingleValue("бухг. ост. после списания", startPos, 8);
                    temp.Merge(startPos, 8, startPos + 1, 8);
                    temp.SetWrapText(startPos, 8, startPos, 8);
                    temp.AddSingleValue("разница", startPos, 9);
                    temp.Merge(startPos, 9, startPos + 1, 9);
                    temp.SetCellAlignmentToCenter(startPos, 2, startPos + 1, 9);
                    temp.SetFontBold(i + 3, 2, startPos + 1, 9);

                    i = startPos + 2;
                    foreach (DataRow dr in dt.Rows)
                    {
                        temp.AddSingleValue(dr["name"].ToString(), i, 2);
                        temp.AddSingleValue(dr["buh_ost_without_spis"].ToString(), i, 3);
                        temp.AddSingleValue(dr["zsum"].ToString(), i, 4);
                        temp.AddSingleValue(dr["psum"].ToString(), i, 5);
                        temp.AddSingleValue(dr["pereocsum"].ToString(), i, 6);
                        temp.AddSingleValue(dr["allsum"].ToString(), i, 7);
                        temp.AddSingleValue(dr["buh_ost"].ToString(), i, 8);
                        temp.AddSingleValue(dr["raznsum"].ToString(), i, 9);
                        i++;
                    }

                    temp.AddSingleValue("Общая сумма", i, 2);
                    temp.AddSingleValue("=СУММ(C" + (startPos + 2).ToString() + ":C" + (i - 1).ToString() + ")", i, 3);
                    temp.AddSingleValue("=СУММ(D" + (startPos + 2).ToString() + ":D" + (i - 1).ToString() + ")", i, 4);
                    temp.AddSingleValue("=СУММ(E" + (startPos + 2).ToString() + ":E" + (i - 1).ToString() + ")", i, 5);
                    temp.AddSingleValue("=СУММ(F" + (startPos + 2).ToString() + ":F" + (i - 1).ToString() + ")", i, 6);
                    temp.AddSingleValue("=СУММ(G" + (startPos + 2).ToString() + ":G" + (i - 1).ToString() + ")", i, 7);
                    temp.AddSingleValue("=СУММ(H" + (startPos + 2).ToString() + ":H" + (i - 1).ToString() + ")", i, 8);
                    temp.AddSingleValue("=СУММ(I" + (startPos + 2).ToString() + ":I" + (i - 1).ToString() + ")", i, 9);
                    temp.SetFontBold(i, 2, i, 9);
                    temp.SetCellAlignmentToRight(startPos + 2, 3, i, 9);

                    temp.SetBorders(startPos, 2, i, 9);
                    temp.SetFormat(startPos, 3, i, 9, "### ##0,00");
                    //temp.SetColumnAutoSize(3, 3, i, 9);
                    temp.SetColumnWidth(2, 2, 1, i, 15);
                    temp.SetColumnWidth(3, 3, 1, i, 16);
                    temp.SetColumnWidth(4, 4, 1, i, 17);
                    temp.SetColumnWidth(5, 5, 1, i, 12);
                    temp.SetColumnWidth(6, 6, 1, i, 12);
                    temp.SetColumnWidth(7, 7, 1, i, 12);
                    temp.SetColumnWidth(8, 8, 1, i, 15);
                    temp.SetColumnWidth(9, 9, 1, i, 10);
                    temp.SetPageOrientationToLandscape();

                    temp.Show();
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                MessageBox.Show("Отсутствует установленный пакет Microsoft Office Excel.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Enabled = true;
        }
    }
}
