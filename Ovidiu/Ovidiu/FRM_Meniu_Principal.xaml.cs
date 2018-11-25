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
            System.Diagnostics.Process.Start("https://www.e-intrastat.ro/inregistrare.php");
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

            Frm_HS frm_HS = new Frm_HS("Tari- Total Inregistrari: " );
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



        // private void Window_Activated(object sender, EventArgs e)

    }
}
