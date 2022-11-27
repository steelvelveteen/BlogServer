using BlogAPI.Models;

namespace BlogAPI.Repository;

public interface ICustomerRepository
{
	Task<IEnumerable<Customer>> GetCustomers();
	Task<Customer?> GetCustomerById(int Id);
	Task DeleteCustomer(int Id);
	Task<Customer> CreateCustomer(Customer customer);
	Task<Customer?> UpdateCustomer(Customer updatedCustomer);
}