using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
    public static class CONSTANTE
    {
        public static class Val_Implicite
        {
            public static string I_Tara_Exp { get; set; }
            public static string I_Incoterms { get; set; }
            public static string I_Nat_Transp { get; set; }
            public static string I_Mod_Transp { get; set; }
            public static string O_Tara_Dest { get; set; }
            public static string O_Incoterms { get; set; }
            public static string O_Nat_Tranz { get; set; }
            public static string O_Mod_Transp { get; set; }
        }

        public static string Setting_XML_file="";
        public static EUConst eu = new EUConst();
        public static bool UseFormat=false;
        public static string wwwRadacina="www";
        public static string[,] vs = new string[10, 2];
        public static FRM_Meniu_Principal Meniu = new FRM_Meniu_Principal();
    }
}
