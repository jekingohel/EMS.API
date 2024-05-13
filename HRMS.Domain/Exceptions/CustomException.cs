using System.Net;

namespace HRMS.Domain.Exceptions;

public class CustomException : Exception
{
	public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
		: base(message)
	{
	}
}
