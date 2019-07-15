using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Interaction logic for Frm_Structura_Fisiere.xaml
    /// </summary>
    public partial class Frm_Structura_Fisiere : Window
    {
        List<Declaratii> lista = new List<Declaratii>();


        public Frm_Structura_Fisiere()
        {
            InitializeComponent();
        }

        private void RetineModificari_Click(object sender, RoutedEventArgs e)
        {
            //IncarcaGrid("");
        }

        private void IncarcaGrid(string tableName)
        {

            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM " + tableName;
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    //dbReader.Read();
                    if (dbReader[0].ToString() != string.Empty)
                        lista.Add(new Declaratii(dbReader[0].ToString(), dbReader[1].ToString(), dbReader[2].ToString(), dbReader[3].ToString(), dbReader[4].ToString(), dbReader[5].ToString(), dbReader[6].ToString(), dbReader[7].ToString()));
                    //lista.Add(new Declaratii(dbReader[0].ToString()));

                }
            }
            gridIntrastat.ItemsSource = lista;
            //gridInsta.ItemsSource = lista;
            dbConn.Close();
        }

        class Declaratii
        {
            string Sens, Tip_Declaratie, Anul, Luna, Valoare_Valuta, Valoare_Ron, Greutate_Neta_KG, Pozitii;

            public Declaratii(string sens, string tip_Declaratie, string anul, string luna, string valoare_Valuta, string valoare_Ron, string greutate_Neta_KG, string pozitii)
            {
                Sens1 = sens;
                Tip_Declaratie1 = tip_Declaratie;
                Anul1 = anul;
                Luna1 = luna;
                Valoare_Valuta1 = valoare_Valuta;
                Valoare_Ron1 = valoare_Ron;
                Greutate_Neta_KG1 = greutate_Neta_KG;
                Pozitii1 = pozitii;
            }
            public Declaratii(string sens)
            {
                Sens1 = sens;

            }


            public string Sens1 { get => Sens; set => Sens = value; }
            public string Tip_Declaratie1 { get => Tip_Declaratie; set => Tip_Declaratie = value; }
            public string Anul1 { get => Anul; set => Anul = value; }
            public string Luna1 { get => Luna; set => Luna = value; }
            public string Valoare_Valuta1 { get => Valoare_Valuta; set => Valoare_Valuta = value; }
            public string Valoare_Ron1 { get => Valoare_Ron; set => Valoare_Ron = value; }
            public string Greutate_Neta_KG1 { get => Greutate_Neta_KG; set => Greutate_Neta_KG = value; }
            public string Pozitii1 { get => Pozitii; set => Pozitii = value; }
        }
    }
}
