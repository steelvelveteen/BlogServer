using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace BlogAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CustomerController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly ICustomerRepository _repository;

	/// <summary>
	/// Initializes a new instance of the CustomerController class.
	/// </summary>
	/// <param name="dbContext"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	public CustomerController(ApplicationDbContext dbContext, IMapper mapper, ICustomerRepository repository)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_repository = repository;
	}

	/// <summary>
	/// Fetches all customers from database
	/// </summary>
	/// <response code="200">Returns a list of customer dtos</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<CustomerReadDto>>> Get()
	{
		var result = await _repository.GetCustomers();

		return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(result));

	}

	/// <summary>
	/// Fetches a single customer
	/// </summary>
	/// <param name="Id">The Id of the customer we want to fetch</param>
	/// <response code="200">Returns the customer dto</response>
	/// <response code="404">If customer is not found in database</response>
	[HttpGet("{Id}", Name = "GetCustomerById")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CustomerReadDto>> GetCustomerById(int Id)
	{
		var customer = await _repository.GetCustomerById(Id);
		if (customer == null) return NotFound("Customer not found");

		return Ok(_mapper.Map<CustomerReadDto>(customer));
	}

	/// <summary>
	/// Updates existing customer
	/// </summary>
	/// <param name="customerUpdateDto">The customer dto object to update.</param>
	/// <returns>The updated object</returns>
	/// <response code="200">Returns the modified customer</response>
	/// <response code="404">If the customer is not found in the db in the first place</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Customer>> Put(CustomerUpdateDto customerUpdateDto)
	{
		var customerInDb = await _dbContext.Customers.FindAsync(customerUpdateDto.Id);
		if (customerInDb == null)
		{
			return NotFound("Customer not found");
		}

		var customerUpdated = await _repository.UpdateCustomer(customerUpdateDto);

		return Ok(customerUpdated);
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
		var customerInDb = await _repository.GetCustomerById(Id);
		if (customerInDb == null)
		{
			return NotFound("Customer not found.");
		}

		await _repository.DeleteCustomer(customerInDb);

		return Ok("Customer deleted from db.");
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
		var customerInDb = await _repository.GetCustomerById(customerCreateDto.Id);
		if (customerInDb is not null)
		{
			return Conflict("Customer already exists in db.");
		}
		else
		{
			var customer = await _repository.CreateCustomer(_mapper.Map<Customer>(customerCreateDto));

			var customerReadDto = _mapper.Map<CustomerReadDto>(customer);

			return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);

		}
	}
}