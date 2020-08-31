using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KehityksenSeuranta.SeurantaClasses
{
    class infoClass
    {

        // Data carrier.
        public int Data { get; set; }
        public string PelaajaID { get; set; }
        public int Tapot { get; set; }
        public int Kuolemat { get; set; }
        public int Damage { get; set; }
        public string Ase { get; set; }
        public int Viikko { get; set; }
        public string Peliaika { get; set; }
        public string Kommentti { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["KehityksenSeuranta.Properties.Settings.cn"].ConnectionString;

        // Valitaan Data Databasesta
        public DataTable Select()
        {
            //1. Database yhteys
        SqlConnection conn = new SqlConnection(myconnstrng);
        DataTable dt = new DataTable();
        try
        {
            //2. SQL Query
            string sql = "SELECT * FROM Info";
        SqlCommand cmd = new SqlCommand(sql, conn);
        // SQL DataAdapter using cmd
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        conn.Open();
        adapter.Fill(dt);
        }
        catch(Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }
        return dt;
        }
        //Datan syöttäminen databaseen
        public bool Insert (infoClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL datan päivittämiseen
                //Tapa jolla päivitetään tiedot databaseen sovelluksesta
                string sql = "INSERT INTO Info (PelaajaID, Tapot, Kuolemat, Damage, Ase, Viikko, Peliaika, Kommentti) VALUES (@PelaajaID, @Tapot, @Kuolemat, @Damage, @Ase, @Viikko, @Peliaika, @Kommentti)";

                //SQL komennon luonti
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Luodaan parametrit datan lisäämiseen             
                cmd.Parameters.AddWithValue("@PelaajaID", c.PelaajaID);
                cmd.Parameters.AddWithValue("@Tapot", c.Tapot);
                cmd.Parameters.AddWithValue("@Kuolemat", c.Kuolemat);
                cmd.Parameters.AddWithValue("@Damage", c.Damage);
                cmd.Parameters.AddWithValue("@Ase", c.Ase);
                cmd.Parameters.AddWithValue("@Viikko", c.Viikko);
                cmd.Parameters.AddWithValue("@Peliaika", c.Peliaika);
                cmd.Parameters.AddWithValue("@Kommentti", c.Kommentti);
                // avataan yhteys
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                } 
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;     
        }

        //SQL datan päivittämiseen

        public bool Update(infoClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE Info SET PelaajaID=@PelaajaID, Tapot=@Tapot, Kuolemat=@Kuolemat, Damage=@Damage, Ase=@Ase, Viikko=@Viikko, Peliaika=@Peliaika, Kommentti=@Kommentti WHERE Data=@Data";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Data", c.Data);
                cmd.Parameters.AddWithValue("@PelaajaID", c.PelaajaID);
                cmd.Parameters.AddWithValue("@Tapot", c.Tapot);
                cmd.Parameters.AddWithValue("@Kuolemat", c.Kuolemat);
                cmd.Parameters.AddWithValue("@Damage", c.Damage);
                cmd.Parameters.AddWithValue("@Ase", c.Ase);
                cmd.Parameters.AddWithValue("@Viikko", c.Viikko);
                cmd.Parameters.AddWithValue("@Peliaika", c.Peliaika);
                cmd.Parameters.AddWithValue("@Kommentti", c.Kommentti);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        // Datan poistaminen tietokannasta
        public bool Delete(infoClass c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL Query datan poistoon
                string sql = "DELETE FROM Info WHERE Data=@Data";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Data", c.Data);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // Jos Query onnistuu, arvo 0, ellei suurempi kuin 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return isSuccess;
        }
        
    }

}
