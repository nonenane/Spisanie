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
    public partial class frmNaklsP : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
 
        private int callType;
        private DataTable dtDeps, dtNakls, dtTovars;
        private bool editable;

        public frmNaklsP(int Call)
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
            t.SetToolTip(btPrint, "Печать документов");
            t.SetToolTip(btDelete, "Удалить накладную");
            t.SetToolTip(btEdit, "Редактировать накладную");
            t.SetToolTip(cbDeps, "Выберите отдел");
            t.SetToolTip(dtpStartDate, "Введите дату периода");
            t.SetToolTip(dtpEndDate, "Введите дату периода");

            DataTable dtTemp = proc.CheckDate();
            if (int.Parse(dtTemp.Rows[0][0].ToString()) == 0)
            {
                editable = false;
            }
            else
            {
                editable = true;
            }
            
            //editable = true;
            
            dtTemp.Dispose();

            dtDeps = proc.GetDeps();

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
            wait.Visible = true;
            Refresh();
            dgvNakls.DataSource = null;
            dtNakls = proc.GetRegdok(dtpStartDate.Value, dtpEndDate.Value, int.Parse(cbDeps.SelectedValue.ToString()),1);
            dgvNakls.AutoGenerateColumns = false;
            dgvNakls.DataSource = dtNakls;

            nkdate.DataPropertyName = "dprihod";
            nkttn.DataPropertyName = "ttn";
            nkdep.DataPropertyName = "dep";
            nkpost.DataPropertyName = "post";
            nksumnakl.DataPropertyName = "sum";
            nksumtov.DataPropertyName = "osumt";
            nkvnudok.DataPropertyName = "vnudok";
            nkSF.DataPropertyName = "sf";
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
                btPrint.Enabled = true;
                tbSumNak.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(sum)", "").ToString()),2).ToString();
                tbSumTov.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(osumt)", "").ToString()),2).ToString();
            }
            else
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;
                btPrint.Enabled = false;
                tbDateEdit.Text = "";
                tbEditor.Text = "";
                tbSumNak.Text = decimal.Parse("0").ToString();
                tbSumTov.Text = decimal.Parse("0").ToString();
            }
            wait.Visible = false;
            Refresh();
        }

        private void cbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvNakls_init();
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
            DialogResult d = MessageBox.Show("Вы действительно хотите удалить накладную №" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()+"\n и связаные с ней накладную отгрузки и акт переоценки?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.No)
            {
                return;
            }
            wait.Visible = true;
            Refresh();

            dtTovars = proc.GetAdvige(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()), 1);
            if (dtTovars==null)
            {
                return;
            }
            if (dtTovars.Rows.Count == 0)
            {
                MessageBox.Show("Накладная не содержит записей о товаре!");
                return;
            }
            else
            {
                Logging.StartFirstLevel(30);
                Logging.Comment("Начало удаления накладной id= " +dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()+", ttn ="+ dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + " прихода поставщика из j_allprihod");


                proc.DeleteNakls(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString()), DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString()));
                if (TempValues.Error)
                {
                    TempValues.Error = false;
                    return;
                }
                //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " удалил накладную прихода " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + ".");

                
                Logging.Comment("Накладная id= " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString() + ", ttn =" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + " прихода поставщика из j_allprihod удалена");

                Logging.StopFirstLevel();

                dtNakls = proc.ChangeCloseDate(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString()));
                if (dtNakls==null)
                {
                    return;
                }
                wait.Visible = false;
                Refresh();

                MessageBox.Show("Накладная прихода и связанные с ней\n накладная отгрузки и акт переоценки\n удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvNakls_init();
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {

            dtTovars = proc.GetAdvige(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()), 1);

            if (dtTovars==null)
            {
                return;
            }
            if (dtTovars.Rows.Count == 0)
            {
                MessageBox.Show("Накладная не содержит записей о товаре!");
                return;
            }
            else
            {          

                frmNaklP editForm = new frmNaklP(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString())
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["sf"].ToString().Trim()
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["vnudok"].ToString().Trim()
                                                , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString())
                                                , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dkor"].ToString())
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["oper1"].ToString().Trim()
                                                , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_post"].ToString())
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["post"].ToString().Trim()
                                                , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString())){ Text=Text+" "+dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dep"].ToString()};

                editForm.ShowDialog();
                dgvNakls_init();
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            frmPrint printForm = new frmPrint(1, int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()));
            printForm.ShowDialog();
        }

        private void dgvNakls_Paint(object sender, PaintEventArgs e)
        {
            tbSumNak.Left = dgvNakls.Left + nkdate.Width + nkttn.Width + nkSF.Width + nkdep.Width + nkpost.Width+nkUL.Width;
            tbSumNak.Width = nksumnakl.Width;
            tbSumTov.Left = tbSumNak.Left + tbSumNak.Width;
            tbSumTov.Width = nksumtov.Width;
        }

        private void dgvNakls_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && btEdit.Enabled)
                btEdit_Click(sender, e);
        }
    }
}
