using Ovidiu.Miscellaneous;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Deployment.Application;
using System.Reflection;
using Ovidiu.EU;
using System.IO;
using System.Net;
using System.Data.OleDb;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Frm_Pornire : Window
    {
        public Frm_Pornire()
        {
            InitializeComponent();


            if( ClasaSuport.ProgramIsAlreadyRunning() )
            {
                MessageBox.Show( "Aplicatia ruleaza deja", "Eroare", MessageBoxButton.OK);

                Application.Current.Shutdown();
            }
        }

        private void Frm_Pornire_Loaded(object sender, RoutedEventArgs e)
        {
            string formatNrScurt = "##,##0";
            string formatNrLung = "##,##0.00";
           // string Settings_XML_File = string.Empty;
            string sPath = string.Empty;
            string numeFisierVers = string.Empty;
            try
            {
                // Determin locatia unde este fisierul Settings.XML      
               string path= Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                // Settings_XML_File = Environment.CurrentDirectory + @"\E_Intrastat'Settings.xml";
                CONSTANTE.Setting_XML_file = path + @"\E_Intrastat\Settings.xml";
               // if (!XML_Operatii.Verifica_Fisier(Settings_XML_File))
              //  {
                //    MessageBox.Show("EROARE identificare fisier setari: " + Settings_XML_File + " nu exista");
                //    return;
              //  }
                XML_Setari_Default.Setari_Default_XML();
               

                EU_Registrii_Operatii.EU_Registrii();
                XML_Public_Citeste.Citeste_CUlori();
                XML_Public_Citeste.Citeste_Zecimale();
                XML_Public_Citeste.Citeste_FileLocation();
                XML_Public_Citeste.Citeste_Diverse();

                if (Diverse.VerificaUpdate == true) 
                {
                    numeFisierVers = UpdatesHelper.Verifica_Update_Versiune(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    if (numeFisierVers != "0")
                    {
                        if ( MessageBoxResult.Yes ==
                            MessageBox.Show("Exista o versiune noua pentru descarcare\nDoriti descarcarea si instalarea noii versiuni?","Info", MessageBoxButton.YesNo))
                        {
                            sPath = Environment.CurrentDirectory + @"UpdateWEB\UpdateWEB.exe";
                            ClasaSuport.StartProgramByFileName(sPath, true);
                            Application.Current.Shutdown();
                            return;
                        }
                    }
                }

                string comunpath = "C:\\E_Intrastat\\System\\DataBase\\Comun.mdb";
                bool flag = false;
                if (!Verifica_Exista_Fisier.Verifica_Fisier(comunpath))
                { 
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        if (Verifica_Exista_Fisier.Verifica_Fisier(drive + "E_Intrastat\\System\\DataBase\\Comun.mdb"))
                        // MessageBox.Show("FIșierul a fost gasit!");
                        {
                            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DataBase", drive + "E_Intrastat\\System\\DataBase\\", true);
                            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "System", drive + "E_Intrastat\\System\\", true);
                            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DirectorSalvare", drive + "E_Intrastat\\System\\DeclaratiiXML\\", true);
                            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "ReportDefinitionPath", drive + "E_Intrastat\\System\\RaportDefinition", true);

                             flag = true;   
                        }
                    }
                    
                }
                else
                {
                    flag = true;
                }
                if (flag == false)
                {
                    MessageBox.Show("Baza de date NU a fost gasita! Exemplu locatie : D:\\E-Intrastat\\System");
                    Application.Current.Shutdown();
                }
                else
                {
                    Update_Curs();
                    Open_Conection_Common();
                    this.Hide();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Frm_Pornire_Loaded Error: " + exp.Message);
                Application.Current.Shutdown();
            }
            
        }
         
        public static void Open_Conection_Common()
        {
            
            OleDbConnection conn = new
        OleDbConnection
            {
                // TODO: Modify the connection string and include any
                // additional required properties for your database.
                ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source=" + FileLocation.DataBase + "Comun.mdb"
            };
            try
            {
                bool flag = false;
              
                int i = 0;
                conn.Open();
                OleDbCommand Command = new OleDbCommand("SELECT Cod_Fiscal,Nume_Firma,DataBaseFile,Nr_Inregistrare,Key_Inregistrare from Firme", conn);
                OleDbDataReader DB_Reader = Command.ExecuteReader();
                if (DB_Reader.HasRows)
                {
                    DB_Reader.Read();
                    Firma.CodFiscal = DB_Reader[0].ToString();
                    Firma.NumeFirma = DB_Reader[1].ToString();
                    CONSTANTE.vs[i,0] = DB_Reader[0].ToString();
                    CONSTANTE.vs[i,1] = DB_Reader[1].ToString();
                    while (DB_Reader.Read())
                    {
                        flag = true;
                        i++;
                        CONSTANTE.vs[i,0] = DB_Reader[0].ToString();
                        CONSTANTE.vs[i,1] = DB_Reader[1].ToString();
                    }
                    if(flag==true)
                    {
                        
                        Frm_Selectie_Firma frm_Selectie_Firma = new Frm_Selectie_Firma(CONSTANTE.vs);
                        frm_Selectie_Firma.Show();
                    }
                    // textbox1.Text = DB_Reader.GetString("your_column_name");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update_Curs()
        {
            DateTime cursBNR_time = File.GetLastWriteTime(FileLocation.System + "CursBNR\\curs.txt");

            if ( DateTime.Now.Year > cursBNR_time.Year)
            {
                File.Replace(FileLocation.System + "CursBNR\\curs.txt", FileLocation.System + "CursBNR\\curs_old.txt", FileLocation.System + "CursBNR\\backup.txt");
                string pathURL = "https://www.soviaserv.ro/curs_bnr/curs.txt";
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(pathURL, FileLocation.System + "CursBNR\\curs.txt");
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Eroare accesare Curs Valutar!" + exp);
                }               
            }

            if (DateTime.Now.Year == cursBNR_time.Year)
            {
                if (DateTime.Now.DayOfYear > cursBNR_time.DayOfYear)
                {
                    File.Replace(FileLocation.System + "CursBNR\\curs.txt", FileLocation.System + "CursBNR\\curs_old.txt", FileLocation.System + "CursBNR\\backup.txt");
                    string pathURL = "https://www.soviaserv.ro/curs_bnr/curs.txt";
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(pathURL, FileLocation.System + "CursBNR\\curs.txt");

                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Eroare accesare Curs Valutar!" + exp);
                    }
                }                
            }
        }
    }
}
