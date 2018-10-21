using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ovidiu.Modules
{
    public static class UpdatesHelper
    {
        public static string Verifica_Update_Versiune(string appVersion)
        {
            string result = "-1";
            string path = "http://www.e-intrastat.ro/download/dat/vers.txt";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(path);
                StreamReader reader = new StreamReader(stream);
                string siteVersion = reader.ReadToEnd();
                if (string.Compare(appVersion, siteVersion) >= 0)
                    result = "0";
            }
            catch(Exception exp)
            {
                MessageBox.Show("Eroare accesare versiune site: " + Environment.NewLine + exp.Message);
            }

            return result;// 0  IF does not exist
        }
    }
}
