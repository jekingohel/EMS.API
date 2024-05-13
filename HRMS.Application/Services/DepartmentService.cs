using HRMS.Application.Services.Interfaces;
using HRMS.Data.Repositories.Interfaces;
using HRMS.Domain.Exceptions;
using HRMS.Domain.Models;
using Microsoft.Identity.Client;

namespace HRMS.Application.Services;

public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
{
	public async Task<List<Department>> GetAllAsync() => await departmentRepository.GetAllAsync();

	public async Task<Department> GetByIdAsync(int id) => await departmentRepository.GetByIdAsync(id);

	public async Task<int> AddAsync(Department department) => await departmentRepository.AddAsync(department);

	public async Task UpdateAsync(Department department) => await departmentRepository.UpdateAsync(department);

	public async Task DeleteAsync(int id) => await departmentRepository.DeleteAsync(id);

	public async Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId) => await departmentRepository.GetDepartmentsByCompanyIdAsync(companyId);

	public async Task<CompanyDepartmentInfo> GetAllDepartmentsByCompanyIdAsync(int companyId)
	{
		List<CompanyDepartments> data = await departmentRepository.GetAllDepartmentsByCompanyId(companyId);
		CompanyDepartmentInfo companyDepartments = new()
		{
			CompanyId = companyId
		};
		foreach (CompanyDepartments info in data)
			companyDepartments.Departments.Add(new DeparmentInfo { DepartmentId = info.DepartmentId, DepartmentName = info.DepartmentName, IsLinked = info.CompanyId is not null });
		return companyDepartments;
	}

	public async Task MapCompanyDepartmentsAsync(CompanyDepartmentInfo companyDepartmentInfo)
	{
		var data = await GetAllDepartmentsByCompanyIdAsync(companyDepartmentInfo.CompanyId);
		List<int> deparmentsToRemove = [];
		List<int> deparmentsToAdd = [];

		foreach (var department in companyDepartmentInfo.Departments)
		{
			var departmentInfo = data.Departments.Single(x => x.DepartmentId == department.DepartmentId);
			if (departmentInfo.IsLinked != department.IsLinked)
			{
				if (department.IsLinked)
					deparmentsToAdd.Add(department.DepartmentId);
				else
					deparmentsToRemove.Add(department.DepartmentId);
			}
		}

		await departmentRepository.MapCompanyDepartmentsAsync(companyDepartmentInfo.CompanyId, deparmentsToRemove, deparmentsToAdd);
	}
}
