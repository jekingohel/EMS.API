using HRMS.Domain.Models;

namespace HRMS.Application.Services.Interfaces;

public interface IEmployeeService
{
	Task<List<EmployeeInfo>> GetAllAsync();
	Task<Employee> GetByIdAsync(int id);
	Task<int> AddAsync(Employee employee);
	Task UpdateAsync(Employee employee);
	Task DeleteAsync(int id);
	Task<(List<Company> companies, List<Department> departments, List<CompanyDepartments> companyDepartments)> GetCompanyDepartmentsAsync();
}

