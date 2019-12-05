using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using Ovidiu.EU;
using Ovidiu.Miscellaneous;

namespace e_Intrastat
{
    /// <summary>
    /// Interaction logic for Frm_Fisier_Optiuni.xaml
    /// </summary>
    public partial class Frm_Fisier_Optiuni : Window
    {
        string path1;
        string file1;
        public Frm_Fisier_Optiuni(string path, string numeXML)
        {
            InitializeComponent();
            path1 = path;
            file1 = numeXML;
            pathXML.Text = path+numeXML;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if(openFile.IsChecked== true)
                Process.Start(path1);
            var message = new MailMessage();
            if (trimiteEmail.IsChecked == true)
            {
                var attachement = pathXML.Text;

                Attachment attachment = new Attachment(attachement);
                message.Attachments.Add(attachment);
                message.To.Add("delcaratie.intrastat@insse.ro");


               // message.From = new MailAddress("exemplu@gmail.com");
                message.Subject = "Declaratie Intrastat";
                message.IsBodyHtml = true;
                message.Body = "<span style='font-size: 12pt; color: black;'>Declaratie Intrastat</span>";

                var filename = FileLocation.System +"mymessage.eml";
                message.Save(filename);
                //var url = $"mailto:&attachment={attachement}";
                Process.Start(filename);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start($"http://www.e-intrastat.ro/download/diverse/Intrastat_Incarcare_Online.pdf");
        }
    }
}
