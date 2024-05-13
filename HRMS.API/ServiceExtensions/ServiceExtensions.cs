using HRMS.Application.Services;
using HRMS.Application.Services.Interfaces;
using HRMS.Data.Context;
using HRMS.Data.Repositories;
using HRMS.Data.Repositories.Interfaces;

namespace HRMS.API.ServiceExtensions;

public static class ServiceExtensions
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddScoped<DbContext>();

		#region Services
		services.AddScoped<ICompanyService, CompanyService>();
		services.AddScoped<IDepartmentService, DepartmentService>();
		services.AddScoped<IEmployeeService, EmployeeService>();
		#endregion Services

		#region Repositories
		services.AddScoped<ICompanyRepository, CompanyRepository>();
		services.AddScoped<IDepartmentRepository, DepartmentRepository>();
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		#endregion Repositories
	}
}
