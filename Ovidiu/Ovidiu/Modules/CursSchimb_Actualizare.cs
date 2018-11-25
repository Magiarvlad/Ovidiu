using Ovidiu.EU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ovidiu.Modules
{
    public static class CursSchimb_Actualizare
    {

        public static void Actualiza_curs()
        {
            try
            {
                DateTime cursBNR_time = File.GetLastWriteTime(FileLocation.System + "CursBNR\\curs.txt");

                if (DateTime.Now.DayOfYear > cursBNR_time.DayOfYear)
                {
                    File.Replace(FileLocation.System + "CursBNR\\curs.txt", FileLocation.System + "CursBNR\\curs_old.txt", FileLocation.System + "CursBNR\\backup.txt");
                    string pathURL = "https://www.soviaserv.ro/curs_bnr/curs.txt";
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(pathURL, FileLocation.System + "CursBNR\\curs.txt");

                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Eroare accesare Curs Valutar!" + exp);
                    }
                    MessageBox.Show("Cursul a fost actualizat cu success!");
                }
                else
                    MessageBox.Show("Cursul valutar este la zi!");
            }
            catch
            {
                MessageBox.Show("Actualizarea automata a EȘUAT!");
            }
        }
    }
}
