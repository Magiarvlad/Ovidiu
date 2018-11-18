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
    /// Interaction logic for Frm_Creare_Firma.xaml
    /// </summary>
    public partial class Frm_Creare_Firma : Window
    {
        public Frm_Creare_Firma()
        {
            InitializeComponent();
        }

        private void InfoBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Daca se bifeaza aceasta optiune programul NU va mai cumula pozitii care au acelasi cod valmal; tara origine; conditii de livrare. In Acest caz declaratia Intrastat va contine toate liniile necumulate.");
        }

        private void LabelCif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void NumeFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Nume firma, va recomand fara SC";
        }

        private void RegComertLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "J00/AA/YYYY: Nr de inregistrare de la registrul comertului";
        }

        private void AdresaFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Adresa oficiala, fara oras si judet";
        }

        private void CreazaFirma_Click(object sender, RoutedEventArgs e)
        {
            Frm_Setari_Implicite frm_Setari_Implicite = new Frm_Setari_Implicite();
            frm_Setari_Implicite.Show();
            this.Hide();
        }
    }
}
