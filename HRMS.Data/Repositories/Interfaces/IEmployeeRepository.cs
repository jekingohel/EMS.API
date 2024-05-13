using HRMS.Domain.Models;

namespace HRMS.Data.Repositories.Interfaces;

public interface IEmployeeRepository
{
	Task<List<EmployeeInfo>> GetAllAsync();
	Task<int> AddAsync(Employee employee);
	Task UpdateAsync(Employee employee);
	Task<Employee> GetByIdAsync(int id);
	Task DeleteAsync(int id);
	Task<(List<Company> companies, List<Department> departments, List<CompanyDepartments> companyDepartments)> GetCompaniesWithDepartmentsAsync();
}