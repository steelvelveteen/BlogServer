using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository;

public class CustomerRepository : ICustomerRepository
{
	private readonly ApplicationDbContext _dbContext;

	public CustomerRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<Customer>> GetCustomers()
	{
		var result = await _dbContext
		.Customers
		.AsNoTracking().
		ToListAsync();

		return result;
	}

	public async Task<Customer?> GetCustomerById(int Id)
	{
		var customer = await _dbContext
		.Customers
		.AsNoTracking()
		.SingleOrDefaultAsync(c => c.Id == Id);

		return customer;
	}

	public async Task DeleteCustomer(int Id)
	{
		var customerInDb = _dbContext.Customers.Find(Id);

		if (customerInDb is not null)
			_dbContext.Remove(customerInDb);

		await _dbContext.SaveChangesAsync();
	}

	public async Task<Customer> CreateCustomer(Customer customer)
	{
		_dbContext.Add(customer);
		await _dbContext.SaveChangesAsync();

		return customer;
	}

	public async Task<Customer?> UpdateCustomer(Customer updatedCustomer)
	{
		_dbContext.Customers.Update(updatedCustomer);

		await _dbContext.SaveChangesAsync();

		return updatedCustomer;
	}
}