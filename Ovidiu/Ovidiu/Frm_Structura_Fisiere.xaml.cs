using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Structura_Fisiere.xaml
    /// </summary>
    public partial class Frm_Structura_Fisiere : System.Windows.Window
    {
        List<Macheta> lista = new List<Macheta>();
        List<Int32> sort_order = new List<Int32>();
        string saveFilePath = FileLocation.System + "Exemplu\\";
        

        public Frm_Structura_Fisiere()
        {
            InitializeComponent();
            IncarcaGrid("StructuraFisiereContinut");
            locatieImplicitaTxt.Text = saveFilePath;
        }

        private void RetineModificari_Click(object sender, RoutedEventArgs e)
        {
            SalvareMacheta();
        }

        private void SalvareMacheta()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = new OleDbCommand();
            dbCommand.CommandTimeout = 2000;
            string dbQuery = string.Empty;

            try
            {
                dbConn.Open();

                if (numeMachetaTxt.Text != String.Empty)
                    dbQuery = "SELECT COUNT([Nume_structura]) FROM StructuraFisiereAntet where Nume_structura='"+ numeMachetaTxt.Text + "';";
                else
                {
                    MessageBox.Show("Introduceti numele machetei");
                    return;
                }

                dbCommand = new OleDbCommand(dbQuery, dbConn);
                int count = Convert.ToInt32(dbCommand.ExecuteScalar());
                if(count>=1)
                {
                    MessageBox.Show("O macheta cu acest nume exista deja!");
                    return;
                }

                dbQuery = @"Insert into StructuraFisiereAntet (Nume_structura,TIP,Locatie_Implicita,Work_Sheet_Name,Sort_Order,SampleExcelFile) VALUES (?,?,?,?,?,?);";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                if(numeMachetaTxt.Text != String.Empty)
                    dbCommand.Parameters.AddWithValue("@Nume_structura", numeMachetaTxt.Text);
                else
                {
                    MessageBox.Show("Introduceti numele machetei");
                    return;
                }

                if (tipMacheta.Text != String.Empty)
                    dbCommand.Parameters.AddWithValue("@TIP", tipMacheta.Text);
                else
                {
                    MessageBox.Show("Introduceti tipul machetei");
                    return;
                }
                dbCommand.Parameters.AddWithValue("@Locatie_Implicita", locatieImplicitaTxt.Text);
                dbCommand.Parameters.AddWithValue("@Work_Sheet_Name", worksheetName.Text);
                dbCommand.Parameters.AddWithValue("@Sort_Order", "");
                dbCommand.Parameters.AddWithValue("@SampleExcelFile", sampleExcelFile.Text);

                dbCommand.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adaugare macheta noua");
            }
        }

        private void IncarcaGrid(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Goala.mdb";  //+ Firma.CodFiscal 
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT DISTINCT Dest_ColumnDescription,Exista_Coloana,Numar_Coloana,Valoare_Implicita,Marime_Maxima,Format, Sort_Order FROM " + tableName + ";"; // order by Sort_Order
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    //dbReader.Read();
                    if (dbReader[0].ToString() != string.Empty)
                    {
                        lista.Add(new Macheta(dbReader[0].ToString(), false, dbReader[2].ToString(), dbReader[3].ToString(), dbReader[4].ToString(), dbReader[5].ToString()));
                        sort_order.Add(Convert.ToInt32(dbReader[6]));
                    }
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

        private void SalveazaContinutMacheta_Click(object sender, RoutedEventArgs e)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = new OleDbCommand();
            dbCommand.CommandTimeout = 2000;
            string dbQuery = string.Empty;

            try
            {
                dbConn.Open();

                if (numeMachetaTxt.Text != String.Empty)
                    dbQuery = "SELECT COUNT([Nume_structura]) FROM StructuraFisiereContinut where Nume_structura='" + numeMachetaTxt.Text + "';";
                else
                {
                    MessageBox.Show("Introduceti numele machetei");
                    return;
                }

                dbCommand = new OleDbCommand(dbQuery, dbConn);
                int count = Convert.ToInt32(dbCommand.ExecuteScalar());
                if (count >= 1)
                {
                    MessageBox.Show("O macheta cu acest nume exista deja!");
                    return;
                }
                int index = 0;
                foreach (Macheta macheta in lista)
                {
                    dbQuery = @"Insert into StructuraFisiereContinut (Nume_structura,Exista_Coloana,Numar_Coloana,Dest_ColumnName,Dest_ColumnDescription,Valoare_Implicita,Marime_Maxima,Format,Sort_Order) VALUES (?,?,?,?,?,?,?,?,?);";
                    dbCommand = new OleDbCommand(dbQuery, dbConn);
                    if (numeMachetaTxt.Text != String.Empty)
                        dbCommand.Parameters.AddWithValue("@Nume_structura", numeMachetaTxt.Text);
                    else
                    {
                        MessageBox.Show("Introduceti numele machetei");
                        return;
                    }

                    dbCommand.Parameters.AddWithValue("@Exista_Coloana", macheta.Exista_In_Fisierul_Excel);
                    if (macheta.Numar_Coloana_Fisier_Excel != String.Empty)
                        dbCommand.Parameters.AddWithValue("@Numar_Coloana", Convert.ToInt32(macheta.Numar_Coloana_Fisier_Excel));
                    else
                        dbCommand.Parameters.AddWithValue("@Numar_Coloana", 0);
                    DestColumName(dbCommand, index);
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnDescription", macheta.Informatie_Necesara);
                    dbCommand.Parameters.AddWithValue("@Valoare_Implicita", macheta.Valoare_Implicita);
                    if (macheta.Caractere_Maxime != String.Empty)
                        dbCommand.Parameters.AddWithValue("@Marime_Maxima", Convert.ToInt32(macheta.Caractere_Maxime));
                    else
                        dbCommand.Parameters.AddWithValue("@Marime_Maxima", 0);
                    dbCommand.Parameters.AddWithValue("@Format", macheta.Formatul_Datelor);
                    dbCommand.Parameters.AddWithValue("@Sort_Order", Convert.ToInt32(sort_order[index]));

                    dbCommand.ExecuteNonQuery();
                    index++;
                }
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adaugare macheta noua");
            }
        }

        private static void DestColumName(OleDbCommand dbCommand, int index)
        {
            switch(index)
            {
                case 0:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Cantitate");
                    break;
                case 1:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Incoterms");
                    break;
                case 2:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Curs_Schimb");
                    break;
                case 3:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Factura_Data");
                    break;
                case 4:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "DataReceptiei");
                    break;
                case 5:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Descriere");
                    break;
                case 6:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Net");
                    break;
                case 7:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Cod_NC");
                    break;
                case 8:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Mod_Transp");
                    break;
                case 9:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Moneda");
                    break;
                case 10:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Nat_Tranz");
                    break;
                case 11:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Factura_Numar");
                    break;
                case 12:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "PU");
                    break;
                case 13:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Tara_Exp");
                    break;
                case 14:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Tara_Orig");
                    break;
                case 15:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "UM");
                    break;
                case 16:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Val_Fiscala");
                    break;
                case 17:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Val_Stat");
                    break;
                case 18:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "Valoare_Valuta");
                    break;
                case 19:
                    dbCommand.Parameters.AddWithValue("@Dest_ColumnName", "VAT_ID");
                    break;
            }
        }
    }
}
