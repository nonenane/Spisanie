using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Spisanie
{
    public partial class frmNaklPc : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private int id, id_post, id_dep, id_ul;
        private string oldttn;
        private DataTable dtTovars;

        public frmNaklPc(int inId, string inTtn, string inVnudok, DateTime inDatein, DateTime inDkor, string inEditor, int inId_post, int inid_dep)
        {
            id = inId;
            id_post = inId_post;
            id_dep = inid_dep;
            InitializeComponent();

            tbTTN.Text = inTtn;
            tbDate.Text = inDatein.ToShortDateString();
            tbVnudok.Text = inVnudok;
            tbEditor.Text = inEditor;
            tbDateEdit.Text = inDkor.ToString();
            oldttn = inTtn;
        }

        private void frmNakl_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть акт");
            t.SetToolTip(btSave, "Сохранить акт");
            t.SetToolTip(tbSumNetto, "Итого по количеству");
            t.SetToolTip(tbSumR, "Итого по сумме");

            initUL();
            dgvTovar_Init();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DataRow[] tmp = dtTovars.Select("rcena<>oldrcena or bcena<>oldbcena");
            if (tmp.Length > 0 || oldttn.Trim() != tbTTN.Text.Trim())
            {
                DialogResult d = MessageBox.Show("В акт были внесены\n изменения. Сохранить акт?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    btSave_Click(sender, e);
                }
                else
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void dgvTovar_Init()
        {
            wait.Visible = true;
            Refresh();
            dgvTovars.DataSource = null;

            dtTovars = proc.GetAdvige(id, 5);  

           
                dgvTovars.AutoGenerateColumns = false;
                dgvTovars.DataSource = dtTovars;

                tvEan.DataPropertyName = "ean";
                tvName.DataPropertyName = "name";
                tvNds.DataSource = proc.GetSpravNDS();
                tvNds.DisplayMember = "nds";
                tvNds.ValueMember = "id";
                tvNds.DataPropertyName = "nds";
                tvNetto.DataPropertyName = "netto";
                tvRcena.DataPropertyName = "rcena";
                tvBcena.DataPropertyName = "bcena";
                tvSumB.DataPropertyName = "bsum";

                tbSumNetto.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(netto)", "")), 3));
                tbSumR.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(bsum)", "")), 2));
           
            wait.Visible = false;
            Refresh();
        }

        private void ResizeSumElements()
        {
            tbSumNetto.Left = dgvTovars.Left + tvEan.Width + tvName.Width;
            tbSumNetto.Width = tvNetto.Width;
            tbSumNetto.Top = dgvTovars.Top + dgvTovars.Height;
            label7.Left = tbSumNetto.Left - label7.Width;
            label7.Top = tbSumNetto.Top+3;
            tbSumR.Width = tvSumB.Width;
            tbSumR.Left = dgvTovars.Left + tvEan.Width + tvName.Width + tvNds.Width + tvNetto.Width + tvRcena.Width + tvBcena.Width;
            tbSumR.Top = dgvTovars.Top + dgvTovars.Height;
        }

        private void dgvTovars_Resize(object sender, EventArgs e)
        {
            //ResizeSumElements();
        }

        private void dgvTovars_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //ResizeSumElements();
        }

        private void dgvTovars_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:
                case 4:
                    dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Math.Abs(decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace('.',',')));
                    dgvTovars.Rows[e.RowIndex].Cells[6].Value = decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[2].Value.ToString()) * decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[4].Value.ToString())
                                                                - decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[2].Value.ToString()) * decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[3].Value.ToString());
                    tbSumR.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(bsum)", "")), 2));
                    break;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            wait.Visible = true;
            Refresh();
            
            #region Проверки
            if (tbTTN.Text.Trim().Length == 0)
            {
                MessageBox.Show("Не указан номер ТТН!", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wait.Visible = false;
                Refresh();
                return;
            }
            DataRow[] tmp = dtTovars.Select("bcena=0 or rcena=0");
            if (tmp.Length > 0)
            {
                MessageBox.Show("Цена не должна быть = 0!", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wait.Visible = false;
                Refresh();
                return;
            }
            #endregion

            #region Запись изменений
            proc.ChangeAllPrihod(id, tbTTN.Text, "", id_post, int.Parse(cbUL.SelectedValue.ToString()));
            int resultValidate = int.Parse(proc.ValidateNtypeorg(id_dep, int.Parse(cbUL.SelectedValue.ToString())).Rows[0]["result"].ToString());

            if (TempValues.Error)
            {
                return;
            }

            Logging.StartFirstLevel(27);

            Logging.Comment("Начало редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
                      
            foreach (DataRow dr in dtTovars.Rows)
            {
                if (dr["rcena"].ToString() != dr["oldrcena"].ToString()
                    || dr["bcena"].ToString() != dr["oldbcena"].ToString()
                    || dr["nds"].ToString() != dr["oldnds"].ToString())
                {
                    Logging.Comment("Изменение цен в акте переоценки");
                    proc.ChangeAdvige(int.Parse(dr["id"].ToString()), null, decimal.Parse(dr["rcena"].ToString()), decimal.Parse(dr["bcena"].ToString()), int.Parse(dr["nds"].ToString()), 5);
                    if (TempValues.Error)
                    {
                        return;
                    }
                    //Log
                    if (dr["rcena"].ToString() != dr["oldrcena"].ToString())
                    {
                        Logging.Comment("Изменение цены продажи в акте переоценки");
                        Logging.VariableChange("rcena", dr["rcena"].ToString(), dr["oldrcena"].ToString());
                    }
                    if (dr["bcena"].ToString() != dr["oldbcena"].ToString())
                    {
                        Logging.Comment("Изменение старой цены в акте переоценки");
                        Logging.VariableChange("bcena", dr["bcena"].ToString(), dr["oldbcena"].ToString());
                    }
                }
                proc.editNtypeOrgTovar(int.Parse(dr["kodt"].ToString()), int.Parse(cbUL.SelectedValue.ToString()), resultValidate); 
            }
            DataTable dt = proc.GetCloseDate();
            proc.SetRests(DateTime.Parse(tbDate.Text), DateTime.Parse(dt.Rows[0]["dinv"].ToString()), id_dep);
            Logging.Comment("Конец редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
            Logging.StopFirstLevel();

            if (oldttn.Trim() != tbTTN.Text.Trim())
            {
                Logging.StartFirstLevel(23);
                Logging.Comment("Начало редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
                Logging.Comment("Изменение ttn акта переоценки");
                Logging.VariableChange("ttn", tbTTN.Text.Trim(), oldttn.Trim());
                Logging.Comment("Конец редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion
            
            wait.Visible = false;
            Refresh();

            MessageBox.Show("Данные в акте сохранены.","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
           // Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " сохранил изменения в акте переоценки " + tbTTN.Text.Trim() + ".");
            Close();
        }

        private void dgvTovars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTovars_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Decimal decimaltest;

            if (
                 (e.ColumnIndex == 3 || e.ColumnIndex == 4) && (!decimal.TryParse(e.FormattedValue.ToString(), out decimaltest)
                || decimal.Parse(e.FormattedValue.ToString()) >= 1000000000))
            {
                MessageBox.Show("Неверно указано значение поля!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void dgvTovars_Paint(object sender, PaintEventArgs e)
        {
            ResizeSumElements();
        }

        private void initUL()
        {
            try
            {
                id_ul = int.Parse(proc.getNtypeorgJAllPrihod(id).Rows[0]["ntypeorg"].ToString());
            }
            catch { id_ul = 0; };

            DataTable dtUL = proc.getNtypeorgALL();

            cbUL.DataSource = dtUL;
            cbUL.DisplayMember = "Abbriviation";
            cbUL.ValueMember = "nTypeOrg";
            if (id_ul != 0)
                cbUL.SelectedValue = id_ul;
            else
                cbUL.SelectedIndex = -1;
        }
    }
}
