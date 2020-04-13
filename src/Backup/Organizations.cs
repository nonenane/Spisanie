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
    public partial class frmOrganizations : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            
        private int id_byer, id_post, days, oldid_byer, oldid_post, olddays;
        public frmOrganizations()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            if (id_post!=oldid_post
                || id_byer != oldid_byer
                || nudDays.Value != olddays)
            {
                DialogResult d = MessageBox.Show("Настройки были изменены.\nСохранить изменения?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    Save();
                    if (TempValues.Error)
                    {
                        TempValues.Error = false;
                        return;
                    }
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

        private void frmOrganizations_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть форму");
            t.SetToolTip(btSave, "Сохранить настройки");
            t.SetToolTip(btGetOrg2, "Вызов справочника организаций");
            t.SetToolTip(btGetOrg3, "Вызов справочника организаций");

            DataTable temp = proc.GetPost(0, 1);
            if (temp==null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-поставщик удалена!");
            }
            else
            {
                tbPost.Text = temp.Rows[0]["cname"].ToString();
                id_post = oldid_post = int.Parse(temp.Rows[0]["id"].ToString());
            }

            temp = proc.GetPost(0, 2);
            if (temp == null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-покупатель удалена!");
            }
            else
            {
                tbBuyer.Text = temp.Rows[0]["cname"].ToString();
                id_byer = oldid_byer = int.Parse(temp.Rows[0]["id"].ToString());
            }

            temp = proc.GetConfig();
            if (temp == null)
            {
                return;
            }
            nudDays.Value = days = olddays = int.Parse(temp.Rows[0]["value"].ToString());
            temp.Dispose();
        }

        private void btGetOrg2_Click(object sender, EventArgs e)
        {
            frmSpravOrg spr = new frmSpravOrg(1);
            TempValues.id_post = 0;
            spr.ShowDialog();
            if (TempValues.id_post > 0)
            {
                id_post = TempValues.id_post;
                tbPost.Text = TempValues.name_post;
            }
        }

        private void btGetOrg3_Click(object sender, EventArgs e)
        {
            frmSpravOrg spr = new frmSpravOrg(1);
            TempValues.id_post = 0;
            spr.ShowDialog();
            if (TempValues.id_post > 0)
            {
                id_byer = TempValues.id_post;
                tbBuyer.Text = TempValues.name_post;
            }
        }

        private void tbPost_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(tbPost, tbPost.Text.Trim());
        }

        private void tbBuyer_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(tbBuyer, tbBuyer.Text.Trim());
        }

        private void Save()
        {
            if (tbPost.Text.Trim().Length == 0
                || tbBuyer.Text.Trim().Length == 0)
            {
                MessageBox.Show("Не все обязательные поля\nзаполнены!", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);

                TempValues.Error = true;
                return;
            }
            
            proc.ChangeConfig("buy", id_byer.ToString());
            if (TempValues.Error)
            {
                return;
            }

            proc.ChangeConfig("post", id_post.ToString());
            if (TempValues.Error)
            {
                return;
            }

            proc.ChangeConfig("days", nudDays.Value.ToString());
            if (TempValues.Error)
            {
                return;
            }
            if (id_post != oldid_post || id_byer != oldid_byer || nudDays.Value != olddays)
            {
                Logging.StartFirstLevel(11);
                Logging.Comment("Редактирование настроек организаций");
                if (id_post != oldid_post)
                {
                    Logging.Comment("Изменение кода поставщика id = " + oldid_post.ToString()+" из s_post");
                    Logging.VariableChange("id_post", id_post.ToString(), oldid_post.ToString());
                }
                if (id_byer != oldid_byer)
                {
                    Logging.Comment("Изменение кода покупателя id=" + oldid_byer.ToString() + " из s_post");
                    Logging.VariableChange("id_byer", id_byer.ToString(), oldid_byer.ToString());
                }
                if (nudDays.Value != olddays)
                {
                    Logging.Comment("Изменение количества дней");
                    Logging.VariableChange("nudDays", nudDays.Value, olddays);
                }
                Logging.StopFirstLevel();
            }
            MessageBox.Show("Данные в настройках сохранены.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);            
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Сохранить изменения в настройках?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                Save();
                TempValues.Error = false;
            }
            else
            {
                Close();
            }
        }
    }
}
