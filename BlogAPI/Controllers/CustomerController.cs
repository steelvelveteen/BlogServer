using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;
using BlogAPI.Repository;
using Microsoft.AspNetCore.Mvc;
namespace BlogAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CustomerController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly ICustomerRepository _repository;

	/// <summary>
	/// Initializes a new instance of the CustomerController class.
	/// </summary>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	public CustomerController(IMapper mapper, ICustomerRepository repository)
	{
		_mapper = mapper;
		_repository = repository;
	}

	/// <summary>
	/// Fetches all customers from database
	/// </summary>
	/// <response code="200">Returns a list of customer dtos</response>
	[HttpGet]
	[ProducesResponseType(typeof(List<CustomerReadDto>), StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<CustomerReadDto>>> Get()
	{
		var customers = await _repository.GetCustomers();

		var result = _mapper.Map<IEnumerable<CustomerReadDto>>(customers);

		return result.ToList();

	}

	/// <summary>
	/// Fetches a single customer
	/// </summary>
	/// <param name="Id">The Id of the customer we want to fetch</param>
	/// <response code="400">If Id is not supplied</response>
	/// <response code="200">Returns the customer dto</response>
	/// <response code="404">If customer is not found in database</response>
	[HttpGet("{Id}", Name = "GetCustomerById")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CustomerReadDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CustomerReadDto>> GetCustomerById([Required] int Id)
	{
		if (Id <= 0) return BadRequest();

		var customer = await _repository.GetCustomerById(Id);

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
		var customer = await _repository.GetCustomerById(Id);
		if (customer == null)
		{
			return NotFound("Customer not found");
		}
		else
		{
			var updatedCustomer = _mapper.Map<CustomerUpdateDto, Customer>(customerUpdateDto, customer);
			await _repository.UpdateCustomer(customer);
		}

		return NoContent();
	}

	/// <summary>
	/// Deletes a specific Customer
	/// </summary>
	/// <param name="Id">The Id of the customer to delete from db.</param>
	/// <returns>Ok</returns>
	/// <response code="400">If Id is not supplied</response>
	/// <response code="200">Returns ok containing string</response>
	/// <response code="404">If the customer is not found in the db in the first place</response>
	[HttpDelete("{Id}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Delete([Required] int Id)
	{
		if (Id <= 0) return BadRequest();

		var customerInDb = await _repository.GetCustomerById(Id);

		if (customerInDb == null)
		{
			return NotFound("Customer not found.");
		}

		await _repository.DeleteCustomer(customerInDb);

		return NoContent();
	}

	/// <summary>
	/// Creates a new customer and saves it to db
	/// </summary>
	/// <returns>The newly created customer dto</returns>
	/// <response code="201">Returns the newly created customer dto</response>
	/// <response code="409">Returns conflict error if customer already exists in db.</response>
	[HttpPost]
	[ProducesResponseType(typeof(CustomerReadDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<CustomerReadDto>> Post(CustomerCreateDto customerCreateDto)
	{
		var customers = await _repository.GetCustomers();

		if (customers.Any(c => c.FirstName == customerCreateDto.FirstName))
		{
			return Conflict("Customer already exists  in db.");
		}
		else
		{
			var customer = await _repository.CreateCustomer(_mapper.Map<Customer>(customerCreateDto));

			var customerReadDto = _mapper.Map<CustomerReadDto>(customer);

			return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);
		}
	}
}