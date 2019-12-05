using Ovidiu.EU;
using Ovidiu.Modules;
using System;
using System.Windows;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Frm_Selectie_Firma : Window
    {
        string[,] v;
        Boolean _deschisLaPornire = false;
        public Frm_Selectie_Firma(string[,] vs, Boolean deschisLaPornire)
        {
            _deschisLaPornire = deschisLaPornire;
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
            Frm_Setari_Implicite frm_Setari_Implicite = new Frm_Setari_Implicite(true);
            this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(FRM_Meniu_Principal.IsActiveProperty.ToString()=="false")
                Application.Current.Shutdown();

            if(_deschisLaPornire == true)
                Application.Current.Shutdown();
        }
    }
}
