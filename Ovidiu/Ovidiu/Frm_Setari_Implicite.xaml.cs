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
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        #endregion

        private void btnRetineDatele_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                Val_Implicite.I_Incoterms = cmbAchizitiiCondLivrare.SelectedValue.ToString();
                Val_Implicite.I_Mod_Transp = cmbAchizitiiModTransport.SelectedValue.ToString();
                Val_Implicite.I_Nat_Transp = cmbAchizitiiNatTranzactiei.SelectedValue.ToString();
                Val_Implicite.I_Tara_Exp = cmbArchizitiiTaraExpediere.SelectedValue.ToString();
                Val_Implicite.O_Incoterms = cmbLivrariCondLivrare.SelectedValue.ToString();
                Val_Implicite.O_Mod_Transp = cmbLivrariModTransport.SelectedValue.ToString();
                Val_Implicite.O_Nat_Tranz = cmbLivrariNatTranzactiei.SelectedValue.ToString();
                Val_Implicite.O_Tara_Dest = cmbLivrariTaraDestinatie.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }
    }
}