using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Spisanie
{
    public partial class frmLoad : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        
        private DataTable dtDeps;
        private DateTime closeDate;
        private DataTable dtCens;
        private DataTable dtPrihZ;

        public frmLoad()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(btClose, "Закрыть форму");
            t.SetToolTip(btSave, "Загрузить данные");
            t.SetToolTip(cbDeps, "Выберите отдел");

            dtDeps = proc.GetDeps();

            cbDeps.DataSource = dtDeps;
            cbDeps.DisplayMember = "name";
            cbDeps.ValueMember = "id";
            if (dtDeps.Rows.Count > 0)
            {
                cbDeps.SelectedIndex = 0;
            }

            DataTable dtTemp = proc.GetCloseDate();
            dtpDate.Value = (DateTime)dtTemp.Rows[0][0];
            closeDate = (DateTime)dtTemp.Rows[0][0];
            dtTemp.Dispose();
        }

        private void work(int id_dep)
        {
            #region Формирование курсоров с накладными
            int naklCount = 0;
            int doubles = 0;
            DataTable dtTovars = proc.GetInventTovars(id_dep);
            if (dtTovars==null)
            {
                return;
            }

            DataTable dtPrihod = new DataTable();
            dtPrihod.Columns.Add("id_tovar", typeof(int));
            dtPrihod.Columns.Add("netto", typeof(decimal));
            dtPrihod.Columns.Add("ntypeorg", typeof(int));

            DataTable dtPereoc = new DataTable();
            dtPereoc.Columns.Add("id_tovar", typeof(int));
            dtPereoc.Columns.Add("netto", typeof(decimal));
            dtPereoc.Columns.Add("ntypeorg", typeof(int));

            DataTable dtOtgruz = new DataTable();
            dtOtgruz.Columns.Add("id_tovar", typeof(int));
            dtOtgruz.Columns.Add("netto", typeof(decimal));
            dtOtgruz.Columns.Add("ntypeorg", typeof(int));
            dtOtgruz.Columns.Add("isCredit", typeof(int));

            DataTable dtVozvKass = new DataTable();
            dtVozvKass.Columns.Add("id_tovar", typeof(int));
            dtVozvKass.Columns.Add("netto", typeof(decimal));
            dtVozvKass.Columns.Add("ntypeorg", typeof(int));

            DataTable dtSpis = new DataTable();
            dtSpis.Columns.Add("id_tovar", typeof(int));
            dtSpis.Columns.Add("netto", typeof(decimal));
            dtSpis.Columns.Add("ntypeorg", typeof(int));
            dtSpis.Columns.Add("isCredit", typeof(int));

            DataTable dtNedost = new DataTable();
            dtNedost.Columns.Add("id_tovar", typeof(int));
            dtNedost.Columns.Add("netto", typeof(decimal));
            dtNedost.Columns.Add("delta", typeof(decimal));
            dtNedost.Columns.Add("ntypeorg", typeof(int));

            foreach (DataRow dr in dtTovars.Rows)
            {
                if ((decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString())) != 0)
                {
                    if ((decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString())) > 0)
                    {
                        if (id_dep == 6 || (id_dep == 8 && rbVozvr.Checked))
                        {
                            dtVozvKass.Rows.Add(dr["newid"], decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                        }
                        else
                        {
                            decimal allnetto = decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString());
                            DataRow[] drs = dtPrihZ.Select("id_tovar=" + dr["newid"].ToString());
                            if (drs.Count() > 0)
                            {
                                int i = 0;
                                while (allnetto > 0 && i < drs.Count())
                                {
                                    if (decimal.Parse(drs[i]["netto"].ToString()) >= allnetto)
                                    {
                                        dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                        allnetto = 0;
                                    }
                                    else
                                    {
                                        dtPrihod.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                                        allnetto -= decimal.Parse(drs[i]["netto"].ToString());
                                    }
                                    i++;
                                }
                                if (allnetto > 0)
                                {
                                    dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                }
                            }
                            else
                            {
                                dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                            }
                        }
                    }
                    else
                    {
                        if (id_dep == 6 || (id_dep == 8 && rbSpis.Checked))
                        {
                            dtSpis.Rows.Add(dr["newid"], decimal.Parse(dr["oldnetto"].ToString()) - decimal.Parse(dr["newnetto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                        }
                        else
                        {
                            decimal allnetto = - decimal.Parse(dr["newnetto"].ToString()) + decimal.Parse(dr["oldnetto"].ToString());
                            DataRow[] drs = dtPrihZ.Select("id_tovar=" + dr["newid"].ToString());
                            if (drs.Count() > 0)
                            {
                                int i = 0;
                                while (allnetto != 0 && i < drs.Count())
                                {
                                    if (Math.Abs(decimal.Parse(drs[i]["netto"].ToString())) >= allnetto)
                                    {
                                        dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                        dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                        allnetto = 0;
                                    }
                                    else
                                    {

                                        if (allnetto > 0)
                                        {
                                            dtPereoc.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                                            dtOtgruz.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                                            allnetto -= decimal.Parse(drs[i]["netto"].ToString());
                                        }
                                        else
                                        {
                                            dtPereoc.Rows.Add(dr["newid"], -decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                                            dtOtgruz.Rows.Add(dr["newid"], -decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                                            allnetto += decimal.Parse(drs[i]["netto"].ToString());
                                        }
                                    }
                                    i++;
                                }
                                if (allnetto != 0)
                                {
                                    dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                    dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                }
                            }
                            else
                            {
                                dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                                dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
                            }
                        }
                    }
                }
                if ((decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString())) != 0)
                {
                    if (decimal.Parse(dr["newnetto"].ToString()) > 0)
                    {
                        dtNedost.Rows.Add(dr["newid"], decimal.Parse(dr["newnetto"].ToString()), decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                    }
                    else
                    {
                        dtNedost.Rows.Add(dr["newid"], 0, decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
                    }
                }
            }
            dtTovars.Dispose();
            if (dtVozvKass.Rows.Count + dtNedost.Rows.Count + dtOtgruz.Rows.Count + dtPereoc.Rows.Count + dtPrihod.Rows.Count + dtSpis.Rows.Count == 0)
            {
                return;
            }
            #endregion

            #region Формирование приходов
            while (dtPrihod.Rows.Count > 0)
            {
                DataRow[] drsPrih = dtPrihod.Select("ntypeorg="+dtPrihod.Rows[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(15);
                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 1, int.Parse(drsPrih[0]["ntypeorg"].ToString()),1);
                if (dtTemp==null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }
                  Logging.Comment("Добавление шапки накладной прихода id= " +   dtTemp.Rows[0]["id"].ToString()+ " из j_allprihod");
              
                naklCount++;
                int i = 1;
                int last_id_tov = 0;
                int cntTov = 0;
                DataRow[] drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                foreach (DataRow dr in drsPrih)
                {
                    DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                    Logging.Comment("Добавление тела накладной прихода " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ПП" + i.ToString().Trim() + " из j_prihod");
                    if (last_id_tov != int.Parse(dr["id_tovar"].ToString()))
                    {
                        last_id_tov = int.Parse(dr["id_tovar"].ToString());
                        cntTov = 0;
                        drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                    }
                    proc.AddPrihod(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()), id_dep
                                    , decimal.Parse(dr["netto"].ToString()), dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ПП" , i
                                    , cntTov<drs.Count()?decimal.Parse(drs[cntTov]["zcena"].ToString()):0, decimal.Parse(rCn["rcena"].ToString()), int.Parse(rCn["id_nds"].ToString()), dtpDate.Value.Date);
                    cntTov++;
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }
                    i++;
                    dtPrihod.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление шапки накладной прихода id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

             
            #region Формирование отгрузок
            dtOtgruz = editTable(dtOtgruz).Copy();
            int OtgruzCount = dtOtgruz.Rows.Count;
            int VozvKassCount = dtVozvKass.Rows.Count;
            int SpisCount = dtSpis.Rows.Count;

            while (dtOtgruz.Rows.Count > 0)
            {
                DataRow[] drsOtgruz = dtOtgruz.Select("ntypeorg=" + dtOtgruz.Rows[0]["ntypeorg"].ToString() + " and isCredit=" + dtOtgruz.Rows[0]["isCredit"].ToString());
                //DataRow[] drsOtgruz = dtOtgruz.Select("ntypeorg=" + dtOtgruz.Rows[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(16);
                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 3, int.Parse(drsOtgruz[0]["ntypeorg"].ToString()), int.Parse(drsOtgruz[0]["isCredit"].ToString()));
                if (dtTemp == null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }
                Logging.Comment("Добавление шапки накладной отгрузки id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");

                naklCount++;
                int i = 1;
                int last_id_tov = 0;
                int cntTov = 0;
                DataRow[] drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                foreach (DataRow dr in drsOtgruz)
                {
                    Logging.Comment("Добавление тела накладной отгрузки " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОП" + i.ToString().Trim() + " из j_otgruz");

                    DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];                   
                    if (last_id_tov != int.Parse(dr["id_tovar"].ToString()))
                    {
                        last_id_tov = int.Parse(dr["id_tovar"].ToString());
                        cntTov = 0;
                        drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString() + " and isCredit=" + dr["isCredit"].ToString());
                    }
                    proc.AddOtgruz(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()), id_dep
                                    , decimal.Parse(dr["netto"].ToString()),dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОП", i
                                    , cntTov < drs.Count() ? decimal.Parse(drs[cntTov]["zcena"].ToString()) : 0 
                                    , cntTov < drs.Count() ? decimal.Parse(drs[cntTov]["zcena"].ToString()) : 0, int.Parse(rCn["id_nds"].ToString()), dtpDate.Value.Date);
                    cntTov++;
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }
                    i++;
                    dtOtgruz.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление шапки накладной отгрузки id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

            #region Формирование возвратов с касс
            while (dtVozvKass.Rows.Count > 0)
            {
                DataRow[] drsVozvKass = dtVozvKass.Select("ntypeorg=" + dtVozvKass.Rows[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(19);

                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 4, int.Parse(drsVozvKass[0]["ntypeorg"].ToString()),1);
                if (dtTemp == null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }
                Logging.Comment("Добавление шапки накладной возврата с касс id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                naklCount++;

                //акт переоценки для возврата с касс - шапка
                DataTable dtTempPereoc = proc.AddAllPrihod(dtpDate.Value.Date, 7, int.Parse(drsVozvKass[0]["ntypeorg"].ToString()),1);
                if (dtTempPereoc == null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }
                Logging.Comment("Добавление шапки накладной переоценки id= " + dtTempPereoc.Rows[0]["id"].ToString() + " из j_allprihod");
                naklCount++;

                int i = 1;
                foreach (DataRow dr in drsVozvKass)
                {
                    Logging.Comment("Добавление тела возврата с касс " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "БР" + i.ToString().Trim() + " из j_vozvkass");
                    DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                    proc.AddVozvKass(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()),id_dep
                                    , decimal.Parse(dr["netto"].ToString()), dtTemp.Rows[0]["vnudok"].ToString().Trim() + "БР" + i.ToString().Trim()
                                    , decimal.Parse(rCn["zcena"].ToString()), decimal.Parse(rCn["rcena"].ToString()));
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }

                    //акт переоценки для возвратов с касс - тело
                    Logging.Comment("Добавление тела переоценки " + dtTempPereoc.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim() + " из j_pereoc");
                    proc.AddPereoc(int.Parse(dtTempPereoc.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()), id_dep
                                   , decimal.Parse(dr["netto"].ToString()), dtTempPereoc.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim()
                                   , decimal.Parse(rCn["rcena"].ToString()), decimal.Parse(rCn["zcena"].ToString()), int.Parse(rCn["id_nds"].ToString()));
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }
                    doubles++;

                    i++;
                    dtVozvKass.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление накладной возврата с касс id= " + dtTemp.Rows[0]["id"].ToString()+" из j_allprihod");
                Logging.Comment("Завершено добавление накладной переоценки id= " + dtTempPereoc.Rows[0]["id"].ToString() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

            #region Формирование списаний
            dtSpis = editTable(dtSpis).Copy();            
            while (dtSpis.Rows.Count > 0)
            {
                DataRow[] drsSpis = dtSpis.Select("ntypeorg=" + dtSpis.Rows[0]["ntypeorg"].ToString() + " and isCredit=" + dtSpis.Rows[0]["isCredit"].ToString());
                //DataRow[] drsSpis = dtSpis.Select("ntypeorg=" + dtSpis.Rows[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(17);

                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 5, int.Parse(drsSpis[0]["ntypeorg"].ToString()), int.Parse(drsSpis[0]["isCredit"].ToString()));
                if (dtTemp==null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }

                Logging.Comment("Добавление шапки акта списания id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
               
                naklCount++;
                int i = 1;
                foreach (DataRow dr in drsSpis)
                {
                    Logging.Comment("Добавление тела акта списания " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "АС" + i.ToString().Trim()+" из j_spis0.");
                    
                    DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                    proc.AddSpis(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()),id_dep
                                , decimal.Parse(dr["netto"].ToString()), dtTemp.Rows[0]["vnudok"].ToString().Trim() + "АС" + i.ToString().Trim()
                                , decimal.Parse(rCn["zcena"].ToString()), decimal.Parse(rCn["rcena"].ToString()), int.Parse(rCn["id_nds"].ToString()));
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }
                    i++;
                    dtSpis.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление шапки акта списания id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

            #region Формирование переоценок

            while (dtPereoc.Rows.Count > 0)
            {
                DataRow[] drsPereoc = dtPereoc.Select("ntypeorg=" + dtPereoc.Rows[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(18);
                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 2, int.Parse(drsPereoc[0]["ntypeorg"].ToString()),1);
                if (dtTemp == null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }

                Logging.Comment("Добавление шапки акта переоценки id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");

                naklCount++;
                int i = 1;
                int last_id_tov = 0;
                int cntTov = 0;
                DataRow[] drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                foreach (DataRow dr in drsPereoc)
                {
                    Logging.Comment("Добавление тела акта переоценки " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim() + " из j_pereoc");

                    DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                    if (last_id_tov != int.Parse(dr["id_tovar"].ToString()))
                    {
                        last_id_tov = int.Parse(dr["id_tovar"].ToString());
                        cntTov = 0;
                        drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                    }
                    proc.AddPereoc(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()), id_dep
                                    , decimal.Parse(dr["netto"].ToString()), dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim()
                                    , cntTov < drs.Count() ? decimal.Parse(drs[cntTov]["zcena"].ToString()) : 0, decimal.Parse(rCn["rcena"].ToString()), int.Parse(rCn["id_nds"].ToString()));
                    cntTov++;
                    if (TempValues.Error)
                    {
                        Logging.StartFirstLevel(8);
                        proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                        Logging.StopFirstLevel();
                        return;
                    }
                    i++;
                    dtPereoc.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление шапки акта переоценки id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

            #region Формирование переоценок по недостаче
            while (dtNedost.Rows.Count > 0)
            {
                DataRow[] drsNedost = dtNedost.Select("ntypeorg=" + dtNedost.Rows[0]["ntypeorg"].ToString());
                int NtypeOrg = int.Parse(drsNedost[0]["ntypeorg"].ToString());
                Logging.StartFirstLevel(18);
                DataTable dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 6, int.Parse(drsNedost[0]["ntypeorg"].ToString()),1);
                if (dtTemp==null)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                    Logging.StopFirstLevel();
                    return;
                }
                Logging.Comment("Добавление шапки акта переоценки по недосдаче id= " + dtTemp.Rows[0]["id"].ToString() + " из j_allprihod");
                naklCount++;
                int i = 1;
                decimal s = 0;
                foreach (DataRow dr in drsNedost)
                {
                    if (Math.Abs(s + decimal.Parse(dr["delta"].ToString())) > 30000)
                    {
                        decimal k = decimal.Parse(dr["delta"].ToString());                        
                        while (k != 0)
                        {
                            dtTemp = proc.AddAllPrihod(dtpDate.Value.Date, 6, NtypeOrg, 1);
                            if (dtTemp==null)
                            {
                                Logging.StartFirstLevel(8);
                                proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                                Logging.StopFirstLevel();
                                return;
                            }
                            naklCount++;
                            i = 1;
                            s = 0;
                            decimal tdelta = 0;
                            if (k > 30000)
                            {
                                doubles++;
                                tdelta = 30000;
                                s += k;
                                k -= 30000;
                                if (k < 0)
                                {
                                    k = 0;
                                }
                            }
                            else
                            {
                                if (k < -30000)
                                {
                                    doubles++;
                                    tdelta=-30000;
                                    s += k;
                                    k += 30000;
                                    if (k > 0)
                                    {
                                        k = 0;
                                    }
                                }
                                else
                                {
                                    tdelta=k;
                                    s += k;
                                    k = 0;
                                }
                            }
                           
                            Logging.Comment("Добавление тела акта переоценки по недосдаче " + dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim() +" из j_pereoc");

                            
                            int ttype = 0;
                            if (decimal.Parse(dr["netto"].ToString()) != 0)
                            {
                                ttype=1;
                            }
                            else
                            {
                                ttype=2;
                            }
                            DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                            proc.AddNedost(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()),id_dep
                                            , decimal.Parse(dr["netto"].ToString()), tdelta, dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim(), ttype
                                            , decimal.Parse(rCn["zcena"].ToString()), decimal.Parse(rCn["rcena"].ToString()), int.Parse(rCn["id_nds"].ToString()));
                            if (TempValues.Error)
                            {
                                return;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        int ttype = 0;
                        if (decimal.Parse(dr["netto"].ToString()) != 0)
                        {
                           ttype=1;
                        }
                        else
                        {
                            ttype=2;
                        }
                        Logging.Comment("Добавление тела акта переоценки по недосдаче " +
                                        dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim() + " из j_pereoc");
                        
                        DataRow rCn = dtCens.Select("id_tovar=" + dr["id_tovar"].ToString())[0];
                        proc.AddNedost(int.Parse(dtTemp.Rows[0]["id"].ToString()), int.Parse(dr["id_tovar"].ToString()),id_dep
                                        , decimal.Parse(dr["netto"].ToString()), decimal.Parse(dr["delta"].ToString())
                                        , dtTemp.Rows[0]["vnudok"].ToString().Trim() + "ОЦ" + i.ToString().Trim(), ttype
                                        , decimal.Parse(rCn["zcena"].ToString()), decimal.Parse(rCn["rcena"].ToString()), int.Parse(rCn["id_nds"].ToString()));
                        if (TempValues.Error)
                        {
                            return;
                        }
                        i++;
                        s += decimal.Parse(dr["delta"].ToString());
                    }
                    dtNedost.Rows.Remove(dr);
                }
                dtTemp.Dispose();
                Logging.Comment("Завершено добавление шапки акта переоценки по недосдаче id= " + dtTemp.Rows[0]["id"].ToString()+" из j_allprihod");
                Logging.StopFirstLevel();
            }
            #endregion

            #region Проверка заливки накладных на сервер
            DataTable dtStat = proc.GetStatistics(dtpDate.Value.Date, id_dep);
            if (dtStat == null)
            {
                return;
            }
            if (int.Parse(dtStat.Rows[0]["allprihods"].ToString()) == 0
                && int.Parse(dtStat.Rows[0]["prihod"].ToString()) == 0
                && int.Parse(dtStat.Rows[0]["otgruz"].ToString()) == 0
                && int.Parse(dtStat.Rows[0]["pereoc"].ToString()) == 0
                && int.Parse(dtStat.Rows[0]["vozvkass"].ToString()) == 0
                && int.Parse(dtStat.Rows[0]["spis"].ToString()) == 0)
            {
                MessageBox.Show("Загрузите данные повторно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempValues.Error = true;
                return;
            }
            if (int.Parse(dtStat.Rows[0]["allprihods"].ToString()) != naklCount
                //|| int.Parse(dtStat.Rows[0]["prihod"].ToString()) != dtPrihod.Rows.Count
                || int.Parse(dtStat.Rows[0]["otgruz"].ToString()) != OtgruzCount
                //|| int.Parse(dtStat.Rows[0]["pereoc"].ToString()) != dtPereoc.Rows.Count + dtNedost.Rows.Count + doubles
                || int.Parse(dtStat.Rows[0]["vozvkass"].ToString()) != VozvKassCount
                || int.Parse(dtStat.Rows[0]["spis"].ToString()) != SpisCount)
            {
                MessageBox.Show("Ошибка записи в таблицы!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logging.StartFirstLevel(8);
                proc.DeleteNakls(id_dep, dtpDate.Value.Date);
                Logging.StopFirstLevel();
                if (TempValues.Error)
                {
                    return;
                }
                MessageBox.Show("Загрузите данные повторно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempValues.Error = true;
                return;
            }
            #endregion
            proc.ChangeCloseDate(id_dep);
        }

        private void btSave_Click_1(object sender, EventArgs e)
        {
            TempValues.Error = false;

            #region Проверки
            DataTable temp = proc.GetPost(0, 1);
            if (temp == null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-поставщик удалена!");
                return;
            }

            temp = proc.GetPost(0, 2);
            if (temp == null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-покупатель удалена!");
                return;
            }
            
            temp = proc.GetPost(0, 0);
            if (temp == null)
            {
                return;
            }
            
            if (closeDate.Date != dtpDate.Value.Date)
            {
                DialogResult d = MessageBox.Show("Дата формирования накладных\nдолжна быть \"" + closeDate.ToShortDateString() + "\"!\nХотите изменить дату?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (d == DialogResult.Yes)
                {
                    return;
                }
            }

            DataTable dtTemp = proc.GetSpisDate();
            if (dtTemp==null)
            {
                return;
            }
            if (int.Parse(cbDeps.SelectedValue.ToString()) == 0)
            {
                DataRow[] rows = dtTemp.Select("date = \'" + dtpDate.Value.ToShortDateString() + "\'");
                if (rows.Count() > 0)
                {
                    string str = "";
                    foreach (DataRow dr in rows)
                    {
                        str += "\"" + dr["name"].ToString().Trim() + "\", ";
                    }
                    DialogResult d = MessageBox.Show("Накладные для отделов " + str.Substring(0, str.Length - 2) + "\nуже сформированы!\n\nПереформировать накладные?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (d == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (dtTemp.Select("date = \'" + dtpDate.Value.ToShortDateString() + "\' and id = " + cbDeps.SelectedValue.ToString().Trim()).Count() > 0)
                {
                    DialogResult d = MessageBox.Show("Накладные для отдела \"" + cbDeps.Text.Trim() + "\"\nуже сформированы!\n\nПереформировать накладные?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (d == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            // проверка существования поставщика
            temp = proc.GetPost(0, 1);
            if (temp==null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-поставщик удалена!");
                return;
            }           

            temp = proc.GetPost(0, 2);
            if (temp==null)
            {
                return;
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("Организация-покупатель удалена!");
                return;
            }

            dtTemp.Dispose();
            #endregion

            #region Удаление старых накладных
            wait.Visible = true;
            this.Refresh();
            if (int.Parse(cbDeps.SelectedValue.ToString()) == 0)
            {
                foreach (DataRow dr in dtDeps.Rows)
                {
                    Logging.StartFirstLevel(8);
                    proc.DeleteNakls(int.Parse(dr["id"].ToString()), dtpDate.Value);
                    Logging.StopFirstLevel();
                    if (TempValues.Error)
                    {
                        wait.Visible = false;
                        this.Refresh();
                        return;
                    }
                }
            }
            else
            {
                Logging.StartFirstLevel(8);
                proc.DeleteNakls(int.Parse(cbDeps.SelectedValue.ToString()), dtpDate.Value);
                Logging.StopFirstLevel();
                if (TempValues.Error)
                {
                    wait.Visible = false;
                    this.Refresh();
                    return;
                }
            }
            #endregion
            #region Формирование накладных
            dtCens = proc.GetCen();
            if (dtCens == null)
            {
                MessageBox.Show("Не удалось получить цены по товарам!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }          
            dtPrihZ = proc.GetPrihZcena(dtpDate.Value.Date);
            if (dtPrihZ == null)
            {
                MessageBox.Show("Нет доступа к накладным прихода!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(cbDeps.SelectedValue.ToString()) == 0)
            {
                foreach (DataRow dr in dtDeps.Rows)
                {
                    if (int.Parse(dr["id"].ToString()) != 0)
                    {
                        work(int.Parse(dr["id"].ToString()));
                        if (TempValues.Error)
                        {
                            wait.Visible = false;
                            this.Refresh();
                            return;
                        }
                        else
                        {
                            proc.BlockPeriod_forLoadForm(int.Parse(dr["id"].ToString()), dtpDate.Value);
                        }
                    }
                }
            }
            else
            {
                work(int.Parse(cbDeps.SelectedValue.ToString()));
                if (TempValues.Error)
                {
                    wait.Visible = false;
                    this.Refresh();
                    return;
                }
                else
                {
                    proc.BlockPeriod_forLoadForm(int.Parse(cbDeps.SelectedValue.ToString()), dtpDate.Value);
                }
            }
            int gen = GC.GetGeneration(dtCens);
            GC.Collect(gen);
            gen = GC.GetGeneration(dtPrihZ);
            GC.Collect(gen);
            dtCens.Rows.Clear();
            dtCens.Dispose();
            dtPrihZ.Rows.Clear();
            dtPrihZ.Dispose();
            #endregion
            MessageBox.Show("Данные загружены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataTable dtZero = proc.GetZeroDocs(dtpDate.Value.Date, int.Parse(cbDeps.SelectedValue.ToString()));
            if (dtZero.Rows.Count > 0)
            {
                frmZero zero = new frmZero(dtZero);
                zero.ShowDialog();
            }
            gen = GC.GetGeneration(dtZero);
            GC.Collect(gen);
            dtZero.Rows.Clear();
            dtZero.Dispose();
            TempValues.Error = false;
            wait.Visible = false;
            this.Refresh();
        }

        private void cbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (int.Parse(cbDeps.SelectedValue.ToString()) == 0
                || int.Parse(cbDeps.SelectedValue.ToString()) == 8)
            {
                gbIn.Enabled = true;
                gbOut.Enabled = true;
            }
            else
            {
                gbIn.Enabled = false;
                gbOut.Enabled = false;
            }
        }

        private void rbPrih_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrih.Checked)
            {
                rbOtgruz.Checked = true;
                rbSpis.Checked = false;
            }
            else
            {
                rbOtgruz.Checked = false;
                rbSpis.Checked = true;
            }
        }

        private void rbOtgruz_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOtgruz.Checked)
            {
                rbPrih.Checked = true;
                rbVozvr.Checked = false;
            }
            else
            {
                rbPrih.Checked = false;
                rbVozvr.Checked = true;
            }
        }
     

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    proc.DeleteNakls(int.Parse(cbDeps.SelectedValue.ToString()), dtpDate.Value);
        //    int id_dep = int.Parse(cbDeps.SelectedValue.ToString());
        //    dtCens = proc.GetCen();
        //    dtPrihZ = proc.GetPrihZcena(dtpDate.Value.Date);
        //    #region Формирование курсоров с накладными
        //    int naklCount = 0;
        //    int doubles = 0;
        //    DataTable dtTovars = proc.GetInventTovars(id_dep);
        //    if (dtTovars == null)
        //    {
        //        return;
        //    }

        //    DataTable dtPrihod = new DataTable();
        //    dtPrihod.Columns.Add("id_tovar", typeof(int));
        //    dtPrihod.Columns.Add("netto", typeof(decimal));
        //    dtPrihod.Columns.Add("ntypeorg", typeof(int));

        //    DataTable dtPereoc = new DataTable();
        //    dtPereoc.Columns.Add("id_tovar", typeof(int));
        //    dtPereoc.Columns.Add("netto", typeof(decimal));
        //    dtPereoc.Columns.Add("ntypeorg", typeof(int));

        //    DataTable dtOtgruz = new DataTable();
        //    dtOtgruz.Columns.Add("id_tovar", typeof(int));
        //    dtOtgruz.Columns.Add("netto", typeof(decimal));
        //    dtOtgruz.Columns.Add("ntypeorg", typeof(int));
        //    dtOtgruz.Columns.Add("isCredit", typeof(int));

        //    DataTable dtVozvKass = new DataTable();
        //    dtVozvKass.Columns.Add("id_tovar", typeof(int));
        //    dtVozvKass.Columns.Add("netto", typeof(decimal));
        //    dtVozvKass.Columns.Add("ntypeorg", typeof(int));

        //    DataTable dtSpis = new DataTable();
        //    dtSpis.Columns.Add("id_tovar", typeof(int));
        //    dtSpis.Columns.Add("netto", typeof(decimal));
        //    dtSpis.Columns.Add("ntypeorg", typeof(int));

        //    DataTable dtNedost = new DataTable();
        //    dtNedost.Columns.Add("id_tovar", typeof(int));
        //    dtNedost.Columns.Add("netto", typeof(decimal));
        //    dtNedost.Columns.Add("delta", typeof(decimal));
        //    dtNedost.Columns.Add("ntypeorg", typeof(int));

        //    foreach (DataRow dr in dtTovars.Rows)
        //    {
        //        if ((decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString())) != 0)
        //        {
        //            if ((decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString())) > 0)
        //            {
        //                if (id_dep == 6 || (id_dep == 8 && rbVozvr.Checked))
        //                {
        //                    dtVozvKass.Rows.Add(dr["newid"], decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                }
        //                else
        //                {
        //                    decimal allnetto = decimal.Parse(dr["newnetto"].ToString()) - decimal.Parse(dr["oldnetto"].ToString());
        //                    DataRow[] drs = dtPrihZ.Select("id_tovar=" + dr["newid"].ToString());
        //                    if (drs.Count() > 0)
        //                    {
        //                        int i = 0;
        //                        while (allnetto > 0 && i < drs.Count())
        //                        {
        //                            if (decimal.Parse(drs[i]["netto"].ToString()) >= allnetto)
        //                            {
        //                                dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                                allnetto = 0;
        //                            }
        //                            else
        //                            {
        //                                dtPrihod.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                                allnetto -= decimal.Parse(drs[i]["netto"].ToString());
        //                            }
        //                            i++;
        //                        }
        //                        if (allnetto > 0)
        //                        {
        //                            dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dtPrihod.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (id_dep == 6 || (id_dep == 8 && rbSpis.Checked))
        //                {
        //                    dtSpis.Rows.Add(dr["newid"], decimal.Parse(dr["oldnetto"].ToString()) - decimal.Parse(dr["newnetto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                }
        //                else
        //                {
        //                    decimal allnetto = -decimal.Parse(dr["newnetto"].ToString()) + decimal.Parse(dr["oldnetto"].ToString());
        //                    DataRow[] drs = dtPrihZ.Select("id_tovar=" + dr["newid"].ToString());
        //                    if (drs.Count() > 0)
        //                    {
        //                        int i = 0;
        //                        while (allnetto != 0 && i < drs.Count())
        //                        {
        //                            if (Math.Abs(decimal.Parse(drs[i]["netto"].ToString())) >= allnetto)
        //                            {
        //                                dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                                dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                                allnetto = 0;
        //                            }
        //                            else
        //                            {

        //                                if (allnetto > 0)
        //                                {
        //                                    dtPereoc.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                                    dtOtgruz.Rows.Add(dr["newid"], decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                                    allnetto -= decimal.Parse(drs[i]["netto"].ToString());
        //                                }
        //                                else
        //                                {
        //                                    dtPereoc.Rows.Add(dr["newid"], -decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                                    dtOtgruz.Rows.Add(dr["newid"], -decimal.Parse(drs[i]["netto"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //                                    allnetto += decimal.Parse(drs[i]["netto"].ToString());
        //                                }
        //                            }
        //                            i++;
        //                        }
        //                        if (allnetto != 0)
        //                        {
        //                            dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                            dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dtPereoc.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                        dtOtgruz.Rows.Add(dr["newid"], allnetto, int.Parse(dr["ntypeorg"].ToString()));
        //                    }
        //                }
        //            }
        //        }
        //        if ((decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString())) != 0)
        //        {
        //            if (decimal.Parse(dr["newnetto"].ToString()) > 0)
        //            {
        //                dtNedost.Rows.Add(dr["newid"], decimal.Parse(dr["newnetto"].ToString()), decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //            }
        //            else
        //            {
        //                dtNedost.Rows.Add(dr["newid"], 0, decimal.Parse(dr["newsumma"].ToString()) - decimal.Parse(dr["oldsumma"].ToString()) - decimal.Parse(dr["nedosum"].ToString()), int.Parse(dr["ntypeorg"].ToString()));
        //            }
        //        }
        //    }
        //    dtTovars.Dispose();
        //    if (dtVozvKass.Rows.Count + dtNedost.Rows.Count + dtOtgruz.Rows.Count + dtPereoc.Rows.Count + dtPrihod.Rows.Count + dtSpis.Rows.Count == 0)
        //    {
        //        return;
        //    }
        //    #endregion
        //    int OtgruzCount = dtOtgruz.Rows.Count;

        //    dtOtgruz = editTable(dtOtgruz).Copy();
        //    /*DataTable dtOtgruz_Buff = new DataTable();
        //    dtOtgruz_Buff.Columns.Add("id_tovar", typeof(int));
        //    dtOtgruz_Buff.Columns.Add("netto", typeof(decimal));
        //    dtOtgruz_Buff.Columns.Add("ntypeorg", typeof(int));
        //    dtOtgruz_Buff.Columns.Add("isCredit", typeof(int));


        //    while (dtOtgruz.Rows.Count > 0)
        //    {
        //        DataRow[] drsOtgruz = dtOtgruz.Select("ntypeorg=" + dtOtgruz.Rows[0]["ntypeorg"].ToString());
        //        int i = 1;
        //        int last_id_tov = 0;
        //        int cntTov = 0;
        //        DataRow[] drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
        //        foreach (DataRow dr in drsOtgruz)
        //        {
        //            if (last_id_tov != int.Parse(dr["id_tovar"].ToString()))
        //            {
        //                last_id_tov = int.Parse(dr["id_tovar"].ToString());
        //                cntTov = 0;
        //                drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
        //            }
        //            if(cntTov==1)
        //                Console.WriteLine(1);
        //            dr["isCredit"] = drs[cntTov]["isCredit"].ToString();
        //            dtOtgruz_Buff.ImportRow(dr);
        //            cntTov++;
        //            if (TempValues.Error)
        //            {
        //                Logging.StartFirstLevel(8);
        //                proc.DeleteNakls(id_dep, dtpDate.Value.Date);
        //                Logging.StopFirstLevel();
        //                return;
        //            }
        //            i++;
        //            dtOtgruz.Rows.Remove(dr);
        //        }
        //    }
        //    dtOtgruz_Buff.AcceptChanges();
        //    dtOtgruz.Merge(dtOtgruz_Buff);*/

        //    while (dtOtgruz.Rows.Count > 0)
        //    {
        //        DataRow[] drsOtgruz = dtOtgruz.Select("ntypeorg=" + dtOtgruz.Rows[0]["ntypeorg"].ToString() + " and isCredit=" + dtOtgruz.Rows[0]["isCredit"].ToString());
        //        foreach (DataRow dr in drsOtgruz)
        //        {
        //            dtOtgruz.Rows.Remove(dr);
        //        }
        //    }
        //}


        private DataTable editTable(DataTable table)
        {
            DataTable dtOtgruz_Buff = new DataTable();
            dtOtgruz_Buff.Columns.Add("id_tovar", typeof(int));
            dtOtgruz_Buff.Columns.Add("netto", typeof(decimal));
            dtOtgruz_Buff.Columns.Add("ntypeorg", typeof(int));
            dtOtgruz_Buff.Columns.Add("isCredit", typeof(int));

            while (table.Rows.Count > 0)
            {
                DataRow[] drsOtgruz = table.Select("ntypeorg=" + table.Rows[0]["ntypeorg"].ToString());
                int i = 1;
                int last_id_tov = 0;
                int cntTov = 0;
                DataRow[] drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                foreach (DataRow dr in drsOtgruz)
                {
                    if (last_id_tov != int.Parse(dr["id_tovar"].ToString()))
                    {
                        last_id_tov = int.Parse(dr["id_tovar"].ToString());
                        cntTov = 0;
                        drs = dtPrihZ.Select("id_tovar=" + last_id_tov.ToString());
                        if(drs.Length==1)
                            Console.WriteLine(1);
                    }
                    if (cntTov == 1)
                        Console.WriteLine(1);
                    if (drs.Length <= cntTov && drs.Length != 0)
                        dr["isCredit"] = drs[drs.Length - 1]["isCredit"].ToString();
                    else
                    {
                        if (drs.Length == 0)
                        {
                            dr["isCredit"] = "1";
                        }
                        else
                        {
                            dr["isCredit"] = drs[cntTov]["isCredit"].ToString();
                        }
                    }
                    dtOtgruz_Buff.ImportRow(dr);
                    cntTov++;
                   
                    i++;
                    table.Rows.Remove(dr);
                }
            }
            return dtOtgruz_Buff;
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
          
        //}

        //private void button1_Click_2(object sender, EventArgs e)
        //{
        //    DataTable dtTemp = proc.GetCloseDate();
        //    //closeDate -  DateLastInv
        //    //dateStart - DateInvSpis 
        //    //closeDate = (DateTime)dtTemp.Rows[0][0];
        //    closeDate = (DateTime)dtTemp.Rows[0]["old_dinv"];
        //    DateTime DateStartMonth = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);
        //   DateTime DateEndLastMonth = DateStartMonth.AddDays(-1);
        //    //            DateEndMonth = DateEndLastMonth.AddMonths(1);
        //    //DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
        //    DateTime DateEndMonth = new DateTime();
        //    if (DateEndLastMonth.Month > 10)
        //    {
        //        DateEndMonth = new DateTime(DateTime.Now.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
        //    }
        //    else
        //    {
        //        DateEndMonth = new DateTime(DateEndLastMonth.Year, DateEndLastMonth.AddMonths(2).Month, 1).AddDays(-1);
        //    }

        //    //TimeSpan ts = DateEndMonth - dateStart.Value;
        //    // Difference in days.
        //    //int differenceInDays = ts.Days;
        //    //differenceInDays = Math.Abs(differenceInDays);

        //    DateTime d2 = DateEndMonth.Date;
        //    DateTime d1 = dtpDate.Value.Date;
        //    long time = 0;

        //    time = d2.Ticks - d1.Ticks;
        //    DateTime time2 = new DateTime(time);
        //    long countDays = time2.Day - 1;
        //    //label4.Text = DateStartMonth + "   \r\n" + DateEndLastMonth + " \r\n " + DateEndMonth + "\r\n" + countDays;


        //    //if (countDays <= 14)
        //    //{
        //    //    MessageBox.Show(" c " + closeDate.ToShortDateString() + " по " + DateEndMonth.ToShortDateString());
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show(" c " + closeDate.ToShortDateString() + " по " + DateEndLastMonth.ToShortDateString());
        //    //}

        //    dtTemp.Dispose();
        //}

    }
}
