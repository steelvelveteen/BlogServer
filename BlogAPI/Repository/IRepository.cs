
using BlogAPI.DTOs;
using BlogAPI.Models;

public interface IRepository
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<CustomerReadDto> GetCustomerById(int Id);
    void DeleteCustomer(int Id);
    Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto);

}