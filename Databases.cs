using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using Npgsql;
using tryout_gigamin1_nugi_mulya_nugraha.Models;


namespace tryout_gigamin1_nugi_mulya_nugraha
{
    public interface IDatabase
    {
        //Customer
        int CreateCustomer(Customer customer);
        List<Customer> ReadCustomer();
        Customer GetByIdCustomer(int id);
        int UpdateCustomer([FromBody]JsonPatchDocument<Customer> customer, int id);
        int DeleteCustomer(int id);
        //Product
        int CreateProduct(Product product);
        List<Product> ReadProduct();
        Product GetByIdProduct(int id);
        int UpdateProduct([FromBody]JsonPatchDocument<Product> product, int id);
        int DeleteProduct(int id);
        //Driver
        int CreateDriver(Driver driver);
        List<Driver> ReadDriver();
        Driver GetByIdDriver(int id);
        int UpdateDriver([FromBody]JsonPatchDocument<Driver> driver, int id);
        int DeleteDriver(int id);
        //Product
    }
    class Database : IDatabase
    {   
       NpgsqlConnection _connection;

        public Database (NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        // Customer Table
        public int CreateCustomer(Customer customer)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO customer (username, fullname, email, phone_number) VALUES (@username, @full_name, @email, @phone_number) RETURNING id";

            command.Parameters.AddWithValue("@username", customer.Username);
            command.Parameters.AddWithValue("@full_name", customer.Full_name);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@phone_number", customer.Phone_number);

            command.Prepare();

            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public List<Customer> ReadCustomer()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM customer";
            var result = command.ExecuteReader();
            var customers = new List<Customer>();

            while (result.Read())
                customers.Add(new Customer(){
                    Id = (int)result[0], 
                    Username = (string)result[1], 
                    Full_name = (string)result[2], 
                    Email = (string)result[3], 
                    Phone_number = (string)result[4]
                });

            _connection.Close();

            return customers;
        }

        public Customer GetByIdCustomer(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM customer WHERE id={id}";
            var result = command.ExecuteReader();
            result.Read();
            var customer = new Customer()
            { 
                Id = (int)result[0], 
                Username = (string)result[1], 
                Full_name = (string)result[2], 
                Email = (string)result[3], 
                Phone_number = (string)result[4]
            };
            return customer;
        }

        public int UpdateCustomer([FromBody]JsonPatchDocument<Customer> customer, int id)
        {
            var command = _connection.CreateCommand();
            var customers = GetByIdCustomer(id);
            _connection.Open();
            customer.ApplyTo(customers);

            command.CommandText = $"UPDATE customer SET (username, fullname, email, phone_number) = VALUES (@username, @full_name, @email, @phone_number) WHERE id={id}";

            
            command.Parameters.AddWithValue("@username", customers.Username);
            command.Parameters.AddWithValue("@full_name", customers.Full_name);
            command.Parameters.AddWithValue("@email", customers.Email);
            command.Parameters.AddWithValue("@phone_number", customers.Phone_number);

            command.Prepare();

            var result = command.ExecuteNonQuery();
            _connection.Close();

            return (int)result;
        }

        public int DeleteCustomer(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"DELETE FROM member WHERE id={id}";
            var result = command.ExecuteNonQuery();
            _connection.Close();
            return (int)result;
        }

        // Product Table
        public int CreateProduct(Product product)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO product (name, price ) VALUES (@name, @price) RETURNING id";

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);

            command.Prepare();

            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public List<Product> ReadProduct()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM product";
            var result = command.ExecuteReader();
            var products = new List<Product>();

            while (result.Read())
                products.Add(new Product(){
                    Id = (int)result[0], 
                    Name = (string)result[1], 
                    Price = (int)result[2] 

                });

            _connection.Close();

            return products;
        }

        public Product GetByIdProduct(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM product WHERE id={id}";
            var result = command.ExecuteReader();
            result.Read();
            var product = new Product()
            { 
                Id = (int)result[0], 
                Name = (string)result[1], 
                Price = (int)result[2]
            };
            return product;
        }

        public int UpdateProduct([FromBody]JsonPatchDocument<Product> product, int id)
        {
            var command = _connection.CreateCommand();
            var products = GetByIdProduct(id);
            _connection.Open();
            product.ApplyTo(products);

            command.CommandText = $"UPDATE product SET (name, price) = VALUES (@name, @price) WHERE id={id}";

            
            command.Parameters.AddWithValue("@name", products.Name);
            command.Parameters.AddWithValue("@price", products.Price);

            command.Prepare();

            var result = command.ExecuteNonQuery();
            _connection.Close();

            return (int)result;
        }

        public int DeleteProduct(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"DELETE FROM product WHERE id={id}";
            var result = command.ExecuteNonQuery();
            _connection.Close();
            return (int)result;
        }

        // driver table
        public int CreateDriver(Driver driver)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO driver (fullname,phone_number  ) VALUES (@name, @phone) RETURNING id";

            command.Parameters.AddWithValue("@name", driver.Full_name);
            command.Parameters.AddWithValue("@phone", driver.Phone_number);

            command.Prepare();

            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public List<Driver> ReadDriver()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM driver";
            var result = command.ExecuteReader();
            var drivers = new List<Driver>();

            while (result.Read())
                drivers.Add(new Driver(){
                    Id = (int)result[0], 
                    Full_name = (string)result[1], 
                    Phone_number = (string)result[2] 

                });

            _connection.Close();

            return drivers;
        }

        public Driver GetByIdDriver(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM product WHERE id={id}";
            var result = command.ExecuteReader();
            result.Read();
            var product = new Driver()
            { 
                Id = (int)result[0], 
                Full_name = (string)result[1], 
                Phone_number = (string)result[2]
            };
            return product;
        }

        public int UpdateDriver([FromBody]JsonPatchDocument<Driver> driver, int id)
        {
            var command = _connection.CreateCommand();
            var drivers = GetByIdDriver(id);
            _connection.Open();
            driver.ApplyTo(drivers);

            command.CommandText = $"UPDATE product SET (name, price) = VALUES (@name, @price) WHERE id={id}";

            
            command.Parameters.AddWithValue("@name", drivers.Full_name);
            command.Parameters.AddWithValue("@price", drivers.Phone_number);

            command.Prepare();

            var result = command.ExecuteNonQuery();
            _connection.Close();

            return (int)result;
        }

        public int DeleteDriver(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"DELETE FROM product WHERE id={id}";
            var result = command.ExecuteNonQuery();
            _connection.Close();
            return (int)result;
        }

        public int CreateOrder(Order order)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO order (customer_id, driver_id, product_id, status_order ) VALUES (@customer_id, @driver_id) RETURNING id";

            command.Parameters.AddWithValue("@customer_id",order.Cstmer_id);
            command.Parameters.AddWithValue("@driver_id", order.Driver_id);
            command.Parameters.AddWithValue("@product_id", order.Order_detail);

            command.Prepare();

            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }
    }
}