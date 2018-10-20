using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
   public class EUConst
    {
        private static string moneda;
        private static string nume;
        private static string numeFirma;
        private static string telefon;
        private static string fax;
        private static string email;
        private static string adresa;
        private static string localitate;
        private static string codFiscal;
        private static string regComert;
        private static string banca;
        private static string contBanca;
        private static string wWW;
        private static string valLicLocal;

        public string Nume { get => nume; set => nume = value; }
        public string NumeFirma { get => numeFirma; set => numeFirma = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string Localitate { get => localitate; set => localitate = value; }
        public string CodFiscal { get => codFiscal; set => codFiscal = value; }
        public string RegComert { get => regComert; set => regComert = value; }
        public string Banca { get => banca; set => banca = value; }
        public string ContBanca { get => contBanca; set => contBanca = value; }
        public string WWW { get => wWW; set => wWW = value; }
        public string ValLicLocal { get => valLicLocal; set => valLicLocal = value; }
        public string Moneda { get => moneda; set => moneda = value; }
    }
}
