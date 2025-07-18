using Reservation.Shared.BaseResponse;
using System.Text.Json;

namespace Reservation.API.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                ResponseDto<string> response;
                int statusCode;

                switch (ex)
                {
                    case Reservation.Domain.Exceptions.Company.EmailAlreadyExistsException:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = ResponseDto<string>.ErrorResponse(ex.Message, statusCode);
                        break;
                    case Reservation.Domain.Exceptions.Customer.EmailAlreadyExistsException:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = ResponseDto<string>.ErrorResponse(ex.Message, statusCode);
                        break;
                    case Reservation.Domain.Exceptions.Customer.PhoneNumberAlreadyExistsException:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = ResponseDto<string>.ErrorResponse(ex.Message, statusCode);
                        break;
                    default:
                        statusCode = StatusCodes.Status200OK;
                        response = ResponseDto<string>.ErrorResponse(new List<string>() { ex.Message, "Gözlənilməz xəta baş verdi" }, statusCode);
                        break;
                }

                context.Response.StatusCode = 200;
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

    }
}
