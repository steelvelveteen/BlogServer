using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlogServer.Data;
using BlogServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CustomerController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of the CustomerController class.
	/// </summary>
	/// <param name="dbContext"></param>
	/// <param name="mapper"></param>
	public CustomerController(ApplicationDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	/// <summary>
	/// Fetches all customers from database
	/// </summary>
	/// <response code="200">Returns a list of customers</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<Customer>>> Get()
	{
		var result = await _dbContext.Customers.ToListAsync();
		return Ok(result);
	}

	/// <summary>
	/// Fetches a single customer
	/// </summary>
	/// <response code="200">Returns the customer</response>
	/// <response code="404">If customer is not found in database</response>
	[HttpGet("{Id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Customer>> Get(int Id)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);
		if (customer == null) return NotFound("Customer not found");

		return Ok(customer);
	}

	/// <summary>
	/// Updates existing customer
	/// </summary>
	/// <param name="c">The customer object to update.</param>
	/// <returns>The updated object</returns>
	/// <response code="200">Returns the modified customer</response>
	/// <response code="404">If the customer is not found in the db in the first place</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Customer>> Put(Customer c)
	{
		var customer = await _dbContext.Customers.FindAsync(c.Id);
		if (customer == null)
		{
			return NotFound("Customer not found");
		}
		else
		{
			_mapper.Map<Customer, Customer>(c, customer);
		}

		await _dbContext.SaveChangesAsync();
		return Ok(customer);
	}

	/// <summary>
	/// Deletes a specific Customer
	/// </summary>
	/// <param name="Id">The Id of the customer to delete from db.</param>
	/// <returns>Ok</returns>
	/// <response code="200">Returns ok containing string</response>
	/// <response code="404">If the customer is not found in the db in the first place</response>
	[HttpDelete("{Id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Delete([Required] int Id)
	{
        var customer = await _dbContext.Customers.FindAsync(Id);
		if (customer == null)
		{
			return NotFound("Customer not found.");
		}
		else
		{
			_dbContext.Customers.Remove(customer);
		}

		await _dbContext.SaveChangesAsync();
		return Ok("Customer deleted from db.");
	}

	/// <summary>
	/// Creates a new customer and saves it to db
	/// </summary>
	/// <returns>The newly created customer</returns>
	/// <response code="200">Returns the new customer</response>
	/// <response code="409">Returns conflict error if customer already exists in db.</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<Customer>> Post(Customer customer)
	{
		var customerInDb = await _dbContext.Customers.FindAsync(customer.Id);
		if (customerInDb != null) return Conflict("Customer already exists in db.");
		_dbContext.Customers.Add(customer);
		await _dbContext.SaveChangesAsync();
		// 201 return CreatedAtAction();
		return Ok(customer);
	}
}