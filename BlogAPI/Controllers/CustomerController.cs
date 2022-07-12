using AutoMapper;
using BlogServer.Data;
using BlogServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;
	public CustomerController(ApplicationDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> Get()
	{
		var result = await _dbContext.Customers.ToListAsync();

		return Ok(result);
	}

	[HttpGet("{Id}", Name = "GetCustomerById")]
	public async Task<ActionResult<Customer>> GetCustomerById(int Id)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);

		if (customer == null) return NotFound("Customer not found");

		return Ok(customer);
	}

	[HttpPut]
	public async Task<ActionResult<Customer>> Put(Customer c)
	{
		var customer = await _dbContext.Customers.FindAsync(c.Id);
		if (customer == null)
			return NotFound("Customer not found");
		_mapper.Map<Customer, Customer>(c, customer);

		await _dbContext.SaveChangesAsync();
		return Ok(customer);
	}

	[HttpDelete("{Id}")]
	public async Task<ActionResult> Delete(int Id)
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

	[HttpPost]
	public async Task<ActionResult<Customer>> Post(Customer customer)
	{
		var customerInDb = await _dbContext.Customers.FindAsync(customer.Id);

		if (customerInDb != null) return Conflict("Customer already exists in db.");

		_dbContext.Customers.Add(customer);
		await _dbContext.SaveChangesAsync();

		return CreatedAtRoute(nameof(GetCustomerById), new { Id = customer.Id }, customer);
	}
}