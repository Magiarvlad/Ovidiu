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
          
            if(cmbTipDeclaratie.SelectionBoxItem.ToString()== "ACHIZITIE")
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
    }
}
