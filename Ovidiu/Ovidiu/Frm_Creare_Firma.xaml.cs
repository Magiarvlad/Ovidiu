using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Creare_Firma.xaml
    /// </summary>
    public partial class Frm_Creare_Firma : Window
    {
        public Frm_Creare_Firma(bool isCalledFromMainToolbar)
        {
            InitializeComponent();
            SetLabels(isCalledFromMainToolbar);
        }
        private void SetLabels(bool isCalledFromMainToolbar)
        {
            if (!isCalledFromMainToolbar)
            {
                //this.lblDateFirma.Content = "   Pasul 1 " + this.lblDateFirma.Content.ToString().Trim();
            }
            else
            {

            
            this.Title = "Modificare date firma";
            CreazaFirma.Content = "OK - Retine datele";

            IncarcareDateFirma();
            }
        }

        private void IncarcareDateFirma()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Firme where Cod_Fiscal='"+ Firma.CodFiscal + "'" ;
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    // content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));
                    NumeFirma.Text = dbReader[1].ToString();
                    Cif.Text = dbReader[0].ToString();
                    RegComert.Text = dbReader[7].ToString();
                    AdresaFirma.Text= dbReader[2].ToString();
                    Oras.Text = dbReader[4].ToString();
                    Judet.Text = dbReader[3].ToString();
                    CodPostal.Text = dbReader[5].ToString();
                    Tara.Text = dbReader[6].ToString();
                    Nume.Text = dbReader[8].ToString();
                    Functie.Text = dbReader[9].ToString();
                    Telefon.Text = dbReader[10].ToString();
                    Fax.Text = dbReader[11].ToString();
                    Email.Text = dbReader[12].ToString();
                }
            }
           
            dbConn.Close();
        }

        private void InfoBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Daca se bifeaza aceasta optiune programul NU va mai cumula pozitii care au acelasi cod valmal; tara origine; conditii de livrare. In Acest caz declaratia Intrastat va contine toate liniile necumulate.");
        }

        private void LabelCif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void NumeFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Nume firma, va recomand fara SC";
        }

        private void RegComertLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "J00/AA/YYYY: Nr de inregistrare de la registrul comertului";
        }

        private void AdresaFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Adresa oficiala, fara oras si judet";
        }

        private void CreazaFirma_Click(object sender, RoutedEventArgs e)
        {
            Frm_Setari_Implicite frm_Setari_Implicite = new Frm_Setari_Implicite(false);
            frm_Setari_Implicite.Show();
            this.Hide();
        }
    }
}
