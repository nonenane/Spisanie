using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;

namespace Spisanie
{
    class Procedures: SqlProvider
    {       
        ArrayList ap = new ArrayList();

        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }

        /// <summary>
        /// Получение списка коммерческих отделов
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeps()
        {
            ap.Clear();
            ap.Add(1);
            ap.Add(1);
            return executeProcedure("dbo.cnGet_deps", new string[2] { "@comm", "@type" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение приходов для разбивки товаров на закупочные цены
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetPrihZcena(DateTime date)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            return executeProcedure("spisanie.GetPrihodsForZcena", new string[2] { "@close_date", "@id_prog" }, new DbType[2] { DbType.DateTime, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение результатов инветаризации
        /// </summary>
        /// <param name="date">Дата основной инвентаризации</param>
        /// <returns></returns>
        public DataTable GetInvRezults(DateTime date)
        {
            ap.Clear();
            ap.Add(date);
            return executeProcedure("spisanie.GetInvResults", new string[1] { "@date" }, new DbType[1] { DbType.DateTime }, ap);
        }

        /// <summary>
        /// Блокирование периода между инвентаризациями
        /// </summary>
        /// <param name="id_dep">Номер отдела</param>
        public void BlockPeriod(int id_dep,DateTime dateTMP)
        {
            ap.Clear();
            DataTable dt = this.GetCloseDate();

            DateTime DateStartMonth = new DateTime(dateTMP.Year, dateTMP.Month, 1);
            DateTime DateEndLastMonth = DateStartMonth.AddDays(-1);
            DateTime DateEndMonth = new DateTime();
            if (DateEndLastMonth.Month > 10)
            {
                DateEndMonth = new DateTime(DateTime.Now.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }
            else
            {
                DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }
            DateTime d2 = DateEndMonth.Date;
            DateTime d1 = dateTMP.Date;
            long time = 0;

            time = d2.Ticks - d1.Ticks;
            DateTime time2 = new DateTime(time);
            long countDays = time2.Day - 1;
            if (countDays <= 14)
            {
                ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
                ap.Add(DateTime.Parse(DateEndMonth.ToShortDateString()));
            }
            else
            {
                ap.Add(DateTime.Parse(DateEndLastMonth.ToShortDateString()));
                ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
            }

            ap.Add(id_dep);
            ap.Add(1);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(1);
            executeProcedure("invoices.invsBlock", new string[6] { "@d_start", "@d_end", "@id_dep", "@id_status", "@id_user", "@fullBlock" },
                                                     new DbType[6] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            SetRests(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).Date.AddDays(1), DateTime.Now.Date, id_dep);
        }

        /// <summary>
        /// Блокирование периода между инвентаризациями
        /// </summary>
        /// <param name="id_dep">Номер отдела</param>
        public void BlockPeriod_forLoadForm(int id_dep, DateTime dateTMP)
        {
            ap.Clear();
            DataTable dt = this.GetCloseDate();
            /*
            DateTime DateStartMonth = new DateTime(dateTMP.Year, dateTMP.Month, 1);
            DateTime DateEndLastMonth = DateStartMonth.AddDays(-1);
            DateTime DateEndMonth = new DateTime();
            if (DateEndLastMonth.Month > 10)
            {
                DateEndMonth = new DateTime(DateTime.Now.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }
            else
            {
                DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
            }
            DateTime d2 = DateEndMonth.Date;
            DateTime d1 = dateTMP.Date;
            long time = 0;

            time = d2.Ticks - d1.Ticks;
            DateTime time2 = new DateTime(time);
            long countDays = time2.Day - 1;
            if (countDays <= 14)
            {
                ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
                ap.Add(DateTime.Parse(DateEndMonth.ToShortDateString()));
            }
            else
            {
                ap.Add(dateTMP.ToShortDateString());
                ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
                

                //MY PREV vers
                //ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
                //ap.Add(dateTMP.Date);

                //old vers
                //ap.Add(DateTime.Parse(DateEndLastMonth.ToShortDateString()));
                //ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
            }
            */

            //период закрываем датой последней инвентаризации
            ap.Add(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).AddDays(1));
            ap.Add(DateTime.Parse(dt.Rows[0]["dinv"].ToString()));

            ap.Add(id_dep);
            ap.Add(1);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(1);
            executeProcedure("invoices.invsBlock", new string[6] { "@d_start", "@d_end", "@id_dep", "@id_status", "@id_user", "@fullBlock" },
                                                     new DbType[6] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            SetRests(DateTime.Parse(dt.Rows[0]["old_dinv"].ToString()).Date.AddDays(1), DateTime.Now.Date, id_dep);
        }

        /// <summary>
        /// Проставление остатков
        /// </summary>
        /// <param name="oldInv">Дата начала периода</param>
        /// <param name="date">Дата конца периода</param>
        /// <param name="id_dep">Код отдела</param>
        public void SetRests(DateTime oldInv, DateTime date, int id_dep)
        {
            for (DateTime d = oldInv; d <= date; d = d.AddDays(1))
            {
                ArrayList param = new ArrayList();

                ap.Clear();
                ap.Add(d.Date);
                ap.Add(id_dep);
                ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
                executeProcedure("traderep.setOneRestSum", new string[3] { "@date", "@dep", "@idUser" },
                                                       new DbType[3] { DbType.DateTime, DbType.Int32, DbType.Int32 }, ap);
            }
        }

        /// <summary>
        /// Получение накладных с 0 ценами
        /// </summary>
        /// <param name="date">Дата закрытия месяца</param>
        /// <returns></returns>
        public DataTable GetZeroDocs(DateTime date, int id_dep)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(id_dep);
            return executeProcedure("spisanie.GetZeroNakls", new string[2] { "@date", "@id_dep" }, new DbType[2] { DbType.DateTime, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение справочника НДС
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpravNDS()
        {
            ap.Clear();
            return executeProcedure("invoices.invsGet_Nds", new string[0] { }, new DbType[0] { }, ap);
        }

        /// <summary>
        /// Получение бухгалтерских остатков
        /// </summary>
        /// <param name="idDep">Код отдела</param>
        /// <param name="date">Дата</param>
        /// <param name="noIs">Не учитывать ис</param>
        /// <returns></returns>
        public DataTable GetBuhOsts(int idDep, DateTime date, bool noIs)
        {
            ap.Clear();
            ap.Add(idDep);
            ap.Add(date);
            ap.Add(noIs);
            return executeProcedure("spisanie.GetBuhOst", new string[3] { "@id_otdel", "@date", "@noIs" }, new DbType[3] { DbType.Int32, DbType.DateTime, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение цен по товарам
        /// </summary>
        /// <returns></returns>
        public DataTable GetCen()
        {
            ap.Clear();
            return executeProcedure("spisanie.GetCenTovars", new string[0] {  }, new DbType[0] {  }, ap);
        }

        /// <summary>
        /// Получение списка отделов на которые формируются списания и возвраты
        /// </summary>
        /// <returns></returns>
        public DataTable GetVVODeps()
        {
            ap.Clear();
            return executeProcedure("spisanie.Get_VVODeps", new string[0] { }, new DbType[0] { }, ap);
        }

        /// <summary>
        /// Получение тела приходной накладной для печати отчета
        /// </summary>
        /// <param name="id">Код накладной прихода</param>
        /// <returns></returns>
        public DataTable GetPrintedPrih(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.PrintPrih", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Проверка даты закрытия месяца
        /// </summary>
        /// <returns></returns>
        public DataTable GetCloseDate()
        {
            ap.Clear();
            return executeProcedure("spisanie.Get_CloseDate", new string[0] { }, new DbType[0] {  }, ap);
        }

        /// <summary>
        /// Получение справочника поставщиков
        /// </summary>
        /// <param name="name">Фильтр по наименованию</param>
        /// <returns></returns>
        public DataTable GetSpravPosts(string name)
        {
            ap.Clear();
            if (name.Trim().Length > 0)
            {
                ap.Add(name);
            }
            else
            {
                ap.Add(null);
            }
            return executeProcedure("spisanie.GetSpravPost", new string[1] { "@name" }, new DbType[1] { DbType.String }, ap);
        }

        /// <summary>
        /// Получение данных по поставщику
        /// </summary>
        /// <param name="id">Код поставщика</param>
        /// <param name="type">Тип вызова</param>
        /// <returns></returns>
        public DataTable GetPost(int id, int type)
        {
            ap.Clear();
            if (id > 0)
            {
                ap.Add(id);
            }
            else
            {
                ap.Add(null);
            }
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            return executeProcedure("spisanie.GetPostNum", new string[3] { "@id", "@type", "@id_prog" },
                                    new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение содержимого накладной
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="type">Тип накладной</param>
        /// <returns></returns>
        public DataTable GetAdvige(int id, int type)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(type);
            return executeProcedure("spisanie.GetAdvige", new string[2] { "@id", "@type" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Проверка дат
        /// </summary>
        /// <returns></returns>
        public DataTable CheckDate()
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            return executeProcedure("spisanie.CheckDate", new string[1] { "@id_prog" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение заголовка накладной для печати
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <returns></returns>
        public DataTable GetPrintTitle(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.PrintTitle", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение реквизитов нашей организации
        /// </summary>
        /// <param name="date">Дата накладной</param>
        /// <param name="ntypeorg">Тип организации</param>
        /// <returns></returns>
        public DataTable GetOurOrg(DateTime date, int ntypeorg)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(ntypeorg);
            return executeProcedure("spisanie.PrintTitleOur", new string[2] { "@date", "@ntypeorg" }, new DbType[2] { DbType.DateTime, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение даты последнего списания
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpisDate()
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            return executeProcedure("spisanie.Get_DateSpis", new string[1] { "@id_prog" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение заголовков накладных
        /// </summary>
        /// <param name="dStart">Дата начала периода выборки</param>
        /// <param name="dEnd">Дата конца периода выборки</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="type">Тип накладных</param>
        /// <returns></returns>
        public DataTable GetRegdok(DateTime dStart, DateTime dEnd, int id_dep, int type)
        {
            ap.Clear();
            ap.Add(dStart);
            ap.Add(dEnd);
            ap.Add(id_dep);
            ap.Add(type);
            return executeProcedure("spisanie.GetRegdok", new string[4] { "@close_date", "@close_date2", "@id_dep", "@type" },
                                                             new DbType[4] { DbType.DateTime, DbType.DateTime,  DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение настроек
        /// </summary>
        /// <returns></returns>
        public DataTable GetConfig(string id_value)
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            return executeProcedure("Processing.petGetConfig", new string[2] { "@id_prog", "@idvalue" }, new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        /// <summary>
        /// Получение содержимого отгрузки для отчета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetPrintOtgruz(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.PrintOtgruz", new string[1] { "@id" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Изменение настроек
        /// </summary>
        /// <param name="id_value">Ключ</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        public void ChangeConfig(string id_value, string value)
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add(value);
            executeProcedure("Processing.UpdateProgConfig", new string[3] { "@Idprogramm", "@idvalue", "@value" }, 
                                                            new DbType[3] { DbType.Int32, DbType.String, DbType.String }, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Изменение заголовка накладной
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="ttn">ТТН</param>
        /// <param name="id_post">Код поставщика</param>
        /// <returns></returns>
        public void ChangeAllPrihod(int id, string ttn, string sf, int id_post,int id_ul)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(ttn);
            if (sf.Length == 0)
            {
                ap.Add(null);
            }
            else
            {
                ap.Add(sf);
            }
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(id_post);
            ap.Add(id_ul);
            executeProcedure("spisanie.ChangeAllPrihod", new string[6] { "@id", "@ttn", "@sf", "@id_user", "@id_post","@id_ul" },
                                                         new DbType[6] { DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Изменение шапки накладной
        /// </summary>
        /// <param name="id">Код строки</param>
        /// <param name="zcena">Цена закупки</param>
        /// <param name="rcena">Цена продажи</param>
        /// <param name="bcena">Старая цена продажи</param>
        /// <param name="type">Тип накладной</param>
        public void ChangeAdvige(int id, object zcena, decimal rcena, object bcena, object nds, int type)
        {
            ap.Clear();
            ap.Add(id);
            if (zcena == null)
            {
                ap.Add(null);
            }
            else
            {
                ap.Add(zcena);
            }
            ap.Add(rcena);
            if (bcena == null)
            {
                ap.Add(null);
            }
            else
            {
                ap.Add(bcena);
            }
            ap.Add(nds);
            ap.Add(type);
            executeProcedure("spisanie.ChangeAdvige", new string[6] { "@id", "@zcena", "@rcena","bcena", "@nds", "@type" },
                                                      new DbType[6] { DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32 }, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Установка даты закрытия месяца для отдела
        /// </summary>
        /// <param name="id_dep">Код отдела</param>
        /// <returns></returns>
        public DataTable ChangeCloseDate(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            return executeProcedure("spisanie.ChangeCloseDate", new string[2] { "@id_dep", "@id_prog" },
                                                                   new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение результатов инвентаризации по товарам
        /// </summary>
        /// <param name="id_dep">Код отдела</param>
        /// <returns></returns>
        public DataTable GetInventTovars(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            return executeProcedure("spisanie.GetInventTovars", new string[1] { "@id_dep" },
                                                                   new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Добавление заголовка накладных
        /// </summary>
        /// <param name="date">Дата прихода</param>
        /// <param name="type">Тип накладной</param>
        /// <returns></returns>
        public DataTable AddAllPrihod(DateTime date, int type, int ntypeorg, int isCredit)
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(date);
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(ntypeorg);
            ap.Add(isCredit);
            return executeProcedure("spisanie.Add_Allprihod", new string[6] { "@id_user", "@close_date", "@type", "@id_prog", "@ntypeorg","@isCredit" },
                                                              new DbType[6] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32
                                                                              , DbType.Int32,DbType.Int32 }, ap);
        }

        /// <summary>
        /// Добавление товарной позиции прихода
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="npp">Порядковый номер строки</param>
        public void AddPrihod(int id, int id_tovar, int id_dep, decimal netto, string npp, int inpp, decimal zcena, decimal rcena, int id_nds, DateTime CloseDate)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(npp);
            ap.Add(inpp);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(id_nds);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(CloseDate);
            executeProcedure("spisanie.Add_prihod", new string[11] { "@id", "@id_tovar", "@id_dep", "@netto", "@npp", "@inpp", "@zcena", "@rcena"
                                                                    , "@id_nds", "@id_user", "@close_date" },
                                                    new DbType[11] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String, DbType.Int32,
                                                                    DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.DateTime }, ap);
        }

        /// <summary>
        /// Добавление товарной позиции переоценки
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="npp">Порядковый номер строки</param>
        public void AddPereoc(int id, int id_tovar, int id_dep, decimal netto, string npp, decimal zcena, decimal rcena, int id_nds)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(npp);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(id_nds);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            executeProcedure("spisanie.Add_pereoc", new string[9] { "@id", "@id_tovar", "@id_dep", "@netto", "@npp", "@zcena", "@rcena"
                                                                     , "@id_nds", "@id_user"},
                                                    new DbType[9] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String
                                                                    ,DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32}, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Добавление товарной позиции отгрузки
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="npp">Порядковый номер строки</param>
        public void AddOtgruz(int id, int id_tovar, int id_dep, decimal netto, string npp, int inpp, decimal zcena, decimal rcena, int id_nds, DateTime CloseDate)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(npp);
            ap.Add(inpp);
            ap.Add(decimal.Round(zcena,2));
            ap.Add(decimal.Round(rcena,2));
            ap.Add(id_nds);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(CloseDate);
            executeProcedure("spisanie.Add_otgruz", new string[11] { "@id", "@id_tovar", "@id_dep", "@netto", "@npp", "@inpp", "@zcena", "@rcena"
                                                                        , "@id_nds", "@id_user", "@close_date" },
                                                    new DbType[11] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String, DbType.Int32 
                                                                     ,DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.DateTime}, ap);
        }

        /// <summary>
        /// Добавление товарной позиции возврата с касс
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="npp">Порядковый номер строки</param>
        public void AddVozvKass(int id, int id_tovar, int id_dep, decimal netto, string npp, decimal zcena, decimal rcena)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(npp);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            executeProcedure("spisanie.Add_vozvkass", new string[8] { "@id", "@id_tovar", "@id_dep", "@netto", "@npp", "@zcena", "@rcena"
                                                                     , "@id_user" },
                                                       new DbType[8] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String 
                                                                      ,DbType.Decimal, DbType.Decimal, DbType.Int32}, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Добавление товарной позиции списания
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="npp">Порядковый номер строки</param>
        public void AddSpis(int id, int id_tovar, int id_dep, decimal netto, string npp, decimal zcena, decimal rcena, int id_nds)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(npp);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(id_nds);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            executeProcedure("spisanie.Add_spis", new string[9] { "@id", "@id_tovar", "@id_dep", "@netto", "@npp", "@zcena", "@rcena"
                                                                   , "@id_nds", "@id_user" },
                                                  new DbType[9] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String 
                                                                   ,DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32}, ap);
            TempValues.Error = WasError;
        }
        /// <summary>
        /// Добавление записи о недостаче
        /// </summary>
        /// <param name="id">Код заголовка</param>
        /// <param name="id_tovar">Код товара</param>
        /// <param name="id_dep">Код отдела</param>
        /// <param name="netto">Колличество</param>
        /// <param name="delta">Дельта</param>
        /// <param name="npp">Порядковый номер строки</param>
        /// <param name="type">Тип</param>
        public void AddNedost(int id, int id_tovar, int id_dep, decimal netto, decimal delta, string npp, int type, decimal zcena, decimal rcena, int id_nds)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_tovar);
            ap.Add(id_dep);
            ap.Add(netto);
            ap.Add(delta);
            ap.Add(npp);
            ap.Add(type);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(id_nds);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            executeProcedure("spisanie.Add_nedost", new string[11] { "@id", "@id_tovar", "@id_dep", "@netto", "@delta", "@npp", "@type"
                                                                    , "@zcena", "@rcena", "@id_nds", "@id_user" },
                                                     new DbType[11] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal
                                                                        , DbType.String, DbType.Int32 ,DbType.Decimal, DbType.Decimal
                                                                        , DbType.Int32, DbType.Int32}, ap);
            TempValues.Error = WasError;
        }

        /// <summary>
        /// Получение статистики по созданным накладным
        /// </summary>
        /// <param name="date">Дата расчета</param>
        /// <param name="id_dep">Код отдела</param>
        /// <returns></returns>
        public DataTable GetStatistics(DateTime date, int id_dep)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(id_dep);
            return executeProcedure("spisanie.GetStat", new string[2] { "@close_date", "@id_dep" },
                                                           new DbType[2] { DbType.DateTime, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение переоценки для отчета
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <returns></returns>
        public DataTable GetPrintPereoc(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.PrintPereoc", new string[1] { "@id" },
                                                                new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение списания для отчета
        /// </summary>
        /// <param name="id">Код накладной</param>
        /// <returns></returns>
        public DataTable GetPrintSpis(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.PrintSpis", new string[1] { "@id" },
                                                                new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Удаление накладных
        /// </summary>
        /// <param name="iDep">Код отдела</param>
        /// <param name="date">Дата создания</param>
        public void DeleteNakls(int iDep, DateTime date)
        {        
            Logging.Comment("Удаление накладных отдела " + iDep.ToString().Trim() + " на дату " + date.ToString());

            ap.Clear();
            ap.Add(iDep);
            ap.Add(date);
            DataTable dtTempdel = executeProcedure("spisanie.SelectDel_Nakls", new string[2] { "@id_dep", "@date" },
                                                                                  new DbType[2] { DbType.Int32, DbType.DateTime }, ap);
            if (dtTempdel == null)
            {
                TempValues.Error = true;
                return;
            }

            string iddel = "";
            if (dtTempdel.Rows.Count != 0)
            {
                foreach (DataRow dr in dtTempdel.Rows)
                {
                    iddel = iddel + " , " + dr["id"].ToString().Trim();
                }
                Logging.Comment("Начало удаления id:" + iddel + " из j_allprihod");
            }
            ap.Clear();
            ap.Add(iDep);
            ap.Add(date);
            DataTable dtTemp = executeProcedure("spisanie.Del_Nakls", new string[2] { "@id_dep", "@date" },
                                                                        new DbType[2] { DbType.Int32, DbType.DateTime }, ap);
            if (dtTemp == null)
            {
                TempValues.Error = true;
                return;
            }
            Logging.Comment("Завершено удаление id:" + iddel + " из j_allprihod");
        }

        /// <summary>
        /// Получение списка юр. лиц
        /// </summary>
        /// <returns></returns>
        public DataTable GetLegaPersons()
        {
            ap.Clear();
            return executeProcedure("spisanie.GetLegalPersons", new string[0] {  },
                                                                new DbType[0] {  }, ap);
        }


        /// <summary>
        /// Получение периода блокировки за промежуток времени для отделов
        /// </summary>
        /// <param name="id">Код отдела</param>
        /// <param name="start">Дата начала</param>
        /// <param name="end">Дата окончания</param>        
        /// <returns></returns>

        public DataTable Get_List_Block(DateTime start, DateTime end,string id)
        {
            ap.Clear();
            ap.Add(start.ToString("yyyy-MM-dd"));
            ap.Add(end.ToString("yyyy-MM-dd"));
            ap.Add(id);
            return executeProcedure("spisanie.Get_List_Block", new string[] { "@time_start", "@time_end", "@id" },
                                                                new DbType[] { DbType.DateTime, DbType.DateTime,DbType.String }, ap);
        }

        /// <summary>
        /// Поиск записи для блокировки
        /// </summary>
        /// <param name="id">Код отдела</param>
        /// <param name="date">Дата</param>     
        /// <returns></returns>
        public DataTable FindDateBlock(DateTime date , int id)
        {
            ap.Clear();
            ap.Add(date.ToString("yyyy-MM-dd"));           
            ap.Add(id);
            return executeProcedure("spisanie.FindDateBlock", new string[] { "@time_start", "@id" },
                                                                new DbType[] { DbType.DateTime, DbType.String }, ap);
        }

        /// <summary>
        /// Обновление записи для блокировки
        /// </summary>
        /// <param name="id">Код отдела</param>
        /// <param name="id_block">Код записи</param>     
        /// <returns></returns>

        public DataTable UpdateDateBlock(int id, int id_block)
        {
            ap.Clear();            
            ap.Add(id);
            ap.Add(id_block);
            return executeProcedure("spisanie.UpdateDateBlock", new string[] { "@id", "@id_block_period" },
                                                                new DbType[] { DbType.String,DbType.Int32 }, ap);
        }

        /// <summary>
        /// Обновление записи для блокировки
        /// </summary>
        /// <param name="id">Код отдела</param>
        /// <param name="tipe">тип</param>     
        /// <returns></returns>
        /// 
        public DataTable UpdatePeriodsToBlockInvoices(int id,int tipe)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(tipe);
            return executeProcedure("spisanie.UpdatePeriodsToBlockInvoices", new string[] { "@id", "@tipe" },
                                                                new DbType[] { DbType.String, DbType.String }, ap);
        }

        public void InsertBlockPeriodInvoices(int id,int block)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(DateTime.Now);
            ap.Add(UserSettings.User.Id);
            ap.Add(block);
            executeProcedure("spisanie.InsertBlockPeriodInvoices", new string[] { "@id", "@time", "@idUser", "@id_Block" },
                                                                new DbType[] { DbType.String,DbType.Date,DbType.Int32,DbType.Int32 }, ap);
        }

        public void InsertBlockInvoicess(int id,DateTime date)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(date.ToShortDateString());
            executeProcedure("spisanie.InsertBlockInvoicess", new string[] { "@id", "@time" },
                                                                new DbType[] { DbType.String, DbType.Date}, ap);
        }


        public DataTable UpdatePeriodsToBlockInvoices(int id, int tipe,string time)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(tipe);
            ap.Add(time);
            ap.Add(UserSettings.User.Id);
            return executeProcedure("spisanie.UpdatePeriodsToBlockInvoices", new string[] { "@id", "@tipe", "@time", "@user_id " },
                                                                new DbType[] { DbType.String, DbType.String,DbType.Time,DbType.Int32 }, ap);
        }

        public DataTable selectPeriodsToBlockInvoices(int id)
        {
            ap.Clear();
            ap.Add(id);
            return executeProcedure("spisanie.selectPeriodsToBlockInvoices", new string[] { "@id" },
                                                                new DbType[] { DbType.String }, ap);
        }

        public DataTable insertPeriodsToBlockInvoices(int id, string time)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(time);
            ap.Add(UserSettings.User.Id);
            return executeProcedure("spisanie.insertPeriodsToBlockInvoices", new string[] { "@id", "@time", "@id_editor" },
                                                                new DbType[] { DbType.String, DbType.Time,DbType.Int32 }, ap);
        }
        
        public DataTable getPassword()
        {
            ap.Clear();
            ap.Add(UserSettings.User.Id);
            return executeProcedure("spisanie.getPassword", new string[] { "@id_user" },
                                                                new DbType[] {DbType.Int32 }, ap);
        }

        public DataTable GetDatesForClosingPartPeriod()
        {
            ap.Clear();
            return executeProcedure("[spisanie].[GetDatesForClosingPartPeriod]", 
                new string[] { },
                new DbType[] { }, ap);
        }

        public bool ExistFutureInvDates()
        {
            bool res = false;

            ap.Clear();

            DataTable dt = new DataTable();

            dt = executeProcedure("[spisanie].[GetFutureInvDates]",
                new string[] { },
                new DbType[] { }, ap);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    res = true;
                }
            }

            return res;
        }

        public bool CalMonthEnded()
        {
            bool res = false;

            ap.Clear();

            DataTable dt = new DataTable();

            dt = executeProcedure("[spisanie].[CheckCalMonthEnded]",
                new string[] { },
                new DbType[] { }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    res =  true;
                }
            }

            return res;            
        }

        public int CountClosed(DateTime ds, DateTime de)
        {
            int res = 0;

            ap.Clear();
            ap.Add(ds);
            ap.Add(de);

            DataTable dt = new DataTable();

            dt = executeProcedure("[spisanie].[GetCountClosed]",
                new string[] { "@dateStart", "@dateEnd" },
                new DbType[] { DbType.DateTime, DbType.DateTime }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = int.Parse(dt.Rows[0][0].ToString());
            }

            return res;
        }

        public DataTable NotClosedDepsList(DateTime ds, DateTime de)
        {
            ap.Clear();
            ap.Add(ds);
            ap.Add(de);

            return executeProcedure("[spisanie].[GetNotClosedDepsList]",
                new string[] { "@dateStart", "@dateEnd" },
                new DbType[] { DbType.DateTime, DbType.DateTime }, ap);
        }

        public DataTable SetStatusBuh(DateTime ds, DateTime de, string id_dep)
        {
            ap.Clear();
            ap.Add(ds);
            ap.Add(de);
            ap.Add(id_dep);

            return executeProcedure("[spisanie].[SetStatusBuh]",
                new string[] { "@dateStart", "@dateEnd", "@input_str" },
                new DbType[] { DbType.DateTime, DbType.DateTime, DbType.String }, ap);
        }

        public void AddBlockInvoices(DateTime dateStart, DateTime dateEnd, int id_dep)
        {
            for (DateTime d = dateStart; d <= dateEnd; d = d.AddDays(1))
            {
                ap.Clear();
                ap.Add(d.Date);
                ap.Add(id_dep);
                ap.Add(UserSettings.User.Id);

                executeProcedure("[spisanie].[AddBlockInvoices]",
                    new string[] { "@date", "@id_dep", "@id_editor" },
                    new DbType[] { DbType.DateTime, DbType.Int32, DbType.Int32 }, ap);
            }

        }

        public DataTable getNtypeorgJAllPrihod(int id)
        {
            ap.Clear();          
            ap.Add(id);

            return executeProcedure("[spisanie].[getNtypeorgJAllPrihod]",
                new string[] { "id" },
                new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getNtypeorgALL()
        {
            ap.Clear();
            return executeProcedure("[spisanie].[getNtypeorgALL]",
                new string[] { },
                new DbType[] {  }, ap);
        }

        public DataTable ValidateNtypeorg(int id_dep, int id_ul)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(id_ul);
            return executeProcedure("[spisanie].[ValidateNtypeorg]",
                new string[] { "@id_dep", "@ntypeorg" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public void editNtypeOrgTovar(int id_tovar, int id_ul, int type)
        {
            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(id_ul);
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            executeProcedure("[spisanie].[editNtypeOrgTovar]",
                new string[] { "@id_tovar", "@ntypeorg", "@type", "@id_user" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        #region "Договор 2670"
        public bool getPriceTovarWithPrcn(int id_tovar, DateTime date, decimal decimaltest)
        {
            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(date);
            DataTable dtResult = executeProcedure("[spisanie].[getPriceTovarWithPrcn]",
                new string[2] { "@id_tovar", "@date" },
                new DbType[2] { DbType.Int32, DbType.Date }, ap);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                decimal minPrice, maxPrice, prc;
                minPrice = (decimal)dtResult.Rows[0]["minPrice"];
                maxPrice = (decimal)dtResult.Rows[0]["maxPrice"];
                prc = (decimal)dtResult.Rows[0]["prnc"];

                if (decimaltest > maxPrice || decimaltest < minPrice)
                {
                    MessageBox.Show(TempValues.centralText($"Введённая цена выходит за\nдиапазон цены, определяемой\nпроцентом наценки {prc.ToString("0.00")}%\n"), "Проверка цены", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return false;
                }
            }
            return true;
        }

        public DataTable getTovarOutPrcnPrice(DateTime date)
        {
            ap.Clear();            
            ap.Add(date);
            return executeProcedure("[spisanie].[getTovarOutPrcnPrice]",
                new string[1] { "@date" },
                new DbType[1] {  DbType.Date }, ap);
           
        }


        #endregion
    }

    static class TempValues
    {
        public static int id_post = 0;
        public static string name_post ="";
        public static bool Error = false;

        public static string centralText(string str)
        {
            int[] arra = new int[255];
            int count = 0;
            int maxLength = 0;
            int indexF = -1;
            arra[count] = 0;
            count++;
            indexF = str.IndexOf("\n");
            arra[count] = indexF;
            while (indexF != -1)
            {
                count++;
                indexF = str.IndexOf("\n", indexF + 1);
                arra[count] = indexF;
            }
            maxLength = arra[1] - arra[0];
            for (int i = 1; i < count; i++)
            {
                if (maxLength < (arra[i] - arra[i - 1]))
                {

                    maxLength = arra[i] - arra[i - 1];
                    if (i >= 2)
                    {
                        maxLength = maxLength - 1;
                    }
                }
            }
            string newString = "";
            string buffString = "";
            for (int i = 1; i < count; i++)
            {
                if (i >= 2)
                {

                    buffString = str.Substring(arra[i - 1] + 1, (arra[i] - arra[i - 1] - 1));
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    //    buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2)*2);
                    newString += buffString + "\n";
                }
                else
                {
                    buffString = str.Substring(arra[i - 1], arra[i]);
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    // buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1])) / 2)*2);
                    newString = buffString + "\n";
                }

            }

            return newString;
        }
    }
}
