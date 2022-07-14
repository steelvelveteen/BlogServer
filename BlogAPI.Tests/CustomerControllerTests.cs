using Moq;
using BlogAPI.Repository;
using AutoMapper;
using BlogAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Tests;

public class CustomerControllerTests
{
	[Fact]
	// Naming convention: unitOfWork_stateUnderTest_expectedBehaviour
	public async Task GetCustomerById_withUnexistingCustomerId_ReturnsNotFound()
	{
		// Arrange
		var repositoryStub = new Mock<ICustomerRepository>();
		repositoryStub.Setup(repo => repo.GetCustomerById(It.IsAny<int>()));

		var mapperStub = new Mock<IMapper>();

		var controller = new CustomerController(mapperStub.Object, repositoryStub.Object);

		// Act
		var result = await controller.GetCustomerById(9999);

		// Assert
		Assert.IsType<NotFoundObjectResult>(result.Result);
	}
}