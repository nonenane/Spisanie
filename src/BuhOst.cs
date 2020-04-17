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
    public partial class frmBuhOst : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        DataTable dtDeps, dtOsts;

        public frmBuhOst()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Filtering()
        {
            string filter = "";
            if (int.Parse(cbDeps.SelectedValue.ToString()) >0)
            {
                filter += "id = " + cbDeps.SelectedValue.ToString();
            }
         
            bds.Filter = filter;
        }

        private void frmBuhOst_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть форму");
            t.SetToolTip(btPrint, "Выгрузка данных");
            t.SetToolTip(cbDeps, "Выберите отдел");
            t.SetToolTip(dtpStartDate, "Введите дату");

            dgvSums.AutoGenerateColumns = false;

            dtDeps = proc.GetDeps();
            dtDeps.Rows[0]["name"] = dtDeps.Rows[0]["name"].ToString().ToUpper();
            cbDeps.DataSource = dtDeps;
            cbDeps.DisplayMember = "name";
            cbDeps.ValueMember = "id";
            if (dtDeps.Rows.Count > 0)
            {
                cbDeps.SelectedIndex = 0;
            }

            dtpStartDate.Value = DateTime.Now;
            this.Visible = true;
            this.Refresh();
            dgvSums_Refresh();
        }

        private void dgvSums_Refresh()
        {
            wait.Visible = true;
            wait.Refresh();
            dtOsts = proc.GetBuhOsts(0, dtpStartDate.Value.Date, chbIs.Checked);
            bds.DataSource = dtOsts;
            dgvSums.DataSource = dtOsts;
            
            nkdep.DataPropertyName = "name";
            nksumnakl.DataPropertyName = "summa";

            Filtering();
            wait.Visible = false;
            wait.Refresh();
        }

        private void cbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filtering();
        }

        private void frmBuhOst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dtpStartDate.Focused)
            {
                dgvSums_Refresh();
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (dgvSums.RowCount > 0)
            {
                Logging.StartFirstLevel(79);
                Logging.Comment($"Произведена выгрузка отчета с формы 'Бухгалтерские остатки' в Excel");
                Logging.Comment($"Дата Остаток на утро {dtpStartDate.Value.ToShortDateString()}");
                Logging.Comment($"Отдел ID:{cbDeps.SelectedValue}; Наименование:{cbDeps.Text}");
                Logging.Comment($"Количество выгруженных записей: {dgvSums.RowCount}");
                Logging.StopFirstLevel();

                if (HandmadeReport.OOAvailable || HandmadeReport.ExcelAvailable)
                {
                    try
                    {
                        HandmadeReport temp = new HandmadeReport();
                        temp.AddSingleValue("Бухгалтерские остатки на " + dtpStartDate.Value.ToShortDateString(), 1, 2);
                        temp.Merge(1, 2, 1, 4);
                        int i = 3;
                        foreach (DataRowView dr in dtOsts.DefaultView)
                        {
                            temp.AddSingleValue(dr["name"].ToString(), i, 2);
                            temp.AddSingleValue(decimal.Parse(dr["summa"].ToString()).ToString("N2"), i, 3);
                            i++;
                        }
                        temp.SetBorders(3, 2, i - 1, 3);
                        temp.SetColumnAutoSize(3, 2, i - 1, 3);
                        temp.Show();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    if (MessageBox.Show("На рабочей станции\nне установлены ПО\nExcel и OpenCalc.\nРаспечатать отчет?", "Запрос на печать отчета", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmPrintPreView frmOtchet = new frmPrintPreView(dtOsts.DefaultView.ToTable(), dtpStartDate.Value);
                        frmOtchet.ShowDialog();
                    }
                }
            }
        }

        private void chbIs_CheckedChanged(object sender, EventArgs e)
        {
            dgvSums_Refresh();
        }



    }
}
