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
    /// Interaction logic for Frm_IntroduceKEY.xaml
    /// </summary>
    public partial class Frm_IntroduceKEY : Window
    {
        public Frm_IntroduceKEY()
        {
            InitializeComponent();
        }

        private void BtnVizualizareKey_Click(object sender, RoutedEventArgs e)
        {
            Frm_VizualizareKEY frm_VizualizareKEY = new Frm_VizualizareKEY();
            frm_VizualizareKEY.Show();
        }
    }
}
