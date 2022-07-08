using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs;

public class CustomerCreateDto
{
	[Required]
	public int Id { get; set; }
	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;
	public string? Address { get; set; }
	public string? Phone { get; set; }
}