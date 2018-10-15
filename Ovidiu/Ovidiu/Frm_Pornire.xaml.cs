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

            XML_Setari_Default.Setari_Default_XML();
        }
    }
}
