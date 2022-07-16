using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs;

public class CustomerCreateDto
{
	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;

	public string? Address { get; set; } = string.Empty;

	public string? Phone { get; set; } = string.Empty;
}