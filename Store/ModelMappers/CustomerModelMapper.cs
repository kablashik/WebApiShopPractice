using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public class CustomerModelMapper : ICustomerModelMapper
{
    public Customer MapFromModel(CustomerModel model)
    {
        return new Customer()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
            Country = model.Country
        };
    }

    public CustomerModel MapToModel(Customer entity)
    {
        return new CustomerModel()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Age = entity.Age,
            Country = entity.Country
        };
    }
}