using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Ovidiu.Modules
{
    public static class XML_Operatii
    {

        static int a;

        public static XmlNode Citeste_XML( string XML_file, string Nodul, string Element)
        {
            if(Nodul.Substring(Nodul.Length - 1, 1) != "/")
            {
                Nodul = Nodul + "/";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(XML_file);
            XmlNode node_p = doc.SelectSingleNode(Nodul + Element);
            return node_p;
        }

        public static string CitesteValoareNodXML(string XMLFile, string caleNod)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(XMLFile);
                XmlNode xmlNode = xDoc.SelectSingleNode(caleNod);
                return xmlNode.InnerText;
            } 
            catch(Exception exp)
            {
                throw new Exception("Eroare la citirea valorilor din xml pentru nodul: " + caleNod + Environment.NewLine + exp.Message);
            }
        }


       

        public static void Actualizare_XML(string xML_file, string nodul, string elementul, string valoare, bool v)
        {
            if (nodul.Substring(nodul.Length-1, 1) != "/")
                nodul = nodul + "/";
            XmlDocument doc = new XmlDocument();
            XmlNode node_p = doc.SelectSingleNode(nodul + elementul);
            if (node_p.Value == null)
            {
                //MessageBox.Show("Se poate crea", "Info", MessageBoxButton.OK);
                node_p = doc.SelectSingleNode(nodul);
                Creaza_XML("C:\\ProgramData\\E_Intrastat\\Settings.xml", node_p.ToString(), elementul, valoare, v);
                doc.Save("C:\\ProgramData\\E_Intrastat\\Settings.xml");
               
            }
            else
                        
            {

                node_p.Value = valoare.ToString();
                doc.Save("C:\\ProgramData\\E_Intrastat\\Settings.xml");
            }

        }

        public static bool Creaza_XML(string XML_file, string Nodul, string Elementul, string Valoare, bool OverWrite)
        {
            long NrTab;

            if (Nodul.Substring(Nodul.Length - 1, 1) == "/")
            {
                Nodul = Nodul.Substring(0, Nodul.Length - 1);
                if (Nodul.Split('/').Count() <= 1)
                    NrTab = 0;
                else
                    NrTab = Nodul.Split('/').Count() - 1;

                XmlDocument doc = new XmlDocument();
                bool success;
                using (FileStream s = new FileStream("C:\\ProgramData\\E_Intrastat\\Settings.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlNode node_p = doc.SelectSingleNode(Nodul + "/" + Elementul);


                    if (node_p.Value == null)
                    {
                        //MessageBox.Show("Se poate crea", "Info", MessageBoxButton.OK);
                        node_p = doc.SelectSingleNode(Nodul);
                        CreateNode(node_p, Elementul, Valoare, NrTab);
                        doc.Save("C:\\ProgramData\\E_Intrastat\\Settings.xml");
                        return true;
                    }
                    else
                        if (OverWrite == true)
                    {
                        Actualizare_XML(XML_file, Nodul, Elementul, Valoare, false);
                        return true;
                    }

                }

                //LoadData.Document = doc;

                return true;
            }
            else
                return false;

        }

        private static void CreateNode(XmlNode node_p, string elementul, string valoare, long nrTab)
        {
            XmlNode new_node = node_p.OwnerDocument.CreateElement(elementul);
            new_node.Value = valoare.ToString();
            node_p.AppendChild(new_node);

            XmlNode childBankNode1 = node_p.OwnerDocument.CreateTextNode("ChildBlank1");
            if ( nrTab == 0 || nrTab == null)

             childBankNode1.Value = Environment.NewLine+ " ";

              else
             childBankNode1.Value = Environment.NewLine + " " + nrTab +'\t';

            node_p.AppendChild(childBankNode1);
        }


        private static bool Sterge_XML(string XML_file, string Nodul, string Elementul)
        {

            XmlDocument doc = new XmlDocument();
            XmlNode node_p = doc.SelectSingleNode(Nodul + Elementul);
            node_p.ParentNode.RemoveChild(node_p);
            doc.Save("C:\\ProgramData\\E_Intrastat\\Settings.xml");
            return true;
        }

       /* public static bool Verifica_Fisier(string filePath)
        {
            bool result = false;
            // here check if the file is correct
            return result;
        }*/
    }
}
