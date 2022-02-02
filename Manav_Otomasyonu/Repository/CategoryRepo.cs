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
    public class CategoryRepo : RepositoryBase, IRepository<Category>
    {
        public int Create(Category category)
        {
            int id = 0;
            try
            {
                if (category.CategoryName == "")
                {
                    throw new Exception("CategoryName alanı boş bırakılamaz");
                }
                SqlCommand command = new SqlCommand("Sp_Category_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                if (con.State==ConnectionState.Closed)
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
                SqlCommand command = new SqlCommand("delete Categories where CategoryID=@CategoryID ", con);
                command.Parameters.AddWithValue("@CategoryID", id);
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

        public List<Category> Get()
        {
            List<Category> categories = new List<Category>();
            try
            {
                SqlCommand command = new SqlCommand("select CategoryID,CategoryName from Categories", this.con);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = CategoryMapping(reader);
                    categories.Add(category);
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
            return categories;
        }

        private Category CategoryMapping(SqlDataReader reader)
        {
            Category category = new Category() { CategoryID=Convert.ToInt32(reader["CategoryID"]),CategoryName=reader["CategoryName"].ToString() };
            return category;

        }

        public Category GetById(int id)
        {
            Category category = new Category();
            try
            {
                SqlCommand command = new SqlCommand("select CategoryID,CategoryName from Categories where CategoryID=@CategoryID", this.con);
                command.Parameters.AddWithValue("@CategoryID", id);
                if (this.con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category = CategoryMapping(reader);
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
            return category;
        }

        public int Update(Category category)
        {
            int id = 0;
            
            try
            {
                if (category.CategoryName == "")
                {
                    throw new Exception("CategoryName alanı boş bırakılamaz");
                }
                SqlCommand command = new SqlCommand("Sp_Category_Create_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
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
    }
}
