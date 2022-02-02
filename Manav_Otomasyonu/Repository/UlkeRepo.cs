using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Manav_Otomasyonu.Repository
{
    using Entities;
    using System.Data.SqlClient;

    public class UlkeRepo : RepositoryBase, IRepository<Ulke>
    {
        public int Create(Ulke item)
        {
            throw new NotImplementedException();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ulke> Get()
        {
            List<Ulke> ulkeler = new List<Ulke>();
            try
            {
                SqlCommand command = new SqlCommand("select UlkeId,UlkeAdi from ulkeler where UlkeId=212", con);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ulkeler.Add(new Ulke()
                    {
                        UlkeId = Convert.ToInt32(reader["UlkeId"]),
                        UlkeAdi = reader["UlkeAdi"].ToString()
                    }); 
                }
            }
            catch (Exception ex)
            {
                throw;
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State==System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ulkeler;
        }

        public Ulke GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Ulke item)
        {
            throw new NotImplementedException();
        }
    }
}
