using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Globalization;


namespace Spisanie
{
    class Conv
    {
        static public string NumberDecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        #region DecToStr
        static public string GetDecimalToString(string sValue, bool isMany, bool QuotaTypeString, bool IsInt)
        {
            //decimal dValue = 0.0m;
            string sOut = "", sSep = ".";
            decimal dValue = 0.0m;
            if (sValue.IndexOf(NumberDecimalSeparator) < 0)
            {
                if (NumberDecimalSeparator == ".")
                {
                    sSep = ",";
                }
                if (sValue.IndexOf(sSep) < 0)
                {
                    try
                    {
                        dValue = Convert.ToDecimal(sValue);
                        sOut = GetDecimalToString(dValue, isMany, QuotaTypeString, IsInt);
                        return sOut;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return "";
                    }
                }
                sOut = sValue.Substring(0, sValue.IndexOf(sSep)) + NumberDecimalSeparator +
                    sValue.Substring(sValue.IndexOf(sSep) + 1, sValue.Length - sValue.IndexOf(sSep) - 1);
            }
            else {
                sOut = sValue;
            }
            dValue = Convert.ToDecimal(sOut);
            sOut = GetDecimalToString(dValue, isMany, QuotaTypeString, IsInt);
            return sOut;
        }


        static public string GetDecimalToString(decimal dValue, bool isMany, bool QuotaTypeString,bool IsInt)
        {

            if (dValue >= 1000000000000)
            {
                return "Нет отображения более триллиона";
            }
            if (dValue < 0)
            {
                return "Нет отображения отрицательных значений";
            }
            if (dValue == 0.0m)
            {
                if (isMany)
                {
                    if (QuotaTypeString)
                    {
                        return "ноль рублей ноль копеек";
                    }
                    else
                    {
                        return "ноль рублей 00 коп.";
                    }
                }
                else
                {
                    return "ноль";
                }
            }
            ///////////////////////////////////////////////////
            int nThousand = 0, nMillion = 0, nBillion = 0, nHundred = 0, nTen = 0, nUnit = 0;
            int nPartial = 0;
            string sValue = dValue.ToString();
            string sInt = sValue, sPartial = "";
            if (sValue.IndexOf(NumberDecimalSeparator) > 0)
            {
                sInt = sValue.Substring(0, sValue.IndexOf(NumberDecimalSeparator));
                sPartial = sValue.Substring(sValue.IndexOf(NumberDecimalSeparator) + 1,
                    sValue.Length - sValue.IndexOf(NumberDecimalSeparator) - 1);
            }
            if (sPartial.Length == 0) {
                sPartial = "00";
            }
            string sTmpStr = sInt;
            if (sInt.Length > 9)
            {     //1 000 000 000 000 
                nBillion = int.Parse(sInt.Substring(0, sInt.Length - 9));
                sTmpStr = sValue.Substring(sInt.Length - 9, 9);
            }
            if (sTmpStr.Length > 6)
            {
                nMillion = int.Parse(sTmpStr.Substring(0, sTmpStr.Length - 6));
                sTmpStr = sTmpStr.Substring(sTmpStr.Length - 6, 6);
            }
            if (sTmpStr.Length > 3)
            {
                nThousand = int.Parse(sTmpStr.Substring(0, sTmpStr.Length - 3));
                sTmpStr = sTmpStr.Substring(sTmpStr.Length - 3, 3);
            }
            if (sTmpStr.Length > 2)
            {
                nHundred = int.Parse(sTmpStr.Substring(0, 1));
                sTmpStr = sTmpStr.Substring(sTmpStr.Length - 2, 2);
            }
            if (sTmpStr.Length > 1)
            {
                nTen = int.Parse(sTmpStr.Substring(0, 1));
                sTmpStr = sTmpStr.Substring(sTmpStr.Length - 1, 1);
            }
            if (sTmpStr.Length == 1)
            {
                nUnit = int.Parse(sTmpStr);
            }
            if (sPartial.Length > 0)
            {
                nPartial = int.Parse((sPartial + "  ").Substring(0, 2));
            }

            string sOut = GetNameParse(nBillion, 1) + GetUnitsName(nBillion, 1) + GetNameParse(nMillion, 2) +
                GetUnitsName(nMillion, 2) + GetNameParse(nThousand, 3) + GetUnitsName(nThousand, 3) +
                GetNameParse(nHundred * 100 + nTen * 10 + nUnit, 0);

            if (!IsInt)
            {
                sOut = sOut.Trim() + GetNameIntegerUnit(nTen,nUnit, isMany);
                if (QuotaTypeString)
                {
                    sOut = sOut.Trim() + GetNameParse(nPartial, 4);
                }
                else
                {
                    sOut = sOut.Trim() + " " + (sPartial + "  ").Substring(0, 2);
                }
                sOut = sOut.Trim() + GetNamePartialUnit(sPartial, isMany);
            }
            return sOut;
            //sPartial;
        }
        static string GetNamePartialUnit(string sUnit,bool isMany) {
            int n_Unit = 0;
            int nUnit = 0;
            if (sUnit.Length > 0)
            {
                n_Unit = int.Parse((sUnit + "  ").Substring(0, 2));
            }
            if (n_Unit > 9) { nUnit = n_Unit % 10; } else { nUnit = n_Unit; }
            string sOut = " копеек";
            if (isMany)
            {
                if (nUnit == 1) { sOut = " копейка"; }
                if (nUnit > 1 && nUnit < 5) { sOut = " копейки"; }
            }
            else {
                if (sUnit.Length > 1)
                {
                    if (nUnit == 1) { sOut = " сотая"; }
                    else { sOut = " сотых"; }
                }
                else {
                    if (nUnit == 1) { sOut = " десятая"; }
                    else { sOut = " десятых"; }
                }
            }
            return sOut;
        }

