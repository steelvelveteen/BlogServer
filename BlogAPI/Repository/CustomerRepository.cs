using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
	private readonly ApplicationDbContext _dbContext;

	public CustomerRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
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

	public Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto)
	{
		throw new NotImplementedException();
	}

	public void DeleteCustomer(int Id)
	{
		throw new NotImplementedException();
	}
}