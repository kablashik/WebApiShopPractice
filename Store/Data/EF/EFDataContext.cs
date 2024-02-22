using Microsoft.EntityFrameworkCore;
using WebApplicationL5.Data.Models;
using WebApplicationL5.ModelMappers;
using WebApplicationL5.Models;

namespace WebApplicationL5.Data.EF;

public class EFDataContext : DbContext, IDataContext
{
    public EFDataContext(ICustomerModelMapper customerModelMapper, IOrderModelMapper orderModelMapper)
    {
        _orderModelMapper = orderModelMapper;
        _customerModelMapper = customerModelMapper;
        //Database.EnsureDeleted();
        Database.EnsureCreated();
        //Database.Migrate();
    }

    private DbSet<Customer> _customers { get; set; }
    private DbSet<Order> Orders { get; set; }

    private ICustomerModelMapper _customerModelMapper;
    private IOrderModelMapper _orderModelMapper;
    private IProductModelMapper _productModelMapper;

    public IList<ProductModel> SelectProducts()
    {
        throw new NotImplementedException();
    }

    public int AddProduct(ProductModel productModel)
    {
        throw new NotImplementedException();
    }

    public int UpdateProduct(ProductModel productModel)
    {
        throw new NotImplementedException();
    }

    public int DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public IList<CustomerModel> SelectCustomers()
    {
        var result = _customers.Select(customer => _customerModelMapper.MapToModel(customer)).ToList();

        return result;
    }

    public int AddCustomer(Customer customer)
    {
        _customers.Add(customer);

        return SaveChanges();
    }

    public int UpdateCustomer(Customer customer)
    {
        var foundCustomer = _customers.Find(customer.Id);
        if (foundCustomer == null) return 0;

        foundCustomer.FirstName = customer.FirstName;
        foundCustomer.LastName = customer.LastName;
        foundCustomer.Age = customer.Age;
        foundCustomer.Country = customer.Country;

        return SaveChanges();
    }

    public int DeleteCustomer(int id)
    {
        var foundCustomer = _customers.FirstOrDefault(row => row.Id == id);

        if (foundCustomer != null) _customers.Remove(foundCustomer);

        return SaveChanges();
    }

    public int GetCustomerId()
    {
        var maxCustomerId = _customers.Max(customer => customer.Id);
        return maxCustomerId;
    }

    public IList<OrderModel> SelectOrders()
    {
        return Orders.Select(order => _orderModelMapper.MapToModel(order)).ToList();
    }

    public int AddOrder(Order order)
    {
        Orders.Add(order);

        return SaveChanges();
    }

    public int UpdateOrder(Order order)
    {
        var foundOrder = Orders.Find(order.Id);
        if (foundOrder == null) return 0;

        foundOrder.CustomerId = order.CustomerId;
        foundOrder.ProductId = order.ProductId;
        foundOrder.Count = order.Count;
        foundOrder.CreatedAt = order.CreatedAt;

        return SaveChanges();
    }

    public int DeleteOrder(int id)
    {
        var foundOrder = Orders.FirstOrDefault(order => order.Id == id);

        if (foundOrder != null) Orders.Remove(foundOrder);

        return SaveChanges();
    }

    public int GetOrderId()
    {
        var maxOrderId = Orders.Max(order => order.Id);
        return maxOrderId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Customer>().Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Customer>().Property(p => p.FirstName).HasColumnName("first_name");
        modelBuilder.Entity<Customer>().Property(p => p.LastName).HasColumnName("last_name");
        modelBuilder.Entity<Customer>().Property(p => p.Age).HasColumnName("age");
        modelBuilder.Entity<Customer>().ToTable(p => p.HasCheckConstraint("age", "age > 0"));
        modelBuilder.Entity<Customer>().Property(p => p.Country).HasColumnName("country");

        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<Order>().Property(p => p.CustomerId).HasColumnName("customer_id");
        modelBuilder.Entity<Order>().Property(p => p.ProductId).HasColumnName("product_id");
        modelBuilder.Entity<Order>().Property(p => p.CreatedAt).HasColumnName("created_at");
        modelBuilder.Entity<Order>().Property(p => p.Count).HasColumnName("count");
        modelBuilder.Entity<Order>().ToTable(p => p.HasCheckConstraint("count", "count >= 0"));

        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Product>().Property(p => p.Type).HasColumnName("product_type");
        modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("Price", "Price > 0"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseMySQL("Datasource=localhost;Database=usersdb;User=root;Password=111;");
        optionsBuilder.UseMySQL("Datasource=localhost;Database=usersdb3;User=root;Password=111;");
        //optionsBuilder.UseInMemoryDatabase(new Guid().ToString());
    }
    
}