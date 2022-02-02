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

    public class IlceRepo : RepositoryBase, IRepository<Ilce>
    {
        public int Create(Ilce item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ilce> Get()
        {
            List<Ilce> ilceler = new List<Ilce>();
            try
            {
                SqlCommand command = new SqlCommand("select * from Ilceler", con);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ilce ilce = new Ilce();
                    ilce.IlceId = Convert.ToInt32(reader["IlceId"]);
                    ilce.SehirId = Convert.ToInt32(reader["SehirId"]);
                    ilce.IlceAdi = reader["IlceAdi"].ToString();
                    ilceler.Add(ilce);
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
            return ilceler;
        }

        public Ilce GetById(int id)
        {
            Ilce ilce = new Ilce();
            try
            {
                SqlCommand command = new SqlCommand("select * from Ilceler where SehirId=@id", con);
                command.Parameters.AddWithValue("@id", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ilce.IlceId = Convert.ToInt32(reader["IlceId"]);
                    ilce.SehirId = Convert.ToInt32(reader["SehirId"]);
                    ilce.IlceAdi = reader["IlceAdi"].ToString();
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
            return ilce;
        }
        public List<Ilce> GetBySehirId(int id)
        {
            List<Ilce> ilceler = new List<Ilce>();
            try
            {
                SqlCommand command = new SqlCommand("select * from Ilceler where SehirId=@id", con);
                command.Parameters.AddWithValue("@id", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ilce ilce = new Ilce();
                    ilce.IlceId = Convert.ToInt32(reader["IlceId"]);
                    ilce.SehirId = Convert.ToInt32(reader["SehirId"]);
                    ilce.IlceAdi = reader["IlceAdi"].ToString();
                    ilceler.Add(ilce);
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
            return ilceler;
        }

        public int Update(Ilce item)
        {
            throw new NotImplementedException();
        }
    }
}
