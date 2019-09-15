using Ovidiu.EU;
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

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_DateCurs.xaml
    /// </summary>
    public partial class Frm_DateCurs : Window
    {
        List<DateCurs> lista = new List<DateCurs>();
        List<DateCurs>  lista_scrie = new List<DateCurs>();
        List<String> lines =  new List<String > ();
        string path = FileLocation.System + "CursBNR\\curs.txt";


        public Frm_DateCurs()
        {
            InitializeComponent();
            IncarcaDateGrid();
        }


        private void IncarcaDateGrid()
        {
            
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] value = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                lista.Add(new DateCurs(value[0], value[1], value[2], value[3]));
            }

            GridDateCurst.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            lines.Clear();
            foreach (var item in GridDateCurst.Items.OfType<DateCurs>())
            {
                var data = item.Data;
                var moneda = item.Moneda;
                var numar = item.Numar;
                var valoare = item.Valoare;
                // lista_scrie.Add(new DateCurs(data, moneda, numar, valoare));
                lines.Add( "" + data + " " + moneda + " " + numar + " " + valoare );
            }
            // File.Delete(@"\E_Intrastat\System\CursBNR\curstest.txt");

            File.WriteAllText(path, String.Empty);

            File.WriteAllLines(path, lines);

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // adauga linie noua
            GridDateCurst.ItemsSource = null;
            DateCurs data = new DateCurs("", "", "", "");
            lista.Add(data);
            GridDateCurst.ItemsSource = lista;
            GridDateCurst.SelectedItem = data;
            GridDateCurst.ScrollIntoView(GridDateCurst.SelectedItem);
        }
    }

    class DateCurs
    {
        string data, moneda, numar,valoare;

        public DateCurs(string data, string moneda, string numar, string valoare)
        {
            this.data = data;
            this.moneda = moneda;
            this.numar = numar;
            this.valoare = valoare;
        }

        public string Data { get => data; set => data = value; }
        public string Moneda { get => moneda; set => moneda = value; }
        public string Numar { get => numar; set => numar = value; }
        public string Valoare { get => valoare; set => valoare = value; }
    }
}
