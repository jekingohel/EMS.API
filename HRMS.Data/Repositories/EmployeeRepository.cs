using HRMS.Data.Context;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Exceptions;
using HRMS.Domain.Models;
using System.Net;

namespace HRMS.Data.Repositories;

public class EmployeeRepository(DbContext context) : IEmployeeRepository
{
	public async Task<List<EmployeeInfo>> GetAllAsync()
	{
		List<EmployeeInfo> employees = (await context.CallStoreProcedureDirectly<EmployeeInfo>("uspGetAllEmployee")).ToList();
		return employees;
	}

	public async Task<int> AddAsync(Employee employee)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Employee] WHERE Email = '{employee.Email}'")).First();
		if (count == 1) throw new CustomException($"Email is already exists", HttpStatusCode.Conflict);

		int id = (await context.CallStoreProcedureDirectly<int>("uspInsEmployee", new { employee.Firstname, employee.Middlename, employee.Lastname, employee.Email, employee.Phone, employee.CompanyId, employee.DepartmentId })).First();
		return id;
	}

	public async Task UpdateAsync(Employee employee)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Employee] WHERE Id = {employee.Id}")).First();
		if (count == 0) throw new CustomException($"Employee not found with id {employee.Id}", HttpStatusCode.NotFound);

		int count_duplicate = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Employee] WHERE Email = '{employee.Email}' and Id != {employee.Id}")).First();
		if (count_duplicate == 1) throw new CustomException($"Email is already exists", HttpStatusCode.Conflict);

		await context.CallStoreProcedureDirectly<int>("uspUpdEmployee", new { employee.Id, employee.Firstname, employee.Middlename, employee.Lastname, employee.Email, employee.Phone, employee.CompanyId, employee.DepartmentId });
	}

	public async Task<Employee> GetByIdAsync(int id)
	{
		var data = (await context.CallStoreProcedureDirectly<Employee>("uspGetEmployeeById", new { id })).FirstOrDefault();
		return data;
	}

	public async Task DeleteAsync(int id)
	{
		await context.CallStoreProcedureDirectly<Employee>("uspDelEmployee", new { id });
	}

	public async Task<(List<Company> companies, List<Department> departments, List<CompanyDepartments> companyDepartments)> GetCompaniesWithDepartmentsAsync()
	{
		List<Company> companies = (await context.ExecuteQuery<Company>("SELECT * FROM Company")).ToList();
		List<Department> departments = (await context.ExecuteQuery<Department>("SELECT * FROM Department")).ToList();
		List<CompanyDepartments> companyDepartments = (await context.ExecuteQuery<CompanyDepartments>("SELECT * FROM CompanyDepartment")).ToList();
		return (companies, departments, companyDepartments);
	}

}
