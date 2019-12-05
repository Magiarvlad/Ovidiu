using System.Windows;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Mesaj_Demo.xaml
    /// </summary>
    public partial class Frm_Mesaj_Demo : Window
    {
        public Frm_Mesaj_Demo(string context)
        {
            InitializeComponent();

            if (context == "Inregistrare")
            {
                Txt_Titlu.Content = "ATENTIE! Aceasta firma NU este inregistrata";
                Txt_Continut.Text = "Programul e-Intrastat este oferit in varianta GRATUITA fara nici un fel de obligatie de plata.\n\n\n" +
                    "Pentru a beneficia de facilitatile prgramului trebuie sa inregistrati online aceasta firma\n\n" +
                    "Datele transmise de d-voastra in procesul de inregistrare online nu vor fi facute publice si vor fi folosite doar in corespondeta necesara cu d-voastra (transmitere cheie de inregistrare si actualizari ulterioare\n\n" +
                    "Pentru a inregistra online firma va rugam sa apasati butonul INREGISTREAZA ONLINE";
            }
        }

        private void Btn_Inregistreaza_Click(object sender, RoutedEventArgs e)
        {
            Frm_WEB frm_WEB = new Frm_WEB();
            frm_WEB.Show();
            this.Close();
        }

        private void Btn_Inchide_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
