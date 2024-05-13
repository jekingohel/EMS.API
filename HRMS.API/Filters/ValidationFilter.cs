using HRMS.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.API.FIlters;

public class ValidationFilter : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.ModelState.IsValid)
		{
			string messages = string.Join("; ", context.ModelState.Values
					 .SelectMany(x => x.Errors)
					 .Select(x => !string.IsNullOrWhiteSpace(x.ErrorMessage) ? x.ErrorMessage : x.Exception?.Message.ToString()));

			var problemDetails = new APIResponseDTO(false, "Model validation failed", messages);

			context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			await context.HttpContext.Response.WriteAsJsonAsync(problemDetails);

			return;
		}

		await next();
	}
}
