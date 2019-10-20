using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Ovidiu.Modules.CONSTANTE;
using Excel = Microsoft.Office.Interop.Excel;
namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Intrastat.xaml
    /// </summary>
    public partial class Frm_Intrastat : Window
    {
        ObservableCollection<Intrastat> lista = new ObservableCollection<Intrastat>();
        List<String> listaDescrieri = new List<String>();
        bool isLoaded = false;

        public Frm_Intrastat(string tip, string luna, string an)
        {
            InitializeComponent();
            cmbTipDeclaratie.SelectedItem = cmbTipDeclaratie.Items[0];
            txtCUI.Text = Firma.CodFiscal;
            txtVATID.Text = Firma.NumeFirma;
            IncarcaDateFirma();
            txtTip.Text = tip;
            txtLuna.Text = luna;
            txtAn.Text = an;

            IncarcaGrid(tip, luna, an);
            IncarcaDescrieri();

            isLoaded = true;
        }

        private void cbDescriere_Initialized(object sender, EventArgs e)
        {
            ComboBox obj = sender as ComboBox;
            obj.ItemsSource = listaDescrieri;
        }
        private void IncarcaDescrieri()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT DISTINCT Descriere FROM Intrastat";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    listaDescrieri.Add(dbReader[0].ToString());
                }
            }
            dbConn.Close();
        }

        private void IncarcaGrid(string tip, string luna, string an)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Intrastat where Anul=" + an + " AND Luna=" + luna + " AND TIP='" + tip + "'";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    lista.Add(new Intrastat(dbReader[5].ToString(), dbReader[6].ToString(), dbReader[7].ToString(), dbReader[9].ToString(), dbReader[10].ToString(), dbReader[11].ToString(), dbReader[12].ToString(), dbReader[13].ToString(), dbReader[14].ToString(), dbReader[15].ToString(), dbReader[16].ToString(), dbReader[17].ToString(), dbReader[18].ToString(), dbReader[19].ToString(), dbReader[21].ToString(), dbReader[22].ToString(), dbReader[23].ToString(), dbReader[24].ToString(), dbReader[25].ToString(), dbReader[26].ToString(), dbReader[27].ToString(), dbReader[28].ToString(), dbReader[29].ToString()));
                }
            }
            dbConn.Close();
            AddNewLine();
            
            
            gridIntrastat.ItemsSource = lista;
        }

        private void AddNewLine()
        {
            bool canBeSaved = false;

            if (gridIntrastat.SelectedIndex == lista.Count - 1)
            {
                if(gridIntrastat.SelectedIndex > 0)
                {
                    if (lista[gridIntrastat.SelectedIndex - 1].Cantitate != "")
                    {
                        if (lista[gridIntrastat.SelectedIndex - 1].DataReceptiei != "")
                        {
                            if (lista[gridIntrastat.SelectedIndex - 1].CodVamal != "")
                            {
                                if (lista[gridIntrastat.SelectedIndex - 1].ValoareValuta != "")
                                {
                                    if (lista[gridIntrastat.SelectedIndex - 1].TaraOrigine != "")
                                    {
                                        if (lista[gridIntrastat.SelectedIndex - 1].TaraExport != "")
                                        {
                                            if (lista[gridIntrastat.SelectedIndex - 1].TaraDestinatie != "")
                                            {
                                                if (lista[gridIntrastat.SelectedIndex - 1].NatTranz != "")
                                                {
                                                    if (lista[gridIntrastat.SelectedIndex - 1].CondLivrare != "")
                                                    {
                                                        if (lista[gridIntrastat.SelectedIndex - 1].ModTransp != "")
                                                        {
                                                            if (lista[gridIntrastat.SelectedIndex - 1].Moneda != "")
                                                            {
                                                                canBeSaved = true;
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Coloana Moneda nu poate fi goala");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Coloana Mod Transport nu poate fi goala");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Coloana Conditii Livrare nu poate fi goala");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Coloana Natura Tranactie nu poate fi goala");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Coloana Tara Destinatie nu poate fi goala");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Coloana Tara Export nu poate fi goala");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Coloana Tara Origine nu poate fi goala");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Coloana Valoare Valuta nu poate fi goala");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Coloana Cod Vamal nu poate fi goala");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Coloana Data Receptiei nu poate fi goala");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Coloana Cantitate nu poate fi goala");
                    }
                }
               if(canBeSaved==true)
                {
                    SaveLine();

                    if (txtTip.Text == "I")
                    {
                        Intrastat a = new Intrastat("", "", "", "", "", "", "", "", "", "", "", Val_Implicite.I_Tara_Exp, "RO", "", "", "", Val_Implicite.I_Nat_Transp, Val_Implicite.I_Incoterms, Val_Implicite.I_Mod_Transp, "", "", "", "");
                        lista.Add(a);
                    }
                    else
                    {
                        Intrastat a = new Intrastat("", "", "", "", "", "", "", "", "", "", "", "RO", Val_Implicite.O_Tara_Dest, "", "", "", Val_Implicite.O_Nat_Tranz, Val_Implicite.O_Incoterms, Val_Implicite.O_Mod_Transp, "", "", "", "");
                        lista.Add(a);
                    }
                }
               
            }
        }

        private void SaveLine()
        {
            int numarInregistrari = ReturneazaNumarInregistrari();
            
            if(numarInregistrari<lista.Count)
            {
                string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
                OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
                OleDbCommand dbCommand = null;
                string dbQuery = string.Empty;
                string data = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                try
                {
                    dbConn.Open();
                    //dbQuery = "UPDATE [Intrastat_Default] SET [I_Tara_Exp]='" + Val_Implicite.I_Tara_Exp + "', [I_Incoterm]='" + Val_Implicite.I_Incoterms + "', [I_Nat_Tranz]='" + Val_Implicite.I_Nat_Transp + "', [I_Mod_Transp]='" + Val_Implicite.I_Mod_Transp + "', [O_Tara_Dest]='" + Val_Implicite.O_Tara_Dest + "', [O_Incoterm]='" + Val_Implicite.O_Incoterms + "', [O_Nat_Tranz]='" + Val_Implicite.O_Nat_Tranz + "', [O_Mod_Transp]='" + Val_Implicite.O_Mod_Transp + "' WHERE [Cod_Fiscal]='" + Firma.CodFiscal + "';";
                    dbQuery = @"Insert into Intrastat (TIP,Cod_Fiscal,Anul,Luna,DataReceptiei,Descriere,Cod_NC,Descriere_NC,Cantitate,UM,Valoare_Valuta,Moneda,Curs_Schimb,Val_Fiscala,Val_Stat,Tara_Orig,Tara_Exp,Tara_Dest,Net,Exista_UMS,Cod_UMS,Val_UMS,Nat_Tranz,Incoterms,Mod_transp,Factura_Numar,Factura_Data,VAT_ID,PU,Net_Unitar,Raport_Stat,SourceFile,SourceFile_FullPath,Data_Preluare,UserName,Probleme,Transp_Document,Transport_Suma,Transport_Moneda,Transport_Curs) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";
                    dbCommand = new OleDbCommand(dbQuery, dbConn);
                    dbCommand.Parameters.AddWithValue("@TIP", txtTip.Text);
                    dbCommand.Parameters.AddWithValue("@Cod_Fiscal", txtCUI.Text);
                    dbCommand.Parameters.AddWithValue("@Anul", txtAn.Text);
                    dbCommand.Parameters.AddWithValue("@Luna", txtLuna.Text);
                    dbCommand.Parameters.AddWithValue("@DataReceptiei", lista[lista.Count - 1].DataReceptiei);
                    dbCommand.Parameters.AddWithValue("@Descriere", lista[lista.Count - 1].Descriere);
                    dbCommand.Parameters.AddWithValue("@Cod_NC", lista[lista.Count - 1].CodVamal);
                    dbCommand.Parameters.AddWithValue("@Descriere_NC", lista[lista.Count - 1].Descriere);
                    dbCommand.Parameters.AddWithValue("@Cantitate", lista[lista.Count - 1].Cantitate);
                    dbCommand.Parameters.AddWithValue("@UM", lista[lista.Count - 1].UM);
                    dbCommand.Parameters.AddWithValue("@Valoare_Valuta", lista[lista.Count - 1].ValoareValuta);
                    dbCommand.Parameters.AddWithValue("@Moneda", lista[lista.Count - 1].Moneda);
                    dbCommand.Parameters.AddWithValue("@Curs_Schimb", lista[lista.Count - 1].CursSchimb);
                    dbCommand.Parameters.AddWithValue("@Val_Fiscala", lista[lista.Count - 1].ValoareFiscala);
                    dbCommand.Parameters.AddWithValue("@Val_Stat", lista[lista.Count - 1].ValoareStatistica);
                    dbCommand.Parameters.AddWithValue("@Tara_Orig", lista[lista.Count - 1].TaraOrigine);
                    dbCommand.Parameters.AddWithValue("@Tara_Exp", lista[lista.Count - 1].TaraExport);
                    dbCommand.Parameters.AddWithValue("@Tara_Dest", lista[lista.Count - 1].TaraDestinatie);
                    dbCommand.Parameters.AddWithValue("@Net", lista[lista.Count - 1].Net);
                    if (lista[lista.Count - 1].UmSupl == "")
                        dbCommand.Parameters.AddWithValue("@Exista_UMS", false);
                    else
                        dbCommand.Parameters.AddWithValue("@Exista_UMS", true);
                    dbCommand.Parameters.AddWithValue("@Cod_UMS", lista[lista.Count - 1].UmSupl);
                    dbCommand.Parameters.AddWithValue("@Val_UMS", lista[lista.Count - 1].CantitateSupl);
                    dbCommand.Parameters.AddWithValue("@Nat_Tranz", lista[lista.Count - 1].NatTranz);
                    dbCommand.Parameters.AddWithValue("@Incoterms", lista[lista.Count - 1].CondLivrare);
                    dbCommand.Parameters.AddWithValue("@Mod_transp", lista[lista.Count - 1].ModTransp);
                    dbCommand.Parameters.AddWithValue("@Factura_Numar", lista[lista.Count - 1].FacturaNumar);
                    dbCommand.Parameters.AddWithValue("@Factura_Data", lista[lista.Count - 1].DocumentData);
                    dbCommand.Parameters.AddWithValue("@VAT_ID", lista[lista.Count - 1].DestTVA);
                    dbCommand.Parameters.AddWithValue("@PU", "0");
                    dbCommand.Parameters.AddWithValue("@Net_Unitar", "0");
                    dbCommand.Parameters.AddWithValue("@Raport_Stat", "1");
                    dbCommand.Parameters.AddWithValue("@SourceFile", "");
                    dbCommand.Parameters.AddWithValue("@SourceFile_FullPath", "");
                    dbCommand.Parameters.AddWithValue("@Data_Preluare", lista[lista.Count - 1].DocumentData);
                    dbCommand.Parameters.AddWithValue("@UserName", txtPrenume.Text);
                    dbCommand.Parameters.AddWithValue("@Probleme", "0");
                    dbCommand.Parameters.AddWithValue("@Transp_Document", "");
                    dbCommand.Parameters.AddWithValue("@Transport_Suma", "0");
                    dbCommand.Parameters.AddWithValue("@Transport_Moneda", "");
                    dbCommand.Parameters.AddWithValue("@Transport_Curs", "0");
                    /*
                     * @"UPDATE emp SET ename = ?, job = ?, sal = ?, dept = ? WHERE eno = ?";
                         OleDbCommand cmd = new OleDbCommand(query, con)
                         cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
                         cmd.Parameters.AddWithValue("@job", TextBox3.Text);
                         cmd.Parameters.AddWithValue("@sal", TextBox4.Text);
                         cmd.Parameters.AddWithValue("@dept", TextBox5.Text);
                         cmd.ParametersAddWithValue("@eno", TextBox1.Text);
                     */


                    dbCommand.ExecuteNonQuery();
                    dbConn.Close();
                }
                catch (Exception ex)
                {

                }
            }
           
        }

        private int ReturneazaNumarInregistrari()
        {
            int count = 0;
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT COUNT(*) FROM Intrastat where Anul=" + txtAn.Text + " AND Luna=" + txtLuna.Text + " AND TIP='" + txtTip.Text + "'";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            count = (int)dbCommand.ExecuteScalar();
            dbConn.Close();

            return count;
        }

        public class Intrastat : INotifyPropertyChanged
        {
            string dataReceptiei, descriere, codVamal, cantitate, uM, valoareValuta, moneda, cursSchimb, valoareFiscala, valoareStatistica, taraOrigine, taraExport, taraDestinatie, net, umSupl, cantitateSupl, natTranz, condLivrare, modTransp, facturaNumar, documentData, destTVA, pozitia;
            public Intrastat()
            {
                // data = Data;

            }
            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

            public Intrastat(string DataReceptiei, string Descriere, string CodVamal, string Cantitate, string UM, string valoareValuta, string moneda, string cursSchimb, string valoareFiscala, string valoareStatistica, string taraOrigine, string taraExport, string taraDestinatie, string net, string umSupl, string cantitateSupl, string natTranz, string condLivrare, string modTransp, string facturaNumar, string documentData, string destTVA, string pozitia)
            {
                this.DataReceptiei = DataReceptiei;
                this.Descriere = Descriere;
                this.CodVamal = CodVamal;
                this.Cantitate = Cantitate;
                this.UM = UM;
                ValoareValuta = valoareValuta;
                Moneda = moneda;
                CursSchimb = cursSchimb;
                ValoareFiscala = valoareFiscala;
                ValoareStatistica = valoareStatistica;
                TaraOrigine = taraOrigine;
                TaraExport = taraExport;
                TaraDestinatie = taraDestinatie;
                Net = net;
                UmSupl = umSupl;
                CantitateSupl = cantitateSupl;
                NatTranz = natTranz;
                CondLivrare = condLivrare;
                ModTransp = modTransp;
                FacturaNumar = facturaNumar;
                DocumentData = documentData;
                DestTVA = destTVA;
                Pozitia = pozitia;
            }

            public string DataReceptiei { get => dataReceptiei; set => dataReceptiei = value; }
            public string Descriere { get => descriere; set { descriere = value; this.NotifyPropertyChanged("Descriere"); } }
            public string CodVamal { get => codVamal; set { codVamal = value; this.NotifyPropertyChanged("CodVamal"); } }
            public string Cantitate { get => cantitate; set => cantitate = value; }
            public string UM { get => uM; set => uM = value; }
            public string ValoareValuta { get => valoareValuta; set => valoareValuta = value; }
            public string Moneda { get => moneda; set => moneda = value; }
            public string CursSchimb { get => cursSchimb; set => cursSchimb = value; }
            public string ValoareFiscala { get => valoareFiscala; set => valoareFiscala = value; }
            public string ValoareStatistica { get => valoareStatistica; set => valoareStatistica = value; }
            public string TaraOrigine { get => taraOrigine; set => taraOrigine = value; }
            public string TaraExport { get => taraExport; set => taraExport = value; }
            public string TaraDestinatie { get => taraDestinatie; set => taraDestinatie = value; }
            public string Net { get => net; set => net = value; }
            public string UmSupl { get => umSupl; set { umSupl = value; this.NotifyPropertyChanged("UmSupl"); } }
            public string CantitateSupl { get => cantitateSupl; set => cantitateSupl = value; }
            public string NatTranz { get => natTranz; set => natTranz = value; }
            public string CondLivrare { get => condLivrare; set => condLivrare = value; }
            public string ModTransp { get => modTransp; set => modTransp = value; }
            public string FacturaNumar { get => facturaNumar; set => facturaNumar = value; }
            public string DocumentData { get => documentData; set => documentData = value; }
            public string DestTVA { get => destTVA; set => destTVA = value; }
            public string Pozitia { get => pozitia; set => pozitia = value; }
        }

        private void IncarcaDateFirma()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Firme where Cod_Fiscal='" + Firma.CodFiscal + "'";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    // content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));

                    txtPozComp.Text = dbReader[9].ToString();
                    string[] numeprenume = dbReader[8].ToString().Split(' ');
                    txtNume.Text = numeprenume[0];
                    txtPrenume.Text = numeprenume[1];
                    txtTelefon.Text = dbReader[10].ToString();
                    txtFax.Text = dbReader[11].ToString();
                    txtEmail.Text = dbReader[12].ToString();
                }
            }

            dbConn.Close();
        }

        private async void CodVamal_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;   
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "HS_8");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_codVamal;
            lista[gridIntrastat.SelectedIndex].Descriere= Frm_HS.s_Descriere;
            lista[gridIntrastat.SelectedIndex].UmSupl = Frm_HS.s_UM_Supl;
        }

        private async void Moneda_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "Monezi");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private async void Tari_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "TARI_UE");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private void BtnExportaExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < gridIntrastat.Columns.Count; j++)
            {
                Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = gridIntrastat.Columns[j].Header;
            }
            for (int i = 0; i < gridIntrastat.Columns.Count; i++)
            {
                for (int j = 0; j < gridIntrastat.Items.Count; j++)
                {
                    TextBlock b = gridIntrastat.Columns[i].GetCellContent(gridIntrastat.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    try
                    {
                        myRange.Value2 = b.Text;
                    }
                    catch
                    {

                        // myRange.Value2 = gridIntrastat.Columns[i].GetCellContent(gridIntrastat.Items[j]).ToString();
                    }
                }
            }
        }
        public static int coloaneIntrastat = 0;
        private void BtnTipareste_Click(object sender, RoutedEventArgs e)
        {
            PrintDG print = new PrintDG();

            coloaneIntrastat = 23;
            print.printDG(gridIntrastat, "Intrastat");
        }

        private class PrintDG
        {
            public void printDG(DataGrid dataGrid, string title)
            {



                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    FlowDocument fd = new FlowDocument();

                    Paragraph p = new Paragraph(new Run(title));
                    p.FontStyle = dataGrid.FontStyle;
                    p.FontFamily = dataGrid.FontFamily;
                    p.FontSize = 18;
                    fd.Blocks.Add(p);

                    Table table = new Table();
                    TableRowGroup tableRowGroup = new TableRowGroup();
                    TableRow r = new TableRow();
                    fd.PageWidth = printDialog.PrintableAreaWidth;
                    fd.PageHeight = printDialog.PrintableAreaHeight;
                    fd.BringIntoView();

                    fd.TextAlignment = TextAlignment.Center;
                    fd.ColumnWidth = 500;
                    table.CellSpacing = 0;

                    var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();
                    List<dynamic> bindList = new List<dynamic>();


                    for (int j = 0; j < headerList.Count; j++)
                    {

                        r.Cells.Add(new TableCell(new Paragraph(new Run(headerList[j]))));
                        r.Cells[j].ColumnSpan = 4;
                        r.Cells[j].Padding = new Thickness(4);



                        r.Cells[j].BorderBrush = Brushes.Black;
                        r.Cells[j].FontWeight = FontWeights.Bold;
                        r.Cells[j].Background = Brushes.DarkGray;
                        r.Cells[j].Foreground = Brushes.White;
                        r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                        var binding = (dataGrid.Columns[j] as DataGridBoundColumn).Binding as Binding;

                        bindList.Add(binding.Path.Path);
                    }
                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {

                        dynamic row;
                        if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                        { row = (System.Data.DataRowView)dataGrid.Items.GetItemAt(i); }
                        else
                        {
                            row = dataGrid.Items.GetItemAt(i);

                        }

                        table.BorderBrush = Brushes.Gray;
                        table.BorderThickness = new Thickness(1, 1, 0, 0);
                        table.FontStyle = dataGrid.FontStyle;
                        table.FontFamily = dataGrid.FontFamily;
                        table.FontSize = 13;
                        tableRowGroup = new TableRowGroup();
                        r = new TableRow();
                        for (int j = 0; j < coloaneIntrastat; j++)
                        {

                            if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                            {
                                r.Cells.Add(new TableCell(new Paragraph(new Run(row.Item[j].ToString()))));

                            }
                            else
                            {

                                r.Cells.Add(new TableCell(new Paragraph(new Run(row.GetType().GetProperty(bindList[j]).GetValue(row, null)))));

                            }



                            r.Cells[j].ColumnSpan = 4;
                            r.Cells[j].Padding = new Thickness(4);

                            r.Cells[j].BorderBrush = Brushes.DarkGray;
                            r.Cells[j].BorderThickness = new Thickness(0, 0, 1, 1);
                        }

                        tableRowGroup.Rows.Add(r);
                        table.RowGroups.Add(tableRowGroup);

                    }
                    fd.Blocks.Add(table);

                    printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");

                }
            }

        }

        private void GridIntrastat_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GridIntrastat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Cancel [Enter] key event.
                e.Handled = true;
                // Press [Tab] key programatically.
                var tabKeyEvent = new KeyEventArgs(
                  e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Tab);
                tabKeyEvent.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(tabKeyEvent);
            }
        }

        private async void TextBox_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "UM");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private async void NatTranz_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "Tranzactii");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private async void CondLiv_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "Incoterms");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private async void ModTran_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.Text = "";

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "ModTransp");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            obj.Text = Frm_HS.s_moneda;
        }

        private async void cbDescriere_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "HS_8");
            frm_HS.InfoCautareLabel.Content = "DUBLU CLICK pentru a selecta inregistrarea curenta";
            frm_HS.Show();
            frm_HS.Topmost = true;
            while (Frm_HS.s_go == false)
            {
                await Task.Delay(25);
            }

            Frm_HS.s_go = false;
            lista[gridIntrastat.SelectedIndex].CodVamal = Frm_HS.s_codVamal;
            lista[gridIntrastat.SelectedIndex].Descriere = Frm_HS.s_Descriere;
            lista[gridIntrastat.SelectedIndex].UmSupl = Frm_HS.s_UM_Supl;

            AddNewLine();
        }

        private void CodVamal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_3(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_4(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_5(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModTran_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = sender as ComboBox;
            a.IsDropDownOpen = true;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            List<Intrastat> gridView = gridIntrastat.ItemsSource as List<Intrastat>;
            //gridView.SelectedIndex = gridIntrastat.SelectedIndex;

            if (cb.Text != null)
            {
                AddNewLine();
                string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
                OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
                OleDbCommand dbCommand = null;
                OleDbDataReader dbReader = null;
                string dbQuery = string.Empty;
                dbConn.Open();
                dbQuery = "SELECT Cod_NC,Descriere FROM Intrastat Where Descriere='" + cb.Text.ToString() + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    dbReader.Read();
                    try
                    {
                        //gridIntrastat.ItemsSource = null;
                        lista[gridIntrastat.SelectedIndex].CodVamal = dbReader[0].ToString();
                        lista[gridIntrastat.SelectedIndex].Descriere = dbReader[1].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //listaDescrieri.Add(dbReader[0].ToString());

                }
                dbConn.Close();

            }
        }
    }
}
