using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Spisanie
{
    public partial class frmNaklsV : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
 
        private int callType;
        private DataTable dtDeps, dtNakls;
        private bool editable;

        public frmNaklsV(int Call)
        {
            callType = Call;
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNakls_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть форму");
            t.SetToolTip(dtpStartDate, "Введите дату периода");
            t.SetToolTip(dtpEndDate, "Введите дату периода");
            t.SetToolTip(btDelete, "Удалить накладную");
            t.SetToolTip(btEdit, "Редактировать накладную");

            DataTable dtTemp = proc.CheckDate();
            if (int.Parse(dtTemp.Rows[0][0].ToString()) == 0)
            {
                editable = false;
            }
            else
            {
                editable = true;
            }
            dtDeps = proc.GetVVODeps();

            cbDeps.DataSource = dtDeps;
            cbDeps.DisplayMember = "name";
            cbDeps.ValueMember = "id";
            if (dtDeps.Rows.Count > 0)
            {
                cbDeps.SelectedIndex = 0;
            }

            dtTemp = proc.GetCloseDate();
            dtpStartDate.Value = (DateTime)dtTemp.Rows[0][0];
            dtpEndDate.Value = (DateTime)dtTemp.Rows[0][0];
            dtTemp.Dispose();

            dgvNakls_init();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                dtpStartDate.Value = dtpEndDate.Value;
            }
            dgvNakls_init();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
            dgvNakls_init();
        }

        private void dgvNakls_init()
        {
            dtNakls = proc.GetRegdok(dtpStartDate.Value, dtpEndDate.Value, int.Parse(cbDeps.SelectedValue.ToString()), 3);
            dgvNakls.AutoGenerateColumns = false;
            dgvNakls.DataSource = null;
            dgvNakls.DataSource = dtNakls;

            nkdate.DataPropertyName = "dprihod";
            nkttn.DataPropertyName = "ttn";
            nksumnakl.DataPropertyName = "osumt";
            nkSummZ.DataPropertyName = "sum";
            nkvnudok.DataPropertyName = "vnudok";
            nkUL.DataPropertyName = "UL";

            if (dtNakls.Rows.Count > 0)
            {
                if (editable)
                {
                    btDelete.Enabled = true;
                    btEdit.Enabled = true;
                }
                else
                {
                    btDelete.Enabled = false;
                    btEdit.Enabled = false;
                }
                tbSumNak.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(osumt)", "").ToString()),2).ToString();
                tbSumTov.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(sum)", "").ToString()),2).ToString();
            }
            else
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;
                tbDateEdit.Text = "";
                tbEditor.Text = "";
                tbSumNak.Text = decimal.Parse("0").ToString();
                tbSumTov.Text = decimal.Parse("0").ToString();
            }
        }

        private void cbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void dgvNakls_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvNakls.CurrentRow != null)
            {
                tbEditor.Text = dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["oper1"].ToString().Trim();
                tbDateEdit.Text = dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dkor"].ToString();
            }
        }

        private void dgvNakls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
             
            DialogResult d = MessageBox.Show("Вы действительно хотите удалить накладную №" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()+"\n и связаный с ней акт списания?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.No)
            {
                return;
            }
            Logging.StartFirstLevel(34);
            Logging.Comment("Начало удаления накладной id= "+ dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()+
                                                               dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() +
                                                               " возврата от покупателя из j_allprihod");

            proc.DeleteNakls(int.Parse(cbDeps.SelectedValue.ToString()), DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString()));
            if (TempValues.Error)
            {
                TempValues.Error = false;
                return;
            }
            Logging.Comment("Накладная id= " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString() +
                                                         dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() +
                                                         " возврата от покупателя из j_allprihod удалена");
            Logging.StopFirstLevel();

            dtNakls = proc.ChangeCloseDate(int.Parse(cbDeps.SelectedValue.ToString()));
             if (dtNakls==null)
            {
                return;
            }
            MessageBox.Show("Накладная возврата от покупателя\nи связанный с ней акт списания\nудалены!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            dgvNakls_init();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
           // Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " начал редактировать накладную возврата " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + ".");
            frmNaklV editForm = new frmNaklV(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString())
                                            , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()
                                            , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["vnudok"].ToString().Trim()
                                            , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString())
                                            , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dkor"].ToString())
                                            , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["oper1"].ToString().Trim()
                                            , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_post"].ToString())
                                            , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString()));

            editForm.ShowDialog();
            dgvNakls_init();
        }

        private void cbDeps_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            dgvNakls_init();
        }

        private void dgvNakls_Paint(object sender, PaintEventArgs e)
        {
            
            tbSumTov.Left = dgvNakls.Left + nkdate.Width + nkttn.Width+nkUL.Width;
            tbSumTov.Width = nkSummZ.Width;
            tbSumNak.Left = tbSumTov.Left + tbSumTov.Width;
            tbSumNak.Width = nksumnakl.Width;
        }

        private void dgvNakls_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && btEdit.Enabled)
                btEdit_Click(sender, e);
        }
    }
}
