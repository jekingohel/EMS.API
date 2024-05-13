using HRMS.Domain.Models;

namespace HRMS.Application.Services.Interfaces;

public interface IDepartmentService
{
	Task<int> AddAsync(Department department);
	Task DeleteAsync(int id);
	Task<List<Department>> GetAllAsync();
	Task<Department> GetByIdAsync(int id);
	Task UpdateAsync(Department department);
	Task<List<Department>> GetDepartmentsByCompanyIdAsync(int id);
	Task<CompanyDepartmentInfo> GetAllDepartmentsByCompanyIdAsync(int companyId);
	Task MapCompanyDepartmentsAsync(CompanyDepartmentInfo companyDepartmentInfo);
}