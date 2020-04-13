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
    public partial class frmPrint : Form
    {
        private int callType, id;

        public frmPrint(int Call, int Id)
        {
            callType = Call;
            id = Id;
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            switch (callType)
            {
                case 1:
                    chk1.Text = "ТОРГ-12";
                    chk2.Text = "Счет-фактура";
                    chk2.Visible = true;
                    break;
                case 2:
                    chk1.Text = "ТОРГ-12";
                    chk2.Text = "Счет-фактура";
                    chk2.Visible = true;
                    break;
                case 3:
                    chk1.Text = "Акт переоценки";
                    chk2.Visible = false;
                    break;
                case 4:
                    chk1.Text = "Акт списания";
                    chk2.Visible = false;
                    break;
            }

            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть");
            t.SetToolTip(btPrint, "Печать");
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            switch (callType)
            {
                case 1:
                case 2:
                    if (!chk1.Checked && !chk2.Checked)
                    {
                        btPrint.Enabled = false;
                    }
                    else
                    {
                        btPrint.Enabled = true;
                    }
                    break;
                case 3:
                case 4:
                    btPrint.Enabled = chk1.Checked;
                    break;
            }
        }

        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk1.Checked && !chk2.Checked)
            {
                btPrint.Enabled = false;
            }
            else
            {
                btPrint.Enabled = true;
            }

        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            frmPrintPreView PreView = new frmPrintPreView(id, callType, chk1.Checked, chk2.Checked);
            PreView.ShowDialog();
        }
    }
}
