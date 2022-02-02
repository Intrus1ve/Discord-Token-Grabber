using Manav_Otomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Manav_Otomasyonu.Repository
{
    public class ProductRepo : RepositoryBase, IRepository<Product>
    {
        public int Create(Product product)
        {
            int id = 0;
            try
            {
                if (product.ProductName == "")
                {
                    throw new Exception("ProductName alanı boş bırakılamaz");
                }
                if (product.CategoryID == 0)
                {
                    throw new Exception("CategoryID alanı boş bırakılamaz");
                }
                SqlCommand command = new SqlCommand("Sp_Product_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
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
                SqlCommand command = new SqlCommand("delete Product where ProductID=@ProductID ", con);
                command.Parameters.AddWithValue("@ProductID", id);
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

        public List<Product> Get()
        {
            List<Product> products = new List<Product>();
            try
            {
                SqlCommand command = new SqlCommand("select ProductID,ProductName,CategoryID,UnitPrice,UnitsInStock from Product", this.con);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = ProductMapping(reader);
                    products.Add(product);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                if (this.con.State==ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return products;
        }

        private Product ProductMapping(SqlDataReader reader)
        {
            Product product = new Product() {
                ProductID=Convert.ToInt32(reader["ProductID"]),
                ProductName=reader["ProductName"].ToString(),
                CategoryID=Convert.ToInt32(reader["CategoryID"]),
                UnitPrice=Convert.ToDecimal(reader["UnitPrice"]),
                UnitsInStock=Convert.ToInt32(reader["UnitsInStock"]) };
            return product;
        }

        public Product GetById(int id)
        {
            Product product = new Product();
            try
            {
                SqlCommand command = new SqlCommand("select ProductID,ProductName,CategoryID,UnitPrice,UnitsInStock from Product where ProductID=@ProductID", this.con);
                command.Parameters.AddWithValue("@ProductID", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product = ProductMapping(reader);
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
            return product;
        }

        public int Update(Product product)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Product_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                id = Convert.ToInt32(command.ExecuteScalar());
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
            return id;
        }
    }
}
