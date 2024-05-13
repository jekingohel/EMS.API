using HRMS.Application.Services.Interfaces;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Models;

namespace HRMS.Application.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
	public async Task<List<EmployeeInfo>> GetAllAsync() => await employeeRepository.GetAllAsync();
	public async Task<Employee> GetByIdAsync(int id) => await employeeRepository.GetByIdAsync(id);
	public async Task<int> AddAsync(Employee employee) => await employeeRepository.AddAsync(employee);
	public async Task UpdateAsync(Employee employee) => await employeeRepository.UpdateAsync(employee);
	public async Task DeleteAsync(int id) => await employeeRepository.DeleteAsync(id);
	public async Task<(List<Company> companies, List<Department> departments, List<CompanyDepartments> companyDepartments)> GetCompanyDepartmentsAsync() => await employeeRepository.GetCompaniesWithDepartmentsAsync();
}

