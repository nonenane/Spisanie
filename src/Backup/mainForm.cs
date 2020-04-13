using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;

namespace Spisanie
{
    public partial class mainForm : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public mainForm()
        {
            InitializeComponent();
        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            //Logging.StartFirstLevel(2);
            //Logging.Comment("Выход из программы: User = " + UserInfo.UserLogin);              
            //Logging.StopFirstLevel();
            Close();
        }

        private void MenuItemLoad_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму загрузки данных.");
            frmLoad openForm = new frmLoad();
            openForm.ShowDialog();
        }

        private void MenuItemPrihod_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму просмотра списка накладных прихода.");
            frmNaklsP openForm = new frmNaklsP(1);
            openForm.ShowDialog();
        }

        private void MenuItemOtgruz_Click(object sender, EventArgs e)
        {
           // Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму просмотра списка накладных отгрузки.");
            frmNaklsO openForm = new frmNaklsO(2);
            openForm.ShowDialog();
        }

        private void MenuItemPereoc_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму просмотра списка актов переоценки.");
            frmNaklsPc openForm = new frmNaklsPc(5);
            openForm.ShowDialog();
        }

        private void MenuItemVozvrat_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму просмотра списка накладных возврата от покупателя.");
            frmNaklsV openForm = new frmNaklsV(3);
            openForm.ShowDialog();
        }

        private void MenuItemSpis_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму просмотра списка актов списания.");
            frmNaklsSp openForm = new frmNaklsSp(4);
            openForm.ShowDialog();
        }

        private void MenuItemOrganizations_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму справочника организаций.");
            frmSpravOrg spr = new frmSpravOrg(0);
            spr.ShowDialog();
        }


        private void MenuItemFirms_Click(object sender, EventArgs e)
        {
            //Cfg.LogWrite("Пользователь " + UserInfo.UserName.Trim() + " открыл форму настройки организаций.");
            frmOrganizations config = new frmOrganizations();
            config.ShowDialog();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + ' ' + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
            if (UserSettings.User.StatusCode == "БГЛ")
            {
                упарвлениеПериодамиToolStripMenuItem.Visible = true;
            }
            else
            {
                упарвлениеПериодамиToolStripMenuItem.Visible = false;
            }

            if (UserSettings.User.StatusCode == "ЗВТ")
            {
                ClosePartToolStripMenuItem.Visible = true;
            }
            else
            {
                ClosePartToolStripMenuItem.Visible = false;
            }

            Console.WriteLine(UserSettings.User.StatusCode);
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (TempValues.Error == true)
            {
                Logging.StartFirstLevel(2);
                Logging.Comment("Выход из программы: User = " + Nwuram.Framework.Settings.User.UserSettings.User.Username);
                Logging.StopFirstLevelError();
            }
            else
            {
                Logging.StartFirstLevel(2);
                Logging.Comment("Выход из программы: User = " + Nwuram.Framework.Settings.User.UserSettings.User.Username);
                Logging.StopFirstLevel();
            }
        }

        private void MenuItemBuhOst_Click(object sender, EventArgs e)
        {
            frmBuhOst Ostform = new frmBuhOst();
            Ostform.ShowDialog();
        }

        private void MenuItemInvRezults_Click(object sender, EventArgs e)
        {
            frmInvResults iR = new frmInvResults();
            iR.ShowDialog();
        }

        private void упарвлениеПериодамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.ShowDialog();

            if (loginForm.passwordCheck)
            {
                //Console.WriteLine("In");
                PeriodControl prControl = new PeriodControl();
                prControl.ShowDialog();
            }
            else
            {
                Console.WriteLine("out");
            }
        }

        private void ClosePartToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (proc.ExistFutureInvDates())
            {
                MessageBox.Show("Создана новая запись об \nинвентаризации. \nЗакрыть часть периода невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!proc.CalMonthEnded())
            {
                MessageBox.Show("Календарный месяц не закончился. \nЗакрыть часть периода невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            

            //получение части периода для закрытия
            /*DataTable dtDates = new DataTable();
            dtDates = proc.GetDatesForClosingPartPeriod();

            DateTime ds = DateTime.Now.Date;
            DateTime de = DateTime.Now.Date;

            if ((dtDates != null) && (dtDates.Rows.Count > 0))
            {
                ds = DateTime.Parse(dtDates.Rows[0]["dateStart"].ToString());
                de = DateTime.Parse(dtDates.Rows[0]["dateEnd"].ToString());
            }

            if (proc.CountClosed(ds, de) > 0)
            {
                MessageBox.Show("Часть периода уже закрыта.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }*/

            frmClosePartPeriod fClose = new frmClosePartPeriod();
            fClose.ShowDialog();
        }

    }
}
