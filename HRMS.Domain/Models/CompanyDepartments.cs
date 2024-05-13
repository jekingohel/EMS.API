namespace HRMS.Domain.Models;

public class CompanyDepartments
{
	public int DepartmentId { get; set; }
	public int? CompanyId { get; set; }

	public string DepartmentName { get; set; }
}
