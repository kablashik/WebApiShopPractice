using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;

namespace WebApplicationL5.ModelMappers;

public interface IProductModelMapper
{
    public Product MapFromModel(ProductModel model);
    public ProductModel MapToModel(Product model);
}