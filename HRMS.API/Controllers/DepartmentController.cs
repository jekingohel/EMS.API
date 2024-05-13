using HRMS.Application.Services.Interfaces;
using HRMS.Domain.DTO;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DepartmentController(IDepartmentService departmentService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		var data = await departmentService.GetAllAsync();
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpGet]
	public async Task<ActionResult> GetByIdAsync(int id)
	{
		var data = await departmentService.GetByIdAsync(id);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync(Department department)
	{
		var data = await departmentService.AddAsync(department);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpPost]
	public async Task<IActionResult> UpdateAsync(Department department)
	{
		await departmentService.UpdateAsync(department);
		return Ok(new APIResponseDTO(true, null));
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		await departmentService.DeleteAsync(id);
		return Ok(new APIResponseDTO(true, null));
	}

	[HttpGet]
	public async Task<IActionResult> GetDepartmentsByCompanyId(int companyId)
	{
		var data = await departmentService.GetDepartmentsByCompanyIdAsync(companyId);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllDepartmentsByCompanyId(int companyId)
	{
		var data = await departmentService.GetAllDepartmentsByCompanyIdAsync(companyId);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpPost]
	public async Task<IActionResult> MapCompanyDepartmentsAsync(CompanyDepartmentInfo companyDepartmentInfo)
	{
		await departmentService.MapCompanyDepartmentsAsync(companyDepartmentInfo);
		return Ok(new APIResponseDTO(true, null));
	}
}
