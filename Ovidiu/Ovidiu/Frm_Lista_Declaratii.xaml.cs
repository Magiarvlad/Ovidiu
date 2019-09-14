using Ovidiu.EU;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
                    //dbReader.Read();
                    if(dbReader[0].ToString()!= string.Empty)
                    lista.Add(new Declaratii( dbReader[0].ToString(),dbReader[1].ToString(),dbReader[2].ToString(),dbReader[3].ToString(),
                                              string.Format(spec, Convert.ToDouble(dbReader[4])), string.Format(spec,Convert.ToDouble(dbReader[5])), string.Format(spec, Convert.ToDouble(dbReader[6])), dbReader[7].ToString()));
                    //lista.Add(new Declaratii(dbReader[0].ToString()));

                   
                  

                }
            }
            gridIntrastat.ItemsSource = lista;
            

            //gridInsta.ItemsSource = lista;
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
            string tip = declaratieSelectata.Tip_Declaratie1;
            string luna = declaratieSelectata.Luna1;
            string an = declaratieSelectata.Anul1;

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
            string tip = declaratieSelectata.Tip_Declaratie1;
            string luna = declaratieSelectata.Luna1;
            string an = declaratieSelectata.Anul1;

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
            string tip = declaratieSelectata.Tip_Declaratie1;
            string luna = declaratieSelectata.Luna1;
            string an = declaratieSelectata.Anul1;

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
            string tip = declaratieSelectata.Tip_Declaratie1;
            string luna = declaratieSelectata.Luna1;
            string an = declaratieSelectata.Anul1;
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
                    TextBlock b = gridIntrastat.Columns[i].GetCellContent(gridIntrastat.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    try
                    {
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
                    if (arrKeyTxt[0] == keys[1] && lista[dataIndexNo].Anul1 == keys[2])
                    {
                        flag = true;
                    }
                }
            }

            stream.Close();

            if(flag==true)
            { 
              Frm_Intrastat frmIntrastat = new Frm_Intrastat(lista[dataIndexNo].Sens1, lista[dataIndexNo].Luna1, lista[dataIndexNo].Anul1);
                  frmIntrastat.Show();
            }
            else
            {
                Frm_Mesaj_Demo frmIntrastat = new Frm_Mesaj_Demo("Inregistrare");
                frmIntrastat.Show();
            }
                

          
        }
    }

    class Declaratii
    {
        string Sens, Tip_Declaratie,Anul, Luna, Valoare_Valuta, Valoare_Ron, Greutate_Neta_KG, Pozitii;

        public Declaratii(string sens, string tip_Declaratie, string anul, string luna, string valoare_Valuta, string valoare_Ron, string greutate_Neta_KG, string pozitii)
        {
            Sens1 = sens;
            Tip_Declaratie1 = tip_Declaratie;
            Anul1 = anul;
            Luna1 = luna;
            Valoare_Valuta1 = valoare_Valuta;
            Valoare_Ron1 = valoare_Ron;
            Greutate_Neta_KG1 = greutate_Neta_KG;
            Pozitii1 = pozitii;
        }
        public Declaratii(string sens)
        {
            Sens1 = sens;
            
        }


        public string Sens1 { get => Sens; set => Sens = value; }
        public string Tip_Declaratie1 { get => Tip_Declaratie; set => Tip_Declaratie = value; }
        public string Anul1 { get => Anul; set => Anul = value; }
        public string Luna1 { get => Luna; set => Luna = value; }
        public string Valoare_Valuta1 { get => Valoare_Valuta; set => Valoare_Valuta = value; }
        public string Valoare_Ron1 { get => Valoare_Ron; set => Valoare_Ron = value; }
        public string Greutate_Neta_KG1 { get => Greutate_Neta_KG; set => Greutate_Neta_KG = value; }
        public string Pozitii1 { get => Pozitii; set => Pozitii = value; }
    }
}
