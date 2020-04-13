﻿using System;
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
    public partial class frmNaklO : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
    
        private int id, id_post, oldid_post, id_dep, id_ul;
        private string oldttn;
        private DataTable dtTovars;

        public frmNaklO(int inId, string inTtn, string inVnudok, DateTime inDatein, DateTime inDkor, string inEditor, int inid_post, string inPost, int inid_dep, string typeNack)
        {
            id = inId;
            id_post = inid_post;
            oldid_post = id_post;
            id_dep = inid_dep;

            InitializeComponent();

            tbTTN.Text = inTtn;
            tbDate.Text = inDatein.ToShortDateString();
            tbVnudok.Text = inVnudok;
            tbPost.Text = inPost;
            tbEditor.Text = inEditor;
            tbDateEdit.Text = inDkor.ToString();
            oldttn = inTtn;
            textBox1.Text = typeNack;
        }

        private void frmNakl_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть накладную");
            t.SetToolTip(btSave, "Сохранить накладную");
            t.SetToolTip(btGetPost, "Вызов справочника организаций");
            t.SetToolTip(tbSumNetto, "Итого по количеству");
            t.SetToolTip(tbSumR, "Итого по сумме");

            initUL();
            dgvTovar_Init();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DataRow[] tmp = dtTovars.Select("rcena<>oldrcena");
            if (tmp.Length > 0 || oldid_post != id_post || oldttn.Trim() != tbTTN.Text.Trim())
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

            dtTovars = proc.GetAdvige(id, 2);

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
            tvZcena.DataPropertyName = "cena";
            tvSumR.DataPropertyName = "rsum";

            tbSumNetto.Text = decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(netto)", "")), 3).ToString();
            tbSumR.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(rsum)", "")), 2));
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
            tbSumR.Width = tvSumR.Width;
            tbSumR.Left = dgvTovars.Left + tvEan.Width + tvName.Width + tvNds.Width + tvNetto.Width + tvRcena.Width + tvZcena.Width;
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
                    dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Math.Abs(decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace('.',',')));
                    dgvTovars.Rows[e.RowIndex].Cells[4].Value = dgvTovars.Rows[e.RowIndex].Cells[3].Value;
                    dgvTovars.Rows[e.RowIndex].Cells[6].Value = decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[2].Value.ToString()) * decimal.Parse(dgvTovars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    tbSumR.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(dtTovars.Compute("SUM(rsum)", "")), 2));
                    break;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            wait.Visible = true;
            Refresh();
            
            DataTable dtTmp = proc.GetPost(id_post, 0);
            if (dtTmp == null)
            {
                return;
            }
            string numCode = dtTmp.Rows[0][0].ToString().Trim();
            dtTmp.Dispose();

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
            proc.ChangeAllPrihod(id, tbTTN.Text, "", id_post, int.Parse(cbUL.SelectedValue.ToString()));
            if (TempValues.Error)
            {
                return;
            }

            int resultValidate = int.Parse(proc.ValidateNtypeorg(id_dep, int.Parse(cbUL.SelectedValue.ToString())).Rows[0]["result"].ToString());

            Logging.StartFirstLevel(26);
            Logging.Comment("Начало редактирование накладной отгрузки id= " + id.ToString().Trim() + " из j_allprihod");  
   
            foreach (DataRow dr in dtTovars.Rows)
            {
                if (dr["rcena"].ToString() != dr["oldrcena"].ToString()
                     || dr["nds"].ToString() != dr["oldnds"].ToString())
                {
                    Logging.Comment("Изменение цены продажи в накладной отгрузки покупателю");
                    proc.ChangeAdvige(int.Parse(dr["id"].ToString()), decimal.Parse(dr["cena"].ToString()), decimal.Parse(dr["rcena"].ToString()), null, int.Parse(dr["nds"].ToString()), 2);
                   
                    if (TempValues.Error)
                    {
                        return;
                    }

                
                    if (dr["rcena"].ToString() != dr["oldrcena"].ToString())
                    {
                        Logging.VariableChange("rcena", dr["rcena"].ToString(), dr["oldrcena"].ToString());
                    }
                }
                proc.editNtypeOrgTovar(int.Parse(dr["kodt"].ToString()), int.Parse(cbUL.SelectedValue.ToString()), resultValidate); 
            }
            DataTable dt = proc.GetCloseDate();
            proc.SetRests(DateTime.Parse(tbDate.Text), DateTime.Parse(dt.Rows[0]["dinv"].ToString()), id_dep);
            Logging.Comment("Конец редактирование накладной отгрузки id= " + id.ToString().Trim() + " из j_allprihod");  
            Logging.StopFirstLevel();

            if (oldid_post != id_post || oldttn.Trim() != tbTTN.Text.Trim())
            {
                Logging.StartFirstLevel(21);
                Logging.Comment("Начало редактирование накладной отгрузки id= " + id.ToString().Trim() + " из j_allprihod");  
                if (oldid_post != id_post)
                {
                    Logging.Comment("Изменение кода поставщика в накладной отгрузки покупателю");
                    Logging.VariableChange("id_post", id_post.ToString(), oldid_post.ToString());
                }
                if (oldttn.Trim() != tbTTN.Text.Trim())
                {
                    Logging.Comment("Изменение ttn в накладной отгрузки покупателю");
                    Logging.VariableChange("ttn", tbTTN.Text.Trim(), oldttn.Trim());
                }
                Logging.Comment("Конец редактирование накладной отгрузки id= " + id.ToString().Trim() + " из j_allprihod");  
                Logging.StopFirstLevel();
            } 
            #endregion
            
            wait.Visible = false;
            Refresh();

            MessageBox.Show("Данные в накладной сохранены.","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " сохранил изменения в накладной отгрузки " + tbTTN.Text.Trim() + ".");
            Close();
        }

        private void dgvTovars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTovars_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Decimal decimaltest;

            if (
                e.ColumnIndex == 3 && 
                (!decimal.TryParse(e.FormattedValue.ToString(), out decimaltest)
                 || decimal.Parse(e.FormattedValue.ToString()) >= 1000000000))
            {
                MessageBox.Show("Неверно указано значение поля!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void btGetPost_Click(object sender, EventArgs e)
        {
            frmSpravOrg spr = new frmSpravOrg(1);
            TempValues.id_post = 0;
            spr.ShowDialog();
            if (TempValues.id_post > 0)
            {
                id_post = TempValues.id_post;
                tbPost.Text = TempValues.name_post;
            }

        }

        private void tbPost_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(tbPost, tbPost.Text.Trim());
        }

        private void dgvTovars_Paint(object sender, PaintEventArgs e)
        {
            ResizeSumElements();
        }

        private void initUL()
        {
            try {
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
