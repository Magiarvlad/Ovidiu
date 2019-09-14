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

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_HS.xaml
    /// </summary>
    public partial class Frm_HS : Window
    {
        List<Tari> content_Tari = new List<Tari>();
        List<TARI_UE> content_Tari_UE = new List<TARI_UE>();
        List<Tari> content_Monezi = new List<Tari>();

        List<Cod_Vamal> _cod_Vamal_list = new List<Cod_Vamal>();

        string opentab = "";
        public static bool s_go = false;

        public Frm_HS(string name, string tableName)
        {
            InitializeComponent();
            this.Title = name;
            this.Show();
            s_go = false;
            // ToateInreg_Btn.Visibility = Visibility.Hidden;
            // Cautare_Btn.Visibility = Visibility.Hidden;
            // Cautare_Avansata_Btn.Visibility = Visibility.Hidden;
            //  Legatura_Capitole_Btn.Visibility = Visibility.Hidden;
            m1.Visibility = Visibility.Hidden;
            m2.Visibility = Visibility.Hidden;
            m3.Visibility = Visibility.Hidden;
            m4.Visibility = Visibility.Hidden;
            InfoCautareLabel.Visibility = Visibility.Hidden;
            if (tableName == "Tari")
                IncarcaTabela_Tari(tableName);
            if (tableName == "ModTransp")
                IncarcaTabela_ModTransp(tableName);
            if (tableName == "TARI_UE")
                IncarcaTabela_Tari_UE(tableName);
            if (tableName == "Monezi")
                IncarcaTabela_Tari(tableName);
            if (tableName == "Incoterms")
                IncarcaTabela_Tari(tableName);
            if (tableName == "Tranzactii")
                IncarcaTabela_Tranzactii(tableName);
            if (tableName == "UM")
                IncarcaTabela_UM(tableName);

            if (tableName == "HS_1" || tableName == "HS_2" || tableName == "HS_4" || tableName == "HS_6")
                IncarcaTabela_HS1(tableName);

            if (tableName == "HS_8")
                IncarcaTabela_HS8(tableName);
            opentab = tableName;
        }

        private void IncarcaTabela_ModTransp(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT COD_MOD_TRANS, DESC_MOD_TRANS FROM MOD_TRANS ORDER BY COD_MOD_TRANS";
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));

                }
            }
            Grid_HS.ItemsSource = content_Tari;
            dbConn.Close();
        }

        private void IncarcaTabela_HS8(string tableName)
        {

            m1.Visibility = Visibility.Visible;
            m2.Visibility = Visibility.Visible;
            m3.Visibility = Visibility.Visible;

            InfoCautareLabel.Visibility = Visibility.Visible;
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "CN\\" + "CN_" + System.DateTime.Today.Year + ".mdb";
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
                    _cod_Vamal_list.Add(new Cod_Vamal(dbReader[1].ToString(), dbReader[2].ToString(), dbReader[8].ToString(), dbReader[9].ToString()));

                }
            }
            Grid_HS.ItemsSource = _cod_Vamal_list;
            dbConn.Close();
        }

        private void IncarcaTabela_HS1(string tableName)
        {
            m1.Visibility = Visibility.Visible;
            m2.Visibility = Visibility.Visible;
            m3.Visibility = Visibility.Visible;
            m4.Visibility = Visibility.Visible;
            InfoCautareLabel.Visibility = Visibility.Visible;
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "CN\\" + "CN_" + System.DateTime.Today.Year + ".mdb";
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
                    content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));

                }
            }
            Grid_HS.ItemsSource = content_Tari;
            dbConn.Close();
        }

        private void IncarcaTabela_Tari_UE(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbConnection dbConn1 = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbCommand dbCommand1 = null;
            OleDbDataReader dbReader = null;
            OleDbDataReader dbReader1 = null;
            string dbQuery = string.Empty;
            string dbQuery1 = string.Empty;
            dbConn.Open();
           // dbConn1.Open();
            dbQuery = "SELECT COD_TARA, DATA_ADERARII FROM " + tableName;
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                

                while (dbReader.Read())
                {
                    dbConn1.Open();
                    dbQuery1 = "SELECT [Tara_DESC] FROM " + "[Tari]" + " WHERE [Tara_COD]='" + dbReader["COD_TARA"].ToString()+"'";
                    dbCommand1 = new OleDbCommand(dbQuery1, dbConn1);
                    dbReader1 = dbCommand1.ExecuteReader();
                    dbReader1.Read();
                    content_Tari_UE.Add(new TARI_UE(dbReader["COD_TARA"].ToString(), dbReader1["Tara_DESC"].ToString(), dbReader["DATA_ADERARII"].ToString()));

                    dbReader1.Close();
                    dbConn1.Close();
                }
            }
            Grid_HS.ItemsSource = content_Tari_UE;
            dbConn.Close();
        }

        private void IncarcaTabela_Tari(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
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
                    content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));

                }
            }
            Grid_HS.ItemsSource = content_Tari;
            dbConn.Close();
        }

        private void IncarcaTabela_Tranzactii(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
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
                    content_Tari.Add(new Tari(dbReader[0].ToString() + "." + dbReader[0].ToString(), dbReader[2].ToString()));

                }
            }
            Grid_HS.ItemsSource = content_Tari;
            dbConn.Close();
        }
        private void IncarcaTabela_UM(string tableName)
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
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
                    content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[2].ToString()));

                }
            }
            Grid_HS.ItemsSource = content_Tari;
            dbConn.Close();
        }

        public static int coloane = 2;

        private void Tipareste_Btn_Click(object sender, RoutedEventArgs e)
        {

            PrintDG print = new PrintDG();
            if (opentab == "HS_8")
                coloane = 4;
            print.printDG(Grid_HS, opentab);

        }


        private void Export_Excel_Btn_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < Grid_HS.Columns.Count; j++)
            {
                Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = Grid_HS.Columns[j].Header;
            }
            for (int i = 0; i < Grid_HS.Columns.Count; i++)
            {
                for (int j = 0; j < Grid_HS.Items.Count; j++)
                {
                     try
                    {
                        TextBlock b = Grid_HS.Columns[i].GetCellContent(Grid_HS.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];

                        myRange.Value2 = b.Text;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void ToateInreg_Btn_Click(object sender, RoutedEventArgs e)
        {
            Grid_HS.ItemsSource = null;

            if (opentab == "UM")
                Grid_HS.ItemsSource = content_Tari;
            if (opentab == "HS_1" || opentab == "HS_2" || opentab == "HS_4" || opentab == "HS_6")
                Grid_HS.ItemsSource = content_Tari;
            if (opentab == "HS_8")
                Grid_HS.ItemsSource = _cod_Vamal_list;

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
                        for (int j = 0; j < coloane; j++)
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

        public static string s_codVamal="";
        public static string s_moneda = "";
        

        internal class TARI_UE
        {
            string cod, denumire, data;

            public TARI_UE(string v1,string v2, string v3)
            {
                this.Cod = v1;
                this.Denumire = v2;
                this.Data = v3.Substring(0, 10);
            }

            public string Cod { get => cod; set => cod = value; }
            public string Denumire { get => denumire; set => denumire = value; }
            public string Data { get => data; set => data = value; }

        }

        class Tari
        {
            string cod, denumire;

            public Tari(string v1, string v2)
            {
                this.Cod = v1;
                this.Descriere = v2;
            }

            public string Cod { get => cod; set => cod = value; }
            public string Descriere { get => denumire; set => denumire = value; }
        }

        class Cod_Vamal
        {
            string cod_8, cod_12, denumire, um;

            public Cod_Vamal(string v1, string v2, string v3, string v4)
            {
                this.Cod_8 = v1;
                this.Cod_12 = v2;
                this.Descriere = v3;
                this.UM_SUPL = v4;
            }

            public string Cod_8 { get => cod_8; set => cod_8 = value; }
            public string Cod_12 { get => cod_12; set => cod_12 = value; }

            public string Descriere { get => denumire; set => denumire = value; }

            public string UM_SUPL { get => um; set => um = value; }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            s_go = true;
        }
        string keypressed = "";
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.Key==Key.Back || e.Key==Key.Delete)
            {
                if(keypressed.Length>0)
                    keypressed = keypressed.Remove(keypressed.Length-1);
            }
            else
            {   if(e.Key!=Key.Enter && e.Key!=Key.Down && e.Key != Key.Up && e.Key != Key.Left && e.Key != Key.Right && e.Key != Key.Space)
                    keypressed += e.Key.ToString();
                else
                    if(e.Key == Key.Space)
                    {
                        keypressed += " ";
                    }
            }
            InfoCautareLabel.Content = "Cautare dupa: " + keypressed;
            InfoCautareLabel.Visibility = Visibility.Visible;

            Searchfor(keypressed);
        }

        private void Searchfor(string keypressed)
        {
            if (opentab == "Tari")
                FindIN(content_Tari, keypressed);
            if (opentab == "TARI_UE")
                FindIN(content_Tari_UE, keypressed);
            if (opentab == "Monezi")
                FindIN(content_Tari, keypressed);
            if (opentab == "Incoterms")
                FindIN(content_Tari, keypressed);
            if (opentab == "Tranzactii")
                FindIN(content_Tari, keypressed);
            if (opentab == "UM")
                FindIN(content_Tari, keypressed);

            if (opentab == "HS_1" || opentab == "HS_2" || opentab == "HS_4" || opentab == "HS_6")
                FindIN(content_Tari, keypressed);

            if (opentab == "HS_8")
                FindIN(_cod_Vamal_list, keypressed);
        }

        private void FindIN(List<Cod_Vamal> cod_Vamal_list, string keypressed)
        {
            int i = 0;
            foreach (Cod_Vamal element in cod_Vamal_list)
            {
                try
                {
                    
                        if (keypressed.Length <= element.Descriere.Length)
                        {
                            if (element.Descriere.Substring(0, keypressed.Length).ToUpper().Equals(keypressed))
                            {
                                Grid_HS.SelectedIndex = i;
                                Grid_HS.SelectedItem = element.Descriere;
                                Grid_HS.UpdateLayout();
                                Grid_HS.ScrollIntoView(Grid_HS.SelectedItem);
                                break;
                            }
                        }
                }
                catch (Exception)
                {
                    break;
                }
                i++;
            }
        }

        private void FindIN(List<TARI_UE> content_Tari_UE, string keypressed)
        {
            int i = 0;
            foreach (TARI_UE element in content_Tari_UE)
            {

                try
                {
                    if (element.Cod.Substring(0, keypressed.Length).ToUpper().Equals(keypressed))
                    {
                        Grid_HS.SelectedIndex = i;
                        Grid_HS.SelectedItem = element.Cod;
                        Grid_HS.UpdateLayout();
                        Grid_HS.ScrollIntoView(Grid_HS.SelectedItem);
                        break;
                    }
                }
                catch (Exception)
                {
                    break;
                }
                i++;
            }
        }

        private void FindIN(List<Tari> content_Tari, string keypressed)
        {
            int i = 0;
            foreach(Tari element in content_Tari)
            {
                
                try
                {   if (opentab == "HS_1" || opentab == "HS_2" || opentab == "HS_4" || opentab == "HS_6" || opentab == "HS_8" || opentab == "Tranzactii")
                    {
                        if (keypressed.Length <= element.Descriere.Length)
                        {
                            if (element.Descriere.Substring(0, keypressed.Length).ToUpper().Equals(keypressed))
                            {
                                Grid_HS.SelectedIndex = i;
                                Grid_HS.SelectedItem = element.Descriere;
                                Grid_HS.UpdateLayout();
                                Grid_HS.ScrollIntoView(Grid_HS.SelectedItem);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (keypressed.Length <= element.Cod.Length)
                            if (element.Cod.Substring(0, keypressed.Length).ToUpper().Equals(keypressed))
                            {
                                Grid_HS.SelectedIndex = i;
                                Grid_HS.SelectedItem = element.Cod;
                                Grid_HS.UpdateLayout();
                                Grid_HS.ScrollIntoView(Grid_HS.SelectedItem);
                                break;
                            }
                    }
                }
                catch(Exception)
                {
                    break;
                }
                i++;
            }
              
        }
        private void HS_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (opentab == "HS_8" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Cod_Vamal declaratieSelectata = Grid_HS.SelectedItem as Cod_Vamal;
                s_codVamal = declaratieSelectata.Cod_8;
            }

            if (opentab == "Monezi" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                s_moneda = declaratieSelectata.Cod;
            }
            if (opentab == "UM" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                s_moneda = declaratieSelectata.Cod;
            }
            if (opentab == "Tranzactii" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                s_moneda = declaratieSelectata.Cod;
            }
            if (opentab == "Incoterms" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                s_moneda = declaratieSelectata.Cod;
            }
            
            if (opentab == "ModTransp" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                s_moneda = declaratieSelectata.Cod;
            }
            if (opentab == "TARI_UE" && this.Title == "Selectie / Cautare")
            {
                int linieSelectataGrid = Grid_HS.SelectedIndex;
                if (linieSelectataGrid == -1)
                    Grid_HS.SelectedIndex = 0;
                TARI_UE declaratieSelectata = Grid_HS.SelectedItem as TARI_UE;
                s_moneda = declaratieSelectata.Cod;
            }
            s_go = true;
            this.Close();
        }
        private void Grid_HS_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (opentab == "HS_8" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Cod_Vamal declaratieSelectata = Grid_HS.SelectedItem as Cod_Vamal;
                    s_codVamal = declaratieSelectata.Cod_8;
                }

                if (opentab == "Monezi" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                    s_moneda = declaratieSelectata.Cod;
                }
                if (opentab == "Tranzactii" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                    s_moneda = declaratieSelectata.Cod;
                }
                
                if (opentab == "UM" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                    s_moneda = declaratieSelectata.Cod;
                }
                if (opentab == "ModTransp" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                    s_moneda = declaratieSelectata.Cod;
                }
                if (opentab == "Incoterms" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    Tari declaratieSelectata = Grid_HS.SelectedItem as Tari;
                    s_moneda = declaratieSelectata.Cod;
                }
                if (opentab == "TARI_UE" && this.Title == "Selectie / Cautare")
                {
                    int linieSelectataGrid = Grid_HS.SelectedIndex;
                    if (linieSelectataGrid == -1)
                        Grid_HS.SelectedIndex = 0;
                    TARI_UE declaratieSelectata = Grid_HS.SelectedItem as TARI_UE;
                    s_moneda = declaratieSelectata.Cod;
                }
                s_go = true;
                this.Close();
            }
            
        }
    }
}
