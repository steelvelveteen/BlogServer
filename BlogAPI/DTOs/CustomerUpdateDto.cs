using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs;

public class CustomerUpdateDto
{
	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;
	public string? Address { get; set; }
	public string? Phone { get; set; }
}