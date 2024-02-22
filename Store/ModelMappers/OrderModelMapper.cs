using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public class OrderModelMapper : IOrderModelMapper
{
    public Order MapFromModel(OrderModel source)
    {
        return new Order()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            CustomerId = source.CustomerId,
            Count = source.Count,
            CreatedAt = source.CreatedAt
        };
    }

    public OrderModel MapToModel(Order source)
    {
        return new OrderModel()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            CustomerId = source.CustomerId,
            Count = source.Count,
            CreatedAt = source.CreatedAt
        };
    }
}