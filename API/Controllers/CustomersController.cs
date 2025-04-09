using Application.Features.Customers.CreateCustomer;
using Application.Features.Customers.GetAllCustomers;
using Application.Features.Customers.GetCustomerById;
using Application.Features.Customers.GetCustomersWithVisits;
using Application.Features.Customers.GetCustomerWithCarsById;
using Application.Features.Customers.GetTopSpendingCustomers;
using Application.Features.Customers.RestoreCustomer;
using Application.Features.Customers.SoftDeleteCustomer;
using Application.Features.Customers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : Controller
{
    private readonly IMediator _mediator;
    
    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 10,
        [FromQuery] string sort = "Id",
        [FromQuery] string order = "ASC",
        [FromQuery] string? nameLike = null)
    {
        var query = new GetAllCustomersQuery
        {
            Page = page,
            PageSize = perPage,
            SortField = sort,
            SortOrder = order,
            NameFilter = nameLike,
        };
        
        var result = await _mediator.Send(query);
        
        //Response.Headers.Add("Content-Range", $"customers {0}-{0 + customers.Count() - 1}/{customers.Count()}");
        //Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
        
        return Ok(new { data = result.Items, total = result.TotalCount});
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery{ Id = id });
        if (customer == null)
            return NotFound();
        
        return Ok(new { data = customer });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCustomerById), new {id = customerId}, customerId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id, 
        [FromBody] UpdateCustomerCommand command)
    {
        if(id != command.Id)
            return BadRequest();
        
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/softDelete")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _mediator.Send(new SoftDeleteCustomerCommand(id));
        return NoContent();
    }

    [HttpPut("{id:int}/restore")]
    public async Task<IActionResult> RestoreCustomer(int id)
    {
        await _mediator.Send(new RestoreCustomerCommand(id));
        return NoContent();
    }

    [HttpGet("get-with-visits")]
    public async Task<IActionResult> GetCustomerWithVisits()
    {
        var customers = await _mediator.Send(new GetCustomersWithVisitsQuery()); 
        return Ok(new {data = customers});
    }

    [HttpGet("get-with-cars/{id:int}")]
    public async Task<IActionResult> GetCustomerWithCars(int id)
    {
        var customer = await _mediator.Send(new GetCustomerWithCarsByIdQuery{ CustomerId = id });
        return Ok(new {data = customer});
    }

    [HttpGet("get-top-spending/{limit:int}")]
    public async Task<IActionResult> GetCustomerTopSpending(int limit)
    {
        var customers = await _mediator.Send(new GetTopSpendingCustomersQuery{ Limit = limit });
        return Ok(new {data = customers});
    }
}