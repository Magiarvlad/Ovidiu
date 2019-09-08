using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Ovidiu.Modules
{
   public static class XML_Setari_Default
    {



        public static void Setari_Default_XML()
        {


            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "ValoareLocal", "125", true);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "AppPath", Environment.CurrentDirectory, true);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "Moneda","EUR", true);
            //create_XML Settings _XML file


            try
            {
                XmlNode asd = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "ValoareLocal");
                string ast = asd.InnerText.ToString();
                CONSTANTE.eu.ValLicLocal = ast;
                CONSTANTE.eu.Moneda = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "Moneda").InnerText.ToString();
            }
            catch(Exception exp)
            {
                //MessageBox.Show("Frm_Pornire_Loaded Error: " + exp.Message);
                //Application.Current.Shutdown();
            }


            //setari culori
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "EvenRowStyle_BackColor", "14737632", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "HighlightRowStyle_BackColor", "16711680", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "HighlightRowStyle_ForeColor", "16777215", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "OddRowStyle_BackColor", "12648447", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Culori", "Meniu_Color", "12648447", false);


            //setari diverse
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "UpdateCurs", "1", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificareUpdate", "1", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificareNet", "0", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "www", "https://www.e-intrastat.ro", false);

            //setari FileLocation
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DataBase", "C:\\E_intrastat\\System\\DataBase\\", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "ReportDefinitionPath", "C:\\E_intrastat\\System\\ReportDefinition\\", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "System", "C:\\E_intrastat\\System", false);

            try
            {
                XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DirectorSalvare", "C:\\Program Files\\INTRASTAT\\declaratii\\", false);
            }

            catch
            {
                try
                {
                     XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DirectorSalvare", "C:\\Program Files (x86)\\INTRASTAT\\declaratii\\", false);
                }
                catch
                {
                    XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DirectorSalvare", "C:\\E_intrastat\\System\\DeclaratiiXML", false);
                }
            }


            //Setari Zecimale
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "NrZecTaxare", "0", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "UseFormat", "1", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotCalcule", "4", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotLEI", "0", false);
            XML_Operatii.Creaza_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Zecimale", "ZecRotValuta", "2", false);

        }

       

    }


}
