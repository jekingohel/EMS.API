using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Models;

public class Company
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Company is required")]
	[MaxLength(100, ErrorMessage = "Company name must be less than 100 characters")]
	public string Name { get; set; }

	[MaxLength(255, ErrorMessage = "Company email must be less than 255 characters")]
	[EmailAddress(ErrorMessage = "Invalid company email address")]
	public string Email { get; set; }
}
