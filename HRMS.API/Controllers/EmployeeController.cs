using HRMS.Application.Services.Interfaces;
using HRMS.Domain.DTO;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController(IEmployeeService employeeService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await employeeService.GetAllAsync();
			return Ok(new APIResponseDTO(true, null, data));
		}

		[HttpGet]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var data = await employeeService.GetByIdAsync(id);
			return Ok(new APIResponseDTO(true, null, data));
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync(Employee employee)
		{
			var data = await employeeService.AddAsync(employee);
			return Ok(new APIResponseDTO(true, null, data));
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(Employee employee)
		{
			await employeeService.UpdateAsync(employee);
			return Ok(new APIResponseDTO(true, null));
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await employeeService.DeleteAsync(id);
			return Ok(new APIResponseDTO(true, null));
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyDepartments()
		{
			var data = await employeeService.GetCompanyDepartmentsAsync();
			return Ok(new APIResponseDTO(true, null, new { data.companies, data.departments, data.companyDepartments }));
		}
	}
}
