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
    public partial class frmZero : Form
    {
        DataTable dtIn;

        public frmZero(DataTable dt)
        {
            dtIn = dt;
            InitializeComponent();
        }

        private void frmZero_Load(object sender, EventArgs e)
        {
            DataRow[] drs = dtIn.Select("type=1");
            foreach (DataRow dr in drs)
            {
                tbZcena.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }
            drs = dtIn.Select("type=2");
            foreach (DataRow dr in drs)
            {
                tbRcena.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }
            drs = dtIn.Select("type=3");
            foreach (DataRow dr in drs)
            {
                tbNds.Text += dr["abbriviation"].ToString().Trim() + "   №" + dr["ttn"].ToString().Trim() + "   " + dr["type_name"].ToString().Trim() + "\r\n";
            }
        }
    }
}
