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
    }
}
