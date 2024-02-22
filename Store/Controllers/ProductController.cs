using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class ProductController : Controller
{
    private const string ConnectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=111;";
    private readonly AdoConnectedDataContext _dataContext = new(ConnectionString);

    private static int _id;

    public IActionResult Index()
    {
        _id = _dataContext.GetProductId() + 1;

        return View(_dataContext.SelectProducts());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] ProductModel productModel)
    {
        _dataContext.AddProduct(productModel);
        _id = productModel.Id;

        return Ok(new { productModel.Id });
    }

    [HttpPost("update")]
    public IActionResult UpdateProduct([FromBody] ProductModel updatedProductModel)
    {
        if (updatedProductModel.Id >= _id)
        {
            AddProduct(updatedProductModel);
            return Ok();
        }

        _dataContext.UpdateProduct(updatedProductModel);

        return Ok();
    }

    [HttpGet("delete-{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _dataContext.DeleteProduct(id);

        return RedirectToAction("Index");
    }

    [Route("rows-count")]
    public IActionResult GetRowsCount()
    {
        var rows = _dataContext.ProductsRowsCount();

        return Ok(rows);
    }
}