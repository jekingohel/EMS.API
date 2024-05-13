using HRMS.Domain.Models;

namespace HRMS.Data.Repositories.Interfaces
{
	public interface IDepartmentRepository
	{
		Task<List<Department>> GetAllAsync();
		Task<Department> GetByIdAsync(int id);
		Task<int> AddAsync(Department department);
		Task DeleteAsync(int id);
		Task UpdateAsync(Department department);
		Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId);
		Task<List<CompanyDepartments>> GetAllDepartmentsByCompanyId(int companyId);
		Task MapCompanyDepartmentsAsync(int companyId, List<int> deparmentsToRemove, List<int> deparmentsToAdd);
	}
}