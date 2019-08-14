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
        List<Macheta> lista = new List<Macheta>();


        public Frm_Structura_Fisiere()
        {
            InitializeComponent();
        }

        private void RetineModificari_Click(object sender, RoutedEventArgs e)
        {
            IncarcaGrid("StructuraFisiereContinut");
        }

        private void IncarcaGrid(string tableName)
        {

            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Goala.mdb";  //+ Firma.CodFiscal 
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT DISTINCT Dest_ColumnDescription,Exista_Coloana,Numar_Coloana,Valoare_Implicita,Marime_Maxima,Format, Sort_Order FROM " + tableName + " order by Sort_Order";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    //dbReader.Read();
                    if (dbReader[0].ToString() != string.Empty)
                        lista.Add(new Macheta(dbReader[0].ToString(), false, dbReader[2].ToString(), dbReader[3].ToString(), dbReader[4].ToString(), dbReader[5].ToString()));
                    //lista.Add(new Declaratii(dbReader[0].ToString()));

                }
            }
            gridIntrastat.ItemsSource = lista;
            //gridInsta.ItemsSource = lista;
            dbConn.Close();
        }

        class Macheta
        {
            string informatie_Necesara, numar_Coloana_Fisier_Excel, valoare_Implicita,caractere_Maxime,formatul_Datelor;
            bool exista_In_Fisierul_Excel;
            public Macheta(string informatie_Necesara1, bool exista_In_Fisierul_Excel1, string numar_Coloana_Fisier_Excel1, string valoare_Implicita1, string caractere_Maxime1, string formatul_Datelor1)
            {
                Informatie_Necesara = informatie_Necesara1;
                Exista_In_Fisierul_Excel = exista_In_Fisierul_Excel1;
                Numar_Coloana_Fisier_Excel = "";
                Valoare_Implicita = valoare_Implicita1;
                Caractere_Maxime = caractere_Maxime1;
                Formatul_Datelor = formatul_Datelor1;
            }
          
            public string Informatie_Necesara { get => informatie_Necesara; set => informatie_Necesara = value; }
            public bool Exista_In_Fisierul_Excel { get => exista_In_Fisierul_Excel; set => exista_In_Fisierul_Excel = value; }
            public string Numar_Coloana_Fisier_Excel { get => numar_Coloana_Fisier_Excel; set => numar_Coloana_Fisier_Excel = value; }
            public string Valoare_Implicita { get => valoare_Implicita; set => valoare_Implicita = value; }
            public string Caractere_Maxime { get => caractere_Maxime; set => caractere_Maxime = value; }
            public string Formatul_Datelor { get => formatul_Datelor; set => formatul_Datelor = value; }
        }
    }
}
