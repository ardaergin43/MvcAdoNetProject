using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MvcAdoNetProjem.Models
{
    public class DBHelper
    {
        private readonly string _connectionString;

        public DBHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public User ValidateUser(string email, string password)
        {
            User user = null;
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT Id, Name, Surname, Email FROM dbo.Table_1 WHERE Email = @Email AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                }
                reader.Close();
            }
            return user;
        }

        public bool RegisterUser(User user)
        {
            using (SqlConnection connection = GetConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM dbo.Table_1 WHERE Email = @Email";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Email", user.Email);

                connection.Open();
                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount > 0) return false;

                string insertQuery = "INSERT INTO dbo.Table_1 (Name, Surname, Email, Password) VALUES (@Name, @Surname, @Email, @Password)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Name", user.Name);
                insertCommand.Parameters.AddWithValue("@Surname", user.Surname);
                insertCommand.Parameters.AddWithValue("@Email", user.Email);
                insertCommand.Parameters.AddWithValue("@Password", user.Password);
                return insertCommand.ExecuteNonQuery() > 0;
            }
        }

        public void AddProduct(Product product, string imageUrl)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "INSERT INTO dbo.Table_2 (ProductName, Description, Price, Stock, CreatedDate, IsActive, ImageUrl) " +
                               "VALUES (@ProductName, @Description, @Price, @Stock, @CreatedDate, @IsActive, @ImageUrl)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);
                command.Parameters.AddWithValue("@IsActive", product.Stock > 0 ? 1 : 0);
                command.Parameters.AddWithValue("@ImageUrl", imageUrl);  // Resim yolunu veritabanına ekliyoruz.

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM dbo.Table_2 ORDER BY CreatedDate DESC";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                        ImageUrl = reader["ImageUrl"].ToString() // Resim URL'sini burada alıyoruz.
                    });
                }
                reader.Close();
            }
            return products;
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "DELETE FROM dbo.Table_2 WHERE ProductID = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product, string imageUrl = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "UPDATE dbo.Table_2 SET ProductName = @ProductName, Description = @Description, Price = @Price, Stock = @Stock, IsActive = @IsActive, ImageUrl = @ImageUrl WHERE ProductID = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@IsActive", product.Stock > 0 ? 1 : 0);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@ImageUrl", imageUrl ?? product.ImageUrl); // Resim URL'sini güncellenmişse değiştirecek

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
