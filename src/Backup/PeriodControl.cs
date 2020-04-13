using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;

namespace Spisanie
{
    public partial class PeriodControl : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private DataTable dtDeps;
        private DateTime closeDate;
        private DataTable dtCens;
        private DataTable dtPrihZ;
        private DataTable dtListBlock;
        private DataTable dtTmp = new DataTable();
        private DataTable dtTmp_1 = new DataTable();
        private bool click = false;
        private TimeBlock tmb;
        private string ttt;
        private DateTime DateStartMonth,DateEndLastMonth,DateEndMonth;
        private bool finish_Load = false;
        private int selectindex;
        private int selectvalue;
        public PeriodControl()
        {
            InitializeComponent();
            dataGridView1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (click)
            {
                click = false;
                dataGridView1.Hide();
                this.Height = 220;
                button2.BackgroundImage = new Bitmap(Properties.Resources.application_put);
                toolTip1.SetToolTip(button2, "Развернуть");
            }
            else
            {
                click = true;
                dataGridView1.Show();
                this.Height = 400;
                button2.BackgroundImage = new Bitmap(Properties.Resources.application_get);
                toolTip1.SetToolTip(button2, "Свернуть");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {

                this.Enabled = false;                
                progressBar1.Show();
                backgroundWorker1.RunWorkerAsync();
            }
            /*
            #region Блокировка
            if (radioButton1.Checked)
            {
                wait.Visible = true;
               // List<DateTime> allDays = new List<DateTime>();
                DateTime start = dateStart.Value;
                DateTime maxDate = dateEnd.Value;
                DataTable dtTmp;
                if (cbDeps.SelectedIndex == 0) //Выборка отделов при все отделы
                {
                    DataRow selectedDataRow;

                    for (int i = 1; i < dtDeps.Rows.Count; i++)
                    {
                        start = dateStart.Value;
                       // cbDeps.SelectedIndex = i;
                       // selectedDataRow = ((DataRowView) cbDeps.SelectedItem).Row;
                       // int Id = Convert.ToInt32(selectedDataRow["Id"]);
                        int Id = Convert.ToInt32(dtDeps.Rows[i]["Id"]);
                        while (maxDate.Date.AddDays(1) >= start) 
                        {
                           // allDays.Add(start.Date);
                           // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                            //запрос по отделу и дате
                            dtTmp = proc.FindDateBlock(start.Date, Id);                            
                            if(dtTmp.Rows.Count > 0)
                            {
                              Console.WriteLine(dtTmp.Rows[0]["id"].ToString()+" Обновляем для ID "+Id+" "+start.ToShortDateString());
                              proc.UpdateDateBlock(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                              proc.UpdatePeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]),1);                              
                              proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);                                
                            }
                          else
                            {
                              Console.WriteLine("Создаем для ID " + Id + " " + start.ToShortDateString());
                              proc.InsertBlockInvoicess(Id,start.Date);
                              dtTmp = proc.FindDateBlock(start.Date, Id);
                              proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);                            
                            }
                            dtTmp.Clear();
                            start = start.AddDays(1);
                        }
                        proc.SetRests(dateStart.Value, DateTime.Now, Id);
                        //proc.SetRests(dateStart.Value,dateEnd.Value,Id);
                    }
                    cbDeps.SelectedIndex = 0;                    
                }
                else
                {
                        Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                        while (maxDate.Date.AddDays(1) >= start) 
                        {
                            // allDays.Add(start.Date);
                            // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                            //запрос по отделу и дате
                            dtTmp = proc.FindDateBlock(start.Date, Convert.ToInt32(cbDeps.SelectedValue.ToString()));
                            if (dtTmp.Rows.Count > 0)
                            {
                                Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " + Convert.ToInt32(cbDeps.SelectedValue.ToString()) + " " + start.ToShortDateString());
                                proc.UpdateDateBlock(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                proc.UpdatePeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]),1);
                                proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                            }
                            else
                            {
                                Console.WriteLine("Создаем для ID " + Convert.ToInt32(cbDeps.SelectedValue.ToString()) + " " + start.ToShortDateString());
                                proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                dtTmp = proc.FindDateBlock(start.Date, Convert.ToInt32(cbDeps.SelectedValue.ToString()));
                                proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                            }
                            dtTmp.Clear();
                            start = start.AddDays(1);
                        }
                        proc.SetRests(dateStart.Value, DateTime.Now, Convert.ToInt32(cbDeps.SelectedValue.ToString()));
                    //блокируем                   
                }
                
                load_table();
                wait.Visible = false;
                MessageBox.Show("Период заблокирован!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            #region Разблокировка
            if (radioButton2.Checked)
            {
                wait.Visible = true;
                tmb = new TimeBlock();
                tmb.ShowDialog();
                if (tmb.sendTime != "")
                {
                    DateTime start = dateStart.Value;
                    DateTime maxDate = dateEnd.Value;
                    DataTable dtTmp;

                    if (cbDeps.SelectedIndex == 0) //Выборка отделов при все отделы
                    {
                        DataRow selectedDataRow;
                        for (int i = 1; i < dtDeps.Rows.Count; i++)
                        {
                            start = dateStart.Value;
                            //cbDeps.SelectedIndex = i;
                            //selectedDataRow = ((DataRowView)cbDeps.SelectedItem).Row;
                            //int Id = Convert.ToInt32(selectedDataRow["Id"]);
                            int Id = Convert.ToInt32(dtDeps.Rows[i]["Id"]);
                            while (maxDate.Date.AddDays(1) >= start)
                            {
                                // allDays.Add(start.Date);
                                // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                                //запрос по отделу и дате
                                dtTmp = new DataTable();
                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                if (dtTmp != null)
                                {
                                    if (dtTmp.Rows.Count > 0)
                                    {
                                        int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                        Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " + Id +
                                                          " " + start.ToShortDateString());
                                        proc.UpdateDateBlock(id, 2);
                                        proc.InsertBlockPeriodInvoices(id, 2);
                                        dtTmp = proc.selectPeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]));
                                        if (dtTmp.Rows.Count > 0)
                                        {
                                            proc.UpdatePeriodsToBlockInvoices(id, 0,
                                                tmb.sendTime);
                                        }
                                        else
                                        {
                                            proc.insertPeriodsToBlockInvoices(id, tmb.sendTime);
                                        }
                                    }
                                    else
                                    {
                                        proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                        dtTmp = proc.FindDateBlock(start.Date, Id);
                                        int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                        proc.UpdateDateBlock(id, 2);
                                    }
                                    dtTmp.Clear();
                                }
                                else
                                {
                                    proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                    proc.UpdateDateBlock(id, 2);
                                }
                              
                                start = start.AddDays(1);
                            }
                        }
                        cbDeps.SelectedIndex = 0;
                    }
                    else
                    {
                        Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                        int id;
                        DataRow selectedDataRow = ((DataRowView)cbDeps.SelectedItem).Row;
                        int Id = Convert.ToInt32(selectedDataRow["Id"]);
                        while (maxDate.Date.AddDays(1) >= start)
                        {
                            // allDays.Add(start.Date);
                            // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                            //запрос по отделу и дате
                            dtTmp = new DataTable();
                           
                            dtTmp = proc.FindDateBlock(start.Date, Id);                            
                            if (dtTmp != null)
                            {
                                if (dtTmp.Rows.Count > 0)
                                {
                                    id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                    Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " +
                                                      Convert.ToInt32(cbDeps.SelectedValue.ToString()) + " " +
                                                      start.ToShortDateString());
                                    proc.UpdateDateBlock(id, 2);
                                    proc.InsertBlockPeriodInvoices(id, 2);
                                    dtTmp = proc.selectPeriodsToBlockInvoices(id);
                                    if (dtTmp.Rows.Count > 0)
                                    {
                                        proc.UpdatePeriodsToBlockInvoices(id, 0,
                                            tmb.sendTime);
                                    }
                                    else
                                    {
                                        proc.insertPeriodsToBlockInvoices(id, tmb.sendTime);

                                    }
                                }
                                else
                                {
                                    proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                    proc.UpdateDateBlock(id, 2);
                                }
                                dtTmp.Clear();
                            }
                            else
                            {
                                proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                proc.UpdateDateBlock(id, 2);                                
                            }
                            start = start.AddDays(1);
                        }
                    }
                    load_table();
                    wait.Visible = false;
                    MessageBox.Show("Период разблокирован!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //функция разблокировки   
            }
            #endregion
            */
            installDate();
        }

        private void PeriodControl_Load(object sender, EventArgs e)
        {
            progressBar1.Hide();
            try
            {

            
            DataTable dtTemp = proc.GetCloseDate();
            //closeDate -  DateLastInv
            //dateStart - DateInvSpis 
            closeDate = (DateTime)dtTemp.Rows[0][0];
            //closeDate = (DateTime)dtTemp.Rows[0]["old_dinv"];
            dtTemp.Dispose();
           // dateStart.MinDate = closeDate;
            dateEnd.MaxDate = DateTime.Now;

            dateStart.Value = DateTime.Now.AddDays(-1);
            dateEnd.Value = DateTime.Now.Date;
            
            dtDeps = proc.GetDeps();

            cbDeps.DataSource = dtDeps;
            cbDeps.DisplayMember = "name";
            cbDeps.ValueMember = "id";
            if (dtDeps.Rows.Count > 0)
            {
                cbDeps.SelectedIndex = 0;
                ttt = cbDeps.SelectedValue.ToString();
            }
            //finish_Load = true;
            load_table();
            toolTip1.SetToolTip(button1, "Закрыть форму");
            toolTip1.SetToolTip(button2, "Развернуть");
            toolTip1.SetToolTip(button3, "Сохранить");

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(bw_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            }
            catch (NullReferenceException)
            {
            }

        }

        private void cbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbDeps.SelectedValue.ToString() != "0")
            {
                dataGridView1.Columns[2].Visible = false;
            }
            else
            {
                dataGridView1.Columns[2].Visible = true;
            }
            ttt = cbDeps.SelectedValue.ToString();
            if (finish_Load)
            load_table();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
              //  MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
               
            }
        }

        private void load_table()
        {
            button3.Invoke((Action) (() => button3.Enabled = false));         
            //////
            DateTime start = dateStart.Value;
            DateTime maxDate = dateEnd.Value;
            DataTable dtTmp;
            if (!finish_Load)
            {
                cbDeps.Invoke((MethodInvoker)delegate
                {
                    selectindex = cbDeps.SelectedIndex;
                });
                if (selectindex == 0) //Выборка отделов при все отделы
                {
                    //DataRow selectedDataRow;
                    for (int i = 1; i < dtDeps.Rows.Count; i++)
                    {
                        start = dateStart.Value;
                        //cbDeps.SelectedIndex = i;
                        //selectedDataRow = ((DataRowView)cbDeps.SelectedItem).Row;
                        //int Id = Convert.ToInt32(selectedDataRow["Id"]);
                        int Id = Convert.ToInt32(dtDeps.Rows[i]["Id"]);
                        while (maxDate.Date.AddDays(1) >= start)
                        {
                            //запрос по отделу и дате
                            dtTmp = new DataTable();
                            dtTmp = proc.Get_List_Block(start.Date, start.Date, Id.ToString());
                            if (dtTmp != null)
                            {
                                if (dtTmp.Rows.Count == 0)
                                {
                                    proc.InsertBlockInvoicess(Id,
                                        start.Date);
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                    proc.UpdateDateBlock(id, 2);
                                }
                                dtTmp.Clear();
                            }
                            else
                            {
                                proc.InsertBlockInvoicess(Id, start.Date);
                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                proc.UpdateDateBlock(id, 2);
                            }
                            start = start.AddDays(1);
                        }
                    }
                    cbDeps.Invoke((MethodInvoker) delegate
                    {
                        cbDeps.SelectedIndex = 0;
                    });
                }
                else
                {
                    Console.WriteLine(start.Date.ToShortDateString() + " " + selectindex);
                    int id;
                    //DataRow selectedDataRow = ((DataRowView) cbDeps.SelectedItem).Row;
                   // int Id = Convert.ToInt32(selectedDataRow["Id"]);
                    int Id = selectindex;
                    while (maxDate.Date.AddDays(1) >= start)
                    {
                        //запрос по отделу и дате
                        dtTmp = new DataTable();
                        dtTmp = proc.FindDateBlock(start.Date, Id);
                        if (dtTmp != null)
                        {
                            if (dtTmp.Rows.Count == 0)
                            {
                                proc.InsertBlockInvoicess(selectindex, start.Date);
                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                proc.UpdateDateBlock(id, 2);
                            }
                            dtTmp.Clear();
                        }
                        else
                        {
                            proc.InsertBlockInvoicess(selectindex, start.Date);
                            dtTmp = proc.FindDateBlock(start.Date, Id);
                            id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                            proc.UpdateDateBlock(id, 2);
                        }
                        start = start.AddDays(1);
                    }
                }
                finish_Load = true;
            }
            // 
            //MessageBox.Show(dtListBlock.Rows.Count.ToString());
            dtListBlock = proc.Get_List_Block(dateStart.Value, dateEnd.Value, ttt);
            foreach (DataRow row in dtListBlock.Rows)
            {
                if (row[3].ToString() == "2")
                {
                    row[3] = false;
                }
                else
                {
                    row[3] = true;
                }
            }
            dtListBlock.AcceptChanges();
            dataGridView1.Invoke((Action)(() => dataGridView1.DataSource = dtListBlock));
            if (radioButton1.Checked)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells[3].Value))
                    {
                        button3.Invoke((Action)(() => button3.Enabled = true));   
                        break;
                    }
                }
            }
            else
            {
                if (radioButton2.Checked)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[3].Value))
                        {
                            button3.Invoke((Action)(() => button3.Enabled = true));   
                            break;
                        }
                    }
                }
            }           
        }

        private void cbDeps_SelectedIndexChanged(object sender, EventArgs e)
        {           
        }

        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateStart.Value > dateEnd.Value)
            {
                dateStart.Value = dateEnd.Value;
            }
            if (finish_Load)
            load_table();
           
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateStart.Value > this.dateEnd.Value)
            {
                dateEnd.Value = dateStart.Value;
            }
            if (finish_Load)
            load_table();
        }

        private void installDate()
        {
            DataTable dtTemp = proc.GetCloseDate();
            //closeDate -  DateLastInv
            //dateStart - DateInvSpis 
            //closeDate = (DateTime)dtTemp.Rows[0][0];
            closeDate = (DateTime) dtTemp.Rows[0]["old_dinv"];
            DateStartMonth = new DateTime(dateStart.Value.Year, dateStart.Value.Month, 1);
            DateEndLastMonth= DateStartMonth.AddDays(-1);
//            DateEndMonth = DateEndLastMonth.AddMonths(1);
            //DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            DateTime DateEndMonth = new DateTime();
            if (DateEndLastMonth.Month > 10)
            {
                DateEndMonth = new DateTime(DateTime.Now.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }
            else
            {
                DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }

            //TimeSpan ts = DateEndMonth - dateStart.Value;
            // Difference in days.
            //int differenceInDays = ts.Days;
            //differenceInDays = Math.Abs(differenceInDays);

            DateTime d2 = DateEndMonth.Date;
            DateTime  d1 = dateStart.Value.Date;
            long time = 0;

            time = d2.Ticks - d1.Ticks;
            DateTime time2 = new DateTime(time);
            long countDays = time2.Day-1;
            label4.Text = DateStartMonth + "   \r\n" + DateEndLastMonth + " \r\n " + DateEndMonth + "\r\n" + countDays;
         
            
            //if (countDays <= 14)
            //{
            //    MessageBox.Show(" c " + closeDate.ToShortDateString() + " по " + DateEndMonth.ToShortDateString());
            //}
            //else
            //{
            //    MessageBox.Show(" c " + closeDate.ToShortDateString() + " по " + DateEndLastMonth.ToShortDateString());
            //}
           
            dtTemp.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!Convert.ToBoolean(row.Cells[3].Value))
                {
                    button3.Enabled = true;
                    break;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[3].Value))
                {
                    button3.Enabled = true;
                    break;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            proc.BlockPeriod(1,dateStart.Value);
        }

        private static void DoSomething(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] parameters = (object[])e.Argument;
                backgroundWorker1.ReportProgress(0);
                #region Блокировка
                if (radioButton1.Checked)
                {
                    //wait.Visible = true;
                    // List<DateTime> allDays = new List<DateTime>();
                    DateTime start = dateStart.Value;
                    DateTime maxDate = dateEnd.Value;
                    DataTable dtTmp;
                    cbDeps.Invoke((MethodInvoker)delegate
                    {
                        selectindex = cbDeps.SelectedIndex;
                    });
                    if (selectindex == 0) //Выборка отделов при все отделы
                    {
                        //DataRow selectedDataRow;

                        for (int i = 1; i < dtDeps.Rows.Count; i++)
                        {
                            start = dateStart.Value;
                            // cbDeps.SelectedIndex = i;
                            // selectedDataRow = ((DataRowView) cbDeps.SelectedItem).Row;
                            // int Id = Convert.ToInt32(selectedDataRow["Id"]);
                            int Id = Convert.ToInt32(dtDeps.Rows[i]["Id"]);
                            while (maxDate.Date.AddDays(0).Date >= start.Date)
                            {
                                // allDays.Add(start.Date);
                                // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                                //запрос по отделу и дате
                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                if (dtTmp.Rows.Count > 0)
                                {
                                    Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " + Id + " " + start.ToShortDateString());
                                    proc.UpdateDateBlock(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                    proc.UpdatePeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                    proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                }
                                else
                                {
                                    Console.WriteLine("Создаем для ID " + Id + " " + start.ToShortDateString());
                                    proc.InsertBlockInvoicess(Id, start.Date);
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                }
                                dtTmp.Clear();
                                start = start.AddDays(1);
                            }
                            proc.SetRests(dateStart.Value, DateTime.Now, Id);
                            //proc.SetRests(dateStart.Value,dateEnd.Value,Id);
                        }
                        cbDeps.Invoke((MethodInvoker) delegate
                        {
                            cbDeps.SelectedIndex = 0;
                        });
                    }
                    else
                    {
                        cbDeps.Invoke((MethodInvoker)delegate
                        {
                            selectvalue = Convert.ToInt32(cbDeps.SelectedValue.ToString());
                        });
                        Console.WriteLine(start.Date.ToShortDateString() + " " + selectvalue);
                        while (maxDate.Date.AddDays(0).Date >= start.Date)
                        {
                            // allDays.Add(start.Date);
                            // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                            //запрос по отделу и дате
                            cbDeps.Invoke((MethodInvoker) delegate
                            {
                                selectvalue = Convert.ToInt32(cbDeps.SelectedValue.ToString());
                            });
                            dtTmp = proc.FindDateBlock(start.Date, selectvalue);
                            if (dtTmp.Rows.Count > 0)
                            {
                                //Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " + Convert.ToInt32(cbDeps.SelectedValue.ToString()) + " " + start.ToShortDateString());
                                proc.UpdateDateBlock(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                proc.UpdatePeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                                proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                            }
                            else
                            {
                                //Console.WriteLine("Создаем для ID " + Convert.ToInt32(cbDeps.SelectedValue.ToString()) + " " + start.ToShortDateString());
                                proc.InsertBlockInvoicess(selectvalue, start.Date);
                                dtTmp = proc.FindDateBlock(start.Date, selectvalue);
                                proc.InsertBlockPeriodInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]), 1);
                            }
                            dtTmp.Clear();
                            start = start.AddDays(1);
                        }
                        proc.SetRests(dateStart.Value, DateTime.Now, selectvalue);
                        //блокируем                   
                    }

                    load_table();
                    backgroundWorker1.ReportProgress(100);
                   // wait.Visible = false;
                    MessageBox.Show("Период заблокирован!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region Разблокировка
                if (radioButton2.Checked)
                {
                   // DialogResult result = DialogResult.No;
                   // wait.Visible = true;
                    //TimeBlock tmb = new TimeBlock();
                    //tmb.backgroundWorker1 += new System.ComponentModel.DoWorkEventHandler(DoSomething);
                    DoOnUIThread(delegate() {
                    tmb = new TimeBlock();
                    //tmb.FilesToAddDelete(..);
                   //result = tmb.ShowDialog();
                    tmb.ShowDialog();
                    });
                    
                    if (tmb.sendTime != "")
                    {
                        DateTime start = dateStart.Value;
                        DateTime maxDate = dateEnd.Value;
                        DataTable dtTmp;
                        cbDeps.Invoke((MethodInvoker)delegate
                        {
                            selectindex = cbDeps.SelectedIndex;
                        });
                        if (selectindex == 0) //Выборка отделов при все отделы
                        {
                            //DataRow selectedDataRow;
                            for (int i = 1; i < dtDeps.Rows.Count; i++)
                            {
                                start = dateStart.Value;
                                //cbDeps.SelectedIndex = i;
                                //selectedDataRow = ((DataRowView)cbDeps.SelectedItem).Row;
                                //int Id = Convert.ToInt32(selectedDataRow["Id"]);
                                int Id = Convert.ToInt32(dtDeps.Rows[i]["Id"]);
                                while (maxDate.Date.AddDays(0).Date >= start.Date)
                                {
                                    // allDays.Add(start.Date);
                                    // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                                    //запрос по отделу и дате
                                    dtTmp = new DataTable();
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    if (dtTmp != null)
                                    {
                                        if (dtTmp.Rows.Count > 0)
                                        {
                                            int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                            Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " + Id +
                                                              " " + start.ToShortDateString());
                                            proc.UpdateDateBlock(id, 2);
                                            proc.InsertBlockPeriodInvoices(id, 2);
                                            dtTmp = proc.selectPeriodsToBlockInvoices(Convert.ToInt32(dtTmp.Rows[0]["id"]));
                                            if (dtTmp.Rows.Count > 0)
                                            {
                                                proc.UpdatePeriodsToBlockInvoices(id, 0,
                                                    tmb.sendTime);
                                            }
                                            else
                                            {
                                                proc.insertPeriodsToBlockInvoices(id, tmb.sendTime);
                                            }
                                        }
                                        else
                                        {
                                            proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                            dtTmp = proc.FindDateBlock(start.Date, Id);
                                            int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                            proc.UpdateDateBlock(id, 2);
                                        }
                                        dtTmp.Clear();
                                    }
                                    else
                                    {
                                        proc.InsertBlockInvoicess(Convert.ToInt32(cbDeps.SelectedValue.ToString()), start.Date);
                                        dtTmp = proc.FindDateBlock(start.Date, Id);
                                        int id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                        proc.UpdateDateBlock(id, 2);
                                    }

                                    start = start.AddDays(1);
                                }
                            }
                            cbDeps.Invoke((MethodInvoker) delegate
                            {
                                cbDeps.SelectedIndex = 0;
                            });
                        }
                        else
                        {
                            cbDeps.Invoke((MethodInvoker)delegate
                            {
                                selectvalue = Convert.ToInt32(cbDeps.SelectedValue.ToString());
                            });
                            Console.WriteLine(start.Date.ToShortDateString() + " " + selectvalue);
                            int id;
                            //DataRow selectedDataRow = ((DataRowView)cbDeps.SelectedItem).Row;
                            //int Id = Convert.ToInt32(selectedDataRow["Id"]);
                            int Id = selectvalue;
                            while (maxDate.Date.AddDays(0).Date >= start.Date)
                            {
                                // allDays.Add(start.Date);
                                // Console.WriteLine(start.Date.ToShortDateString() + " " + cbDeps.SelectedValue.ToString());
                                //запрос по отделу и дате
                                dtTmp = new DataTable();

                                dtTmp = proc.FindDateBlock(start.Date, Id);
                                if (dtTmp != null)
                                {
                                    if (dtTmp.Rows.Count > 0)
                                    {
                                        id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                        Console.WriteLine(dtTmp.Rows[0]["id"].ToString() + " Обновляем для ID " +
                                                          selectvalue + " " +
                                                          start.ToShortDateString());
                                        proc.UpdateDateBlock(id, 2);
                                        proc.InsertBlockPeriodInvoices(id, 2);
                                        dtTmp = proc.selectPeriodsToBlockInvoices(id);
                                        if (dtTmp.Rows.Count > 0)
                                        {
                                            proc.UpdatePeriodsToBlockInvoices(id, 0,
                                                tmb.sendTime);
                                        }
                                        else
                                        {
                                            proc.insertPeriodsToBlockInvoices(id, tmb.sendTime);

                                        }
                                    }
                                    else
                                    {
                                        proc.InsertBlockInvoicess(selectvalue, start.Date);
                                        dtTmp = proc.FindDateBlock(start.Date, Id);
                                        id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                        proc.UpdateDateBlock(id, 2);
                                    }
                                    dtTmp.Clear();
                                }
                                else
                                {
                                    proc.InsertBlockInvoicess(selectvalue, start.Date);
                                    dtTmp = proc.FindDateBlock(start.Date, Id);
                                    id = Convert.ToInt32(dtTmp.Rows[0]["id"]);
                                    proc.UpdateDateBlock(id, 2);
                                }
                                start = start.AddDays(1);
                            }
                        }
                        load_table();
                        //wait.Visible = false;
                        backgroundWorker1.ReportProgress(100);
                        MessageBox.Show("Период разблокирован!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //функция разблокировки   
                }
                #endregion
               
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                e.Cancel = true;
                backgroundWorker1.ReportProgress(100);
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {               
                progressBar1.Hide();
                this.Enabled = true;                
                this.progressBar1.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {               
                progressBar1.Hide();
                this.Enabled = true;                
                this.progressBar1.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                
                progressBar1.Hide();
                this.Enabled = true;                
                //MessageBox.Show("Done!");
                this.progressBar1.Text = "Done!";
            }
        }
    }
}
