using Moq;
using BlogAPI.Repository;
using AutoMapper;
using BlogAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Models;
using BlogAPI.DTOs;
using System.Net;
using BlogAPI.Data;

namespace BlogAPI.Tests;

public class CustomerControllerTests
{
    // private readonly Mock<IMapper> mapperStub = new();
    private static IMapper _mapper;
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
	// Naming convention: unitOfWork_stateUnderTest_expectedBehaviour
	public async Task GetCustomerById_withUnexistingCustomerId_ReturnsNotFound()
	{
		// Arrange
		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<int>()))
		.ReturnsAsync((Customer?)null);

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.GetCustomerById(9999);

		// Assert
		Assert.IsType<NotFoundObjectResult>(result.Result);
	}

	[Fact]
	public async Task GetCustomerById_WithExistingCustomer_ReturnsOk()
	{
		// Arrange
		var expectedCustomer = new Customer
		{
			Id = 1,
			FirstName = "Test",
			LastName = "Last Test",
			Address = "",
			Phone = "8994545112"
		};

		repositoryStub
		.Setup(repo => repo.GetCustomerById(It.IsAny<Int32>()))
		.ReturnsAsync(new Customer {
			Id = 1,
			FirstName = "Test 1 First",
			LastName = "Test Last",
			Address = "",
			Phone = "89887983"
		});

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.GetCustomerById(1);
		var okObjResult = result.Result as OkObjectResult;

		// Assert
		Assert.IsType<CustomerReadDto>(result.Value);
	}

	[Fact]
	public async Task DeleteCustomer_WithUnexistingId_ReturnsNotFound()
	{
		// Arrange
		repositoryStub
		.Setup(repo => repo.DeleteCustomer(It.IsAny<Customer>()));

		var controller = new CustomerController(_mapper, repositoryStub.Object);

		// Act
		var result = await controller.Delete(188);

		// Assert
		Assert.IsType<NotFoundObjectResult>(result);

	}
}