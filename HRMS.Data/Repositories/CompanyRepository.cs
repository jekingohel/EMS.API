using HRMS.Data.Context;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Exceptions;
using HRMS.Domain.Models;
using System.Net;

namespace HRMS.Data.Repositories;

public class CompanyRepository(DbContext context) : ICompanyRepository
{
	public async Task<List<CompanyInfo>> GetAllAsync()
	{
		List<CompanyInfo> companies = (await context.CallStoreProcedureDirectly<CompanyInfo>("uspGetAllCompany")).ToList();
		return companies;
	}

	public async Task<Company> GetByIdAsync(int id)
	{
		var data = (await context.CallStoreProcedureDirectly<Company>("uspGetCompanyById", new { id })).FirstOrDefault();
		return data;
	}

	public async Task<int> AddAsync(Company company)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Company] WHERE Email = '{company.Email}'")).First();
		if (count == 1) throw new CustomException($"Email is already exists", HttpStatusCode.Conflict);
		int id = (await context.CallStoreProcedureDirectly<int>("uspInsCompany", new { company.Name, company.Email })).First();
		return id;
	}

	public async Task UpdateAsync(Company company)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Company] WHERE Id = {company.Id}")).First();
		if (count == 0) throw new CustomException($"Company not found with id {company.Id}", HttpStatusCode.NotFound);

		int count_duplicate = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Company] WHERE Email = '{company.Email}' and Id != {company.Id}")).First();
		if (count_duplicate == 1) throw new CustomException($"Email is already exists", HttpStatusCode.Conflict);

		await context.CallStoreProcedureDirectly<int>("uspUpdCompany", new { company.Id, company.Name, company.Email });
	}

	public async Task DeleteAsync(int id)
	{
		await context.CallStoreProcedureDirectly<int>("uspDelCompany", new { id });
	}
}
