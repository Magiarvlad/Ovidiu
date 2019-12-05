using Ovidiu.EU;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Lista_Declaratii.xaml
    /// </summary>
    /// 
   
    public partial class Frm_Lista_Declaratii : Window
    {
        List<Declaratii> lista = new List<Declaratii>();
        int linieSelectataGrid;
        public Frm_Lista_Declaratii()
        {
            InitializeComponent();
            IncarcaGrid("Lista_Intrastat");           
        }

        private void IncarcaGrid(string tableName)
        {
            int decimals = Convert.ToInt32(XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Zecimale/ZecRotCalcule")) ;
            string spec = "{0:N" + decimals + "}";

            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal+ ".mdb";
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
                    if (dbReader[0].ToString()!= string.Empty)
                    {
                        string imagePath = "";

                        if (dbReader[0].ToString() == "I")
                            imagePath = FileLocation.System + "Imagini\\Arrow\\IN.gif";
                        else
                            imagePath = FileLocation.System + "Imagini\\Arrow\\OUT.gif";

                        lista.Add(new Declaratii(toBitmap(File.ReadAllBytes(imagePath)), dbReader[1].ToString(), dbReader[2].ToString(), dbReader[3].ToString(),
                                            string.Format(spec, Convert.ToDouble(dbReader[4])), string.Format(spec, Convert.ToDouble(dbReader[5])), string.Format(spec, Convert.ToDouble(dbReader[6])), dbReader[7].ToString()));

                    }
                }
            }

            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Image));
            Binding bind = new Binding("Sens1");//please keep "image" name as you have set in your class data member name
            factory.SetValue(Image.SourceProperty, bind);
            DataTemplate cellTemplate = new DataTemplate() { VisualTree = factory };
            DataGridTemplateColumn imgCol = new DataGridTemplateColumn()
            {
                Header = "Sens", //this is upto you whatever you want to keep, this will be shown on column to represent the data for helping the user...
                CellTemplate = cellTemplate,
                Width = 42
            };
            gridIntrastat.Columns.Add(imgCol);
            
            gridIntrastat.ItemsSource = lista;
            dbConn.Close();
        }

        private void DeclaratieNula_Click(object sender, RoutedEventArgs e)
        {
           // IncarcaGrid("Lista_Intrastat");
        }

        private void btnGenereazaFisierIntrastat_Click(object sender, RoutedEventArgs e)
        {
            linieSelectataGrid = gridIntrastat.SelectedIndex;
            if (linieSelectataGrid == -1)
                gridIntrastat.SelectedIndex = 0;
            Declaratii declaratieSelectata = gridIntrastat.SelectedItem as Declaratii;
            // DataRowView dataRow = (DataRowView)gridIntrastat.SelectedItem;
            // int index = gridIntrastat.CurrentCell.Column.DisplayIndex;
            string tip = declaratieSelectata.Tip_Declaratie;
            string luna = declaratieSelectata.Luna;
            string an = declaratieSelectata.Anul;

            Frm_Intrastat frmIntrastat = new Frm_Intrastat(tip,luna,an);
            frmIntrastat.Show();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            Frm_Intrastat_Add add = new Frm_Intrastat_Add();
            add.Show();
        }

        private void btnVizualizare_Click(object sender, RoutedEventArgs e)
        {
            linieSelectataGrid = gridIntrastat.SelectedIndex;
            if (linieSelectataGrid == -1)
                gridIntrastat.SelectedIndex = 0;
            Declaratii declaratieSelectata = gridIntrastat.SelectedItem as Declaratii;
            // DataRowView dataRow = (DataRowView)gridIntrastat.SelectedItem;
            // int index = gridIntrastat.CurrentCell.Column.DisplayIndex;
            string tip = declaratieSelectata.Tip_Declaratie;
            string luna = declaratieSelectata.Luna;
            string an = declaratieSelectata.Anul;

            StreamReader stream = new StreamReader(FileLocation.System + "key\\chei.txt");
            string line = "";
            bool flag = false;
            while (true)
            {
                line = stream.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] keys = line.Split('\t');
                string[] arrKeyTxt = new string[4];

                if (keys[0].Length > 17)
                {
                    arrKeyTxt = Inregistrare.DecodeKey(keys[0]);
                    if (arrKeyTxt[0] == keys[1] && an == keys[2])
                    {
                        flag = true;
                    }
                }
            }

            stream.Close();

            if (flag == true)
            {
                Frm_Intrastat frmIntrastat = new Frm_Intrastat(tip, luna, an);
                frmIntrastat.Show();

            }
            else
            {
                Frm_Mesaj_Demo frmIntrastat = new Frm_Mesaj_Demo("Inregistrare");
                frmIntrastat.Show();
            }
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            linieSelectataGrid = gridIntrastat.SelectedIndex;
            if (linieSelectataGrid == -1)
                gridIntrastat.SelectedIndex = 0;
            Declaratii declaratieSelectata = gridIntrastat.SelectedItem as Declaratii;
            // DataRowView dataRow = (DataRowView)gridIntrastat.SelectedItem;
            // int index = gridIntrastat.CurrentCell.Column.DisplayIndex;
            string tip = declaratieSelectata.Tip_Declaratie;
            string luna = declaratieSelectata.Luna;
            string an = declaratieSelectata.Anul;

            StreamReader stream = new StreamReader(FileLocation.System + "key\\chei.txt");
            string line = "";
            bool flag = false;
            while (true)
            {
                line = stream.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] keys = line.Split('\t');
                string[] arrKeyTxt = new string[4];

                if (keys[0].Length > 17)
                {
                    arrKeyTxt = Inregistrare.DecodeKey(keys[0]);
                    if (arrKeyTxt[0] == keys[1] && an == keys[2])
                    {
                        flag = true;
                    }
                }
            }

            stream.Close();

            if (flag == true)
            {
                Frm_Intrastat frmIntrastat = new Frm_Intrastat(tip, luna, an);
                frmIntrastat.Show();

            }
            else
            {
                Frm_Mesaj_Demo frmIntrastat = new Frm_Mesaj_Demo("Inregistrare");
                frmIntrastat.Show();
            }            
        }


        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            StergeInregistrare("Lista_Intrastat");
        }

        private void StergeInregistrare(string tableName)
        {
            Declaratii declaratieSelectata = gridIntrastat.SelectedItem as Declaratii;
            // DataRowView dataRow = (DataRowView)gridIntrastat.SelectedItem;
            // int index = gridIntrastat.CurrentCell.Column.DisplayIndex;
            string tip = declaratieSelectata.Tip_Declaratie;
            string luna = declaratieSelectata.Luna;
            string an = declaratieSelectata.Anul;
            string tipmesaj;
            if (tip == "I")
            {
                tipmesaj = "ACHIZITIE";

            }
            else
            {
                tipmesaj = "LIVRARE";
            }
            if (MessageBox.Show("STERGETI ACEASTA DECLARATIE?"+"\n"+ tipmesaj + "  -  "+ an + "  -  " + luna, "MESAJ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
                OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
                OleDbCommand dbCommand = null;
                string dbQuery = string.Empty;
                dbConn.Open();
                dbQuery = "Delete FROM Intrastat"+ " WHERE Anul=" + an + " AND Luna=" + luna + " AND TIP='" + tip + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbCommand.ExecuteNonQuery();

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ERROARE:  COULD NOT DELETE FROM SPECIFIED TABLES 
                dbQuery = "Delete FROM " + tableName + " WHERE Anul=" + an + " AND Luna=" + luna + " AND TIP='" + tip + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbCommand.ExecuteNonQuery();

                dbConn.Close();
            }
            else
            {
                //do yes stuff
            }           
        }

        private void btnSterge_Copy_Click(object sender, RoutedEventArgs e)
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
                    
                    try
                    {
                        TextBlock b = gridIntrastat.Columns[i].GetCellContent(gridIntrastat.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridIntrastat.SelectedIndex < 0)
            {
                MessageBox.Show("Nici o linie nu este selectata!");
            }
            else
            {
                int dataIndexNo = gridIntrastat.SelectedIndex;

                StreamReader stream = new StreamReader(FileLocation.System + "key\\chei.txt");
                string line = "";
                bool flag = false;
                while (true)
                {
                    line = stream.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    string[] keys = line.Split('\t');
                    string[] arrKeyTxt = new string[4];

                    if (keys[0].Length > 17)
                    {
                        arrKeyTxt = Inregistrare.DecodeKey(keys[0]);
                        if (arrKeyTxt[0] == keys[1] && lista[dataIndexNo].Anul == keys[2])
                        {
                            flag = true;
                        }
                    }
                }

                stream.Close();

                if (flag == true)
                {
                    Frm_Intrastat frmIntrastat = new Frm_Intrastat(lista[dataIndexNo].Tip_Declaratie, lista[dataIndexNo].Luna, lista[dataIndexNo].Anul);
                    frmIntrastat.Show();
                }
                else
                {
                    Frm_Mesaj_Demo frmIntrastat = new Frm_Mesaj_Demo("Inregistrare");
                    frmIntrastat.Show();
                }
            }           
        }
        public static BitmapImage toBitmap(Byte[] value)
        {
            if (value != null && value is byte[])
            {
                byte[] ByteArray = value as byte[];
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(ByteArray);
                bmp.EndInit();
                return bmp;
            }
            return null;
        }

        private void GridIntrastat_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Sens1")
            {
                e.Column = null;
            }
        }
    }


    class Declaratii
    {
        BitmapImage Sens = new BitmapImage();
        string tip_Declaratie,anul, luna, valoare_Valuta, valoare_Ron, greutate_Neta_KG, pozitii;
        

        public Declaratii(BitmapImage sens, string tip_Declaratie, string anul, string luna, string valoare_Valuta, string valoare_Ron, string greutate_Neta_KG, string pozitii)
        {           
            Sens1 = sens;
            Tip_Declaratie = tip_Declaratie;
            Anul = anul;
            Luna = luna;
            Valoare_Valuta = valoare_Valuta;
            Valoare_Ron = valoare_Ron;
            Greutate_Neta_KG = greutate_Neta_KG;
            Pozitii = pozitii;
        }

        //public string Sens1 { get => Sens3; set => Sens3 = value; }
        public BitmapImage Sens1 { get => Sens; set => Sens = value; }
        public string Tip_Declaratie { get => tip_Declaratie; set => tip_Declaratie = value; }
        public string Anul { get => anul; set => anul = value; }
        public string Luna { get => luna; set => luna = value; }
        public string Valoare_Valuta { get => valoare_Valuta; set => valoare_Valuta = value; }
        public string Valoare_Ron { get => valoare_Ron; set => valoare_Ron = value; }
        public string Greutate_Neta_KG { get => greutate_Neta_KG; set => greutate_Neta_KG = value; }
        public string Pozitii { get => pozitii; set => pozitii = value; }
    }
}
