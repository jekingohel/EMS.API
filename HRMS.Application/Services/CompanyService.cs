using HRMS.Application.Services.Interfaces;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Exceptions;
using HRMS.Domain.Models;

namespace HRMS.Application.Services;

public class CompanyService(
	ICompanyRepository companyRepository,
	IDepartmentService departmentService
	) : ICompanyService
{
	public async Task<List<CompanyInfo>> GetAllAsync() => await companyRepository.GetAllAsync();

	public async Task<Company> GetByIdAsync(int id) => await companyRepository.GetByIdAsync(id);

	public async Task<int> AddAsync(Company company) => await companyRepository.AddAsync(company);

	public async Task UpdateAsync(Company company) => await companyRepository.UpdateAsync(company);

	public async Task DeleteAsync(int id)
	{
		var companyDepartments = await departmentService.GetAllDepartmentsByCompanyIdAsync(id);

		if (companyDepartments.Departments.Any(x => x.IsLinked))
			throw new CustomException($"Can't delete company id {id}, Company is Linked with Department");

		await companyRepository.DeleteAsync(id);
	}
}
