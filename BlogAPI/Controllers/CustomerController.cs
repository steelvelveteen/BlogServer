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

		return result;
	}

	[HttpGet("{Id}", Name = "GetCustomerById")]
	public async Task<ActionResult<Customer>> GetCustomerById(int Id)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);

		if (customer == null) return NotFound("Customer not found");

		return customer;
	}

	[HttpPut("{Id}")]
	public async Task<ActionResult> Put(int Id, Customer c)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);

		if (customer == null)
		{
			return NotFound();
		}
		else
		{
			_mapper.Map<Customer, Customer>(c, customer);
		}

		await _dbContext.SaveChangesAsync();

		return NoContent();
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

		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult<Customer>> Post(Customer customer)
	{
		var customerInDb = await _dbContext.Customers.SingleOrDefaultAsync(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName);

		if (customerInDb != null) return Conflict("Customer already exists in db.");
		_dbContext.Customers.Add(customer);

		await _dbContext.SaveChangesAsync();

		return CreatedAtAction(nameof(GetCustomerById), new { Id = customer.Id }, customer);
	}
}