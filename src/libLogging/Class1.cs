using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;
using Microsoft.Win32;
//using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Odbc;
using System.IO;
using System.Net;



namespace libLogging
{
    public static class Class1
    {
        private static int IdProgrammsThis;
        private static string IpCopmThis;
        private static int IdUsersThis;
        private static int IdStatusThis;
        private static string NameBaseThis;
        private static int IdLogThis = -1;

        /// <summary>
        /// Metod Initialization
        /// </summary>
        public static void Initialization()
        {
            try
            {
                //IdProgrammsThis = int.Parse(Cfg.sIdProg);
                //IdUsersThis = UserInfo.IdUser;
                //IdStatusThis = UserInfo.GetAccessProg();
                //NameBaseThis = Cfg.sDBName;
                IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                IPAddress[] addr = ipEntry.AddressList;
                for (int i = 0; i < addr.Length; i++)
                {
                    IpCopmThis = addr[i].ToString();
                }
                IdLogThis = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        /// <summary>
        /// Metod Initialization
        /// </summary>
        /// <param name="IdProgramms">Id programm</param>
        /// <param name="IdUsers">Id user</param>
        /// <param name="IdStatus">Id status</param>
        /// <param name="NameBase">Name Base</param>
        public static void Initialization(int IdProgramms, int IdUsers, int IdStatus, string NameBase)
        {
            try
            {
                IdProgrammsThis = IdProgramms;
                IdUsersThis = IdUsers;
                IdStatusThis = IdStatus;
                NameBaseThis = NameBase;
                IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                IPAddress[] addr = ipEntry.AddressList;
                for (int i = 0; i < addr.Length; i++)
                {
                    IpCopmThis = addr[i].ToString();
                }
                IdLogThis = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        #region Properties
        /// <summary>
        /// Текущий Id записи из таблицы [j_Log] 
        /// </summary>
        public static int IdLog
        {
            get { return IdLogThis; }
            //set { IdLog = value  }
        }
        /// <summary>
        /// Id программы
        /// </summary>
        public static int IdProgramms
        {
            get { return IdProgrammsThis; }
        }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public static int IdUsers
        {
            get { return IdUsersThis; }
        }
        /// <summary>
        /// Id статуса
        /// </summary>
        public static int IdStatus
        {
            get { return IdStatusThis; }
        }
        /// <summary>
        /// Имя БД 
        /// </summary>
        public static string NameBase
        {
            get { return NameBaseThis; }
        }
        /// <summary>
        /// IP компьютера 
        /// </summary>
        public static string IpCopm
        {
            get { return IpCopmThis; }
        }
        #endregion
        /// <summary>
        /// Начало события первого урoвня 
        /// </summary>
        /// <param name="IdEvent">Id события из таблицы [j_Events]</param>
        public static void StartFirstLevel(int IdEvent)
        {
            try
            {


                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        DataTable dt = new DataTable();
                        ArrayList param = new ArrayList();

                        param.Clear();
                        param.Add(IdEvent);
                        param.Add(IdProgrammsThis);
                        param.Add(IpCopmThis);
                        param.Add(IdUsersThis);
                        param.Add(IdStatusThis);
                        param.Add(NameBaseThis);
                        //dt = new SqlResult(Cfg.hConnect, "Logs.log.StartFirstLevel", new string[6] { "@IdEvents", 
                        //                                                                 "@IdProgramm",
                        //                                                                 "@IpComp",
                        //                                                                 "@IdUsers",
                        //                                                                 "@IdStatus",
                        //                                                                 "@NameBase",
                    //}, param);

                        IdLogThis = int.Parse(dt.Rows[0][0].ToString());
                        //new SqlResult(Cfg.hConnect, 1, "select 'IdLog = " + IdLogThis + "'");
                    }
                    else
                    {
                        //MessageBox.Show("Logging - Event not Stop");
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }

        /// <summary>
        /// Окончание события первого уровня с ошибкой
        /// </summary>
        public static void StopFirstLevelError()
        {
            try
            {
                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        //MessageBox.Show("Logging - Event not Start Error");
                    }
                    else
                    {
                        DataTable dt;
                        ArrayList param = new ArrayList();
                        param.Clear();
                        param.Add(IdLogThis);
                        param.Add(1);
                        //dt = new SqlResult(Cfg.hConnect, "Logs.log.StopFirstLevel", new string[2] { "@IdLog",
                    //                                                                            "@Error",   
                    //}, param);
                      //  IdLogThis = 0;
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        /// <summary>
        /// Окончание события первого уровня
        /// </summary>
        public static void StopFirstLevel()
        {
            try
            {
                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        //MessageBox.Show("Logging - Event not Start");
                    }
                    else
                    {
                        DataTable dt;
                        ArrayList param = new ArrayList();
                        param.Clear();
                        param.Add(IdLogThis);
                        param.Add(0);
                        //dt = new SqlResult(Cfg.hConnect, "Logs.log.StopFirstLevel", new string[2] { "@IdLog",
                    //                                                                            "@Error",   
                    //}, param);
                      //  IdLogThis = 0;
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        /// <summary>
        /// Комментарий 
        /// </summary>
        /// <param name="strComment">Строка комментария</param>
        public static void Comment(string strComment)
        {
            try
            {
                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        //MessageBox.Show("Logging - Event not Start");
                    }
                    else
                    {
                        SecondLevel(6, strComment);
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        /// <summary>
        /// Обновление значения переменной
        /// </summary>
        /// <param name="VariableName">Имя переменной</param>
        /// <param name="ValueAfter">До</param>
        /// <param name="ValueBefore">После</param>
        public static void VariableChange(string VariableName, object ValueAfter, object ValueBefore)
        {
            try
            {
                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        //MessageBox.Show("Logging - Event not Start");
                    }
                    else
                    {
                        string strMessage = "";
                        string strAfter = ValueAfter.ToString().Replace("after", "After");
                        string strBefore = ValueBefore.ToString().Replace("before", "Before");
                        strMessage = VariableName.Trim() + " after:" + strAfter + "; before:" + strBefore + ";";
                        SecondLevel(1, strMessage);
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
        /// <summary>
        /// Ошибка в программе
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void Error(Exception ex)
        {
            try
            {
                if (IdLogThis != -1)
                {
                    if (IdLogThis == 0)
                    {
                        //MessageBox.Show("Logging - Event not Start");
                    }
                    else
                    {
                        string strErrorMessage = "";
                        //NumberString: value1 (только для FoxPro); ErrorCode: value2; ErrorMessage: value3.
                        strErrorMessage = strErrorMessage + "Code: " + ex.TargetSite.ToString() + ";" +
                            "Number String: 0;" +
                            "Error Code: " + ex.TargetSite.MethodHandle.Value.ToString() + ";" +
                            "ErrorMessage: " + ex.Message.ToString();
                        SecondLevel(7, strErrorMessage);
                        //StopFirstLevelError();
                    }
                }
                else
                {
                    //MessageBox.Show("Logging - Не выполнен метод Initialization");
                }
            }
            catch (Exception exx)
            {
                //MessageBox.Show("Logging - " + exx.ToString());
            }
        }


        private static void SecondLevel(int IdTypeLog, string strComment)
        {
            try
            {
                ArrayList param = new ArrayList();
                param.Clear();
                param.Add(IdTypeLog);
                param.Add(strComment);
                //new SqlResult(Cfg.hConnect, "Logs.log.SecondLevel", new string[2] { "@IdTypeLog",
                //                                                                "@StrComment",
            //}, param);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }



        /// <summary>
        /// Сравнение двух DataTable и запись изменений в лог
        /// </summary>
        /// <param name="dtOld">Old DataTable</param>
        /// <param name="dtNew">New DataTable</param>
        /// <param name="ColumName">Колонка ID</param>
        /// <param name="CompareColums">Массив индексов колонок по которым надо сравнивать</param>
        public static void CompareDataTable(DataTable dtOld, DataTable dtNew, string ColumName, int[] CompareColums)
        {
            try
            {
                foreach (DataRow drOld in dtOld.Rows)
                {
                    DataRow[] drNewArray = dtNew.Select(ColumName + "=" + drOld[ColumName].ToString());
                    if (drNewArray.Length != 0) // Есть в новой
                    {
                        DataRow drNew = drNewArray[0];
                        int FlComment = 0;

                        for (int i = 0; i < CompareColums.Length; i++) // проходим по массиву, для сравнения
                        {
                            int Index = CompareColums[i];
                            if (drOld[Index].ToString() != drNew[Index].ToString()) // сравнение 
                            {
                                if (FlComment == 0)
                                {
                                    Comment("Update " + ColumName + " = " + drOld[ColumName].ToString());
                                    FlComment = 1;
                                }
                                VariableChange(dtOld.Columns[Index].ColumnName, drOld[Index], drNew[Index]);
                            }
                        }
                        dtNew.Rows.Remove(drNew);
                    }
                    else // нет в новой т.е. удаленные
                    {
                        Comment("Delete " + ColumName + " = " + drOld[ColumName].ToString());
                    }
                }
                foreach (DataRow drNew in dtNew.Rows) // то, что осталось в новой т.е. добавленные
                {
                    if (drNew.RowState != DataRowState.Deleted)
                    {
                        Comment("Insert " + ColumName + " = " + drNew[ColumName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Logging - " + ex.ToString());
            }
        }
    }
}
