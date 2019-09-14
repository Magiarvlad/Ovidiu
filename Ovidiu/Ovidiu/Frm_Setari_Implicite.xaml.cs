using Ovidiu.Clase;
using Ovidiu.EU;
using static Ovidiu.Modules.CONSTANTE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Setari_Implicite.xaml
    /// </summary>
    public partial class Frm_Setari_Implicite : Window
    {
        string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";

        #region Variables

        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiTaraExpediere = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiCondLivrare = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiNatTranzactiei = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiModTransport = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbLivrariTaraDestinatie = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbLivrariCondLivrare = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbLivrariNatTranzactiei = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbLivrariModTransport = new ObservableCollection<DateSetariImplicite>();


        public static int lastSelectedIndexAchizitiiTaraExpediere = 1;
        public static int lastSelectedIndexAchizitiiCondLivrare = 1;
        public static int lastSelectedIndexAchizitiiNatTranzactiei = 1;
        public static int lastSelectedIndexAchizitiiModTransport = 1;
        public static int lastSelectedIndexLivrariTaraExpediere = 1;
        public static int lastSelectedIndexLivrariCondLivrare = 1;
        public static int lastSelectedIndexLivrariNatTranzactiei = 1;
        public static int lastSelectedIndexLivrariModTransport = 1;

        #endregion

        public Frm_Setari_Implicite(bool isCalledFromMainToolbar)
        {
            InitializeComponent();
            SetLabels(isCalledFromMainToolbar);
            InitializeHeaders();
            InitializeLists();
            InitializeComboBoxValues();

            if (isCalledFromMainToolbar)
            {
                ReadIntrastatDefault();
                IncarcaDate();
            }
                
        }

        private void ReadIntrastatDefault()
        { OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            try
            {
                dbConn.Open();
                dbQuery = "SELECT * FROM Intrastat_Default WHERE Cod_Fiscal='" + Firma.CodFiscal + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        Val_Implicite.I_Incoterms = dbReader["I_Incoterms"].ToString();
                        Val_Implicite.I_Mod_Transp = dbReader["I_Mod_Transp"].ToString();
                        Val_Implicite.I_Nat_Transp = dbReader["I_Nat_Tranz"].ToString();
                        Val_Implicite.I_Tara_Exp = dbReader["I_Tara_Exp"].ToString();
                        Val_Implicite.O_Incoterms = dbReader["O_Incoterms"].ToString();
                        Val_Implicite.O_Mod_Transp = dbReader["O_Mod_Transp"].ToString();
                        Val_Implicite.O_Nat_Tranz = dbReader["O_Nat_Tranz"].ToString();
                        Val_Implicite.O_Tara_Dest = dbReader["O_Tara_Dest"].ToString();

                        //_cmbAchizitiiCondLivrare.Add(new DateSetariImplicite { Cod = dbReader["Incoterms_COD"].ToString(), Denumire = dbReader["Incoterms_DESC"].ToString() });
                        //_cmbLivrariCondLivrare.Add(new DateSetariImplicite { Cod = dbReader["Incoterms_COD"].ToString(), Denumire = dbReader["Incoterms_DESC"].ToString() });
                    }
                }
                dbConn.Close();
                
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        private void IncarcaDate()
        {
           
            try
            {
                foreach (DateSetariImplicite element in _cmbAchizitiiCondLivrare)
                {
                    if (element.Cod == Val_Implicite.I_Incoterms)
                        cmbAchizitiiCondLivrare.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbAchizitiiModTransport)
                {
                    if (element.Cod == Val_Implicite.I_Mod_Transp)
                        cmbAchizitiiModTransport.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbAchizitiiTaraExpediere)
                {
                    if (element.Cod == Val_Implicite.I_Tara_Exp)
                        cmbArchizitiiTaraExpediere.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbAchizitiiNatTranzactiei)
                {
                    if (element.Cod == Val_Implicite.I_Nat_Transp)
                        cmbAchizitiiNatTranzactiei.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbLivrariCondLivrare)
                {
                    if (element.Cod == Val_Implicite.O_Incoterms)
                        cmbLivrariCondLivrare.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbLivrariModTransport)
                {
                    if (element.Cod == Val_Implicite.O_Mod_Transp)
                        cmbLivrariModTransport.SelectedItem = element;
                }

                foreach (DateSetariImplicite element in _cmbLivrariNatTranzactiei)
                {
                    if (element.Cod == Val_Implicite.O_Nat_Tranz)
                        cmbLivrariNatTranzactiei.SelectedItem = element;
                }
                foreach (DateSetariImplicite element in _cmbLivrariTaraDestinatie)
                {
                    if (element.Cod == Val_Implicite.O_Tara_Dest)
                        cmbLivrariTaraDestinatie.SelectedItem = element;
                }

                /* Val_Implicite.I_Mod_Transp = .SelectedValue.ToString();
                  = cmbAchizitiiNatTranzactiei.SelectedValue.ToString();
                  = cmbArchizitiiTaraExpediere.SelectedValue.ToString();
                   = cmbAchizitiiNatTranzactiei.SelectedValue.ToString();
                  = cmbLivrariCondLivrare.SelectedValue.ToString();
                  = .SelectedValue.ToString();
                  = .SelectedValue.ToString();
                  = .SelectedValue.ToString();*/
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        #region Events

        #region ComboBox

        private void cmbAchizitiiTaraExpediere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArchizitiiTaraExpediere.SelectedIndex == 0)
            {
                cmbArchizitiiTaraExpediere.SelectedIndex = lastSelectedIndexAchizitiiTaraExpediere;
            }
            lastSelectedIndexAchizitiiTaraExpediere = cmbArchizitiiTaraExpediere.SelectedIndex;
        }

        private void cmbAchizitiiCondLivrare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAchizitiiCondLivrare.SelectedIndex == 0)
            {
                cmbAchizitiiCondLivrare.SelectedIndex = lastSelectedIndexAchizitiiCondLivrare;
            }
            lastSelectedIndexAchizitiiCondLivrare = cmbAchizitiiCondLivrare.SelectedIndex;
        }

        private void cmbAchizitiiModTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAchizitiiModTransport.SelectedIndex == 0)
            {
                cmbAchizitiiModTransport.SelectedIndex = lastSelectedIndexAchizitiiModTransport;
            }
            lastSelectedIndexAchizitiiModTransport = cmbAchizitiiModTransport.SelectedIndex;
        }

        private void cmbAchizitiiNatTranzactiei_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAchizitiiNatTranzactiei.SelectedIndex == 0 )
            {
                cmbAchizitiiNatTranzactiei.SelectedIndex = lastSelectedIndexAchizitiiNatTranzactiei;
            }
            lastSelectedIndexAchizitiiNatTranzactiei = cmbAchizitiiNatTranzactiei.SelectedIndex;
        }

        private void cmbLivrariTaraDestinatie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArchizitiiTaraExpediere.SelectedIndex == 0)
            {
                cmbArchizitiiTaraExpediere.SelectedIndex = lastSelectedIndexLivrariTaraExpediere;
            }
            lastSelectedIndexLivrariTaraExpediere = cmbArchizitiiTaraExpediere.SelectedIndex;
        }

        private void cmbLivrariCondLivrare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( cmbLivrariCondLivrare.SelectedIndex == 0)
            {
                cmbLivrariCondLivrare.SelectedIndex = lastSelectedIndexLivrariCondLivrare;
            }
            lastSelectedIndexLivrariCondLivrare = cmbLivrariCondLivrare.SelectedIndex;
        }

        private void cmbLivrariNatTranzactiei_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLivrariNatTranzactiei.SelectedIndex == 0)
            {
                cmbLivrariNatTranzactiei.SelectedIndex = lastSelectedIndexLivrariNatTranzactiei;
            }
            lastSelectedIndexLivrariNatTranzactiei = cmbLivrariNatTranzactiei.SelectedIndex;
        }

        private void cmbLivrariModTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLivrariModTransport.SelectedIndex == 0)
            {
                cmbLivrariModTransport.SelectedIndex = lastSelectedIndexLivrariModTransport;
            }
            lastSelectedIndexLivrariModTransport = cmbLivrariModTransport.SelectedIndex;
        }

        #endregion

        #endregion

        #region Methods

        private void SetLabels(bool isCalledFromMainToolbar)
        {
            if ( !isCalledFromMainToolbar )
            {
                this.lblSetariImplicite.Content = "   Pasul 2 " + this.lblSetariImplicite.Content.ToString().Trim();
            }
            this.Title = "Setari Implicite";
        }

        private void InitializeHeaders()
        {
            _cmbAchizitiiTaraExpediere.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiCondLivrare.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiNatTranzactiei.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiModTransport.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });

            _cmbLivrariTaraDestinatie.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariCondLivrare.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariNatTranzactiei.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariModTransport.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
        }

        private void InitializeComboBoxValues()
        {
            cmbArchizitiiTaraExpediere.DataContext = _cmbAchizitiiTaraExpediere;
            cmbAchizitiiCondLivrare.DataContext = _cmbAchizitiiCondLivrare;
            cmbAchizitiiNatTranzactiei.DataContext = _cmbAchizitiiNatTranzactiei;
            cmbAchizitiiModTransport.DataContext = _cmbAchizitiiModTransport;

            cmbLivrariTaraDestinatie.DataContext = _cmbLivrariTaraDestinatie;
            cmbLivrariCondLivrare.DataContext = _cmbLivrariCondLivrare;
            cmbLivrariNatTranzactiei.DataContext = _cmbLivrariNatTranzactiei;
            cmbLivrariModTransport.DataContext = _cmbLivrariModTransport;
        }

        private void InitializeLists()
        {
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            try
            {
                dbConn.Open();
                dbQuery = "SELECT COUNT(*) FROM Intrastat_Default WHERE Cod_Fiscal='" + Firma.CodFiscal + "'";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                if ((int)dbCommand.ExecuteScalar() < 1)
                {
                    dbQuery = "INSERT INTO Intrastat_Default (Cod_Fiscal) VALUES ('" + Firma.CodFiscal + "')";
                    dbCommand = new OleDbCommand(dbQuery, dbConn);
                    dbCommand.ExecuteNonQuery();
                }

                // cmbAchizitiiTaraExpediere
                dbQuery = "SELECT COD_TARA, Tara_DESC FROM UE_Tari ORDER BY COD_TARA";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while(dbReader.Read())
                    {
                        _cmbAchizitiiTaraExpediere.Add(new DateSetariImplicite { Cod = dbReader["COD_TARA"].ToString(), Denumire = dbReader["Tara_DESC"].ToString() });
                        _cmbLivrariTaraDestinatie.Add(new DateSetariImplicite { Cod = dbReader["COD_TARA"].ToString(), Denumire = dbReader["Tara_DESC"].ToString() });
                       // bAchizitiiTaraExpediere.Add(new DateSetariImplicite { Cod = dbReader["COD_TARA"].ToString(), Denumire = dbReader["Tara_DESC"].ToString() });
                    }
                }

                // cmbAchizitiiCondLivrare
                dbQuery = "SELECT Incoterms_COD, Incoterms_DESC FROM Incoterms ORDER BY Incoterms_COD";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        _cmbAchizitiiCondLivrare.Add(new DateSetariImplicite { Cod = dbReader["Incoterms_COD"].ToString(), Denumire = dbReader["Incoterms_DESC"].ToString() });
                        _cmbLivrariCondLivrare.Add(new DateSetariImplicite { Cod = dbReader["Incoterms_COD"].ToString(), Denumire = dbReader["Incoterms_DESC"].ToString() });
                       // bAchizitiiCondLivrare.Add(new DateSetariImplicite { Cod = dbReader["Incoterms_COD"].ToString(), Denumire = dbReader["Incoterms_DESC"].ToString() });
                    }
                }

                // cmbAchizitiiNatTranzactiei
                dbQuery = "SELECT TR1_COD + '.' + TR2_COD AS COD_Tranz, TR_DESC FROM Tranzactii ORDER BY TR1_COD,TR2_COD";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        _cmbAchizitiiNatTranzactiei.Add(new DateSetariImplicite { Cod = dbReader["COD_Tranz"].ToString(), Denumire = dbReader["TR_DESC"].ToString() });
                        _cmbLivrariNatTranzactiei.Add(new DateSetariImplicite { Cod = dbReader["COD_Tranz"].ToString(), Denumire = dbReader["TR_DESC"].ToString() });
                       // bAchizitiiNatTranzactiei.Add(new DateSetariImplicite { Cod = dbReader["COD_Tranz"].ToString(), Denumire = dbReader["TR_DESC"].ToString() });
                    }
                }

                // cmbAchizitiiModTranspor
                dbQuery = "SELECT COD_MOD_TRANS, DESC_MOD_TRANS FROM MOD_TRANS ORDER BY COD_MOD_TRANS";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbReader = dbCommand.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        _cmbAchizitiiModTransport.Add(new DateSetariImplicite { Cod = dbReader["COD_MOD_TRANS"].ToString(), Denumire = dbReader["DESC_MOD_TRANS"].ToString() });
                        _cmbLivrariModTransport.Add(new DateSetariImplicite { Cod = dbReader["COD_MOD_TRANS"].ToString(), Denumire = dbReader["DESC_MOD_TRANS"].ToString() });
                        //bAchizitiiModTransport.Add(new DateSetariImplicite { Cod = dbReader["COD_MOD_TRANS"].ToString(), Denumire = dbReader["DESC_MOD_TRANS"].ToString() });
                    }
                }
                dbConn.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        #endregion

        private void btnRetineDatele_Click(object sender, RoutedEventArgs e)
        {
            DateSetariImplicite element;
            try
            {
                if (cmbAchizitiiCondLivrare.SelectedValue!=null)
                {
                    element = (DateSetariImplicite)cmbAchizitiiCondLivrare.SelectedItem;
                    Val_Implicite.I_Incoterms = element.Cod;
                }
                else
                    Val_Implicite.I_Incoterms = "";

                if (cmbAchizitiiModTransport.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbAchizitiiModTransport.SelectedItem;
                    Val_Implicite.I_Mod_Transp = element.Cod;
                }
                else
                    Val_Implicite.I_Mod_Transp = "";

                if (cmbAchizitiiNatTranzactiei.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbAchizitiiNatTranzactiei.SelectedItem;
                    Val_Implicite.I_Nat_Transp = element.Cod;
                }     
                else
                    Val_Implicite.I_Nat_Transp = "";

                if (cmbArchizitiiTaraExpediere.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbArchizitiiTaraExpediere.SelectedItem;
                    Val_Implicite.I_Tara_Exp = element.Cod;
                }
                else
                    Val_Implicite.I_Tara_Exp = "";

                if (cmbLivrariCondLivrare.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbLivrariCondLivrare.SelectedItem;
                    Val_Implicite.O_Incoterms = element.Cod;
                }
                else
                    Val_Implicite.O_Incoterms = "";
     
                if (cmbLivrariModTransport.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbLivrariModTransport.SelectedItem;
                    Val_Implicite.O_Mod_Transp = element.Cod;
                }   
                else
                    Val_Implicite.O_Mod_Transp = "";

                if (cmbLivrariNatTranzactiei.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbLivrariNatTranzactiei.SelectedItem;
                    Val_Implicite.O_Nat_Tranz = element.Cod;
                }  
                else
                    Val_Implicite.O_Nat_Tranz = "";

                if (cmbLivrariTaraDestinatie.SelectedValue != null)
                {
                    element = (DateSetariImplicite)cmbLivrariTaraDestinatie.SelectedItem;
                    Val_Implicite.O_Tara_Dest = element.Cod;
                }
                else
                    Val_Implicite.O_Tara_Dest = "";

                Update_Intrastat_Default();
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        private void Update_Intrastat_Default()
        {
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            string dbQuery = string.Empty;
            try
            {
                dbConn.Open();
                //dbQuery = "UPDATE [Intrastat_Default] SET [I_Tara_Exp]='" + Val_Implicite.I_Tara_Exp + "', [I_Incoterm]='" + Val_Implicite.I_Incoterms + "', [I_Nat_Tranz]='" + Val_Implicite.I_Nat_Transp + "', [I_Mod_Transp]='" + Val_Implicite.I_Mod_Transp + "', [O_Tara_Dest]='" + Val_Implicite.O_Tara_Dest + "', [O_Incoterm]='" + Val_Implicite.O_Incoterms + "', [O_Nat_Tranz]='" + Val_Implicite.O_Nat_Tranz + "', [O_Mod_Transp]='" + Val_Implicite.O_Mod_Transp + "' WHERE [Cod_Fiscal]='" + Firma.CodFiscal + "';";
                dbQuery = @"UPDATE Intrastat_Default SET I_Tara_Exp = ?, I_Incoterms = ?, I_Nat_Tranz = ?, I_Mod_Transp = ?, O_Tara_Dest=?, O_Incoterms = ?, O_Nat_Tranz = ?, O_Mod_Transp = ? WHERE Cod_Fiscal = ?;";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbCommand.Parameters.AddWithValue("@I_Tara_Exp", Val_Implicite.I_Tara_Exp);
                dbCommand.Parameters.AddWithValue("@I_Incoterms", Val_Implicite.I_Incoterms);
                dbCommand.Parameters.AddWithValue("@I_Nat_Tranz", Val_Implicite.I_Nat_Transp);
                dbCommand.Parameters.AddWithValue("@I_Mod_Transp", Val_Implicite.I_Mod_Transp);

                dbCommand.Parameters.AddWithValue("@O_Tara_Dest", Val_Implicite.O_Tara_Dest);
                dbCommand.Parameters.AddWithValue("@O_Incoterms", Val_Implicite.O_Incoterms);
                dbCommand.Parameters.AddWithValue("@O_Nat_Tranz", Val_Implicite.O_Nat_Tranz);
                dbCommand.Parameters.AddWithValue("@O_Mod_Transp", Val_Implicite.O_Mod_Transp);

                dbCommand.Parameters.AddWithValue("@Cod_Fiscal", Firma.CodFiscal);
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
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }
}
}