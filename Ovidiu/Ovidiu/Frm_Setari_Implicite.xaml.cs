using Ovidiu.Clase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiTaraExpediere = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiCondLivrare = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiNatTranzactiei = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbAchizitiiModTransport = new ObservableCollection<DateSetariImplicite>();
        private ObservableCollection<DateSetariImplicite> _cmbLivrariTaraExpediere = new ObservableCollection<DateSetariImplicite>();
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

        public Frm_Setari_Implicite(bool isCalledFromMainToolbar)
        {
            InitializeComponent();
            InitializeHeaders();
            
         //   DataContext = _cmbAchizitiiCondLivrare;

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

        }

        private void cmbAchizitiiNatTranzactiei_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbLivrariTaraDestinatie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbLivrariCondLivrare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbLivrariNatTranzactiei_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbLivrariModTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #endregion

        #region Methods

        private void InitializeHeaders()
        {
            _cmbAchizitiiTaraExpediere.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiCondLivrare.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiNatTranzactiei.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiModTransport.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });

            _cmbLivrariTaraExpediere.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariCondLivrare.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariNatTranzactiei.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariModTransport.Add(new DateSetariImplicite { Cod = "Cod", Denumire = "Denumire" });
        }
        #endregion

     
    }
}