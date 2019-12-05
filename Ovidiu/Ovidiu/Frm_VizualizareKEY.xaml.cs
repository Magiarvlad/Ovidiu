using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_VizualizareKEY.xaml
    /// </summary>
    public partial class Frm_VizualizareKEY : Window
    {
        public Frm_VizualizareKEY()
        {
            InitializeComponent();

            List<Data> items = new List<Data>();
            StreamReader stream =new StreamReader(FileLocation.System + "key\\chei.txt");
            string line = "";
            
            while (true)
            {
                line = stream.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] keys = line.Split('\t');
                items.Add(new Data() { KEY = keys[0], CodFiscal = keys[1], Anul = keys[2] });
            }

            stream.Close();

            
            Lv_Keys.ItemsSource = items;
        }

        private void Tipareste_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Excel_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        public class Data
        {
            public string KEY { get; set; }

            public string CodFiscal { get; set; }

            public string Anul { get; set; }
        }
    }
}
