using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public interface ICustomerModelMapper {
   public Customer MapFromModel(CustomerModel model);
   public CustomerModel MapToModel(Customer entity);
}