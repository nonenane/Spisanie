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
    public partial class frmNaklsSp : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
 
        private int callType;
        private DataTable dtDeps, dtNakls, dtTovars;
        private bool editable;
     

        public frmNaklsSp(int Call)
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
            t.SetToolTip(dtpStartDate, "Введите дату периода");
            t.SetToolTip(dtpEndDate, "Введите дату периода");
            t.SetToolTip(btDelete, "Удалить акт");
            t.SetToolTip(btEdit, "Редактировать акт");

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
            dtNakls = proc.GetRegdok(dtpStartDate.Value, dtpEndDate.Value, int.Parse(cbDeps.SelectedValue.ToString()), 4);
            dgvNakls.AutoGenerateColumns = false;
            dgvNakls.DataSource = null;
            dgvNakls.DataSource = dtNakls;

            nkdate.DataPropertyName = "dprihod";
            nkttn.DataPropertyName = "ttn";
            nksumnakl.DataPropertyName = "osumt";
            nkSumZ.DataPropertyName = "sum";
            nkvnudok.DataPropertyName = "vnudok";
            nkUL.DataPropertyName = "UL";
            typeNack.DataPropertyName = "credit";

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
                tbSumNak.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(osumt)", "").ToString()),2).ToString();
                tbSumTov.Text = decimal.Round(decimal.Parse(dtNakls.Compute("sum(sum)", "").ToString()),2).ToString();
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

        private void btDelete_Click(object sender, EventArgs e)
        {
            
            DialogResult d = MessageBox.Show("Вы действительно хотите удалить акт №" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()+"\n и связаную с ним накладную возврата от покупателя?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.No)
            {
                return;
            }
            dtTovars = proc.GetAdvige(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()),4);
            if (dtTovars == null)
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

                Logging.StartFirstLevel(33);
                //Logging.Comment("Начало удаления акта списания id=" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()+", ttn= " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + " из j_allprihod");
                Logging.Comment("Начало удаления накладной id= " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString());
                Logging.Comment($"Отдел накладной ID:{dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString()}; Наименование:{dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dep"].ToString()}");
                Logging.Comment($"Дата накладной: {((DateTime)dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"]).ToShortDateString()}");
                Logging.Comment($"ТТН: {dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString()}");
                Logging.Comment($"№ внут.док.: {dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["vnudok"].ToString()}");
                Logging.Comment($"ЮЛ: {dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["UL"].ToString()}");
                //              
                Logging.Comment($"Тип накладной ID:{dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_operand"].ToString()}; Наименование:{dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["nameOperand"].ToString()}");

                proc.DeleteNakls(int.Parse(cbDeps.SelectedValue.ToString()), DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString()));
                if (TempValues.Error)
                {
                    Logging.StopFirstLevel();
                    TempValues.Error = false;
                    return;
                }
               
                //Logging.Comment("Акт списания id=" + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString() + ", ttn= " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim() + " из j_allprihod удален");

                Logging.StopFirstLevel();

                dtNakls = proc.ChangeCloseDate(int.Parse(cbDeps.SelectedValue.ToString()));
                if (dtNakls == null)
                {
                    return;
                }
                MessageBox.Show("Акт списания и связанная с ним накладная\nвозврата от покупателя удалены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvNakls_init();
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
                     
            dtTovars = proc.GetAdvige(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()),4);
            if (dtTovars==null)
            {
                TempValues.Error = false;
                return;
            }
            if (dtTovars.Rows.Count == 0)
            {
                MessageBox.Show("Накладная не содержит записей о товаре!");
                return;
            }
            else
            {
                frmNakl_Sp editForm = new frmNakl_Sp(int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString())
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["ttn"].ToString().Trim()
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["vnudok"].ToString().Trim()
                                                , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dprihod"].ToString())
                                                , DateTime.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dkor"].ToString())
                                                , dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["oper1"].ToString().Trim()
                                                , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_post"].ToString())
                                                , int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id_dep"].ToString()),
                                                Convert.ToString(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["credit"].ToString()))
                { Text = Text + " " + dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["dep"].ToString() };

                editForm.ShowDialog();
                dgvNakls_init();
            }
        
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            frmPrint printForm = new frmPrint(4, int.Parse(dtNakls.DefaultView[dgvNakls.CurrentRow.Index]["id"].ToString()));
            printForm.ShowDialog();
        }

        private void cbDeps_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            dgvNakls_init();
        }

        private void dgvNakls_Paint(object sender, PaintEventArgs e)
        {
            tbSumTov.Left = dgvNakls.Left + nkdate.Width + nkttn.Width+nkUL.Width;
            tbSumTov.Width = nkSumZ.Width;
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
