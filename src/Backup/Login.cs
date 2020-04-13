using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;

namespace Spisanie
{
    public partial class Login : Form
    {
        public bool passwordCheck = false;
        private string passwordSTR="";
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        public Login()
        {
            InitializeComponent();
            string LN = UserSettings.User.FullUsername[0].ToString().ToUpper();
            string FN = UserSettings.User.FullUsername;            
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            //passwordSTR = month + year + LN + FN[FN.IndexOf('.') - 1].ToString().ToUpper();
            //passwordSTR = "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passwordCheck = false;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 0)
            {
                if (passwordSTR.Equals(maskedTextBox1.Text.ToString()))
                {
                    Logging.StartFirstLevel(327);
                    Logging.Comment("Ввод верного пароля");
                    Logging.Comment("ID пользователя: "+UserSettings.User.Id);
                    Logging.Comment("Время: "+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
                    Logging.StopFirstLevel();
                    passwordCheck = true;
                    this.Dispose();
                }
                else
                {
                    Logging.StartFirstLevel(328);
                    Logging.Comment("Ввод неверного пароля");
                    Logging.Comment("ID пользователя: " + UserSettings.User.Id);
                    Logging.Comment("Время: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                    Logging.StopFirstLevel();
                    MessageBox.Show("Пароль введен неверно!", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Необходимо ввести пароль!", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Подтвердить");
            toolTip1.SetToolTip(button2, "Выход");
          
            //DataTable tbbfb = proc.getPassword();
            passwordSTR = proc.getPassword().Rows[0][0].ToString();
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        

    }
}
