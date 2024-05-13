using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Models;

public class Employee
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Firstname is required")]
	public string Firstname { get; set; }

	public string? Middlename { get; set; }

	[Required(ErrorMessage = "Lastname is required")]
	public string Lastname { get; set; }

	[MaxLength(255, ErrorMessage = "Email must be less than 255 characters")]
	[EmailAddress(ErrorMessage = "Email is required")]
	public string Email { get; set; }

	public string Phone { get; set; }

	public int CompanyId { get; set; }

	public int DepartmentId { get; set; }
}

