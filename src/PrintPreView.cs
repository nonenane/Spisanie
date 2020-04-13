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
    public partial class frmPrintPreView : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
 
        private int id, callType;
        private bool spis = false, pereoc = false, nakl = false, sf = false;
        private DataTable dtRezult;
        private DateTime dateOt;

        public frmPrintPreView(DataTable dt, DateTime date)
        {
            callType = 5;
            dateOt = date;
            dtRezult = dt;
            InitializeComponent(); 
            tabPage1.Text = "Бухгалтерские остатки";
            tabPage2.Dispose();
        }

        public frmPrintPreView(int Id, int Call, bool ch1, bool ch2)
        {
            id = Id;
            callType = Call;
            InitializeComponent();
            switch (Call)
            {
                case 1:
                case 2:
                    nakl = ch1;
                    sf = ch2;
                    break;
                case 3:
                    pereoc = ch1;
                    tabPage1.Text = "Акт переоценки";
                    tabPage2.Dispose();
                    break;
                case 4:
                    spis = ch1;
                    tabPage1.Text = "Акт списания";
                    tabPage2.Dispose();
                    break;
            }
        }

        private void frmPrintPreView_Load(object sender, EventArgs e)
        {
            DataTable zag = new DataTable();
            DataTable zagour = new DataTable();
            if (callType != 5)
            {
                zag = proc.GetPrintTitle(id);
                if (zag == null || zag.Rows.Count == 0)
                {
                    MessageBox.Show("Не удалось получить заголовок накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                zagour = proc.GetOurOrg((DateTime)zag.Rows[0]["dprihod"], int.Parse(zag.Rows[0]["ntypeorg"].ToString()));
            }

            #region накладная прихода
            if (nakl && callType == 1)
            {
                dsReports ds = new dsReports();

                DataTable temp = proc.GetPrintedPrih(id);

                int page = 1;
                
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    if ((page==1
                        && i == 8)
                    || (System.Data.SqlTypes.SqlInt32.Mod(i-7, 17) == 1 && page != 1))
                    {
                        page++;
                    }
                     ds.Tables["dtNakl"].Rows.Add(temp.Rows[i]["npp"].ToString().Trim()
                                        , temp.Rows[i]["cname"].ToString().Trim()
                                        , temp.Rows[i]["cunit"].ToString().Trim()
                                        , decimal.Parse(temp.Rows[i]["netto"].ToString()).ToString("N3")
                                        , ((decimal)temp.Rows[i]["zcena"]).ToString("N2")
                                        , ((decimal)temp.Rows[i]["sum"]).ToString("N2")
                                        , temp.Rows[i]["nds"].ToString().Trim()
                                        , ((decimal)temp.Rows[i]["sumnds"]).ToString("N2")
                                        , decimal.Parse(temp.Rows[i]["sumwnds"].ToString())
                                        , page);
                }

                crNaklmain reportNakl = new crNaklmain();
                string Gruzootpr = zag.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zag.Rows[0]["inn"].ToString().Trim() 
                                        + ", " + zag.Rows[0]["adressP"].ToString().Trim();
                string GruzoPoluch = zagour.Rows[0]["cname"].ToString().Trim() + ", " + zagour.Rows[0]["inn"].ToString().Trim() 
                                        + ", " + zagour.Rows[0]["adressP"].ToString().Trim();
                string Plat = zagour.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zagour.Rows[0]["inn"].ToString().Trim()
                                        + ", КПП " + zagour.Rows[0]["kpp"].ToString().Trim() +", " + zagour.Rows[0]["adressU"].ToString().Trim()
                                        + ", Р/сч " + zagour.Rows[0]["rsch"].ToString().Trim() + ", " + zagour.Rows[0]["bank"].ToString().Trim()
                                        + ", БИК " + zagour.Rows[0]["bic"].ToString().Trim()
                                        + ", Корр/сч " + zagour.Rows[0]["ksch"].ToString().Trim();
                string Post = zag.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zag.Rows[0]["inn"].ToString().Trim()
                                        + ", КПП " + zag.Rows[0]["kpp"].ToString().Trim() + ", " + zag.Rows[0]["adressU"].ToString().Trim()
                                        + ", Р/сч " + zag.Rows[0]["rsch"].ToString().Trim() + ", " + zag.Rows[0]["bank"].ToString().Trim()
                                        + ", БИК " + zag.Rows[0]["bic"].ToString().Trim()
                                        + ", Корр/сч " + zag.Rows[0]["ksch"].ToString().Trim();

                reportNakl.DataDefinition.FormulaFields["ttn"].Text = "\""+zag.Rows[0]["ttn"].ToString().Trim()+"\"";
                reportNakl.DataDefinition.FormulaFields["date"].Text = "\"" + ((DateTime)zag.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportNakl.DataDefinition.FormulaFields["okpo"].Text = "\"" + zag.Rows[0]["okpo"].ToString().Trim() + "\"";
                reportNakl.DataDefinition.FormulaFields["okpo2"].Text = "\"" + zag.Rows[0]["okpo"].ToString().Trim() + "\"";
                reportNakl.DataDefinition.FormulaFields["owner"].Text = "\"" + Gruzootpr.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["gruz"].Text = "\"" + GruzoPoluch.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["post"].Text = "\"" + Post.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["plat"].Text = "\"" + Plat.Replace("\"", "\'") + "\"";

                decimal x = decimal.Round(Convert.ToDecimal(temp.Compute("SUM(sumwnds)", "")), 3);
                reportNakl.DataDefinition.FormulaFields["sum"].Text = "\"" + Conv.GetDecimalToString(x, true, false, false)+"\"";
                reportNakl.DataDefinition.FormulaFields["rukov"].Text = "\"" + zag.Rows[0]["rukov"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["glavbuh"].Text = "\"" + zag.Rows[0]["glavbuh"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["primech1"].Text = "\"" + zag.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["primech2"].Text = "\"" + zag.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\""; 

                reportNakl.SetDataSource(ds);
                crvReport.ReportSource = reportNakl;
            }
            #endregion

            #region Накладная отгрузки
            if (nakl && callType == 2)
            {
                dsReports ds = new dsReports();

                DataTable temp = proc.GetPrintOtgruz(id);

                int page = 1;

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    if ((page == 1
                        && i == 8)
                    || (System.Data.SqlTypes.SqlInt32.Mod(i - 7, 17) == 1 && page != 1))
                    {
                        page++;
                    }
                    ds.Tables["dtNakl"].Rows.Add(temp.Rows[i]["npp"].ToString().Trim()
                                       , temp.Rows[i]["cname"].ToString().Trim()
                                       , temp.Rows[i]["cunit"].ToString().Trim()
                                       , temp.Rows[i]["netto"].ToString().Trim()
                                       , ((decimal)temp.Rows[i]["zcena"]).ToString("N2")
                                       , ((decimal)temp.Rows[i]["sum"]).ToString("N2")
                                       , temp.Rows[i]["nds"].ToString().Trim()
                                       , ((decimal)temp.Rows[i]["sumnds"]).ToString("N2")
                                       , ((decimal)temp.Rows[i]["sumwnds"]).ToString("N2")
                                       , page);
                }

                crNaklmain reportNakl = new crNaklmain();
                string GruzoPoluch = zag.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zag.Rows[0]["inn"].ToString().Trim()
                                       + ", " + zag.Rows[0]["adressP"].ToString().Trim();
                string Gruzootpr = zagour.Rows[0]["cname"].ToString().Trim() + ", " + zagour.Rows[0]["inn"].ToString().Trim()
                                        + ", " + zagour.Rows[0]["adressP"].ToString().Trim();
                string Post = zagour.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zagour.Rows[0]["inn"].ToString().Trim()
                                        + ", КПП " + zagour.Rows[0]["kpp"].ToString().Trim() + ", " + zagour.Rows[0]["adressU"].ToString().Trim()
                                        + ", Р/сч " + zagour.Rows[0]["rsch"].ToString().Trim() + ", " + zagour.Rows[0]["bank"].ToString().Trim()
                                        + ", БИК " + zagour.Rows[0]["bic"].ToString().Trim()
                                        + ", Корр/сч " + zagour.Rows[0]["ksch"].ToString().Trim();
                string Plat = zag.Rows[0]["cname"].ToString().Trim() + ", ИНН " + zag.Rows[0]["inn"].ToString().Trim()
                                        + ", КПП " + zag.Rows[0]["kpp"].ToString().Trim() + ", " + zag.Rows[0]["adressU"].ToString().Trim()
                                        + ", Р/сч " + zag.Rows[0]["rsch"].ToString().Trim() + ", " + zag.Rows[0]["bank"].ToString().Trim()
                                        + ", БИК " + zag.Rows[0]["bic"].ToString().Trim()
                                        + ", Корр/сч " + zag.Rows[0]["ksch"].ToString().Trim();

                reportNakl.DataDefinition.FormulaFields["ttn"].Text = "\"" + zag.Rows[0]["ttn"].ToString().Trim() + "\"";
                reportNakl.DataDefinition.FormulaFields["date"].Text = "\"" + ((DateTime)zag.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportNakl.DataDefinition.FormulaFields["okpo"].Text = "\"" + zagour.Rows[0]["okpo"].ToString().Trim() + "\"";
                reportNakl.DataDefinition.FormulaFields["okpo2"].Text = "\"" + zagour.Rows[0]["okpo"].ToString().Trim() + "\"";
                reportNakl.DataDefinition.FormulaFields["owner"].Text = "\"" + Gruzootpr.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["gruz"].Text = "\"" + GruzoPoluch.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["post"].Text = "\"" + Post.Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["plat"].Text = "\"" + Plat.Replace("\"", "\'") + "\"";
                decimal x = decimal.Round(Convert.ToDecimal(temp.Compute("SUM(sumwnds)", "")), 3);
                reportNakl.DataDefinition.FormulaFields["sum"].Text = "\"" + Conv.GetDecimalToString(x, true, false, false) + "\"";
                reportNakl.DataDefinition.FormulaFields["rukov"].Text = "\"" + zagour.Rows[0]["rukov"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["glavbuh"].Text = "\"" + zagour.Rows[0]["glavbuh"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["primech1"].Text = "\"" + zagour.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportNakl.DataDefinition.FormulaFields["primech2"].Text = "\"" + zagour.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\""; 
                reportNakl.SetDataSource(ds);
                crvReport.ReportSource = reportNakl;
            }
            #endregion

            #region счет-фактура прихода
            if (sf && callType == 1)
            {
                DataTable temp = proc.GetPrintedPrih(id);

                dsReports.dtSFDataTable sftable = new dsReports.dtSFDataTable();
                int page = 1;
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    if ((page == 1
                        && i == 12)
                    || (System.Data.SqlTypes.SqlInt32.Mod(i - 11, 24) == 1 && page != 1))
                    {
                        page++;
                    }                 
                    sftable.AdddtSFRow(int.Parse(temp.Rows[i]["npp"].ToString())
                                    , temp.Rows[i]["cname"].ToString().Trim()
                                    , temp.Rows[i]["cunit"].ToString().Trim()
                                    , decimal.Parse(temp.Rows[i]["netto"].ToString())
                                    , decimal.Parse(temp.Rows[i]["zcena"].ToString())
                                    , decimal.Parse(temp.Rows[i]["sum"].ToString())
                                    , int.Parse(temp.Rows[i]["nds"].ToString())
                                    ,decimal.Parse(temp.Rows[i]["sumnds"].ToString())                       
                                    , decimal.Parse(temp.Rows[i]["sumwnds"].ToString())
                                    , page);
                }

                crSF reportSF = new crSF();
                reportSF.DataDefinition.FormulaFields["ttn"].Text = "\"" + zag.Rows[0]["SFNumber"].ToString().Trim() + " от " + ((DateTime)zag.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportSF.DataDefinition.FormulaFields["trader"].Text = "\"" + zag.Rows[0]["cname"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["tr_adress"].Text = "\"" + zag.Rows[0]["adressU"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["tr_inn"].Text = "\"" + (zag.Rows[0]["inn"].ToString().Trim() + "/" + zag.Rows[0]["kpp"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["gruz"].Text = "\"" + (zag.Rows[0]["cname"].ToString().Trim() + ", " + zag.Rows[0]["adressP"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["byer"].Text = "\"" + (zagour.Rows[0]["cname"].ToString().Trim() + ", " + zagour.Rows[0]["adressP"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["bname"].Text = "\"" + zagour.Rows[0]["cname"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["badr"].Text = "\"" + zagour.Rows[0]["adressU"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["binn"].Text = "\"" + (zagour.Rows[0]["inn"].ToString().Trim() + "/" + zagour.Rows[0]["kpp"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["rukov"].Text = "\"" + zag.Rows[0]["rukov"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["glavbuh"].Text = "\"" + zag.Rows[0]["glavbuh"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["primech1"].Text = "\"" + zag.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["primech2"].Text = "\"" + zag.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\""; 
                reportSF.SetDataSource((DataTable)sftable);
                crvReport2.ReportSource = reportSF;
            }
            #endregion

            #region счет-фактура отгрузки
            if (sf && callType == 2)
            {
                DataTable temp = proc.GetPrintOtgruz(id);

                dsReports.dtSFDataTable sftable = new dsReports.dtSFDataTable();
                int page = 1;
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    if ((page == 1
                        && i == 12)
                    || (System.Data.SqlTypes.SqlInt32.Mod(i - 11, 24) == 1 && page != 1))
                    {
                        page++;
                    }                 

                    sftable.AdddtSFRow(int.Parse(temp.Rows[i]["npp"].ToString())
                                    , temp.Rows[i]["cname"].ToString().Trim()
                                    , temp.Rows[i]["cunit"].ToString().Trim()
                                    , decimal.Parse(temp.Rows[i]["netto"].ToString())
                                    , decimal.Parse(temp.Rows[i]["zcena"].ToString())
                                    , decimal.Parse(temp.Rows[i]["sum"].ToString())
                                    , int.Parse(temp.Rows[i]["nds"].ToString())
                                    , decimal.Parse(temp.Rows[i]["sumnds"].ToString())
                                    , decimal.Parse(temp.Rows[i]["sumwnds"].ToString())
                                    , page);

                }

                crSF reportSF = new crSF();
                reportSF.DataDefinition.FormulaFields["ttn"].Text = "\"" + zag.Rows[0]["ttn"].ToString().Trim() + " от " + ((DateTime)zag.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportSF.DataDefinition.FormulaFields["trader"].Text = "\"" + zagour.Rows[0]["cname"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["tr_adress"].Text = "\"" + zagour.Rows[0]["adressU"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["tr_inn"].Text = "\"" + (zagour.Rows[0]["inn"].ToString().Trim() + "/" + zagour.Rows[0]["kpp"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["gruz"].Text = "\"" + (zagour.Rows[0]["cname"].ToString().Trim() + ", " + zagour.Rows[0]["adressP"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["byer"].Text = "\"" + (zag.Rows[0]["cname"].ToString().Trim() + ", " + zag.Rows[0]["adressP"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["bname"].Text = "\"" + zag.Rows[0]["cname"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["badr"].Text = "\"" + zag.Rows[0]["adressU"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["binn"].Text = "\"" + (zag.Rows[0]["inn"].ToString().Trim() + "/" + zag.Rows[0]["kpp"].ToString().Trim()).Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["rukov"].Text = "\"" + zagour.Rows[0]["rukov"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["glavbuh"].Text = "\"" + zagour.Rows[0]["glavbuh"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["primech1"].Text = "\"" + zagour.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\"";
                reportSF.DataDefinition.FormulaFields["primech2"].Text = "\"" + zagour.Rows[0]["comment"].ToString().Trim().Replace("\"", "\'") + "\""; 
                reportSF.SetDataSource((DataTable)sftable);
                crvReport2.ReportSource = reportSF;
            }
            #endregion

            #region акт переоценки
            if (pereoc)
            {
                dsReports.dtSpisDataTable dtSpis = new dsReports.dtSpisDataTable();

                DataTable temp = proc.GetPrintPereoc(id);

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    dtSpis.AdddtSpisRow(int.Parse(temp.Rows[i]["npp"].ToString())
                                        ,temp.Rows[i]["cname"].ToString()
                                        ,1
                                        ,decimal.Parse(temp.Rows[i]["netto"].ToString())
                                        ,decimal.Parse(temp.Rows[i]["zcena"].ToString())
                                        ,decimal.Parse(temp.Rows[i]["rcena"].ToString())
                                        ,decimal.Parse(temp.Rows[i]["zsum"].ToString())
                                        ,decimal.Parse(temp.Rows[i]["rsum"].ToString())
                                        ,decimal.Parse(temp.Rows[i]["nds"].ToString()));
                }

                crSpis reportSpis = new crSpis();
                reportSpis.DataDefinition.FormulaFields["Label"].Text = "\"Акт переоценки № " + temp.Rows[0]["ttn"].ToString().Trim() + " от " + ((DateTime)temp.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportSpis.DataDefinition.FormulaFields["Dep"].Text = "\"" + temp.Rows[0]["dep"].ToString() + "\"";
                reportSpis.SetDataSource((DataTable)dtSpis);
                crvReport.ReportSource = reportSpis;
            }
            #endregion

            #region акт списания
            if (spis)
            {
                dsReports.dtSpisDataTable dtSpis = new dsReports.dtSpisDataTable();

                DataTable temp = proc.GetPrintSpis(id);

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    dtSpis.AdddtSpisRow(int.Parse(temp.Rows[i]["npp"].ToString())
                                        , temp.Rows[i]["cname"].ToString()
                                        , 1
                                        , decimal.Parse(temp.Rows[i]["netto"].ToString())
                                        , decimal.Parse(temp.Rows[i]["zcena"].ToString())
                                        , decimal.Parse(temp.Rows[i]["rcena"].ToString())
                                        , decimal.Parse(temp.Rows[i]["zsum"].ToString())
                                        , decimal.Parse(temp.Rows[i]["rsum"].ToString())
                                        , decimal.Parse(temp.Rows[i]["nds"].ToString()));
                }

                crSpis reportSpis = new crSpis();
                reportSpis.DataDefinition.FormulaFields["Label"].Text = "\"Акт списания № " + temp.Rows[0]["ttn"].ToString().Trim() + " от " + ((DateTime)temp.Rows[0]["dprihod"]).ToShortDateString() + "\"";
                reportSpis.DataDefinition.FormulaFields["Dep"].Text = "\"ВВО\"";
                reportSpis.SetDataSource((DataTable)dtSpis);
                crvReport.ReportSource = reportSpis;
            }
            #endregion

            #region Бухгалтерские остатки
            if (callType == 5)
            {
                crBuhSum reportSum = new crBuhSum();
                reportSum.DataDefinition.FormulaFields["date"].Text = "\"" + dateOt.ToShortDateString() + "\"";
                reportSum.SetDataSource((DataTable)dtRezult);
                crvReport.ReportSource = reportSum;
            }
            #endregion
        }
    }
}
