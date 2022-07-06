using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models;

public class Customer
{
	public int Id { get; set; }

	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;
	public string? Address { get; set; }
	public string? Phone { get; set; }
}