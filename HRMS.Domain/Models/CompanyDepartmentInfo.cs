namespace HRMS.Domain.Models;

public class CompanyDepartmentInfo
{
	public int CompanyId { get; set; }
	public List<DeparmentInfo> Departments { get; set; } = [];
}

public class DeparmentInfo
{
	public int DepartmentId { get; set; }
	public string DepartmentName { get; set; } = null!;
	public bool IsLinked { get; set; }
}