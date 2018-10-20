using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ovidiu.EU;
namespace Ovidiu.Modules
{
    public static class XML_Public_Citeste
    {
        public static void Citeste_CUlori()
        {
            EuCulori.EvenRowStyle_BackColor = Convert.ToInt64((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "EvenRowStyle_BackColor").InnerText.ToString()));
            EuCulori.OddRowStyle_BackColor = Convert.ToInt64((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "OddRowStyle_BackColor").InnerText.ToString()));
            EuCulori.HighlightRowStyle_BackColor = Convert.ToInt64((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "HighlightRowStyle_BackColor").InnerText.ToString()));
            EuCulori.HighlightRowStyle_ForeColor = Convert.ToInt64((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "HighlightRowStyle_ForeColor").InnerText.ToString()));
            EuCulori.Meniu_Color = Convert.ToInt64((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "Meniu_Color").InnerText.ToString()));
        }

        public static void Citeste_Zecimale()
        {
            Rot.NrZecLei = Convert.ToByte((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotLEI").InnerText.ToString()));
            Rot.NrZecValuta = Convert.ToByte((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotValuta").InnerText.ToString()));
            Rot.NrZecCalcule = Convert.ToByte((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotCalcule").InnerText.ToString()));
            Rot.NrZecTaxare = Convert.ToByte((XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "NrZecTaxare").InnerText.ToString()));
           CONSTANTE.UseFormat = Convert.ToBoolean(XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "UseFormat").InnerText.ToString());
    }
    }
}
