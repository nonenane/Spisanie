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

            dtTovars.AcceptChanges();
            isEditCell = false;
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
            Logging.Comment("Заголовок накладной");
            Logging.VariableChange("ТТН в накладной прихода", tbTTN.Text.Trim(), oldttn.Trim());
            Logging.VariableChange("Код ЮЛ", cbUL.SelectedValue.ToString(), oldUl_int.ToString());
            Logging.VariableChange("Наименование ЮЛ", cbUL.Text.ToString(), oldUl_string.ToString());

            bool isEditTovar = false;

            foreach (DataRow dr in dtTovars.Rows)
            {
                if (dr["rcena"].ToString() != dr["oldrcena"].ToString()
                    || dr["bcena"].ToString() != dr["oldbcena"].ToString()
                    || dr["nds"].ToString() != dr["oldnds"].ToString())
                {
                    if(!isEditTovar)
                    {
                        Logging.Comment("Произведено редактирование товара");
                        isEditTovar = true;
                    }

                    Logging.Comment($"EAN: {dr["ean"].ToString()}");
                    Logging.Comment($"Товар ID:{dr["kodt"].ToString()}; Наименование:{dr["name"].ToString()}");

                    Logging.VariableChange($"НДС ID", dr["nds"].ToString(), dr["oldnds"].ToString());
                    Logging.VariableChange("Старая цена", dr["bcena"].ToString(), dr["oldbcena"].ToString());
                    Logging.VariableChange("Цены розничная ", dr["rcena"].ToString(), dr["oldrcena"].ToString());

                    Logging.Comment("Изменение цен в акте переоценки");
                    proc.ChangeAdvige(int.Parse(dr["id"].ToString()), null, decimal.Parse(dr["rcena"].ToString()), decimal.Parse(dr["bcena"].ToString()), int.Parse(dr["nds"].ToString()), 5);
                    if (TempValues.Error)
                    {
                        Logging.Comment("Ошибка редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
                        Logging.StopFirstLevel();
                        return;
                    }                
                }
                //proc.editNtypeOrgTovar(int.Parse(dr["kodt"].ToString()), int.Parse(cbUL.SelectedValue.ToString()), resultValidate); 
            }
            DataTable dt = proc.GetCloseDate();
            proc.SetRests(DateTime.Parse(tbDate.Text), DateTime.Parse(dt.Rows[0]["dinv"].ToString()), id_dep);
            Logging.Comment("Конец редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
            Logging.StopFirstLevel();

            //if (oldttn.Trim() != tbTTN.Text.Trim())
            //{
            //    Logging.StartFirstLevel(23);
            //    Logging.Comment("Начало редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
            //    Logging.Comment("Изменение ttn акта переоценки");
            //    Logging.VariableChange("ttn", tbTTN.Text.Trim(), oldttn.Trim());
            //    Logging.Comment("Конец редактирование акта переоценки id= " + id.ToString().Trim() + " из j_allprihod");
            //    Logging.StopFirstLevel();
            //}
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
        bool isEditCell = false;
        private void dgvTovars_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isEditCell = true;
        }

        private void dgvTovars_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtTovars != null && dtTovars.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                dgvTovars.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvTovars.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvTovars.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                decimal minPrice, maxPrice;
                minPrice = (decimal)dtTovars.DefaultView[e.RowIndex]["minPrice"];
                maxPrice = (decimal)dtTovars.DefaultView[e.RowIndex]["maxPrice"];
                decimal _rcena = (decimal)dtTovars.DefaultView[e.RowIndex]["rcena"];
                decimal _cena = (decimal)dtTovars.DefaultView[e.RowIndex]["bcena"];

                if (_rcena > maxPrice || _rcena < minPrice)
                {
                    dgvTovars.Rows[e.RowIndex].Cells[tvRcena.Index].Style.BackColor =
                    dgvTovars.Rows[e.RowIndex].Cells[tvRcena.Index].Style.SelectionBackColor = panel1.BackColor;
                }

                if (_cena > maxPrice || _cena < minPrice)
                {
                    dgvTovars.Rows[e.RowIndex].Cells[tvBcena.Index].Style.BackColor =
                     dgvTovars.Rows[e.RowIndex].Cells[tvBcena.Index].Style.SelectionBackColor = panel1.BackColor;
                }
            }
        }

        private void dgvTovars_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvTovars_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress -= new KeyPressEventHandler(tbDecimal_KeyPress);
                tb.KeyPress += new KeyPressEventHandler(tbDecimal_KeyPress);
            }
        }

        private void tbDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
        }

        private void dgvTovars_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!isEditCell) return;

            Decimal decimaltest=0;

            if (
                 (e.ColumnIndex == 3 || e.ColumnIndex == 4) && (!decimal.TryParse(e.FormattedValue.ToString(), out decimaltest)
                || decimal.Parse(e.FormattedValue.ToString()) >= 1000000000))
            {
                MessageBox.Show("Неверно указано значение поля!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

            if (e.ColumnIndex == tvBcena.Index || e.ColumnIndex == tvRcena.Index)
            {
                //string _ean = (string)dtTovars.DefaultView[e.RowIndex]["ean"];
                //int _id_tovar = (int)dtTovars.DefaultView[e.RowIndex]["kodt"];
                //DateTime _date = DateTime.Parse(tbDate.Text);
                //if (!proc.getPriceTovarWithPrcn(_id_tovar, _date, decimaltest)) {e.Cancel = true; return;}  

                decimal minPrice, maxPrice, prc;
                minPrice = (decimal)dtTovars.DefaultView[e.RowIndex]["minPrice"];
                maxPrice = (decimal)dtTovars.DefaultView[e.RowIndex]["maxPrice"];
                prc = (decimal)dtTovars.DefaultView[e.RowIndex]["prnc"];
                if (decimaltest > maxPrice || decimaltest < minPrice)
                {
                    MessageBox.Show(TempValues.centralText($"Введённая цена выходит за\nдиапазон цены, определяемой\nпроцентом наценки {prc.ToString("0.00")}%\n"), "Проверка цены", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //e.Cancel = true;
                    return;
                }
            }
        }

        private void dgvTovars_Paint(object sender, PaintEventArgs e)
        {
            ResizeSumElements();
        }

        private int oldUl_int;
        private string oldUl_string;
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
            {
                cbUL.SelectedValue = id_ul;
                oldUl_string = cbUL.Text;
            }
            else
                cbUL.SelectedIndex = -1;

            oldUl_int = id_ul;
        }
    }
}
