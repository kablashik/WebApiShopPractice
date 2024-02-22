using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public interface IOrderModelMapper
{
    public Order MapFromModel(OrderModel model);
    public OrderModel MapToModel(Order model);
}