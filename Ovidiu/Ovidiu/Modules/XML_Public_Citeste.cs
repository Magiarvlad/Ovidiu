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
            bool result = false;
            if (XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "UseFormat").InnerText.ToString() == "1")
                result = true;

           CONSTANTE.UseFormat = result;
    }

        public static void Citeste_FileLocation()
        {
            FileLocation.DataBase = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DataBase").InnerText.ToString();
            FileLocation.System = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "System").InnerText.ToString();
            FileLocation.ReportDefinitionPath = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "ReportDefinitionPath").InnerText.ToString();
            FileLocation.DirectorSalvare = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DirectorSalvare").InnerText.ToString();

            if(FileLocation.DataBase.Substring(FileLocation.DataBase.Length-1,1)!="\\")
             {
                FileLocation.DataBase = FileLocation.DataBase + "\\";
            }
            if (FileLocation.System.Substring(FileLocation.System.Length - 1, 1) != "\\")
            {
                FileLocation.System = FileLocation.System + "\\";
            }

            if (FileLocation.ReportDefinitionPath.Substring(FileLocation.ReportDefinitionPath.Length - 1, 1) != "\\")
            {
                FileLocation.ReportDefinitionPath = FileLocation.ReportDefinitionPath + "\\";
            }

            if (FileLocation.DirectorSalvare.Substring(FileLocation.DirectorSalvare.Length - 1, 1) != "\\")
            {
                FileLocation.DirectorSalvare = FileLocation.DirectorSalvare + "\\";
            }
        }

        public static void Citeste_Diverse()
        {
            
            Diverse.UpdateCurs =Verifica_Null.VER(XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "UpdateCurs").InnerText.ToString(), "0");
            Diverse.VerificaNet = Verifica_Null.VER(XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaNet").InnerText.ToString(), "0");

            Diverse.VerificaUpdate = Verifica_Null.VER(XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaUpdate").InnerText.ToString(), "0");
            CONSTANTE.wwwRadacina = Verifica_Null.VERs(XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "www").InnerText.ToString(), "http://www.e-intrastat.ro");
        }
    }
}
