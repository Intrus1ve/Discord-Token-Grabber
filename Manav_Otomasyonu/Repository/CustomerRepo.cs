using Manav_Otomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manav_Otomasyonu.Repository
{
    using Entities;
    using System.Data.SqlClient;
    using System.Data;
    public class CustomerRepo : RepositoryBase, IRepository<Customer>
    {
        public CustomerRepo() : base()
        {

        }
        public int Create(Customer customer)
        {
            int id = 0;
            try
            {
                if (customer.FirstName == "")
                {
                    throw new Exception("FirstName alanı boş bırakılamaz");
                }
                if (customer.LastName == "")
                {
                    throw new Exception("LastName alanı boş bırakılamaz");
                }
                if (customer.Phone == "")
                {
                    throw new Exception("Phone alanı boş bırakılamaz");
                }
                if (customer.Phone.Length<14)
                {
                    throw new Exception("Telefonu eksik yada hatalı girdiniz");
                }
                SqlCommand command = new SqlCommand("Sp_Customer_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Country", customer.Country);
                command.Parameters.AddWithValue("@City", customer.City);
                command.Parameters.AddWithValue("@Region", customer.Region);
                command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@Address", customer.Address);
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
            try
            {
                SqlCommand command = new SqlCommand("delete Customer where MusteriID=@MusteriID ", con);
                command.Parameters.AddWithValue("@MusteriID", id);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public List<Customer> Get()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                SqlCommand command = new SqlCommand("select MusteriID,FirstName,LastName,Country,City,Region,PostalCode,Phone,Address from customer", this.con);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = CustomerMapping(reader);
                    customers.Add(customer);
                }
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
            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer = new Customer();
            try
            {
                SqlCommand command = new SqlCommand("select MusteriID,FirstName,LastName,Country,City,Region,PostalCode,Phone,Address from customer where MusteriID=@p1", this.con);
                command.Parameters.AddWithValue("@p1", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer = CustomerMapping(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customer;
        }

        public int Update(Customer customer)
        {
            int id = 0;
            try
            {
                if (customer.FirstName == "")
                {
                    throw new Exception("FirstName alanı boş bırakılamaz");
                }
                if (customer.LastName == "")
                {
                    throw new Exception("LastName alanı boş bırakılamaz");
                }
                if (customer.Phone=="")
                {
                    throw new Exception("Phone alanı boş bırakılamaz");
                }
                SqlCommand command = new SqlCommand("Sp_Customer_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MusteriID", customer.MusteriID);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Country", customer.Country);
                command.Parameters.AddWithValue("@City", customer.City);
                command.Parameters.AddWithValue("@Region", customer.Region);
                command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@Address", customer.Address);
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
        private Customer CustomerMapping(SqlDataReader reader)
        {
            Customer customer = new Customer() { MusteriID = Convert.ToInt32(reader["MusteriID"]), FirstName = reader["FirstName"].ToString(), LastName = reader["LastName"].ToString(), Country = reader["Country"].ToString(), City = reader["City"].ToString(), Region = reader["Region"].ToString(), PostalCode = reader["PostalCode"].ToString(), Phone = reader["Phone"].ToString(), Address = reader["Address"].ToString() };
            return customer;
        }
    }
}
