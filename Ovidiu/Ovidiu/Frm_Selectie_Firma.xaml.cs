using Ovidiu.EU;
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
using System.Windows.Shapes;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Frm_Selectie_Firma : Window
    {
        string[,] v;
        public Frm_Selectie_Firma(string[,] vs)
        {
            v = vs;
            InitializeComponent();
            int i = 0;
            while (vs[i,1] != null)
            {
                ComboBoxSelectFirma.Items.Add(vs[i,1] + " " + v[i,0]);
                i++;
            }

        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxSelectFirma.SelectedValue!=null)
            {
            string[] aux = ComboBoxSelectFirma.SelectedItem.ToString().Split(' ');
            for (int i=0;i< v.Length/2;i++)
            {
              if(v[i,0]!=null)
                if(aux[aux.Length-1]==v[i,0] )
                {
                    Firma.CodFiscal = v[i, 0].ToString();
                    Firma.NumeFirma = v[i, 1].ToString();
                }
            }
            CONSTANTE.Meniu.LabelFirma.Content = "Firma: "+ Firma.NumeFirma;
            CONSTANTE.Meniu.LabelFirma.Width += Firma.NumeFirma.Length*6;
            CONSTANTE.Meniu.Show();
            this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(FRM_Meniu_Principal.IsActiveProperty.ToString()=="false")
            Application.Current.Shutdown();
        }
    }
}
