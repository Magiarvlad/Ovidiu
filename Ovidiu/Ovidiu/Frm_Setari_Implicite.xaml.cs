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
        private ObservableCollection<SetariImpliciteCmbBox> _cmbAchizitiiTaraExpediere = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbAchizitiiCondLivrare = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbAchizitiiNatTranzactiei = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbAchizitiiModTransport = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbLivrariTaraExpediere = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbLivrariCondLivrare = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbLivrariNatTranzactiei = new ObservableCollection<SetariImpliciteCmbBox>();
        private ObservableCollection<SetariImpliciteCmbBox> _cmbLivrariModTransport = new ObservableCollection<SetariImpliciteCmbBox>();

        public static int lastSelectedIndexAchizitiiTaraExpediere = 1;
        public static int lastSelectedIndexAchizitiiCondLivrare = 1;
        public static int lastSelectedIndexAchizitiiNatTranzactiei = 1;
        public static int lastSelectedIndexAchizitiiModTransport = 1;
        public static int lastSelectedIndexLivrariTaraExpediere = 1;
        public static int lastSelectedIndexLivrariCondLivrare = 1;
        public static int lastSelectedIndexLivrariNatTranzactiei = 1;
        public static int lastSelectedIndexLivrariModTransport = 1;

        public Frm_Setari_Implicite()
        {
            InitializeComponent();
            InitializeHeaders();
            
         //   DataContext = _cmbAchizitiiCondLivrare;

        }

        #region Events

        #region ComboBox
        private void cmbTaraExpediere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbAchizitiiTaraExpediere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArchizitiiTaraExpediere.SelectedIndex == 0)
            {
                cmbArchizitiiTaraExpediere.SelectedIndex = lastSelectedIndexAchizitiiTaraExpediere;
            }
            lastSelectedIndexAchizitiiTaraExpediere = cmbArchizitiiTaraExpediere.SelectedIndex;
        }

        #endregion

        #endregion

        #region Methods

        private void InitializeHeaders()
        {
            _cmbAchizitiiTaraExpediere.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiCondLivrare.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiNatTranzactiei.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbAchizitiiModTransport.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });

            _cmbLivrariTaraExpediere.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariCondLivrare.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariNatTranzactiei.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
            _cmbLivrariModTransport.Add(new SetariImpliciteCmbBox { Cod = "Cod", Denumire = "Denumire" });
        }
        #endregion  
    }
}