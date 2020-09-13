using Microsoft.Win32;
using Ovidiu;
using Ovidiu.EU;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Ovidiu.Frm_Intrastat;
using static Ovidiu.Modules.CONSTANTE;

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_Preluare_Date_Excel.xaml
    /// </summary>
    public partial class Frm_Preluare_Date_Excel : System.Windows.Window
    {
        List<Macheta> lista = new List<Macheta>();
        ObservableCollection<Intrastat> listaIntrastat = new ObservableCollection<Intrastat>();

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
            Frm_Intrastat frmIntrastat = new Frm_Intrastat(tip, Luna.Text, An.Text, listaIntrastat);
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

        private void Rasfoiere_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                PathExcel.Text = openFileDialog.FileName;
            }
        }

        private void PreluareDate_Click(object sender, RoutedEventArgs e)
        {
            IncarcareDateMacheta();

            _Application xlApp = new _Excel.Application();
            //excel.Visible = true;
            Workbook xlWorkbook = xlApp.Workbooks.Open(PathExcel.Text);
            Worksheet xlWorksheet = (Worksheet)xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            pbPreluareDate.Maximum = rowCount;
            pbPreluareDate.Value = 0;
            for (int i = 1; i <= rowCount; i++)
            { 
                Macheta machetaCurenta = null;
                foreach(Macheta macheta in lista)
                {
                if (xlRange.Cells[i+1, macheta.Numar_Coloana_Fisier_Excel] != null && xlRange.Cells[i + 1, macheta.Numar_Coloana_Fisier_Excel].Value2 != null)
                    DestColumName(i,macheta.Informatie_Necesara, xlRange.Cells[i + 1, macheta.Numar_Coloana_Fisier_Excel].Value2.ToString());
                }
                pbPreluareDate.Value++;
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        private void DestColumName(Int32 i,String index, String value)
        {
            string todaydate = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();

            if( i > listaIntrastat.Count)
                if (cbFelOperatiune.SelectedIndex == 0)
                {
                    Intrastat a = new Intrastat(todaydate, "", "", "", "BUC", "", "EUR", "", "", "", "", Val_Implicite.I_Tara_Exp, "RO", "", "", "", Val_Implicite.I_Nat_Transp, Val_Implicite.I_Incoterms, Val_Implicite.I_Mod_Transp, "", todaydate, "", "");
                    listaIntrastat.Add(a);
                }
                else
                {
                    Intrastat a = new Intrastat(todaydate, "", "", "", "BUC", "", "EUR", "", "", "", "", "RO", Val_Implicite.O_Tara_Dest, "", "", "", Val_Implicite.O_Nat_Tranz, Val_Implicite.O_Incoterms, Val_Implicite.O_Mod_Transp, "", todaydate, "", "");
                    listaIntrastat.Add(a);
                }

            switch (index)
            {
                case "Cantitate":
                    listaIntrastat[listaIntrastat.Count - 1].Cantitate = value;
                    break;
                case "Cod_NC":
                    listaIntrastat[listaIntrastat.Count - 1].CodVamal = value;
                    break;
                case "Curs_Schimb":
                    listaIntrastat[listaIntrastat.Count - 1].CursSchimb = value;
                    break;
                case "DataReceptiei":
                    listaIntrastat[listaIntrastat.Count - 1].DataReceptiei = value;
                    break;
                case "Descriere":
                    listaIntrastat[listaIntrastat.Count - 1].Descriere = value;
                    break;
                case  "Factura_Data":
                    listaIntrastat[listaIntrastat.Count - 1].DocumentData = value;
                    break;
                case  "Factura_Numar":
                    listaIntrastat[listaIntrastat.Count - 1].FacturaNumar = value;
                    break;
                case "Incoterms":
                    listaIntrastat[listaIntrastat.Count - 1].CondLivrare = value;
                    break;
                case  "Mod_Transp":
                    listaIntrastat[listaIntrastat.Count - 1].ModTransp = value;
                    break;
                case  "Moneda":
                    listaIntrastat[listaIntrastat.Count - 1].Moneda = value;
                    break;
                case  "Nat_Tranz":
                    listaIntrastat[listaIntrastat.Count - 1].NatTranz = value;
                    break;
                case  "Net":
                    listaIntrastat[listaIntrastat.Count - 1].Net = value;
                    break;
                case  "Tara_Exp":
                    listaIntrastat[listaIntrastat.Count - 1].TaraExport = value;
                    break;
                case "Tara_Orig":
                    listaIntrastat[listaIntrastat.Count - 1].TaraOrigine = value;
                    break;
                case  "UM":
                    listaIntrastat[listaIntrastat.Count - 1].UM = value;
                    break;
                case "Val_Fiscala":
                    listaIntrastat[listaIntrastat.Count - 1].ValoareFiscala = value;
                    break;
                case "Val_Stat":
                    listaIntrastat[listaIntrastat.Count - 1].ValoareStatistica = value;
                    break;
                case "Valoare_Valuta":
                    listaIntrastat[listaIntrastat.Count - 1].ValoareValuta = value;
                    break;
                case "VAT_ID":
                    listaIntrastat[listaIntrastat.Count - 1].DestTVA = value;
                    break;
                //case "PU":
                //    listaIntrastat[listaIntrastat.Count - 1].ValoareValuta = value;
                //    break;
            }
        }


        private void IncarcareDateMacheta()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";  //+ Firma.CodFiscal 
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM StructuraFisiereContinut where Nume_Structura='" + cbMachetaFolosita.Text + "';";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbCommand.CommandTimeout = 2000;
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    try
                    {
                        if ((Boolean)dbReader[2] == true)
                        {
                            Int32 valImplificta= 0;
                            if (dbReader[7].ToString() != "")
                                valImplificta = Convert.ToInt32(dbReader[7].ToString());
                            lista.Add(new Macheta(dbReader[4].ToString(), Convert.ToInt32(dbReader[3].ToString()), dbReader[6].ToString(), valImplificta, dbReader[8].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare la adaugare macheta noua");
                    }
                }
            }
            dbConn.Close();
            lista.Sort((q, p) => p.Numar_Coloana_Fisier_Excel.CompareTo(q.Numar_Coloana_Fisier_Excel));
        }

        private class Macheta
        {
            string informatie_Necesara, valoare_Implicita, formatul_Datelor;
            Int32 numar_Coloana_Fisier_Excel, caractere_Maxime;

            public Macheta(string informatie_Necesara1, Int32 numar_Coloana_Fisier_Excel1, string valoare_Implicita1, Int32 caractere_Maxime1, string formatul_Datelor1)
            {
                Informatie_Necesara = informatie_Necesara1;
                Numar_Coloana_Fisier_Excel = numar_Coloana_Fisier_Excel1;
                Valoare_Implicita = valoare_Implicita1;
                Caractere_Maxime = caractere_Maxime1;
                Formatul_Datelor = formatul_Datelor1;
            }

            public string Informatie_Necesara { get => informatie_Necesara; set => informatie_Necesara = value; }
            public Int32 Numar_Coloana_Fisier_Excel { get => numar_Coloana_Fisier_Excel; set => numar_Coloana_Fisier_Excel = value; }
            public string Valoare_Implicita { get => valoare_Implicita; set => valoare_Implicita = value; }
            public Int32 Caractere_Maxime { get => caractere_Maxime; set => caractere_Maxime = value; }
            public string Formatul_Datelor { get => formatul_Datelor; set => formatul_Datelor = value; }
        }
    }
}
