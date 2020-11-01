using Ovidiu.EU;
using System;
using System.Data.OleDb;
using System.Windows;

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_Sterge_Macheta.xaml
    /// </summary>
    public partial class Frm_Sterge_Macheta : Window
    {
        public Frm_Sterge_Macheta()
        {
            InitializeComponent();
            IncarcareMachete();
        }

        private void IncarcareMachete()
        {
            string tableName = "StructuraFisiereAntet";
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";  //+ Firma.CodFiscal 
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
                    cmbMacheta.Items.Add(dbReader[0].ToString());
                }
            }
            dbConn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Doriti sa stergeti macheta : " + cmbMacheta.SelectedItem.ToString(),"Sterge Macheta",MessageBoxButton.YesNo);  // (cmbMacheta.SelectedIndex).ToString());

            if(messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    //IncarcareDateFisierAntet("StructuraFisiereAntet");
                    DeleteFromFisierContinut();

                    DeleteFromFisierAntet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inregistrarea nu a fost gasita");
                }
            }
            else
            {

            }
        }

        private void DeleteFromFisierContinut()
        {
            OleDbConnection dbConn;
            OleDbCommand dbCommand;
            string dbQuery;
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            dbConn = new OleDbConnection(_oleDBConnectionString);
            dbCommand = new OleDbCommand();
            dbCommand.CommandTimeout = 2000;
            dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = @"Delete * FROM StructuraFisiereContinut WHERE Nume_structura = ?";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbCommand.Parameters.AddWithValue("@Nume_structura", cmbMacheta.SelectedItem.ToString());
            dbCommand.ExecuteNonQuery();

            dbConn.Close();
        }

        private void DeleteFromFisierAntet()
        {
            OleDbConnection dbConn;
            OleDbCommand dbCommand;
            string dbQuery;
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            dbConn = new OleDbConnection(_oleDBConnectionString);
            dbCommand = new OleDbCommand();
            dbCommand.CommandTimeout = 2000;
            dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = @"Delete * FROM StructuraFisiereAntet WHERE Nume_Structura = ?;";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbCommand.Parameters.AddWithValue("@Nume_Structura", cmbMacheta.SelectedItem.ToString());
            dbCommand.ExecuteNonQuery();
            dbConn.Close();
        }

        private void IncarcareDateFisierAntet(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";  //+ Firma.CodFiscal 
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM " + tableName+" WHERE Nume_Structura = ?;";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbCommand.Parameters.AddWithValue("@Nume_Structura", cmbMacheta.SelectedItem.ToString());
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    MessageBox.Show("Inregistrarea a fost gasita", dbReader[0].ToString());
                }
            }
            dbConn.Close();
        }
    }
}
