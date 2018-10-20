using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


            CONSTANTE.eu.ValLicLocal = XML_Operatii.Citeste_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat", "ValoareLocal");

        }

    }


}
