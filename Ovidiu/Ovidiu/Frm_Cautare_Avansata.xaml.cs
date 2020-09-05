using Ovidiu.EU;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_Cautare_Avansata.xaml
    /// </summary>
    public partial class Frm_Cautare_Avansata : Window
    {
        List<Cod_Vamal> _cod_Vamal_list = new List<Cod_Vamal>();
        List<Intrastat> lista = new List<Intrastat>();

        public Frm_Cautare_Avansata(string text)
        {
            InitializeComponent();
            Text.Text = text;

            IncarcaTabela_HS8("HS_8",text);
            IncarcaTabela_Declaratii("Intrastat", text);
        }

        private void IncarcaTabela_Declaratii(string v, string text)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Intrastat WHERE DESCRIERE like '%" + text + "%' ;";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    lista.Add(new Intrastat(dbReader[3].ToString(), dbReader[4].ToString(), dbReader[7].ToString(), dbReader[9].ToString(), dbReader[10].ToString(), dbReader[11].ToString(), dbReader[6].ToString()));
                }
            }
            dbConn.Close();
            LabelDeclaratii.Content = lista.Count.ToString() + LabelDeclaratii.Content + Text.Text;
            dgDeclaratii.ItemsSource = lista;
        }

        private void IncarcaTabela_HS8(string tableName, string textcautare)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "CN\\" + "CN_" + System.DateTime.Today.Year + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM " + tableName + " WHERE DESCRIERE like '%" +textcautare +"%' ;";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    _cod_Vamal_list.Add(new Cod_Vamal(dbReader[1].ToString(), dbReader[2].ToString(), dbReader[3].ToString().Substring(0,10), dbReader[9].ToString(), dbReader[8].ToString()));
                }
            }
            dgTarifVamal.ItemsSource = _cod_Vamal_list;
            LabelTarif.Content = _cod_Vamal_list.Count.ToString() + LabelTarif.Content + Text.Text;
            dbConn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             _cod_Vamal_list = new List<Cod_Vamal>();
             lista = new List<Intrastat>();
            IncarcaTabela_HS8("HS_8", Text.Text);
            IncarcaTabela_Declaratii("Intrastat", Text.Text);           
        }
        
        public class Intrastat
        {
            string anul, luna, cod_NC, cantitate, uM, valoarea_Fiscala, descriere;

            public Intrastat(string anul, string luna, string cod_NC, string cantitate, string uM, string valoarea_Fiscala, string descriere)
            {
                Anul = anul;
                Luna = luna;
                Cod_NC = cod_NC;
                Cantitate = cantitate;
                UM = uM;
                Valoarea_Fiscala = valoarea_Fiscala;
                Descriere = descriere;
            }

            public string Anul { get => anul; set => anul = value; }
            public string Luna { get => luna; set => luna = value; }
            public string Cod_NC { get => cod_NC; set => cod_NC = value; }
            public string Cantitate { get => cantitate; set => cantitate = value; }
            public string UM { get => uM; set => uM = value; }
            public string Valoarea_Fiscala { get => valoarea_Fiscala; set => valoarea_Fiscala = value; }
            public string Descriere { get => descriere; set => descriere = value; }
        }
                
        class Cod_Vamal
        {
            string cod_NC, cod_Vamal, data, um, denumire;

            public Cod_Vamal(string cod_NC, string cod__Vamal, string data, string um, string denumire)
            {
                Cod_NC = cod_NC;
                Cod__Vamal = cod__Vamal;
                Data = data;
                Um = um;
                Denumire = denumire;
            }

            public string Cod_NC { get => cod_NC; set => cod_NC = value; }
            public string Cod__Vamal { get => cod_Vamal; set => cod_Vamal = value; }
            public string Data { get => data; set => data = value; }
            public string Um { get => um; set => um = value; }
            public string Denumire { get => denumire; set => denumire = value; }
        }
    }
}
