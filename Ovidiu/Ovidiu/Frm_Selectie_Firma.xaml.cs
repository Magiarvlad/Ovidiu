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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Frm_Selectie_Firma : Window
    {
        public Frm_Selectie_Firma(string[,] vs)
        {

            InitializeComponent();
            int i = 0;
            while (vs[i,1] != null)
            {
                ComboBoxSelectFirma.Items.Add(vs[i,1]);
                i++;
            }

        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
