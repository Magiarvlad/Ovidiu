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
                /* 
                    '**************************
                
                    Call EU_Registrii
                    '**************************


                    Call Citeste_Culori
                    Call Citeste_Zecimale
                    Call Citeste_FileLocation
                    Call Citeste_Diverse
                */

                EU_Registrii_Operatii.EU_Registrii();
                XML_Public_Citeste.Citeste_CUlori();
                XML_Public_Citeste.Citeste_Zecimale();
                XML_Public_Citeste.Citeste_FileLocation();
                XML_Public_Citeste.Citeste_Diverse();

                if ( true ) // here should be DIV.VerificaUpdate? where the F is DIV?
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
            }
            catch (Exception exp)
            {
                MessageBox.Show("Frm_Pornire_Loaded Error: " + exp.Message);
            }
            
        }
    }
}
