using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public class ProductModelMapper : IProductModelMapper
{
    public Product MapFromModel(ProductModel source)
    {
        return new Product()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            Type = source.TypeModel
        };
    }

    public ProductModel MapToModel(Product source)
    {
        return new ProductModel()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            TypeModel = source.Type
        };
    }
}