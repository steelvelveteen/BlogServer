using Moq;
using BlogAPI.Repository;
using AutoMapper;
using BlogAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Models;
using BlogAPI.Data;
using FluentAssertions;
using BlogAPI.DTOs;

namespace BlogAPI.Tests;

public class CustomerControllerTests
{
	private static IMapper _mapper = null!;
	private readonly Mock<ICustomerRepository> repositoryStub = new();
	public CustomerControllerTests()
	{
		if (_mapper == null)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperProfile());
			});
			IMapper mapper = mappingConfig.CreateMapper();
			_mapper = mapper;
		}
	}

	[Fact]
	public async Task GetCustomerById_WithExistingCustomer_ReturnsOk()
	{
		// Arrange
		var expectedCustomer = new Customer
		{
			Id = 1,
			FirstName = "Test First Name",
			LastName = "Test Last Name",
			Address = "",
			Phone = "8994545112"
		};

		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<Int32>()))
		.ReturnsAsync(expectedCustomer);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.GetCustomerById(1);

		result.Value.Should().BeEquivalentTo(expectedCustomer, options => options.ComparingByMembers<Customer>());
	}

	[Fact]
	public async Task GetCustomerById_WithUnexistingCustomerId_ReturnsNotFound()
	{
		// Arrange
		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<int>()))
		.ReturnsAsync((Customer?)null);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.GetCustomerById(9999);

		// Assert
		result.Value.Should().BeNull();
		result.Result.Should().BeOfType<NotFoundObjectResult>();
	}


	[Fact]
	public async Task DeleteCustomer_WithExistingId_NoContent()
	{
		// Arrange
		var customerToDelete = new Customer
		{
			Id = 188,
			FirstName = "Test First Name",
			LastName = "Test Last Name",
			Address = "",
			Phone = "8994545112"
		};

		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<Int32>()))
		.ReturnsAsync(customerToDelete);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.Delete(customerToDelete.Id);

		// Assert
		result.Should().BeOfType<NoContentResult>();
	}

	[Fact]
	public async Task GetAllCustomers_WithExistingCustomers_ReturnsAllCustomers()
	{
		// Arrange
		var customers = new List<Customer>
		{
			new Customer
			{
				Id = 1,
				FirstName = "First name 1",
				LastName = "Last name 1"
			},
			new Customer
			{
				Id = 2,
				FirstName = "First name 2",
				LastName = "Last name 2"
			},
			new Customer
			{
				Id = 3,
				FirstName = "First name 3",
				LastName = "Last name 3"
			}
		};

		repositoryStub.Setup(repo => repo.GetCustomers()).ReturnsAsync(customers);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.Get();

		// Assert
		result.Value.Should().BeEquivalentTo(customers, options => options.ComparingByMembers<Customer>());
	}

	[Fact]
	public async Task CreateCustomer_WithCustomerToCreate_Returns()
	{
		// Arrange
		var newCustomer = new CustomerCreateDto
		{
			FirstName = "New First Name",
			LastName = "New Last Name",
			Address = null,
			Phone = "898908900"
		};
		var c = new Customer
		{
			Id = 66,
			FirstName = "New First Name",
			LastName = "New Last Name",
			Address = null,
			Phone = "898908900"

		};

		repositoryStub.Setup(repo => repo.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(c);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.Post(newCustomer);

		// Assert
		var objectResult = result.Result as CreatedAtRouteResult;
		var createdItem = objectResult?.Value as CustomerReadDto;

		newCustomer.Should().BeEquivalentTo(createdItem,
		options => options.ComparingByMembers<Customer>().ExcludingMissingMembers());

		if (createdItem is not null)
			createdItem.Id.Should().NotBe(null);
	}

	[Fact]
	public async Task UpdateCustomer_WithExistingCustomer_ReturnsNoContent()
	{
		// Arrange
		// First fetch existing customer
		var existingCustomer = new Customer
		{
			Id = 1,
			FirstName = "Test First Name",
			LastName = "Test Last Name",
			Address = "",
			Phone = "8994545112"
		};

		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<Int32>()))
		.ReturnsAsync(existingCustomer);

		var customerId = existingCustomer.Id;
		var customerToUpdate = new CustomerUpdateDto
		{
			FirstName = "Name updated",
			LastName = "Lastname updated",
			Address = "Random address",
			Phone = "8994545112"
		};

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.Put(customerId, customerToUpdate);

		// Assert
		result.Should().BeOfType<NoContentResult>();
	}
}
