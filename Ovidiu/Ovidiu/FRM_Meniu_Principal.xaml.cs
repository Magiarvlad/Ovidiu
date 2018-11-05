using Ovidiu.EU;
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

        // private void Window_Activated(object sender, EventArgs e)

    }
}
