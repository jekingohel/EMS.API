using HRMS.Data.Context;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Exceptions;
using HRMS.Domain.Models;
using System.Data;
using System.Net;

namespace HRMS.Data.Repositories;

public class DepartmentRepository(DbContext context) : IDepartmentRepository
{
	public async Task<List<Department>> GetAllAsync()
	{
		List<Department> departments = (await context.CallStoreProcedureDirectly<Department>("uspGetAllDepartment")).ToList();
		return departments;
	}

	public async Task<Department> GetByIdAsync(int id)
	{
		var data = (await context.CallStoreProcedureDirectly<Department>("uspGetDepartmentById", new { id })).FirstOrDefault();
		return data;
	}

	public async Task<int> AddAsync(Department department)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Department] WHERE Name = '{department.Name}'")).First();
		if (count == 1) throw new CustomException($"Department is already exists", HttpStatusCode.Conflict);

		int id = (await context.CallStoreProcedureDirectly<int>("uspInsDepartment", new { department.Name })).First();
		return id;
	}

	public async Task UpdateAsync(Department department)
	{
		int count = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Department] WHERE Id = {department.Id}")).First();
		if (count == 0) throw new CustomException($"Department not found with id {department.Id}", HttpStatusCode.NotFound);

		int count_duplicate = (await context.ExecuteQuery<int>($"SELECT COUNT(*) FROM [dbo].[Department] WHERE Name = '{department.Name}' and Id != {department.Id}")).First();
		if (count_duplicate == 1) throw new CustomException($"Department is already exists", HttpStatusCode.Conflict);

		await context.CallStoreProcedureDirectly<int>("uspUpdDepartment", new { department.Id, department.Name });
	}

	public async Task DeleteAsync(int id)
	{
		int count_employee = (await context.ExecuteQuery<int>($"SELECT COUNT(*) from [dbo].[Employee] WHERE DepartmentId = {id}")).First();
		if(count_employee >= 1) throw new CustomException($"Can't delete, Department is Linked with Employee");
		
		int count_company_department = (await context.ExecuteQuery<int>($"SELECT COUNT(*) from [dbo].[CompanyDepartment] WHERE DepartmentId = {id}")).First();
		if (count_company_department >= 1) throw new CustomException($"Can't delete, Department is Linked with Company");

		await context.CallStoreProcedureDirectly<int>("uspDelDepartment", new { id });
	}

	public async Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId)
	{
		List<Department> departments = (await context.CallStoreProcedureDirectly<Department>("uspGetDepartmentsByCompanyId", new { companyId })).ToList();
		return departments;
	}

	public async Task<List<CompanyDepartments>> GetAllDepartmentsByCompanyId(int companyId)
	{
		List<CompanyDepartments> companyDepartments = (await context.CallStoreProcedureDirectly<CompanyDepartments>("uspGetAllDepartmentsByCompanyId", new { companyId })).ToList();
		return companyDepartments;
	}

	public async Task MapCompanyDepartmentsAsync(int companyId, List<int> deparmentsToRemove, List<int> deparmentsToAdd)
	{
		foreach (var item in deparmentsToRemove)
		{
			int count_employee = (await context.ExecuteQuery<int>($"SELECT COUNT(*) from [dbo].[Employee] WHERE CompanyId = {companyId} AND DepartmentId = {Convert.ToInt32(item)}")).First();
			if (count_employee >= 1) throw new CustomException($"Can't remove, Department id {item} is Linked with Employee");
		}

		var departmentsToRemove = new DataTable();
		var departmentsToAdd = new DataTable();

		departmentsToRemove.Columns.Add(new DataColumn("Id", typeof(int)));
		departmentsToAdd.Columns.Add(new DataColumn("Id", typeof(int)));

		foreach (var id in deparmentsToRemove) departmentsToRemove.Rows.Add(id);
		foreach (var id in deparmentsToAdd) departmentsToAdd.Rows.Add(id);

		await context.CallStoreProcedureDirectly<int>("uspUpdCompanyDepartments", new { companyId, departmentsToRemove, departmentsToAdd });
	}
}