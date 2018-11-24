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

        private ObservableCollection<SetariImpliciteCmbBox> _cmbItems = new ObservableCollection<SetariImpliciteCmbBox>();
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

            _cmbItems.Add(new SetariImpliciteCmbBox { Cod = "Name", Denumire = "State" });
            DataContext = _cmbItems;

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
    }
}