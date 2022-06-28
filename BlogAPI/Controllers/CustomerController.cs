using BlogServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controller;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;
	public CustomerController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	[Route("GetCustomers")]
	public async Task<ActionResult<IEnumerable<CustomerController>>> Get()
	{
		var result = await _dbContext.Customers.ToListAsync();
		return Ok(result);
	}
}