using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.IO;
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
using System.Xml;
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
        List<String> listaDescrieriNC = new List<String>();
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
            AddLineToGrid();

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
                    listaDescrieriNC.Add(dbReader[8].ToString());
                }
            }
            dbConn.Close();
            //AddNewLine();

            if (lista.Count == 0)
                AddLineToGrid();


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

                    AddLineToGrid();                   
                }
               
            }
        }

        private void AddLineToGrid()
        {
            string todaydate = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();

            if (txtTip.Text == "I")
            {
                Intrastat a = new Intrastat(todaydate, "", "", "", "", "", "", "", "", "", "", Val_Implicite.I_Tara_Exp, "RO", "", "", "", Val_Implicite.I_Nat_Transp, Val_Implicite.I_Incoterms, Val_Implicite.I_Mod_Transp, "", todaydate, "", "");
                lista.Add(a);
                listaDescrieriNC.Add(string.Empty);
            }
            else
            {
                Intrastat a = new Intrastat(todaydate, "", "", "", "", "", "", "", "", "", "", "RO", Val_Implicite.O_Tara_Dest, "", "", "", Val_Implicite.O_Nat_Tranz, Val_Implicite.O_Incoterms, Val_Implicite.O_Mod_Transp, "", todaydate, "", "");
                lista.Add(a);
                listaDescrieriNC.Add(string.Empty);
            }
        }

        private void SaveLine()
        {
            int numarInregistrari = ReturneazaNumarInregistrari();
            
            if(numarInregistrari<lista.Count-1)
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
                    dbCommand.Parameters.AddWithValue("@Anul", Convert.ToInt32(txtAn.Text));
                    dbCommand.Parameters.AddWithValue("@Luna", Convert.ToInt32(txtLuna.Text));
                    dbCommand.Parameters.AddWithValue("@DataReceptiei", Convert.ToDateTime(lista[lista.Count - 2].DataReceptiei.ToString()));
                    dbCommand.Parameters.AddWithValue("@Descriere", lista[lista.Count - 2].Descriere);
                    dbCommand.Parameters.AddWithValue("@Cod_NC", listaDescrieriNC[lista.Count - 2]);
                    dbCommand.Parameters.AddWithValue("@Descriere_NC", lista[lista.Count - 2].Descriere);
                    dbCommand.Parameters.AddWithValue("@Cantitate", Convert.ToDecimal(lista[lista.Count - 2].Cantitate));
                    dbCommand.Parameters.AddWithValue("@UM", lista[lista.Count - 2].UM);
                    dbCommand.Parameters.AddWithValue("@Valoare_Valuta", Convert.ToDouble(lista[lista.Count - 2].ValoareValuta));
                    dbCommand.Parameters.AddWithValue("@Moneda", lista[lista.Count - 2].Moneda);
                    dbCommand.Parameters.AddWithValue("@Curs_Schimb", Convert.ToDecimal(lista[lista.Count - 2].CursSchimb));
                    dbCommand.Parameters.AddWithValue("@Val_Fiscala", Convert.ToDecimal(lista[lista.Count - 2].ValoareFiscala));
                    dbCommand.Parameters.AddWithValue("@Val_Stat", Convert.ToDecimal(lista[lista.Count - 2].ValoareStatistica));
                    dbCommand.Parameters.AddWithValue("@Tara_Orig", lista[lista.Count - 2].TaraOrigine);
                    dbCommand.Parameters.AddWithValue("@Tara_Exp", lista[lista.Count - 2].TaraExport);
                    dbCommand.Parameters.AddWithValue("@Tara_Dest", lista[lista.Count - 2].TaraDestinatie);
                    dbCommand.Parameters.AddWithValue("@Net", Convert.ToDecimal(lista[lista.Count - 2].Net));
                    if (lista[lista.Count - 2].UmSupl == "")
                        dbCommand.Parameters.AddWithValue("@Exista_UMS", false);
                    else
                        dbCommand.Parameters.AddWithValue("@Exista_UMS", true);
                    dbCommand.Parameters.AddWithValue("@Cod_UMS", lista[lista.Count - 2].UmSupl);

                    if(lista[lista.Count - 2].CantitateSupl!="")
                        dbCommand.Parameters.AddWithValue("@Val_UMS", Convert.ToDecimal(lista[lista.Count - 2].CantitateSupl));
                    else
                        dbCommand.Parameters.AddWithValue("@Val_UMS", 0);

                    dbCommand.Parameters.AddWithValue("@Nat_Tranz", lista[lista.Count - 2].NatTranz);
                    dbCommand.Parameters.AddWithValue("@Incoterms", lista[lista.Count - 2].CondLivrare);
                    dbCommand.Parameters.AddWithValue("@Mod_transp", lista[lista.Count - 2].ModTransp);
                    dbCommand.Parameters.AddWithValue("@Factura_Numar", lista[lista.Count - 2].FacturaNumar);
                    dbCommand.Parameters.AddWithValue("@Factura_Data", lista[lista.Count - 2].DocumentData);
                    dbCommand.Parameters.AddWithValue("@VAT_ID", lista[lista.Count - 2].DestTVA);
                    dbCommand.Parameters.AddWithValue("@PU", 0);
                    dbCommand.Parameters.AddWithValue("@Net_Unitar", 0);
                    dbCommand.Parameters.AddWithValue("@Raport_Stat", 1);
                    dbCommand.Parameters.AddWithValue("@SourceFile", "");
                    dbCommand.Parameters.AddWithValue("@SourceFile_FullPath", "");
                    dbCommand.Parameters.AddWithValue("@Data_Preluare", Convert.ToDateTime(lista[lista.Count - 2].DocumentData.ToString()));
                    dbCommand.Parameters.AddWithValue("@UserName", txtPrenume.Text);
                    dbCommand.Parameters.AddWithValue("@Probleme", false);
                    dbCommand.Parameters.AddWithValue("@Transp_Document", "");
                    dbCommand.Parameters.AddWithValue("@Transport_Suma", 0);
                    dbCommand.Parameters.AddWithValue("@Transport_Moneda", "");
                    dbCommand.Parameters.AddWithValue("@Transport_Curs", 0);
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

        public class Intrastat : DataGrid, INotifyPropertyChanged
        {
            string dataReceptiei, descriere, codVamal, cantitate, uM, valoareValuta, moneda, cursSchimb, valoareFiscala, valoareStatistica, taraOrigine, taraExport, taraDestinatie, net, umSupl, cantitateSupl, natTranz, condLivrare, modTransp, facturaNumar, documentData, destTVA, pozitia;
            public Intrastat()
            {
                // data = Data;

            }
            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }

            public bool BoundCellLevel
            {
                get { return (bool)GetValue(BoundCellLevelProperty); }
                set { SetValue(BoundCellLevelProperty, value); }
            }

            public static readonly DependencyProperty BoundCellLevelProperty =
                DependencyProperty.Register("BoundCellLevel", typeof(bool), typeof(Intrastat), new UIPropertyMetadata(false));

            protected override Size MeasureOverride(Size availableSize)
            {
                var desiredSize = base.MeasureOverride(availableSize);
                if (BoundCellLevel)
                    ClearBindingGroup();
                return desiredSize;
            }

            private void ClearBindingGroup()
            {
                // Clear ItemBindingGroup so it isn't applied to new rows
                ItemBindingGroup = null;
                // Clear BindingGroup on already created rows
                foreach (var item in Items)
                {
                    var row = ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                    row.BindingGroup = null;
                }
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

            public string DataReceptiei { get => dataReceptiei; set{ dataReceptiei = value; this.NotifyPropertyChanged("DataReceptiei"); } }
            public string Descriere { get => descriere; set { descriere = value; this.NotifyPropertyChanged("Descriere"); } }
            public string CodVamal { get => codVamal; set { codVamal = value; this.NotifyPropertyChanged("CodVamal"); } }
            public string Cantitate { get => cantitate; set { cantitate = value; this.NotifyPropertyChanged("Cantitate"); }  }
            public string UM { get => uM; set { uM = value; this.NotifyPropertyChanged("UM"); } }
            public string ValoareValuta { get => valoareValuta; set { valoareValuta = value; this.NotifyPropertyChanged("ValoareValuta"); } }
            public string Moneda { get => moneda; set { moneda = value; this.NotifyPropertyChanged("Moneda"); } }
            public string CursSchimb { get => cursSchimb; set { cursSchimb = value; this.NotifyPropertyChanged("CursSchimb"); } }
            public string ValoareFiscala { get => valoareFiscala; set { valoareFiscala = value;  this.NotifyPropertyChanged("ValoareFiscala"); }    }
            public string ValoareStatistica { get => valoareStatistica; set { valoareStatistica = value;  this.NotifyPropertyChanged("ValoareStatistica"); }}
            public string TaraOrigine { get => taraOrigine; set { taraOrigine = value;  this.NotifyPropertyChanged("TaraOrigine"); }}
            public string TaraExport { get => taraExport; set { taraExport = value;  this.NotifyPropertyChanged("TaraExport"); }}
            public string TaraDestinatie { get => taraDestinatie; set { taraDestinatie = value; this.NotifyPropertyChanged("TaraDestinatie"); } }
            public string Net { get => net; set { net = value; this.NotifyPropertyChanged("Net"); } }
            public string UmSupl { get => umSupl; set { umSupl = value; this.NotifyPropertyChanged("UmSupl"); } }
            public string CantitateSupl { get => cantitateSupl; set { cantitateSupl = value; this.NotifyPropertyChanged("CantitateSupl"); } }
            public string NatTranz { get => natTranz; set { natTranz = value; this.NotifyPropertyChanged("NatTranz"); } }
            public string CondLivrare { get => condLivrare; set { condLivrare = value; this.NotifyPropertyChanged("CondLivrare"); } }
            public string ModTransp { get => modTransp; set { modTransp = value; this.NotifyPropertyChanged("ModTransp"); } }
            public string FacturaNumar { get => facturaNumar; set { facturaNumar = value; this.NotifyPropertyChanged("FacturaNumar"); } }
            public string DocumentData { get => documentData; set { documentData = value; this.NotifyPropertyChanged("DocumentData"); } }
            public string DestTVA { get => destTVA; set { destTVA = value; this.NotifyPropertyChanged("DestTVA"); } }
            public string Pozitia { get => pozitia; set { pozitia = value; this.NotifyPropertyChanged("Pozitia"); } }
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
            listaDescrieriNC[gridIntrastat.SelectedIndex] = Frm_HS.s_Descriere;
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
            listaDescrieriNC[gridIntrastat.SelectedIndex] = Frm_HS.s_Descriere;
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
                //AddNewLine();
                string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + Firma.CodFiscal + ".mdb";
                OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
                OleDbCommand dbCommand = null;
                OleDbDataReader dbReader = null;
                string dbQuery = string.Empty;
                dbConn.Open();
                dbQuery = "SELECT Cod_NC,Descriere,Descriere_NC FROM Intrastat Where Descriere='" + cb.Text.ToString() + "'";
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
                        listaDescrieriNC[gridIntrastat.SelectedIndex] = dbReader[2].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //listaDescrieri.Add(dbReader[0].ToString());

                }
                dbConn.Close();
            }
        }


        private void GridIntrastat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridIntrastat.SelectedIndex == lista.Count - 1)
                AddNewLine();
            else
            try
            {
                lblMesaj.Text = listaDescrieriNC[gridIntrastat.SelectedIndex].ToString();
            }
            catch (Exception)
            {

            }
        }

        private void GridIntrastat_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void BtnGenereazaFisierIntrastat_Click(object sender, RoutedEventArgs e)
        {
            bool canBeSaved = false;    
            for( int i = 0; i <lista.Count -1; i++)
            {
                if (lista[i].Cantitate != "")
                {
                    if (lista[i].DataReceptiei != "")
                    {
                        if (lista[i].CodVamal != "")
                        {
                            if (lista[i].ValoareValuta != "")
                            {
                                if (lista[i].TaraOrigine != "")
                                {
                                    if (lista[i].TaraExport != "")
                                    {
                                        if (lista[i].TaraDestinatie != "")
                                        {
                                            if (lista[i].NatTranz != "")
                                            {
                                                if (lista[i].CondLivrare != "")
                                                {
                                                    if (lista[i].ModTransp != "")
                                                    {
                                                        if (lista[i].Moneda != "")
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
            if (canBeSaved == true)
            {
                GenereazaDeclaratie();
            }
        }

        private void GenereazaDeclaratie()
        {
            XmlDocument doc = new XmlDocument();
            using (FileStream fs = new FileStream(FileLocation.DirectorSalvare+ "\\test.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                string datastring = "<?xml version="+"\""+"1.0" + "\"" + " encoding="+"\"" + "UTF-8" + "\"" +" ?>" + Environment.NewLine; 
                byte[] byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                if(txtTip.Text == "I")
                {
                    if(cmbTipDeclaratie.Text == "N-Noua")
                    {
                        datastring = "<InsNewArrival SchemaVersion=" + "\"" + "1.0" + "\"" + " xmlns=" + "\"" + "http://www.intrastat.ro/xml/InsSchema" + "\"" + ">" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                    }
                    else
                    {
                        datastring = "<InsRevisedArrival SchemaVersion=" + "\"" + "1.0" + "\"" + " xmlns=" + "\"" + "http://www.intrastat.ro/xml/InsSchema" + "\"" + ">" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                    }
                }
                else
                {
                    if (cmbTipDeclaratie.Text == "N-Noua")
                    {
                        datastring = "<InsNewDispatch SchemaVersion=" + "\"" + "1.0" + "\"" + " xmlns=" + "\"" + "http://www.intrastat.ro/xml/InsSchema" + "\"" + ">" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                    }
                    else
                    {
                        datastring = "<InsRevisedDispatch SchemaVersion=" + "\"" + "1.0" + "\"" + " xmlns=" + "\"" + "http://www.intrastat.ro/xml/InsSchema" + "\"" + ">" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                    }                    
                }

                datastring = "<InsCodeVersions>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                string anul = txtAn.Text;

                string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase +  "Comun.mdb";
                OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
                OleDbCommand dbCommand = null;
                OleDbDataReader dbReader = null;
                string dbQuery = string.Empty;
                dbConn.Open();
                dbQuery = "SELECT * FROM InsCodeVersions Where Anul=" + anul + " AND Luna<=" + txtLuna.Text + "";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    dbReader.Read();
                    try
                    {
                        datastring = "	<CountryVer>" + dbReader[5].ToString() + "</CountryVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<EuCountryVer>" + dbReader[6].ToString() + "</EuCountryVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<CnVer>" + dbReader[7].ToString() + "</CnVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<ModeOfTransportVer>" + dbReader[8].ToString() + "</ModeOfTransportVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<DeliveryTermsVer>" + dbReader[9].ToString() + "</DeliveryTermsVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<NatureOfTransactionAVer>" + dbReader[10].ToString() + "</NatureOfTransactionAVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<NatureOfTransactionBVer>" + dbReader[11].ToString() + "</NatureOfTransactionBVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<CountyVer>" + dbReader[12].ToString() + "</CountyVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<LocalityVer>" + dbReader[13].ToString() + "</LocalityVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                        datastring = "	<UnitVer>" + dbReader[14].ToString() + "</UnitVer>" + Environment.NewLine;
                        byteData = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(byteData, 0, byteData.Length);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    dbQuery = "SELECT * FROM InsCodeVersions Where Anul_End IS NULL";
                    dbCommand = new OleDbCommand(dbQuery, dbConn);
                    dbReader = dbCommand.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        dbReader.Read();
                        try
                        {
                            datastring = "	<CountryVer>" + dbReader[5].ToString() + "</CountryVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<EuCountryVer>" + dbReader[6].ToString() + "</EuCountryVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<CnVer>" + dbReader[7].ToString() + "</CnVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<ModeOfTransportVer>" + dbReader[8].ToString() + "</ModeOfTransportVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<DeliveryTermsVer>" + dbReader[9].ToString() + "</DeliveryTermsVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<NatureOfTransactionAVer>" + dbReader[10].ToString() + "</NatureOfTransactionAVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<NatureOfTransactionBVer>" + dbReader[11].ToString() + "</NatureOfTransactionBVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<CountyVer>" + dbReader[12].ToString() + "</CountyVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<LocalityVer>" + dbReader[13].ToString() + "</LocalityVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                            datastring = "	<UnitVer>" + dbReader[14].ToString() + "</UnitVer>" + Environment.NewLine;
                            byteData = new UTF8Encoding(true).GetBytes(datastring);
                            fs.Write(byteData, 0, byteData.Length);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                dbConn.Close();


                datastring = Environment.NewLine + "</InsCodeVersions>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "  <InsDeclarationHeader>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "      <VatNr>"+ Firma.CodFiscal.Replace("RO", "00") + "</VatNr>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "      <FirmName>" + txtVATID.Text + "</FirmName>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);


                string anLuna = txtAn.Text+ "-";
                string createDate = txtAn.Text + "-";
                if (txtLuna.Text.Length == 1)
                {
                    anLuna += "0";
                    createDate += "0";
                }
                anLuna += txtLuna.Text;
                createDate += txtLuna.Text + "-12T12:10:10.625+02:00";
                datastring = "      <RefPeriod>" + anLuna + "</RefPeriod>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "      <CreateDt>" + createDate + "</CreateDt>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "      <ContactPerson>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <LastName>" + txtPrenume.Text + "</LastName>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <FirstName>" + txtNume.Text + "</FirstName>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <Email>" + txtEmail.Text + "</Email>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <Phone>" + txtTelefon.Text + "</Phone>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <Fax>" + txtFax.Text + "</Fax>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "          <Position>" + txtPozComp.Text + "</Position>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                datastring = "      </ContactPerson>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                if(chkFolosireDeclTert.IsEnabled)
                {
                    datastring = "      <DTPDetails>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <VatNr>" + txtCIF.Text.Replace("RO", "00") + "</VatNr>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <FirmName>" + txtNumeSocietate.Text + "</FirmName>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <DTPAddress>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <Street>" + txtStrada.Text + "</Streete>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <StreetNumber>" + txtNr.Text + "</StreetNumber>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <Block>" + txtBloc.Text + "</Block>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <Stairs>" + txtScara.Text + "</Stairs>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <Apartment>" + txtApartament.Text + "</Apartment>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    //TODO
                    datastring = "      <LocalityCode> </LocalityCode>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    //TODO
                    datastring = "      <CountyCode> </CountyCode>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      <PostalCode>" + txtCodPostal.Text + "</PostalCode>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      </DTPAddress>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);

                    datastring = "      </DTPDetails>" + Environment.NewLine;
                    byteData = new UTF8Encoding(true).GetBytes(datastring);
                    fs.Write(byteData, 0, byteData.Length);
                }

                datastring = "  </InsDeclarationHeader>" + Environment.NewLine;
                byteData = new UTF8Encoding(true).GetBytes(datastring);
                fs.Write(byteData, 0, byteData.Length);

                if(chkGenXML.IsChecked == true)
                {

                }









                fs.Close();

                /*
                XmlNode node_p = doc.SelectSingleNode(Nodul + "/" + Elementul);

                if (node_p == null)
                {
                    //MessageBox.Show("Se poate crea", "Info", MessageBoxButton.OK);
                    node_p = doc.SelectSingleNode(Nodul);
                    CreateNode(node_p, Elementul, Valoare, NrTab);
                    doc.Save("C:\\ProgramData\\E_Intrastat\\Settings.xml");
                }*/
            }
        }

        private void CreateNode(XmlNode node_p, string elementul, string valoare)
        {
            XmlNode new_node = node_p.OwnerDocument.CreateElement(elementul);
            new_node.InnerText = valoare.ToString();
            node_p.AppendChild(new_node);
        }
    }
}
