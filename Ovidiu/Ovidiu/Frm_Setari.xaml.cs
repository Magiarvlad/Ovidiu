using Ovidiu.Modules;
using System;
using System.Windows;
using System.Windows.Media;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region Diverse => Settings/E_Intrastat/Setari/Diverse
            // XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/FileLocation", "DataBase", drive + "E_Intrastat\\System\\DataBase\\", true);
            if (chkActualizareAutomataCursValutar.IsChecked == true)
            {
                XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "UpdateCurs",  "1", true);
            }
            else XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "UpdateCurs", "0", true);


            if (chkActualizareAutomataProgram.IsChecked == true)
            {
                XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaUpdate", "1", true);
            }
            else XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaUpdate", "0", true);

            if (chkVerificareaGreutatiiNete.IsChecked == true)
            {

                XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaNet", "1", true);
            }
            else XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file, "/Settings/E_Intrastat/Setari/Diverse", "VerificaNet", "0", true);
            #endregion

            #region  Zecimale => Settings/E_Intrastat/Setari/Zecimale

            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/Zecimale","ZecRotCalcule", txtZecimaleRotunjireCalcule.Text,true);
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/Zecimale","ZecRotValuta", txtZecimaleCalculValuta.Text,true);
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/Zecimale","ZecRotLEI", txtZecimaleCalculLei.Text,true );
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/Zecimale","NrZecTaxare", txtZecimaleCalculTaxare.Text,true);



            #endregion


            #region  FileLocation => Settings/E_Intrastat/Setari/FileLocation


            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/FileLocation","DataBase", txtLocatieDirectorBazaDate.Text, true);
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/FileLocation","System", txtLocatieDirectorSistemExcel.Text,true);
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/FileLocation","ReportDefinitionPath", txtLocatieDefinitieRapoarte.Text,true);
            XML_Operatii.Actualizare_XML(CONSTANTE.Setting_XML_file,"/Settings/E_Intrastat/Setari/FileLocation","DirectorSalvare", txtLocatieSalvareDeclaratiiXML.Text,true);
            #endregion
        }
    }
}
