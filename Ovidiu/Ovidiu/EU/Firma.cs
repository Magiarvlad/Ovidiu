using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
   public class Firma
    {
        private static string codFiscal;
        private static string numeFirma;

        public static string CodFiscal { get => codFiscal; set => codFiscal = value; }
        public static string NumeFirma { get => numeFirma; set => numeFirma = value; }
    }
}
