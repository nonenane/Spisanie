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
    public partial class frmSpravOrg : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
          
        private DataTable dtPost;
        private int callType;

        public frmSpravOrg(int Call)
        {
            callType = Call;
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmSpravOrg_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть справочник");
            t.SetToolTip(btConfirm, "Подтвердить выбор");
            t.SetToolTip(tbPost, "Введите фрагмент полного наименования организации");

            if (callType == 0)
            {
                btConfirm.Enabled = false;
            }
            dgvPosts_Init();
        }

        private void dgvPosts_Init()
        {
            wait.Visible = true;
            Refresh();

            dtPost = proc.GetSpravPosts(tbPost.Text.Trim());

            dgvPosts.AutoGenerateColumns = false;
            dgvPosts.DataSource = null;
            dgvPosts.DataSource = dtPost;

            psName.DataPropertyName = "name";
            psINN.DataPropertyName = "inn";

            if (callType == 1)
            {
                if (dgvPosts.RowCount > 0)
                {
                    btConfirm.Enabled = true;
                }
                else
                {
                    btConfirm.Enabled = false;
                }
            }
            
            wait.Visible = false;
            Refresh();
        }

        private void tbPost_TextChanged(object sender, EventArgs e)
        {
            dgvPosts_Init();
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            TempValues.id_post = int.Parse(dtPost.DefaultView[dgvPosts.CurrentRow.Index]["id"].ToString());
            TempValues.name_post = dtPost.DefaultView[dgvPosts.CurrentRow.Index]["name"].ToString();
            Close();
        }

        private void dgvPosts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (callType == 1 && e.RowIndex>=0)
            {
                btConfirm_Click(sender, e);
            }
        }

        private void dgvPosts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (callType != 0)
            {
                TempValues.id_post = int.Parse(dtPost.DefaultView[dgvPosts.CurrentRow.Index]["id"].ToString());
                TempValues.name_post = dtPost.DefaultView[dgvPosts.CurrentRow.Index]["name"].ToString();
                Close();
            }
        }
    }
}