        static string GetNameIntegerUnit(int nTen, int nUnit,bool isMany)
        {
            string sOut = " рублей";
            if (isMany)
            {
                if (nTen != 1 && nUnit == 1) { sOut = " рубль"; }
                if (nTen != 1 && nUnit > 1 && nUnit < 5) { sOut = " рубля"; }
            }
            else {
                sOut = " целых";
                if (nUnit == 1) { sOut = " целая"; }
            }
            return sOut;
        }

        static string GetHundred(int nH)
        {
            string sOut = "";
            switch (nH)
            {
                case 1:
                    sOut = " сто";
                    break;
                case 2:
                    sOut = " двести";
                    break;
                case 3:
                    sOut = " триста";
                    break;
                case 4:
                    sOut = " четыреста";
                    break;
                case 5:
                    sOut = " пятьсот";
                    break;
                case 6:
                    sOut = " шестьсот";
                    break;
                case 7:
                    sOut = " семьсот";
                    break;
                case 8:
                    sOut = " восемьсот";
                    break;
                case 9:
                    sOut = " девятьсот";
                    break;
                default:
                    sOut = " ";
                    break;
            }
            return sOut;
        }
        static string GetTen(int nT,int nU)
        {
            string sOut = "";
            switch (nT)
            {
                case 1:
                    if (nU == 0)
                    {
                        sOut = " десять";
                    }
                    break;
                case 2:
                    sOut = " двадцать";
                    break;
                case 3:
                    sOut = " тридцать";
                    break;
                case 4:
                    sOut = " сорок";
                    break;
                case 5:
                    sOut = " пятьдесят";
                    break;
                case 6:
                    sOut = " шестьдесят";
                    break;
                case 7:
                    sOut = " семьдесят";
                    break;
                case 8:
                    sOut = " восемьдесят";
                    break;
                case 9:
                    sOut = " девяносто";
                    break;
                default:
                    sOut = " ";
                    break;
            }
            return sOut;
        }
        static string GetUnits(int nT, int nU, int nTypeUnits)
        {
            string sOut = "";
            if (nT == 1)
            {
                switch (nU)
                {
                    case 1:
                        sOut = " одиннадцать"; 
                        break;
                    case 2:
                        sOut = " двенадцать"; 
                        break;
                    case 3:
                        sOut = " тринадцать";
                        break;
                    case 4:
                        sOut = " четырнадцать";
                        break;
                    case 5:
                        sOut = " пятнадцать";
                        break;
                    case 6:
                        sOut = " шестнадцать";
                        break;
                    case 7:
                        sOut = " семнадцать";
                        break;
                    case 8:
                        sOut = " восемнадцать";
                        break;
                    case 9:
                        sOut = " девятнадцать";
                        break;
                    default:
                        sOut = " ";
                        break;
                }
            }
            else{
                switch (nU)
                {
                    case 1:
                        if (nTypeUnits < 3) { sOut = " один"; }
                        if (nTypeUnits > 2) { sOut = " одна"; }
                        break;
                    case 2:
                        if (nTypeUnits < 3) { sOut = " два"; }
                        if (nTypeUnits > 2) { sOut = " две"; }
                        break;
                    case 3:
                        sOut = " три";
                        break;
                    case 4:
                        sOut = " четыре";
                        break;
                    case 5:
                        sOut = " пять";
                        break;
                    case 6:
                        sOut = " шесть";
                        break;
                    case 7:
                        sOut = " семь";
                        break;
                    case 8:
                        sOut = " восемь";
                        break;
                    case 9:
                        sOut = " девять";
                        break;
                    default:
                        sOut = " ";
                        break;
                }
            }
            return sOut;
        }
        static string GetNameParse(int nName, int nTypeUnits)
        {
            string sOut = "";
            if (nName >= 100)
            {
                sOut = GetHundred(nName / 100);
                nName = nName % 100;
            }
            if (nName >= 10)
            {
                sOut = sOut + GetTen(nName / 10, nName % 10);
                //nName = nName % 10;
            }
            if (nName > 0)
            {
                sOut = sOut + GetUnits(nName / 10, nName % 10, nTypeUnits);
            }
            return sOut;
        }

