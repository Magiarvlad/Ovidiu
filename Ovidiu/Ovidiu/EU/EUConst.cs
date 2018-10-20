using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
   public class EUConst
    {
        public  string Nume, NumeFirma, Telefon, Fax, Email, Adresa, Localitate, CodFiscal, RegComert, Banca, COntBanca, WWW, ValLicLocal, Moneda;

        public EUConst(string nume, string numeFirma, string telefon, string fax, string email, string adresa, string localitate, string codFiscal, string regComert, string banca, string cOntBanca, string wWW, string valLicLocal, string moneda)
        {
            Nume = nume;
            NumeFirma = numeFirma;
            Telefon = telefon;
            Fax = fax;
            Email = email;
            Adresa = adresa;
            Localitate = localitate;
            CodFiscal = codFiscal;
            RegComert = regComert;
            Banca = banca;
            COntBanca = cOntBanca;
            WWW = wWW;
            ValLicLocal = valLicLocal;
            Moneda = moneda;
        }
    }
}
