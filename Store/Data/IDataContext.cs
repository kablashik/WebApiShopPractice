using WebApplicationL5.Data.Models;
using WebApplicationL5.Models;
using Customer = WebApplicationL5.Data.Models.Customer;

namespace WebApplicationL5.Data;

public interface IDataContext
{
    public IList<ProductModel> SelectProducts();
    public int AddProduct(ProductModel productModel);
    public int UpdateProduct(ProductModel productModel);
    public int DeleteProduct(int id);

    public IList<CustomerModel> SelectCustomers();
    public int AddCustomer(Customer customer);
    public int UpdateCustomer(Customer customer);
    public int DeleteCustomer(int id);

    public IList<OrderModel> SelectOrders();
    public int AddOrder(Order orderModel);
    public int UpdateOrder(Order orderModel);
    public int DeleteOrder(int id);

}