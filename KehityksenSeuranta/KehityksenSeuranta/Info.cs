using KehityksenSeuranta.SeurantaClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KehityksenSeuranta
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }
        infoClass c = new infoClass();

        private void textBoxData_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            //Noudetaan tiedot input kentistä
            c.PelaajaID = textBoxPelaajaID.Text;
            c.Tapot = int.Parse(textBoxTapot.Text);
            c.Kuolemat = int.Parse(textBoxKuolemat.Text);
            c.Damage = int.Parse(textBoxDamage.Text);
            c.Ase = textBoxAse.Text;
            c.Viikko = int.Parse(textBoxViikko.Text);
            c.Peliaika = textBoxPeliaika.Text;
            c.Kommentti = textBoxKommentti.Text;

            //Lisätään infoa databaseen
            bool success = c.Insert(c);
            if(success==true)
            {
                //Onnistunut lisäys
                MessageBox.Show("Uudet tiedot lisätty");
                // tyhjennetään input kentät lisäyksen jälkeen
                Clear();
            }
            else
            {
                //Epäonnistunut lisäys
                MessageBox.Show("Lisäys epäonnistui, yritä uudestaan");

            }
            //Ladataan tieto tietokenttään
            DataTable dt = c.Select();
            dgvLista.DataSource = dt;
        }

        private void Info_Load(object sender, EventArgs e)
        {
            //Ladataan tieto tietokenttään
            DataTable dt = c.Select();
            dgvLista.DataSource = dt;
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
        }
        public void Clear()
        {            
            textBoxPelaajaID.Text = "";
            textBoxTapot.Text = "";
            textBoxKuolemat.Text = "";
            textBoxDamage.Text = "";
            textBoxAse.Text = "";
            textBoxViikko.Text = "";
            textBoxPeliaika.Text = "";
            textBoxKommentti.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // haetaan Data 
            c.Data = int.Parse(textBoxData.Text);
            c.PelaajaID = textBoxPelaajaID.Text;
            c.Tapot = int.Parse(textBoxTapot.Text);
            c.Kuolemat = int.Parse(textBoxKuolemat.Text);
            c.Damage = int.Parse(textBoxDamage.Text);
            c.Ase = textBoxAse.Text;
            c.Viikko = int.Parse(textBoxViikko.Text);
            c.Peliaika = textBoxPeliaika.Text;
            c.Kommentti = textBoxKommentti.Text;
            //päivitetään data databaseen
            bool success = c.Update(c);
            if (success == true)
            {
                //päivitys onnistui
                MessageBox.Show("Päivitys onnistui.");
                //Päivitetään listanäkymä

                DataTable dt = c.Select();
                dgvLista.DataSource = dt;
            }
            else
            {
                //päivitys epäonnistui
                MessageBox.Show("Päivitys epäonnistui, yritä uudelleen.");
            }
        }

        private void dgvLista_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Haetaan data taulukosta input laatikoihin
            // Tunnistetaan valittu rivi johon on klikattu
            int rowIndex = e.RowIndex;
            textBoxData.Text = dgvLista.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxPelaajaID.Text = dgvLista.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxTapot.Text = dgvLista.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxKuolemat.Text = dgvLista.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxDamage.Text = dgvLista.Rows[rowIndex].Cells[4].Value.ToString();
            textBoxAse.Text = dgvLista.Rows[rowIndex].Cells[5].Value.ToString();
            textBoxViikko.Text = dgvLista.Rows[rowIndex].Cells[6].Value.ToString();
            textBoxPeliaika.Text = dgvLista.Rows[rowIndex].Cells[7].Value.ToString();
            textBoxKommentti.Text = dgvLista.Rows[rowIndex].Cells[8].Value.ToString();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Valitaan Datarivi sovelluksesta joka määrittelee poistettavan rivin.
            c.Data = Convert.ToInt32(textBoxData.Text);
            bool success = c.Delete(c);
            if(success==true)
            {
                //Onnistunut poisto
                MessageBox.Show("Poisto onnistui.");

                //Päivitetään listanäkymä
                DataTable dt = c.Select();
                dgvLista.DataSource = dt;
            }
            else
            {
                //Epäonnistunut poisto
                MessageBox.Show("Rivin poistaminen Epäonnistui.");

            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["KehityksenSeuranta.Properties.Settings.cn"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //Hakukentän arvojen nouto
            string keyword = textBoxSearchPelaaja.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Info WHERE PelaajaID LIKE '%"+keyword+"%' OR Ase LIKE '%"+keyword+"%' OR Viikko LIKE '%"+keyword+"%'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvLista.DataSource = dt;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
