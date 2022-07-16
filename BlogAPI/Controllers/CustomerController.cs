using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
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
	/// <response code="200">Returns a list of customer dtos</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<CustomerReadDto>>> Get()
	{
		var customers = await _dbContext.Customers.ToListAsync();

		var result = _mapper.Map<IEnumerable<CustomerReadDto>>(customers);

		return result.ToList();

	}

	/// <summary>
	/// Fetches a single customer
	/// </summary>
	/// <response code="200">Returns the customer dto</response>
	/// <response code="404">If customer is not found in database</response>
	[HttpGet("{Id}", Name = "GetCustomerById")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CustomerReadDto>> GetCustomerById(int Id)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);
		if (customer == null) return NotFound("Customer not found");

		var customerReadDto = _mapper.Map<CustomerReadDto>(customer);
		return customerReadDto;
	}

	/// <summary>
	/// Updates existing customer
	/// </summary>
	/// <param name="Id">The Id of the customer to be updated</param>
	/// <param name="customerUpdateDto">The customer dto object to update.</param>
	/// <returns>The updated object</returns>
	/// <response code="200">Returns the modified customer</response>
	/// <response code="404">If the customer is not found in the db in the first place</response>
	[HttpPut("{Id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Put(int Id, CustomerUpdateDto customerUpdateDto)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);
		if (customer == null)
		{
			return NotFound("Customer not found");
		}
		else
		{
			_mapper.Map<CustomerUpdateDto, Customer>(customerUpdateDto, customer);
		}

		await _dbContext.SaveChangesAsync();

		return NoContent();
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
		return NoContent();
	}

	/// <summary>
	/// Creates a new customer and saves it to db
	/// </summary>
	/// <returns>The newly created customer dto</returns>
	/// <response code="201">Returns the newly created customer dto</response>
	/// <response code="409">Returns conflict error if customer already exists in db.</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<Customer>> Post(CustomerCreateDto customerCreateDto)
	{
		var customerInDb = await _dbContext.Customers.SingleOrDefaultAsync(c => c.FirstName == customerCreateDto.FirstName && c.LastName == customerCreateDto.LastName);

		if (customerInDb is not null) return Conflict("Customer already exists in db");

		var customerModel = _mapper.Map<Customer>(customerCreateDto);

		_dbContext.Customers.Add(customerModel);
		await _dbContext.SaveChangesAsync();

		var customerReadDto = _mapper.Map<CustomerReadDto>(customerModel);

		return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);
	}
}