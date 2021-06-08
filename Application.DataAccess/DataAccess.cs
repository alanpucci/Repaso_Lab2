using Application.Models;
using Application.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess
{
    public static class DataAccess
    {
        public static List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                String connectionStr = @"Data Source=.; Initial Catalog = RepasoBD; Integrated Security = True";

                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;

                command.CommandText = string.Format($"SELECT * FROM Customers");

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read() != false)
                {
                    customers.Add(new Customer(dataReader["Name"].ToString(), dataReader["LastName"].ToString(), Convert.ToInt32(dataReader["Age"]), Convert.ToInt32(dataReader["Id"])));
                }
                dataReader.Close();
                connection.Close();
                return customers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer GetById(int id)
        {
            String connectionStr = @"Data Source=.; Initial Catalog = RepasoBD; Integrated Security = True";

            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;

            command.CommandText = string.Format($"SELECT * FROM Customers WHERE id = {id}");

            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read() == false)
            {
                throw new Exception("Persona no encontrada");
            }
            Customer customer = new Customer(dataReader["Name"].ToString(), dataReader["LastName"].ToString(), Convert.ToInt32(dataReader["Age"]), Convert.ToInt32(dataReader["Id"]));
            dataReader.Close();
            connection.Close();
            return customer;
        }

        public static void InsertCustomer(Customer customer)
        {
            String connectionStr = @"Data Source=.; Initial Catalog = RepasoBD; Integrated Security = True";
            int columnasAfectadas = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO [Customers] ([Name], [LastName], [Age])" + "Values (@name, @lastName, @age)";

                    command.Parameters.AddWithValue("@name", customer.Name);
                    command.Parameters.AddWithValue("@lastName", customer.LastName);
                    command.Parameters.AddWithValue("@age", customer.Age);

                    columnasAfectadas = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            String connectionStr = @"Data Source=.; Initial Catalog = RepasoBD; Integrated Security = True";
            int columnasAfectadas = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = $"UPDATE [Customers] SET Name = @name, LastName = @lastName, Age = @age WHERE Id = {customer.Id}";

                    command.Parameters.AddWithValue("@name", customer.Name);
                    command.Parameters.AddWithValue("@lastName", customer.LastName);
                    command.Parameters.AddWithValue("@age", customer.Age);

                    columnasAfectadas = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteCustomer(int id)
        {
            try
            {
                String connectionStr = @"Data Source=.; Initial Catalog = RepasoBD; Integrated Security = True";

                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;

                command.CommandText = string.Format($"DELETE Customers WHERE id = {id}");

                int rows = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
