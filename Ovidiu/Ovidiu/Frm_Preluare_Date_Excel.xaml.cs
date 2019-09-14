using Ovidiu;
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

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_Preluare_Date_Excel.xaml
    /// </summary>
    public partial class Frm_Preluare_Date_Excel : Window
    {
        public Frm_Preluare_Date_Excel()
        {
            InitializeComponent();
            An.Text = DateTime.Today.Year.ToString();
            Luna.Text = DateTime.Today.Month.ToString();
            CodFiscal.Text = Firma.CodFiscal;

            IncarcareDateFisierAntet("StructuraFisiereAntet");
        }

        private void IncarcareDateFisierAntet(string tableName)
        {
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
                    cbMachetaFolosita.Items.Add(dbReader[0].ToString());
                   
                }
            }
            
            dbConn.Close();
        }

        private void VizualizareDeclaratie_Click(object sender, RoutedEventArgs e)
        {
            string tip;

            if (cbFelOperatiune.SelectedIndex == 0)
                tip = "I";
            else
                tip = "O";
            Frm_Intrastat frmIntrastat = new Frm_Intrastat(tip, Luna.Text, An.Text);
            frmIntrastat.Show();
        }

        private void CbMachetaFolosita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";  //+ Firma.CodFiscal 
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM StructuraFisiereAntet WHERE Nume_Structura='" + cbMachetaFolosita.SelectedValue+"';";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    if (dbReader["TIP"].ToString() == "ACHIZITIE")
                    {
                        cbFelOperatiune.SelectedIndex = 0;
                    }
                    else
                    {
                        cbFelOperatiune.SelectedIndex = 1;
                    }
                    PathExcel.Text = dbReader["Locatie_Implicita"].ToString();
                    cbSheet.Items.Clear();
                    cbSheet.Items.Add(dbReader["Work_Sheet_Name"].ToString());
                    cbSheet.SelectedValue = dbReader["Work_Sheet_Name"].ToString();
                }
            }

            dbConn.Close();
        }
    }
}
