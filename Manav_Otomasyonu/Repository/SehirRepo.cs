using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manav_Otomasyonu.Repository
{
    using Entities;
    using System.Data;
    using System.Data.SqlClient;

    public class SehirRepo : RepositoryBase, IRepository<Sehir>
    {
        public int Create(Sehir sehir)
        {
            int id = 0;
            try
            {
                if (sehir.SehirAdi == "")
                {
                    throw new Exception("SehirName alanı boş bırakılamaz");
                }
                SqlCommand command = new SqlCommand("Sp_Sehir_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SehirAdi", sehir.SehirAdi);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sehir> Get()
        {
            List<Sehir> sehirler = new List<Sehir>();
            try
            {
                SqlCommand command = new SqlCommand("select * from Sehirler", con);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Sehir sehir = new Sehir();
                    sehir.SehirId = Convert.ToInt32(reader["SehirID"]);
                    sehir.SehirAdi = reader["SehirAdi"].ToString();
                    sehirler.Add(sehir);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (this.con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sehirler;
        }

        public Sehir GetById(int id)
        {
            Sehir sehir = new Sehir();
            try
            {
                SqlCommand command = new SqlCommand("select * from Sehirler where SehirId=@id", con);
                command.Parameters.AddWithValue("@id", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sehir.SehirAdi = reader["SehirAdi"].ToString();
                    
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (this.con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sehir;
        }

        public int Update(Sehir item)
        {
            throw new NotImplementedException();
        }
    }
}
