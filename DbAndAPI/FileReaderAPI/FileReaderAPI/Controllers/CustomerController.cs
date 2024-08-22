using Database.Models;
using Database.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileReaderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("/Customer/{customerRef}")]
    public async Task<IActionResult> GetCustomerByRefAsync(string customerRef)
    {
        var result = await _customerRepository.GetByCustomerRef(customerRef);
        return Ok(result);
    }

    [HttpPost("/Customer")]
    public async Task<IActionResult> PostCustomerByRefAsync([FromBody] Customer customer)
    {
        return await _customerRepository.CreateCustomer(customer) ? Ok() : BadRequest("Unable to create record in database");
    }
}
