
using BlogAPI.DTOs;
using BlogAPI.Models;

public interface ICustomerRepository
{
	Task<IEnumerable<Customer>> GetCustomers();
	Task<Customer?> GetCustomerById(int Id);
	Task DeleteCustomer(Customer customer);
	Task<Customer> CreateCustomer(Customer customer);

	Task<Customer?> UpdateCustomer(CustomerUpdateDto customerUpdateDto);

}