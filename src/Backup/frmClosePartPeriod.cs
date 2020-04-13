using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;

namespace Spisanie
{
    public partial class frmClosePartPeriod : Form
    {
        DataTable dtDep;
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public frmClosePartPeriod()
        {
            InitializeComponent();
        }

        private void frmClosePartPeriod_Load(object sender, EventArgs e)
        {
            DatesFill();
            cbDepartmentFill();
        }

        private void DatesFill()
        {
            DataTable dtDates = new DataTable();
            dtDates = proc.GetDatesForClosingPartPeriod();

            if ((dtDates != null) && (dtDates.Rows.Count > 0))
            {
                dtpDateStart.Value = DateTime.Parse(dtDates.Rows[0]["dateStart"].ToString());
                dtpDateEnd.Value = DateTime.Parse(dtDates.Rows[0]["dateEnd"].ToString());
            }       

        }

        private void cbDepartmentFill()
        {
            dtDep = new DataTable();
            //получение списка отделов, по которым не закрыта часть периода
            dtDep = proc.NotClosedDepsList(dtpDateStart.Value, dtpDateEnd.Value);

            if (dtDep != null && dtDep.Rows.Count > 0)
            {
                DataRow all = dtDep.NewRow();
                all["name"] = "Все отделы";
                all["id"] = 0;
                dtDep.Rows.InsertAt(all, 0);

                cbDepartment.DataSource = dtDep;
                cbDepartment.DisplayMember = "name";
                cbDepartment.ValueMember = "id";
                cbDepartment.SelectedValue = 0;

                if (proc.CountClosed(dtpDateStart.Value, dtpDateEnd.Value) > 0)
                {
                    MessageBox.Show("Часть периода уже закрыта.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }
            }
            else
            {
                MessageBox.Show("Часть периода по всем отделам уже закрыта.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();               
            }            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClosePeriod_Click(object sender, EventArgs e)
        {
            #region Изменение StatusBuh
            string id_dep = "";
            if (cbDepartment.SelectedIndex == 0)
            {
                for (int i=0;dtDep.Rows.Count>i;i++)
                {
                    if (dtDep.Rows[i]["id"].ToString() != "0")
                    {
                        id_dep += dtDep.Rows[i]["id"].ToString() + ",";
                    }
                }
            }
            else
            {
                id_dep += cbDepartment.SelectedValue.ToString() + ",";
            }
                        
            proc.SetStatusBuh(dtpDateStart.Value.Date, dtpDateEnd.Value.Date, id_dep);

            #endregion

            #region добавление записей в j_BlockInvoices и j_BlockPeriodInvoices

            int id_Dep;

            DateTime date = dtpDateStart.Value.Date;

            if (cbDepartment.SelectedIndex == 0)
            {
                for (int i = 0; dtDep.Rows.Count > i; i++)
                {
                    if (dtDep.Rows[i]["id"].ToString() != "0")
                    {                        
                        id_Dep = int.Parse(dtDep.Rows[i]["id"].ToString());
                        proc.AddBlockInvoices(dtpDateStart.Value.Date, dtpDateEnd.Value.Date, id_Dep);
                    }
                }
            }
            else
            {
                id_Dep = int.Parse(cbDepartment.SelectedValue.ToString());
                proc.AddBlockInvoices(dtpDateStart.Value.Date, dtpDateEnd.Value.Date, id_Dep);
            }
            

            #endregion

            #region Перерасчет остатков по существующему алгоритму

            if (cbDepartment.SelectedIndex == 0)
            {
                for (int i = 0; dtDep.Rows.Count > i; i++)
                {
                    if (dtDep.Rows[i]["id"].ToString() != "0")
                    {                        
                        id_Dep = int.Parse(dtDep.Rows[i]["id"].ToString());
                        proc.SetRests(dtpDateStart.Value.Date, dtpDateEnd.Value.Date, id_Dep);
                    }
                }
            }
            else
            {
                id_Dep = int.Parse(cbDepartment.SelectedValue.ToString());
                proc.SetRests(dtpDateStart.Value.Date, dtpDateEnd.Value.Date, id_Dep);
            }

            

            #endregion

            this.Close();
        }

    }
}
