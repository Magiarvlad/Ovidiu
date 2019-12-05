using Ovidiu.EU;
using System;
using System.Data.OleDb;
using System.Windows;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_WEB.xaml
    /// </summary>
    public partial class Frm_WEB : Window
    {
        string AdresaFirma, InregIFirma, PersoanaFirma, TelFirma, EmailFirma, idwww;

        private void Hyper_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(idwww);
        }

        public Frm_WEB()
        {
            InitializeComponent();
        }

        private void BtnInregistrare_Click(object sender, RoutedEventArgs e)
        {
            IncarcareDateFirma();
        }

        private void IncarcareDateFirma()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Firme where Cod_Fiscal='" + Firma.CodFiscal + "'";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    // content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));
                    //NumeFirma.Text = dbReader[1].ToString();
                    //Cif.Text = dbReader[0].ToString();
                    InregIFirma= dbReader[7].ToString();
                    AdresaFirma = dbReader[2].ToString();
                   // Oras.Text = dbReader[4].ToString();
                   // Judet.Text = dbReader[3].ToString();
                   // CodPostal.Text = dbReader[5].ToString();
                   // Tara.Text = dbReader[6].ToString();
                    PersoanaFirma = dbReader[8].ToString();
                    //Functie.Text = dbReader[9].ToString();
                    TelFirma = dbReader[10].ToString();
                    //Fax.Text = dbReader[11].ToString();
                    EmailFirma = dbReader[12].ToString();
                }


                AdresaFirma = AdresaFirma.Replace("&", "-");
                AdresaFirma = AdresaFirma.Replace("@", "-");
                AdresaFirma = AdresaFirma.Replace("!", "-");
                AdresaFirma = AdresaFirma.Replace("'", " ");
                AdresaFirma = AdresaFirma.Replace("\"", " ");
                AdresaFirma = AdresaFirma.Replace("%", " ");
                AdresaFirma = AdresaFirma.Replace(":", " ");
                AdresaFirma = AdresaFirma.Replace(";", " ");
                AdresaFirma = AdresaFirma.Replace("(", " ");
                AdresaFirma = AdresaFirma.Replace(")", " ");

                InregIFirma = InregIFirma.Replace("&", "-");
                InregIFirma = InregIFirma.Replace("@", "-");
                InregIFirma = InregIFirma.Replace("!", "-");
                InregIFirma = InregIFirma.Replace("'", " ");
                InregIFirma = InregIFirma.Replace("\"", " ");
                InregIFirma = InregIFirma.Replace("%", " ");
                InregIFirma = InregIFirma.Replace(":", " ");
                InregIFirma = InregIFirma.Replace(";", " ");
                InregIFirma = InregIFirma.Replace("(", " ");
                InregIFirma = InregIFirma.Replace(")", " ");

                PersoanaFirma = PersoanaFirma.Replace("&", "-");
                PersoanaFirma = PersoanaFirma.Replace("@", "-");
                PersoanaFirma = PersoanaFirma.Replace("!", "-");
                PersoanaFirma = PersoanaFirma.Replace("'", " ");
                PersoanaFirma = PersoanaFirma.Replace("\"", " ");
                PersoanaFirma = PersoanaFirma.Replace("%", " ");
                PersoanaFirma = PersoanaFirma.Replace(":", " ");
                PersoanaFirma = PersoanaFirma.Replace(";", " ");
                PersoanaFirma = PersoanaFirma.Replace("(", " ");
                PersoanaFirma = PersoanaFirma.Replace(")", " ");

                TelFirma = TelFirma.Replace("&", "-");
                TelFirma = TelFirma.Replace("@", "-");
                TelFirma = TelFirma.Replace("!", "-");
                TelFirma = TelFirma.Replace("'", " ");
                TelFirma = TelFirma.Replace("\"", " ");
                TelFirma = TelFirma.Replace("%", " ");
                TelFirma = TelFirma.Replace(":", " ");
                TelFirma = TelFirma.Replace(";", " ");
                TelFirma = TelFirma.Replace("(", " ");
                TelFirma = TelFirma.Replace(")", " ");


                
                idwww = "http://www.e-intrastat.ro/inregistrare.php?" + "mode=" + "PC" + "&" + "firma_CF=" + Firma.CodFiscal+ "&" + "firma_nume=" + Firma.NumeFirma + "&" + "firma_adresa=" +AdresaFirma+ "&" + "firma_inreg_comert=" + InregIFirma+ "&" + "firma_persoana=" + PersoanaFirma+ "&" + "firma_tel=" + TelFirma + "&" + "firma_email=" + EmailFirma;
                wb1.Navigate(idwww);
                tb1.Text = "Va rugam completati formularul de mai jos";
                hyper.NavigateUri = new Uri(idwww);
            }

            dbConn.Close();
        }
    }
}
