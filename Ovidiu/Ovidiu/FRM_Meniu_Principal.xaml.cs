using e_Intrastat;
using Ovidiu.EU;
using Ovidiu.Miscellaneous;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
    /// Interaction logic for FRM_Meniu_Principal.xaml
    /// </summary>
    public partial class FRM_Meniu_Principal : Window
    {
        public FRM_Meniu_Principal()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SelectareFirma_Click(object sender, RoutedEventArgs e)
        {
            
            Frm_Selectie_Firma _Selectie_Firma = new Frm_Selectie_Firma(CONSTANTE.vs);

            _Selectie_Firma.Show();

        }

        private void Creare_Firma_Click(object sender, RoutedEventArgs e)
        {
            Frm_Creare_Firma frm_Creare_Firma = new Frm_Creare_Firma(false);
            frm_Creare_Firma.Show();
        }

        private void _ActualizareProgram_Click(object sender, RoutedEventArgs e)
        {
            string numeFisierVers = string.Empty;
            string sPath = string.Empty;
            numeFisierVers = UpdatesHelper.Verifica_Update_Versiune(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                if (numeFisierVers != "0")
                {
                    if (MessageBoxResult.Yes ==
                        MessageBox.Show("Exista o versiune noua pentru descarcare\nDoriti descarcarea si instalarea noii versiuni?", "Info", MessageBoxButton.YesNo))
                    {
                        sPath = Environment.CurrentDirectory + @"UpdateWEB\UpdateWEB.exe";
                        ClasaSuport.StartProgramByFileName(sPath, true);
                        Application.Current.Shutdown();
                        return;
                    }
                }
                else
                 {
                    MessageBox.Show("Nu este necesara actualizarea programului", "Info", MessageBoxButton.OK);
                 }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Frm_Setari_Implicite frmSetImplicite = new Frm_Setari_Implicite(true);
            frmSetImplicite.Show();
        }

        private void CSIP_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro");
        }

        private void CMILI_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro");
        }

        private void TPOD_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro");
        }

        private void CSTDO_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro");
        }

        private void CSTD_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro");
        }

        private void Inregistrea_Firma_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frm_WEB frm_WEB = new Frm_WEB();
            frm_WEB.Show();
         //   System.Diagnostics.Process.Start("https://www.e-intrastat.ro/inregistrare.php");
        }

        private void ActualizareAutomataCurs_Click(object sender, RoutedEventArgs e)
        {
            CursSchimb_Actualizare.Actualiza_curs();

        } 

        private void _Ajutor_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(FileLocation.System + "Help\\Manual.html");
        }

        private void _DateFirma_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frm_Creare_Firma frm_Creare_Firma = new Frm_Creare_Firma(true);
            frm_Creare_Firma.Show();
        }

        private void btnAjutor_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(FileLocation.System + "Help\\Manual.html");
        }

        string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
        private void _Tari_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("Tari");
            Frm_HS frm_HS = new Frm_HS("Tari- Total Inregistrari: " + _numarInregistrari,"Tari");
        }

        private void _Tari_UE_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("TARI_UE");
            Frm_HS frm_HS = new Frm_HS("Tari UE- Total Inregistrari: " + _numarInregistrari, "TARI_UE");
        }
        private void _Monezi_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("Monezi");
            Frm_HS frm_HS = new Frm_HS("Monezi- Total Inregistrari: " + _numarInregistrari, "Monezi");
        }

        private int getCount(string NumeTabela)
        {
            _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
           
            string dbQuery = string.Empty;
            
            try
            {
                dbConn.Open();
                dbQuery = "SELECT COUNT(*) FROM "+NumeTabela;
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                
                int a= (int)dbCommand.ExecuteScalar();
                dbConn.Close();
                return a;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dbConn.Close();
                return 0;
                
            }
           
        }

        private int getCount_HS(string NumeTabela)
        {
            _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "CN\\" + "CN_" + System.DateTime.Today.Year + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;

            string dbQuery = string.Empty;

            try
            {
                dbConn.Open();
                dbQuery = "SELECT COUNT(*) FROM " + NumeTabela;
                dbCommand = new OleDbCommand(dbQuery, dbConn);

                int a = (int)dbCommand.ExecuteScalar();
                dbConn.Close();
                return a;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dbConn.Close();
                return 0;

            }

        }

        private void _Asd()
        {
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
           // OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            try
            {
                dbConn.Open();
                dbQuery = "SELECT COUNT(*) FROM Tari WHERE Cod_Fiscal='" + Firma.CodFiscal + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                if ((int)dbCommand.ExecuteScalar() < 1)
                {
                    dbQuery = "INSERT INTO Intrastat_Default (Cod_Fiscal) VALUES ('" + Firma.CodFiscal + "')";
                    dbCommand = new OleDbCommand(dbQuery, dbConn);
                    dbCommand.ExecuteNonQuery();
                }
            }
            catch
            {

            }

        }

        private void _SetariGenerale_Click(object sender, RoutedEventArgs e)
        {
            Frm_Setari frm_Setari = new Frm_Setari(true);
            frm_Setari.Show();

        }

        private void Incoterms_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("Incoterms");
            Frm_HS frm_HS = new Frm_HS("Incoterms- Total Inregistrari: " + _numarInregistrari, "Incoterms");

        }

        private void _NaturaTranzactie_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("Tranzactii");
            Frm_HS frm_HS = new Frm_HS("Natura Tranzactiei- Total Inregistrari: " + _numarInregistrari, "Tranzactii");
        }

        private void _UM_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount("UM");
            Frm_HS frm_HS = new Frm_HS("UM suplimnentare- Total Inregistrari: " + _numarInregistrari, "UM");
        }

        private void _Sectiuni_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount_HS("HS_1");
            Frm_HS frm_HS = new Frm_HS("Sectiuni- Total Inregistrari: " + _numarInregistrari, "HS_1");
            
        }

        private void _Capitole_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount_HS("HS_2");
            Frm_HS frm_HS = new Frm_HS("Capitole- Total Inregistrari: " + _numarInregistrari, "HS_2");
        }

        private void _Grupe_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount_HS("HS_4");
            Frm_HS frm_HS = new Frm_HS("Grupe- Total Inregistrari: " + _numarInregistrari, "HS_4");
        }

        private void _HS_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount_HS("HS_6");
            Frm_HS frm_HS = new Frm_HS("HS6- Total Inregistrari: " + _numarInregistrari, "HS_6");
        }

        private void _Cod_Vamal_Btn_Click(object sender, RoutedEventArgs e)
        {
            int _numarInregistrari = getCount_HS("HS_8");
            Frm_HS frm_HS = new Frm_HS("Cod Vamal- Total Inregistrari: " + _numarInregistrari, "HS_8");
        }

        private void _Adauga_Macheta_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frm_Structura_Fisiere frm_Structura_Fisiere = new Frm_Structura_Fisiere();
            frm_Structura_Fisiere.Show();
        }

        private void btnAdministrareDeclaratii_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void _DateFirma_Btn1_Click(object sender, RoutedEventArgs e)
        {
            Frm_Creare_Firma frm_Creare_Firma = new Frm_Creare_Firma(true);
            frm_Creare_Firma.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Frm_Lista_Declaratii frm_Lista_Declaratii = new Frm_Lista_Declaratii();
            frm_Lista_Declaratii.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Frm_IntroduceKEY frm_IntroduceKEY = new Frm_IntroduceKEY();
            frm_IntroduceKEY.Show();
        }

        private void _Introducere_KEY_Click(object sender, RoutedEventArgs e)
        {
            Frm_IntroduceKEY frm_IntroduceKEY = new Frm_IntroduceKEY();
            frm_IntroduceKEY.Show();
        }

        private void CautareAvansata_Click(object sender, RoutedEventArgs e)
        {
            Frm_Cautare_Avansata frm_Cautare = new Frm_Cautare_Avansata(TxtCautare.Text);
            frm_Cautare.Show();

        }



        // private void Window_Activated(object sender, EventArgs e)

    }
}
