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
    public partial class frmNaklV : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private int id, id_post, id_dep, id_ul;
        private string oldttn;
        private DataTable dtTovars;

        public frmNaklV(int inId, string inTtn, string inVnudok, DateTime inDatein, DateTime inDkor, string inEditor, int inid_post, int inid_dep)
        {
            id = inId;
            id_post = inid_post;
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
            t.SetToolTip(btClose, "Закрыть накладную");
            t.SetToolTip(btSave, "Сохранить накладную");
            t.SetToolTip(tbSumNetto, "Итого по количеству");
            t.SetToolTip(tbSumR, "Итого по сумме");

            initUL();
            dgvTovar_Init();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DataRow[] tmp = dtTovars.Select("rcena<>oldrcena");
            if (tmp.Length > 0 || oldttn.Trim() != tbTTN.Text.Trim())
            {
                DialogResult d = MessageBox.Show("В накладную были внесены\n изменения. Сохранить накладную?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
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

            dtTovars = proc.GetAdvige(id, 3);

            dgvTovars.AutoGenerateColumns = false;
            dgvTovars.DataSource = dtTovars;

            tvEan.DataPropertyName = "ean";
            tvName.DataPropertyName = "name";
            tvNetto.DataPropertyName = "netto";
            tvRcena.DataPropertyName = "rcena";
            tvZcena.DataPropertyName = "cena";
            tvSumR.DataPropertyName = "rsum";

            CalculateSumms();
            
            wait.Visible = false;
            Refresh();
        }

        private void CalculateSumms()
        {
            tbSumNetto.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(netto)", "")), 3));
            tbSumR.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(rsum)", "")), 2));
        }

        private void ResizeSumElements()
        {
            tbSumNetto.Left = dgvTovars.Left + tvEan.Width + tvName.Width + tvZcena.Width;
            tbSumNetto.Width = tvNetto.Width;
            tbSumNetto.Top = dgvTovars.Top + dgvTovars.Height;
            label7.Left = tbSumNetto.Left - label7.Width;
            label7.Top = tbSumNetto.Top+3;
            tbSumR.Width = tvSumR.Width;
            tbSumR.Left = dgvTovars.Left + tvEan.Width + tvName.Width  + tvNetto.Width + tvRcena.Width+tvZcena.Width;
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
                case 4:
                    dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Math.Abs(decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace('.',',')));
                    dgvTovars.Rows[e.RowIndex].Cells[3].Value = dgvTovars.Rows[e.RowIndex].Cells[4].Value;
                    dgvTovars.Rows[e.RowIndex].Cells[5].Value = decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[2].Value.ToString()) * decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    CalculateSumms();
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
            DataRow[] tmp = dtTovars.Select("rcena=0");
            if (tmp.Length > 0)
            {
                MessageBox.Show("Цена не должна быть = 0!", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wait.Visible = false;
                Refresh();
                return;
            }
            #endregion

            #region Запись изменений
            proc.ChangeAllPrihod(id, tbTTN.Text.Trim(), "", id_post, int.Parse(cbUL.SelectedValue.ToString()));
            int resultValidate = int.Parse(proc.ValidateNtypeorg(id_dep, int.Parse(cbUL.SelectedValue.ToString())).Rows[0]["result"].ToString());

            if (TempValues.Error)
            {
                return;
            }

            Logging.StartFirstLevel(29);
            Logging.Comment("Редактирование накладной id = " + id.ToString().Trim() + " возврата от покупателя из j_allprihod");        
            foreach (DataRow dr in dtTovars.Rows)
            {
                if (dr["rcena"].ToString() != dr["oldrcena"].ToString()
                     || dr["nds"].ToString() != dr["oldnds"].ToString())
                {
                    proc.ChangeAdvige(int.Parse(dr["id"].ToString()), decimal.Parse(dr["cena"].ToString()), decimal.Parse(dr["rcena"].ToString()), null, int.Parse(dr["nds"].ToString()), 3);
                    if (TempValues.Error)
                    {
                        return;
                    }
                
                    if (dr["rcena"].ToString() != dr["oldrcena"].ToString())
                    {
                        Logging.Comment("Изменение цены продажи в накладной возврата от покупателя");
                        Logging.VariableChange("rcena", dr["rcena"].ToString(), dr["oldrcena"].ToString());
                    }
                }
                proc.editNtypeOrgTovar(int.Parse(dr["kodt"].ToString()), int.Parse(cbUL.SelectedValue.ToString()), resultValidate); 
            }
            DataTable dt = proc.GetCloseDate();
            proc.SetRests(DateTime.Parse(tbDate.Text), DateTime.Parse(dt.Rows[0]["dinv"].ToString()), id_dep);
            Logging.Comment("Конец редактирования накладной id = " + id.ToString().Trim() + " возврата от покупателя из j_allprihod");  
            Logging.StopFirstLevel();

            if (oldttn.Trim() != tbTTN.Text.Trim())
            {
                Logging.StartFirstLevel(24);
                Logging.Comment("Редактирование накладной id = " + id.ToString().Trim() + " возврата от покупателя из j_allprihod"); 
                if (oldttn.Trim() != tbTTN.Text.Trim())
                {
                    Logging.Comment("Изменение ttn в накладной возврата от покупателя");
                    Logging.VariableChange("ttn", tbTTN.Text.Trim(), oldttn.Trim());
                }
                Logging.Comment("Конец редактирования накладной id = " + id.ToString().Trim() + " возврата от покупателя из j_allprihod"); 
                Logging.StopFirstLevel();
            } 
            #endregion
            
            wait.Visible = false;
            Refresh();

            MessageBox.Show("Данные в накладной сохранены.","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " сохранил изменения в накладной возврата " + tbTTN.Text.Trim() + ".");
            Close();
        }

        private void dgvTovars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTovars_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Decimal decimaltest;
            if (
                e.ColumnIndex == 4 && 
                (!decimal.TryParse(e.FormattedValue.ToString(), out decimaltest)
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
