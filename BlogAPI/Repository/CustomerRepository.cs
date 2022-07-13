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
		var customer = await _dbContext.Customers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == Id);

		return customer;
	}

	public async Task DeleteCustomer(Customer customer)
	{
		_dbContext.Remove(customer);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<Customer> CreateCustomer(Customer customer)
	{
		_dbContext.Add(customer);
		await _dbContext.SaveChangesAsync();

		return customer;
	}

	public async Task<Customer?> UpdateCustomer(Customer customerUpdate)
	{
		Customer? customerUpdated = null;

		var customerInDb = await _dbContext.Customers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == customerUpdate.Id);

		if (customerInDb is not null)
		{
			customerUpdated = _mapper.Map<Customer, Customer>(customerUpdate, customerInDb);
		}
		_dbContext.Customers.Update(customerUpdate);
		await _dbContext.SaveChangesAsync();

		return customerUpdated;
	}
}