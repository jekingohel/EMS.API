using HRMS.Domain.Models;

namespace HRMS.Application.Services.Interfaces;

public interface ICompanyService
{
	Task<List<CompanyInfo>> GetAllAsync();
	Task<Company> GetByIdAsync(int id);
	Task<int> AddAsync(Company company);
	Task UpdateAsync(Company company);
	Task DeleteAsync(int id);
}