        /// <summary>
        ///     nTypeUnits: 1 - миллиардов; 2 - миллионов; 3 - тысячи
        /// </summary>
        /// <returns>строка названия единиц счисления </returns>
        static string GetUnitsName(int n_Units, int nTypeUnits)
        {
            string sOut = "";
            int nUnits = n_Units;
            if (n_Units > 19)
            {
                nUnits = Convert.ToInt32(n_Units.ToString().Substring(n_Units.ToString().Length - 1, 1));
            }
            if (nUnits == 1)
            {
                switch (nTypeUnits)
                {
                    case 1:
                        sOut = " миллиард";
                        break;
                    case 2:
                        sOut = " миллион";
                        break;
                    case 3:
                        sOut = " тысяча";
                        break;
                    default:
                        break;
                }
            }
            if (nUnits > 1 && nUnits < 5)
            {
                switch (nTypeUnits)
                {
                    case 1:
                        sOut = " миллиарда";
                        break;
                    case 2:
                        sOut = " миллиона";
                        break;
                    case 3:
                        sOut = " тысячи";
                        break;
                    default:
                        break;
                }
            }
            if (nUnits > 4 || (n_Units > 0 && nUnits == 0))
            {
                switch (nTypeUnits)
                {
                    case 1:
                        sOut = " миллиардов";
                        break;
                    case 2:
                        sOut = " миллионов";
                        break;
                    case 3:
                        sOut = " тысяч";
                        break;
                    default:
                        break;
                }
            }
            return sOut;
        }

    #endregion DecToStr
        public static string GetNameInitials(string sFullName)
        {
            string sNameInitials = sFullName;
            int nSp = sFullName.Trim().IndexOf(" ");
            if (nSp > 0)
            {
                sNameInitials = sFullName.Substring(0, nSp) + " " + sFullName.Substring(nSp + 1, 1) + ".";
                nSp = sFullName.Trim().IndexOf(" ", nSp + 1);
                if (nSp > 0)
                {
                    sNameInitials = sNameInitials + sFullName.Substring(nSp + 1, 1) + ".";
                }
            }
            return sNameInitials;
        }
        public static string GetRoundString(string sSumm) {
            string sOut = sSumm;
            if (sSumm.IndexOf(NumberDecimalSeparator) > 0)
            {
                sOut = sSumm + "00";
                sOut = sSumm.Substring(0, sSumm.IndexOf(NumberDecimalSeparator) + 3);
            }
            return sOut;
        }
    }

}
