using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Frm_Setari_Implicite.xaml
    /// </summary>
    public partial class Frm_Setari_Implicite : Window
    {
        

        private ObservableCollection<Tara> tara = new ObservableCollection<Tara>();
        public Frm_Setari_Implicite()
        {
            InitializeComponent();


            string[,] sv = new string[1, 2];
            sv[0, 0] = "RO";
            sv[0, 1] = "Romania";
            tara.Add(new Tara() { Name = "RO", State = "Romania" });
            tara.Add(new Tara() { Name = "Bg", State = "Bulgaria" });
            tara.Add(new Tara() { Name = "RO", State = "Romania" });
            tara.Add(new Tara() { Name = "RO", State = "Romania" });


            DataContext = tara;
        }
        private class Tara
        {
            public string State
            { get; set; }

            public string Name
            { get; set; }
        }
    }
}
