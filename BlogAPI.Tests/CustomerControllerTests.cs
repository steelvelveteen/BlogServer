using Moq;
using BlogAPI.Repository;
using AutoMapper;
using BlogAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Models;
using BlogAPI.Data;
using FluentAssertions;

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
		// var okObjectResult = result.Result as OkObjectResult;
		// var actual = okObjectResult?.Value;

		// Assert
		// Installed FluentAssertions v6.7.0

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
}