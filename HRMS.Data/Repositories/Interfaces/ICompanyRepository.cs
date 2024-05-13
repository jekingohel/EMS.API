using HRMS.Domain.Models;

namespace HRMS.Data.Repositories.Interfaces;

public interface ICompanyRepository
{
	Task<List<CompanyInfo>> GetAllAsync();
	Task<Company> GetByIdAsync(int id);
	Task<int> AddAsync(Company company);
	Task UpdateAsync(Company company);
	Task DeleteAsync(int id);
}