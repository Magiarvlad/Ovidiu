using Ovidiu.EU;
using Ovidiu.Modules;
using System;
using System.Collections.Generic;
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

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_IntroduceKEY.xaml
    /// </summary>
    public partial class Frm_IntroduceKEY : Window
    {
        public Frm_IntroduceKEY()
        {
            InitializeComponent();
        }

        private void BtnVizualizareKey_Click(object sender, RoutedEventArgs e)
        {
            Frm_VizualizareKEY frm_VizualizareKEY = new Frm_VizualizareKEY();
            frm_VizualizareKEY.Show();
        }

        private void BtnValidare_Click(object sender, RoutedEventArgs e)
        {
            if(TxtKey.Text=="")
            {
                MessageBox.Show("Campul Chei de inregistrare nu poate fi gol");

            }
            else
            {
                try { 
                string txtkey = TxtKey.Text.Replace('\r',' ') ;
                string[] keys = txtkey.Split('\n');
                
                for(int i=keys.Length-1; i>=0; i--)
                {
                    if (keys[i] != " " && keys[i] != "" && keys[i] != null)
                    keys[i] =keys[i].Trim();
                }
                for (int i = keys.Length - 1; i >= 0; i--)
                {
                    string[] arrKeyTxt = new string[4];

                    if (keys[i].Length > 17)
                    {
                        arrKeyTxt = Inregistrare.DecodeKey(keys[i]);
                        if(arrKeyTxt[0]==CodFiscal.Text.Trim())
                        {
                           
                            WriteInFile(keys[i], CodFiscal.Text, arrKeyTxt[2]);
                            MessageBox.Show(string.Format("Cheia: {0} a fost inregistrata cu success!", keys[i]));
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Cheia: {0} era INVALIDA sau Codul fiscal nu coincide!", keys[i]));
                        }
                    }

                }
                }
                catch
                {
                    MessageBox.Show(string.Format("Intregistrarea a esuat!"));
                }
            }
        }

        private void WriteInFile(string v1, string text, string v2)
        {
       
            string[] vs = new string[3];
            StreamWriter stream = File.AppendText(FileLocation.System + "key\\chei.txt");

            stream.WriteLine(v1+"\t"+text + "\t" + v2);
            

            stream.Close();
        }
    }
}
