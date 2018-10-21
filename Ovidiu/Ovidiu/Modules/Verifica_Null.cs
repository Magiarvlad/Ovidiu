using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
   public class Verifica_Null
    {

        public static bool VER(string Expresie, string ValReturnata)
        {
            bool exp=false;
            try
            {
                if (Expresie != "")
                    if (Expresie == "1")
                        exp = true;
                    else
                        exp = false;
            }
            catch
            {
                exp = false;
            }
            return exp;
        }

        internal static string VERs(string v1, string v2)
        {
            string exp="";
            try
            {
                if (v1 != "")
                        exp = v1;
            }
            catch
            {
                exp = v2;
            }
            return exp;
        }
    }
}
