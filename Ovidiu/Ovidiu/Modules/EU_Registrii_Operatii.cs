using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
    public static class EU_Registrii_Operatii
    {
        public static void EU_Registrii()
        {
            CONSTANTE.eu.Nume = RegistriiOperatii.CitesteValoareREG( RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Nume");
            CONSTANTE.eu.NumeFirma = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "NumeFirma");
            CONSTANTE.eu.Telefon = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Telefon");
            CONSTANTE.eu.Fax = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Fax");
            CONSTANTE.eu.Email = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Email");
            CONSTANTE.eu.Adresa = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Adresa");
            CONSTANTE.eu.Localitate = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Localitate");
            CONSTANTE.eu.CodFiscal = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "CodFiscal");
            CONSTANTE.eu.RegComert = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "RegComert");
            CONSTANTE.eu.Banca = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "Banca");
            CONSTANTE.eu.ContBanca = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "ContBanca");
            CONSTANTE.eu.WWW = RegistriiOperatii.CitesteValoareREG(RegistriiOperatii.HKEY_LOCAL_MACHINE, "Software\\SOVIASERV\\EU", "www");
        }       
    }
}
