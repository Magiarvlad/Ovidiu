using Ovidiu.Modules;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Frm_Setari.xaml
    /// </summary>
    public partial class Frm_Setari : Window
    {
        public Frm_Setari(bool isCalledFromMainToolbar)
        {
            InitializeComponent();
            InitializeFormValuesFromXMLFile();
        }

        #region Events

        #endregion

        #region Methods

        public void InitializeFormValuesFromXMLFile()
        {
            try
            {
                // FileLocation => Settings/E_Intrastat/Setari/FileLocation
                txtLocatieDirectorBazaDate.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/FileLocation/DataBase");
                txtLocatieDirectorSistemExcel.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/FileLocation/System");
                txtLocatieDefinitieRapoarte.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/FileLocation/ReportDefinitionPath");
                txtLocatieSalvareDeclaratiiXML.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/FileLocation/DirectorSalvare");

                // Zecimale => Settings/E_Intrastat/Setari/Zecimale
                txtZecimaleRotunjireCalcule.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Zecimale/ZecRotCalcule");
                txtZecimaleCalculValuta.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Zecimale/ZecRotValuta");
                txtZecimaleCalculLei.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Zecimale/ZecRotLEI");
                txtZecimaleCalculTaxare.Text = XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Zecimale/NrZecTaxare");

                // Culori => Settings/E_Intrastat/Setari/Culori

                lblCuloareBaraMeniu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Culori/Meniu_Color")));
                lblCuloareFundalLinieSelectata.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Culori/HighlightRowStyle_BackColor")));
                lblCuloareLinieSelectata.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Culori/HighlightRowStyle_ForeColor")));
                lblCuloareTabelaAlternativa1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Culori/OddRowStyle_BackColor")));
                lblCuloareTabelaAlternativa2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Culori/EvenRowStyle_BackColor")));

                // Diverse => Settings/E_Intrastat/Setari/Diverse

                chkActualizareAutomataCursValutar.IsChecked = Convert.ToBoolean(Convert.ToInt16(XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Diverse/UpdateCurs")));
                chkActualizareAutomataProgram.IsChecked = Convert.ToBoolean(Convert.ToInt16(XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Diverse/VerificaUpdate")));
                chkVerificareaGreutatiiNete.IsChecked = Convert.ToBoolean(Convert.ToInt16(XML_Operatii.CitesteValoareNodXML(CONSTANTE.Setting_XML_file, @"Settings/E_Intrastat/Setari/Diverse/VerificaNet")));

            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        #endregion
    }
}
