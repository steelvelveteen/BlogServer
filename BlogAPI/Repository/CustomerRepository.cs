using AutoMapper;
using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;

	public CustomerRepository(ApplicationDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<IEnumerable<Customer>> GetCustomers()
	{
		var result = await _dbContext.Customers.ToListAsync();
		return result;
	}

	public async Task<Customer?> GetCustomerById(int Id)
	{
		var customer = await _dbContext.Customers.FindAsync(Id);

		return customer;
	}

	public async Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto)
	{
		var customer = _mapper.Map<Customer>(customerCreateDto);

		_dbContext.Add(customer);
		await _dbContext.SaveChangesAsync();

		return customer;
	}

	public void DeleteCustomer(int Id)
	{
		throw new NotImplementedException();
	}
}