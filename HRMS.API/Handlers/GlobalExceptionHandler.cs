using HRMS.Domain.DTO;
using Microsoft.AspNetCore.Diagnostics;

namespace HRMS.API.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError(exception, $"Exception occured: {exception.Message}");
		//var problemDetails = new APIResponseDTO(false, exception.Message, exception.StackTrace);
		var problemDetails = new APIResponseDTO(false, exception.Message, "Something went wrong");
		httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
		return true;
	}
}
