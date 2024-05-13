using HRMS.Application.Services.Interfaces;
using HRMS.Domain.DTO;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		var data = await companyService.GetAllAsync();
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpGet]
	public async Task<ActionResult> GetByIdAsync(int id)
	{
		var data = await companyService.GetByIdAsync(id);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync(Company company)
	{
		var data = await companyService.AddAsync(company);
		return Ok(new APIResponseDTO(true, null, data));
	}

	[HttpPost]
	public async Task<IActionResult> UpdateAsync(Company company)
	{
		await companyService.UpdateAsync(company);
		return Ok(new APIResponseDTO(true, null));
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		await companyService.DeleteAsync(id);
		return Ok(new APIResponseDTO(true, null));
	}
}
