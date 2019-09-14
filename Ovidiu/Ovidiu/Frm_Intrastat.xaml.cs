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
using Excel = Microsoft.Office.Interop.Excel;
namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Intrastat.xaml
    /// </summary>
    public partial class Frm_Intrastat : Window
    {
        List<Intrastat> lista = new List<Intrastat>();
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

            IncarcaGrid(tip,luna,an);
            
        }

        private void IncarcaGrid(string tip, string luna, string an)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal+ ".mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Intrastat where Anul=" + an + " AND Luna="+ luna+" AND TIP='"+tip +"'";
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
            gridIntrastat.ItemsSource = lista;
        }

        public class Intrastat
        {
            string dataReceptiei,descriere,codVamal,cantitate,uM,valoareValuta,moneda,cursSchimb,valoareFiscala,valoareStatistica,taraOrigine,taraExport,taraDestinatie,net,umSupl,cantitateSupl,natTranz,condLivrare,modTransp,facturaNumar,documentData,destTVA,pozitia;
            public Intrastat()
            {
               // data = Data;

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
            public string Descriere { get => descriere; set => descriere = value; }
            public string CodVamal { get => codVamal; set => codVamal = value; }
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
            public string UmSupl { get => umSupl; set => umSupl = value; }
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

            Frm_HS frm_HS = new Frm_HS("Selectie / Cautare", "Tari");
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
    }
}
