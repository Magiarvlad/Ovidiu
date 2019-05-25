using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
    public class Inregistrare
    {
        public static bool Verifica_Fisier(string cod_fiscal)
        {

            return true;
        }
        public static string[] DecodeKey (string a)
          {
            string[] vs = new string[4];
            try {
            
            string CF = a.Substring(2,8);
            CF = ((Convert.ToUInt64(CF)- 28061973)/2).ToString();
            CF = "RO" + CF;
            int gratuit = Convert.ToInt32(a.Substring(10, 1));
            int nr = Convert.ToInt32(a.Substring(11, 2));
            int Anul = Convert.ToInt32(a.Substring(13)) / nr;

            vs[0] = CF;
            vs[1] = gratuit.ToString();
            vs[2] = Anul.ToString();
            vs[3] = "1";
            }
            catch
            {
                vs[0] = "";
                vs[1] = "";
                vs[2] = "";
                vs[3] = "0";
            }

            return vs;
          }

        public static void VerificaKeyAnul()
        {
            string[] keys;
            string line;
            string[] vs = new string[3];
            StreamReader stream = new StreamReader(FileLocation.System + "key\\chei.txt");
            // FileStream fisier = new FileStream( FileLocation.System+"key\\chei.txt", FileMode.Open, FileAccess.ReadWrite);
            int i=0;
            while(stream.ReadLine()!=null)
            {
                i++;
            };
            keys = new string[i];

            int j = 0;
            while ( (line=stream.ReadLine()) != null)
            {
                vs = line.Split(' ');
                if(vs[1]==Firma.CodFiscal)
                {
                    keys[j] = line;
                    j++;
                }
            }

            stream.Close();
        }
    }
}
