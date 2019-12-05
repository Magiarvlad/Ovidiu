using Ovidiu.EU;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Ovidiu
{
    /// <summary>
    /// Interaction logic for Frm_Creare_Firma.xaml
    /// </summary>
    public partial class Frm_Creare_Firma : Window
    {
        string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";

        bool flag = false;
        public Frm_Creare_Firma(bool isCalledFromMainToolbar)
        {
            InitializeComponent();
            SetLabels(isCalledFromMainToolbar);
           // AdaugaFirma();
        }
        private void SetLabels(bool isCalledFromMainToolbar)
        {
            if (!isCalledFromMainToolbar)
            {
                this.lblDateFirma.Content = "   Pasul 1 " + this.lblDateFirma.Content.ToString().Trim();
           
            }
            else
            {
                flag = true;
                this.Title = "Modificare date firma";
                CreazaFirma.Content = "OK - Retine datele";
                IncarcareDateFirma();
            }
        }

        private void IncarcareDateFirma()
        {
            string _oleDBConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + FileLocation.DataBase + "Comun.mdb";
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            OleDbDataReader dbReader = null;
            string dbQuery = string.Empty;
            dbConn.Open();
            dbQuery = "SELECT * FROM Firme where Cod_Fiscal='"+ Firma.CodFiscal + "'" ;
            dbCommand = new OleDbCommand(dbQuery, dbConn);
            dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                while (dbReader.Read())
                {
                    // content_Tari.Add(new Tari(dbReader[0].ToString(), dbReader[1].ToString()));
                    NumeFirma.Text = dbReader[1].ToString();
                    Cif.Text = dbReader[0].ToString();
                    RegComert.Text = dbReader[7].ToString();
                    AdresaFirma.Text= dbReader[2].ToString();
                    Oras.Text = dbReader[4].ToString();
                    Judet.Text = dbReader[3].ToString();
                    CodPostal.Text = dbReader[5].ToString();
                    Tara.Text = dbReader[6].ToString();
                    Nume.Text = dbReader[8].ToString();
                    Functie.Text = dbReader[9].ToString();
                    Telefon.Text = dbReader[10].ToString();
                    Fax.Text = dbReader[11].ToString();
                    Email.Text = dbReader[12].ToString();
                    if (dbReader["ValoareStatistica"].ToString() == "True")
                        CheckBoxValStat.IsChecked = true;
                    if (dbReader["XML_Detaliat"].ToString() == "True")
                        CheckBoxDeclXML.IsChecked = true;
                }
            }
           
            dbConn.Close();
        }

        private void InfoBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Daca se bifeaza aceasta optiune programul NU va mai cumula pozitii care au acelasi cod valmal; tara origine; conditii de livrare. In Acest caz declaratia Intrastat va contine toate liniile necumulate.");
        }

        private void LabelCif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void LabelCif_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "ROXXXXXX - Cod de inregistrare fiscala, fara spatii";
        }

        private void NumeFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Nume firma, va recomand fara SC";
        }

        private void RegComertLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "J00/AA/YYYY: Nr de inregistrare de la registrul comertului";
        }

        private void AdresaFirmaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Labelinfo.Content = "Adresa oficiala, fara oras si judet";
        }

        private void CreazaFirma_Click(object sender, RoutedEventArgs e)
        {
            if(flag==true)
            {
                UpdateDateFirma();
                this.Close();
            }
            else
            {
                if (NumeFirma.Text != "" && AdresaFirma.Text != "" && Oras.Text != "" && Judet.Text != "" && CodPostal.Text != "" && Tara.Text != "" && RegComert.Text != "" && Nume.Text != "" && Functie.Text != "" && Telefon.Text != "" && Fax.Text != "" && Email.Text != "")
                {
                    Firma.CodFiscal = Cif.Text;
                    Firma.NumeFirma = NumeFirma.Text;
                    if (File.Exists(FileLocation.DataBase + Firma.CodFiscal + ".mdb"))
                    {
                        MessageBox.Show("Exista deja o baza de date pentru aceasta firma!");
                    }
                    else
                    {
                        if (File.Exists(FileLocation.DataBase + "Goala.mdb"))
                        {
                            File.Copy(FileLocation.DataBase + "Goala.mdb", FileLocation.DataBase + Firma.CodFiscal + ".mdb");
                            if (File.Exists(FileLocation.DataBase + Firma.CodFiscal + ".mdb"))
                            {
                                AdaugaFirma();
                                Frm_Setari_Implicite frm_Setari_Implicite = new Frm_Setari_Implicite(false);
                                frm_Setari_Implicite.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Firma nu poate fi creata! Verificati drepturile de aministrator.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Goala.mdb nu exista!"); ;
                        }
                    }                
                }
                else
                {
                    MessageBox.Show("Completati toate campurile pentru a crea o firma noua");
                }
            }       
        }

        private void AdaugaFirma()
        {
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            string dbQuery = string.Empty;
            string data = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                dbConn.Open();
                //dbQuery = "UPDATE [Intrastat_Default] SET [I_Tara_Exp]='" + Val_Implicite.I_Tara_Exp + "', [I_Incoterm]='" + Val_Implicite.I_Incoterms + "', [I_Nat_Tranz]='" + Val_Implicite.I_Nat_Transp + "', [I_Mod_Transp]='" + Val_Implicite.I_Mod_Transp + "', [O_Tara_Dest]='" + Val_Implicite.O_Tara_Dest + "', [O_Incoterm]='" + Val_Implicite.O_Incoterms + "', [O_Nat_Tranz]='" + Val_Implicite.O_Nat_Tranz + "', [O_Mod_Transp]='" + Val_Implicite.O_Mod_Transp + "' WHERE [Cod_Fiscal]='" + Firma.CodFiscal + "';";
                dbQuery = @"Insert into Firme (Cod_Fiscal,Nume_Firma,Adresa_Firma,Judet_Firma,Oras_Firma,CodPostal_Firma,Tara_Firma,Reg_No_Firma,Persoana_Intrastat,Persoana_Functie,Tel_Firma,Fax_Firma,Email_Firma,DataBaseFile,Nr_Inregistrare,Key_Inregistrare,CreataLaData,ValoareStatistica,XML_Detaliat) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbCommand.Parameters.AddWithValue("@Cod_Fiscal", Firma.CodFiscal);
                dbCommand.Parameters.AddWithValue("@Nume_Firma", NumeFirma.Text);
                dbCommand.Parameters.AddWithValue("@Adresa_Firma", AdresaFirma.Text);
                dbCommand.Parameters.AddWithValue("@Oras_Firma", Oras.Text);
                dbCommand.Parameters.AddWithValue("@Judet_Firma", Judet.Text);
                dbCommand.Parameters.AddWithValue("@CodPostal_Firma", CodPostal.Text);
                dbCommand.Parameters.AddWithValue("@Tara_Firma", Tara.Text);
                dbCommand.Parameters.AddWithValue("@Reg_No_Firma", RegComert.Text);
                dbCommand.Parameters.AddWithValue("@Persoana_Intrastat", Nume.Text);
                dbCommand.Parameters.AddWithValue("@Persoana_Functie", Functie.Text);
                dbCommand.Parameters.AddWithValue("@Tel_Firma", Telefon.Text);
                dbCommand.Parameters.AddWithValue("@Fax_Firma", Fax.Text);
                dbCommand.Parameters.AddWithValue("@Email_Firma", Email.Text);
                dbCommand.Parameters.AddWithValue("@DataBaseFile", Firma.CodFiscal+".mdb");
                dbCommand.Parameters.AddWithValue("@Nr_Inregistrare", "");
                dbCommand.Parameters.AddWithValue("@Key_Inregistrare", "");
                dbCommand.Parameters.AddWithValue("@CreataLaData", data);
                dbCommand.Parameters.AddWithValue("@ValoareStatistica", CheckBoxValStat.IsChecked);
                dbCommand.Parameters.AddWithValue("@XML_Detaliat", CheckBoxDeclXML.IsChecked);           
                /*
                 * @"UPDATE emp SET ename = ?, job = ?, sal = ?, dept = ? WHERE eno = ?";
                     OleDbCommand cmd = new OleDbCommand(query, con)
                     cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
                     cmd.Parameters.AddWithValue("@job", TextBox3.Text);
                     cmd.Parameters.AddWithValue("@sal", TextBox4.Text);
                     cmd.Parameters.AddWithValue("@dept", TextBox5.Text);
                     cmd.ParametersAddWithValue("@eno", TextBox1.Text);
                 */


                dbCommand.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }

        private void UpdateDateFirma()
        {
            OleDbConnection dbConn = new OleDbConnection(_oleDBConnectionString);
            OleDbCommand dbCommand = null;
            string dbQuery = string.Empty;
            try
            {
                dbConn.Open();
                //dbQuery = "UPDATE [Intrastat_Default] SET [I_Tara_Exp]='" + Val_Implicite.I_Tara_Exp + "', [I_Incoterm]='" + Val_Implicite.I_Incoterms + "', [I_Nat_Tranz]='" + Val_Implicite.I_Nat_Transp + "', [I_Mod_Transp]='" + Val_Implicite.I_Mod_Transp + "', [O_Tara_Dest]='" + Val_Implicite.O_Tara_Dest + "', [O_Incoterm]='" + Val_Implicite.O_Incoterms + "', [O_Nat_Tranz]='" + Val_Implicite.O_Nat_Tranz + "', [O_Mod_Transp]='" + Val_Implicite.O_Mod_Transp + "' WHERE [Cod_Fiscal]='" + Firma.CodFiscal + "';";
                dbQuery = @"UPDATE Firme SET Nume_Firma = ?, Adresa_Firma = ?, Judet_Firma = ?, Oras_Firma = ?, CodPostal_Firma=?, Tara_Firma = ?, Reg_No_Firma = ?, Persoana_Intrastat = ?, Persoana_Functie=?, Tel_Firma=?, Fax_Firma=?, Email_Firma=?, ValoareStatistica=?, XML_Detaliat=? WHERE Cod_Fiscal = ?;";
                dbCommand = new OleDbCommand(dbQuery, dbConn);
                dbCommand.Parameters.AddWithValue("@Nume_Firma", NumeFirma.Text);
                dbCommand.Parameters.AddWithValue("@Adresa_Firma", AdresaFirma.Text);
                dbCommand.Parameters.AddWithValue("@Oras_Firma", Oras.Text);
                dbCommand.Parameters.AddWithValue("@Judet_Firma", Judet.Text);
                dbCommand.Parameters.AddWithValue("@CodPostal_Firma", CodPostal.Text);
                dbCommand.Parameters.AddWithValue("@Tara_Firma", Tara.Text);
                dbCommand.Parameters.AddWithValue("@Reg_No_Firma", RegComert.Text);
                dbCommand.Parameters.AddWithValue("@Persoana_Intrastat", Nume.Text);
                dbCommand.Parameters.AddWithValue("@Persoana_Functie", Functie.Text);
                dbCommand.Parameters.AddWithValue("@Tel_Firma",Telefon.Text);
                dbCommand.Parameters.AddWithValue("@Fax_Firma", Fax.Text);
                dbCommand.Parameters.AddWithValue("@Email_Firma", Email.Text);
                /*
                dbCommand.Parameters.AddWithValue("@DataBaseFile", Firma. );
                dbCommand.Parameters.AddWithValue("@Nr_Inregistrare", Val_Implicite.I_Mod_Transp);
                dbCommand.Parameters.AddWithValue("@Key_Inregistrare", Val_Implicite.O_Tara_Dest);
                dbCommand.Parameters.AddWithValue("@CreataLaData", Val_Implicite.O_Incoterms);
                */
                dbCommand.Parameters.AddWithValue("@ValoareStatistica", CheckBoxValStat.IsChecked);
                dbCommand.Parameters.AddWithValue("@XML_Detaliat", CheckBoxDeclXML.IsChecked);


                dbCommand.Parameters.AddWithValue("@Cod_Fiscal", Firma.CodFiscal);
                /*
                 * @"UPDATE emp SET ename = ?, job = ?, sal = ?, dept = ? WHERE eno = ?";
                     OleDbCommand cmd = new OleDbCommand(query, con)
                     cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
                     cmd.Parameters.AddWithValue("@job", TextBox3.Text);
                     cmd.Parameters.AddWithValue("@sal", TextBox4.Text);
                     cmd.Parameters.AddWithValue("@dept", TextBox5.Text);
                     cmd.ParametersAddWithValue("@eno", TextBox1.Text);
                 */


                dbCommand.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Eroare: " + exp.Message);
            }
        }
    }
}
