using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using Serilog.Context;
using WebApplicationL5.Data;
using WebApplicationL5.Data.EF;
using WebApplicationL5.Data.Email;
using WebApplicationL5.Data.Models;
using WebApplicationL5.ModelMappers;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Customer")]
public class CustomerController : Controller
{
    private readonly EFDataContext _efDataContext;
    private readonly ICustomerModelMapper _customerModelMapper;
    private readonly ILogger<CustomerController> _logger;
    //private readonly IEmailSender _emailSenderService;

    private static int _id;

    public CustomerController(EFDataContext dataContext, ICustomerModelMapper modelMapper,
        ILogger<CustomerController> logger) //IEmailSender emailSenderService
    {
        _efDataContext = dataContext;
        _customerModelMapper = modelMapper;
        _logger = logger;
        //_emailSenderService = emailSenderService;
    }

    [AgeAuthorize(MinimumAge = 18)]
    [Authorize(Roles = "admin, user")]
    public IActionResult Index(string param)
    {
        _logger.LogInformation("New request from CustomerController");

        var counter = Metrics.CreateCounter("my_test_count", "CustomerController Index requests count");
        counter.Inc();

        _id = _efDataContext.GetCustomerId() + 1;
        return View(_efDataContext.SelectCustomers());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
        
    }

    [Route("add")]
    public IActionResult Add([FromBody] CustomerModel customer)
    {
        var dbCustomer = _customerModelMapper.MapFromModel(customer);
        _efDataContext.AddCustomer(dbCustomer);

        _id = customer.Id;
        
        return Ok(new { customer.Id });
    }

    [Route("update")]
    public IActionResult Update([FromBody] CustomerModel updatedCustomer)
    {
        if (updatedCustomer.Id >= _id)
        {
            Add(updatedCustomer);
            return Ok();
        }

        var dbCustomer = _customerModelMapper.MapFromModel(updatedCustomer);
        _efDataContext.UpdateCustomer(dbCustomer);
        
        return Ok();
    }

    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        _efDataContext.DeleteCustomer(id);
        
        return RedirectToAction("Index");
    }

   //[Route("email")]
   //public async void Email()
   //{
   //    await _emailSenderService.SendEmailAsync("aspnetsendertest@gmail.com");
   //}
    
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}