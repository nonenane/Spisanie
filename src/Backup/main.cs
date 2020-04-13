using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Project;

namespace Spisanie
{
    static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Project.FillSettings(args);
                InitAndRun();
            }
        }

        private static void InitAndRun()
        {
            Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Logging.StartFirstLevel(1);
            Logging.Comment("Вход в программу");
            Logging.StopFirstLevel();
            Logger.NewMessage("начало работы");
            Application.Run(new mainForm());
            Logging.StartFirstLevel(2);
            Logging.Comment("Выход из программы");
            Logging.StopFirstLevel();
            Logger.NewMessage("конец работы");
        }
    }
}
