using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
   public class Verifica_Null
    {

        public static string VER(string Expresie, string ValReturnata)
        {
            string exp="0";
            try
            {
                if(Expresie != "")
                exp = Expresie;
            }
            catch
            {
                exp = ValReturnata;
            }
            return exp;
        }

    }
}
