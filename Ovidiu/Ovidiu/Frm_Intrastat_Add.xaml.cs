using Ovidiu.EU;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Frm_Intrastat_Add.xaml
    /// </summary>
    public partial class Frm_Intrastat_Add : Window
    {
        public Frm_Intrastat_Add()
        {
            InitializeComponent();
            var today = DateTime.Today;
            var month = today.Month;
            var year = today.Year;
            if (month == 1)
            { year -= 1;
                month = 12;
            }

            txtLuna.Text = month.ToString() ;
            txtAn.Text = year.ToString() ;
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {

            StreamReader stream = new StreamReader(FileLocation.System + "key\\chei.txt");
            string line = "";
            bool flag = false;
            while (true)
            {
                line = stream.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] keys = line.Split('\t');
                string[] arrKeyTxt = new string[4];

                if (keys[0].Length > 17)
                {
                    arrKeyTxt = Inregistrare.DecodeKey(keys[0]);
                    if (arrKeyTxt[0] == keys[1] && txtAn.Text== keys[2])
                    {
                        flag = true;
                    }
                }
            }

            stream.Close();

            if (flag == true)
            {
                if (cmbTipDeclaratie.SelectionBoxItem.ToString() == "ACHIZITIE")
                {
                    Frm_Intrastat frmIntrastat = new Frm_Intrastat("I", txtLuna.Text, txtAn.Text);
                    frmIntrastat.Show();
                }
                else
                {
                    Frm_Intrastat frmIntrastat = new Frm_Intrastat("O", txtLuna.Text, txtAn.Text);
                    frmIntrastat.Show();
                }
            }
            else
            {
                Frm_Mesaj_Demo frmIntrastat = new Frm_Mesaj_Demo("Inregistrare");
                frmIntrastat.Show();
            }


            
        }
    }
}
