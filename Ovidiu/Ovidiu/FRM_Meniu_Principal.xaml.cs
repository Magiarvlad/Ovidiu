using Ovidiu.EU;
using Ovidiu.Miscellaneous;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Frm_Creare_Firma frm_Creare_Firma = new Frm_Creare_Firma();
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



        // private void Window_Activated(object sender, EventArgs e)

    }
}
