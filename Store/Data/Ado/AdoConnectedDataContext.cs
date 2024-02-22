using WebApplicationL5.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Metadata;
using MySql.Data.MySqlClient;
using WebApplicationL5.Data.Models;

namespace WebApplicationL5.Data;

public class AdoConnectedDataContext : IDataContext
{
    private readonly string? _connectionString;

    public AdoConnectedDataContext(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public IList<ProductModel> SelectProducts()
    {
        var products = new List<ProductModel>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT * FROM Products";
        var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var product = new ProductModel()
            {
                Id = (int)reader["id"],
                Name = reader["name"] as string,
                Description = reader["description"] as string,
                Price = (double)reader["price"],
                TypeModel = (ProductTypeModel)Enum.Parse(typeof(ProductTypeModel), reader["product_type"].ToString())
            };

            products.Add(product);
        }

        return products;
    }

    public int AddProduct(ProductModel productModel)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "INSERT INTO Products (name, description, price, product_type) VALUES (@Name, @Description, @Price, @Type)";
        var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", productModel.Name);
        command.Parameters.AddWithValue("@Description", productModel.Description);
        command.Parameters.AddWithValue("@Price", productModel.Price);
        command.Parameters.AddWithValue("@Type", productModel.TypeModel.ToString());

        return command.ExecuteNonQuery();
    }

    public int UpdateProduct(ProductModel productModel)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "UPDATE Products SET name = @Name, description = @Description, price = @Price, product_type = @Type WHERE Id = @Id";
        var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Name", productModel.Name);
        command.Parameters.AddWithValue("Description", productModel.Description);
        command.Parameters.AddWithValue("Price", productModel.Price);
        command.Parameters.AddWithValue("Type", productModel.TypeModel.ToString());
        command.Parameters.AddWithValue("Id", productModel.Id);


        return command.ExecuteNonQuery();
    }

    public int DeleteProduct(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM products WHERE Id = @Id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Id", id);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public int ProductsRowsCount()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT COUNT(*) FROM products";
        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();

        return Convert.ToInt32(result);
    }

    public int GetProductId()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT MAX(id) FROM products";

        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }


    public IList<CustomerModel> SelectCustomers()
    {
        var customers = new List<CustomerModel>();

        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        var query = "SELECT * FROM Customers";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var customer = new CustomerModel
            {
                Id = (int)reader["id"],
                FirstName = reader["first_name"] as string,
                LastName = reader["last_name"] as string,
                Age = (int)reader["age"],
                Country = reader["country"] as string
            };

            customers.Add(customer);
        }

        return customers;
    }

    public int AddCustomer(Customer customer)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "INSERT INTO Customers (first_name, last_name, age, country) VALUES (@FirstName, @LastName, @Age, @Country)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("FirstName", customer.FirstName);
        command.Parameters.AddWithValue("LastName", customer.LastName);
        command.Parameters.AddWithValue("Age", customer.Age);
        command.Parameters.AddWithValue("Country", customer.Country);

        return command.ExecuteNonQuery();
    }

    public int UpdateCustomer(Customer customer)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query =
            "UPDATE Customers SET first_name = @FirstName, last_name = @LastName, age = @Age, country = @Country WHERE id = @Id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("FirstName", customer.FirstName);
        command.Parameters.AddWithValue("LastName", customer.LastName);
        command.Parameters.AddWithValue("Age", customer.Age);
        command.Parameters.AddWithValue("Country", customer.Country);
        command.Parameters.AddWithValue("Id", customer.Id);

        return command.ExecuteNonQuery();
    }

    public int DeleteCustomer(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM customers WHERE id = @Id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("Id", id);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public int CustomersRowsCount()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT COUNT(*) FROM customers";
        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();

        return Convert.ToInt32(result);
    }

    public int GetCustomerId()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT MAX(id) FROM customers";

        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }


    public IList<OrderModel> SelectOrders()
    {
        var orders = new List<OrderModel>();
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();
        var query = "SELECT * FROM orders";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var order = new OrderModel()
            {
                Id = (int)reader["id"],
                CustomerId = (int)reader["customer_id"],
                ProductId = (int)reader["product_id"],
                Count = (int)reader["count"],
                CreatedAt = (DateTime)reader["created_at"]
            };

            orders.Add(order);
        }

        return orders;
    }

    public int AddOrder(Order orderModel)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query =
            "INSERT INTO orders (customer_id, product_id, count, created_at) VALUES (@CustomerId, @ProductId, @Count, @CreatedAt)";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("CustomerId", orderModel.CustomerId);
        command.Parameters.AddWithValue("ProductId", orderModel.ProductId);
        command.Parameters.AddWithValue("Count", orderModel.Count);
        command.Parameters.AddWithValue("CreatedAt", orderModel.CreatedAt);

        return command.ExecuteNonQuery();
    }

    public int UpdateOrder(Order orderModel)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var query =
            "UPDATE ORDERS SET customer_id = @CustomerId, product_id = @ProductId, count = @Count, created_at = @CreatedAt WHERE id = @Id";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("CustomerId", orderModel.CustomerId);
        command.Parameters.AddWithValue("ProductId", orderModel.ProductId);
        command.Parameters.AddWithValue("Count", orderModel.Count);
        command.Parameters.AddWithValue("CreatedAt", orderModel.CreatedAt);
        command.Parameters.AddWithValue("Id", orderModel.Id);

        return command.ExecuteNonQuery();
    }

    public int DeleteOrder(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "DELETE FROM orders WHERE id = @Id";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("Id", id);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public int OrdersRowsCount()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT COUNT(*) FROM orders";
        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();

        return Convert.ToInt32(result);
    }

    public int GetOrderId()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT MAX(id) FROM orders";

        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }
}