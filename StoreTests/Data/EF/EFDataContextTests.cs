using Microsoft.EntityFrameworkCore;
using WebApplicationL5.Data.EF;
using WebApplicationL5.Data.Models;
using WebApplicationL5.ModelMappers;

namespace StoreTests.Data.EF;

public class EFDataContextTests
{
    private EFDataContext _dataContext;
    private int _id;

    [SetUp]
    public void Setup()
    {
        ICustomerModelMapper customerModelMapper = new CustomerModelMapper();
        IOrderModelMapper orderModelMapper = new OrderModelMapper();
        _dataContext = new EFDataContext(customerModelMapper, orderModelMapper);
        _id = _dataContext.GetCustomerId() + 1;
    }

    [Test]
    public void AgeConstraint_Should_Not_Allow_Negative_Age()
    {
        //Arrange
        var customer = new Customer { FirstName = "Джон", LastName = "Уик", Age = -5, Country = "США" };

        //Act & Assert
        Assert.Throws<DbUpdateException>(() => _dataContext.AddCustomer(customer));
    }

    [Test]
    public void CountConstraint_Should_Not_Allow_Negative_Count()
    {
        //Arrange
        var order = new Order { CustomerId = 1, ProductId = 1, CreatedAt = DateTime.Now, Count = -2 };

        //Act & Assert
        Assert.Throws<DbUpdateException>(() => _dataContext.AddOrder(order));
    }

    [Test]
    public void DataConstraint_Should_Not_Allow_Future_Date()
    {
        //Arrange
        var order = new Order { CustomerId = 1, ProductId = 1, CreatedAt = new DateTime(2023, 11, 10), Count = 5 };

        //Act & Assert
        Assert.Throws<DbUpdateException>(() => _dataContext.AddOrder(order));
    }

    [Test]
    public void AddCustomerTest()
    {
        // Arrange
        var customer = new Customer { FirstName = "Андрей", LastName = "Пупкин", Age = 15, Country = "Румыния" };

        // Act
        var result = _dataContext.AddCustomer(customer);

        //Assert
        Assert.That(result, Is.EqualTo(1));
        var addedCustomer = _dataContext.SelectCustomers().FirstOrDefault(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName);
        Assert.That(addedCustomer, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(addedCustomer?.FirstName, Is.EqualTo(customer.FirstName));
            Assert.That(addedCustomer?.LastName, Is.EqualTo(customer.LastName));
            Assert.That(addedCustomer?.Age, Is.EqualTo(customer.Age));
            Assert.That(addedCustomer?.Country, Is.EqualTo(customer.Country));
        });
    }

    [Test]
    public void UpdateCustomerTest()
    {
        //Arrange
        var customer = new Customer { FirstName = "Андрей", LastName = "Пупкин", Age = 15, Country = "Румыния"};
        var customerForUpdate = _dataContext.SelectCustomers().FirstOrDefault(c => c.FirstName == customer.FirstName
            && c.LastName == customer.LastName);
        customer.Id = customerForUpdate.Id;

        var newName = "Алексей";
        customer.FirstName = newName;
        
        //Act
        var result = _dataContext.UpdateCustomer(customer);
        
        //Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void DeleteCustomerTest()
    {
        //Arrange
        var id = _dataContext.SelectCustomers().FirstOrDefault(c => c.Id == 12)!.Id;
        
        //Act
        var result = _dataContext.DeleteCustomer(id);
        
        //Assert
        Assert.That(result, Is.EqualTo(1));
    }
}