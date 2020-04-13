using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spisanie
{
    public partial class TimeBlock : Form
    {
        public string sendTime = "";
        public TimeBlock()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "HH:mm:ss"; // Only use hours and minutes
            dateTimePicker1.MinDate = new DateTime(1985, 6, 20, 01, 00, 00);
            dateTimePicker1.MaxDate = new DateTime(1985, 6, 20, 23, 59, 59);
            dateTimePicker1.Value = new DateTime(1985, 6, 20, 02, 30, 00);
            dateTimePicker1.ShowUpDown = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendTime = "";
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dateTimePicker1.Value.ToLongTimeString());
            sendTime = dateTimePicker1.Value.ToLongTimeString();
            Dispose();
            //MessageBox.Show(sendTime);
            //установка времени отсчёта
        }

        private void TimeBlock_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
