using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Models;

public class Department
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Department is required")]
	[MaxLength(100, ErrorMessage = "Department name must be less than 100 characters")]
	public string Name { get; set; }
}
