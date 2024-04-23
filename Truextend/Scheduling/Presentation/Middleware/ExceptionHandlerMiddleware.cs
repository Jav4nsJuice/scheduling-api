using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Truextend.Scheduling.Data.Exceptions;
using Truextend.Scheduling.Logic.Exceptions;

namespace Truextend.Scheduling.Presentation.Middleware
{
	public class ExceptionHandlerMiddleware
	{
        private const string _jsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
		{
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var ErrorResponse = new MiddlewareResponse<string>(null);
            if (ex is LogicException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.UnprocessableEntity;
                ErrorResponse.error.Message = "Logic Exception" + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine;
            }
            else if (ex is DatabaseException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.InternalServerError;
                ErrorResponse.error.Message = $"Data Error [Database] {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
            }
            else if (ex is BadRequestException exception)
            {
                ErrorResponse.Status = (int)HttpStatusCode.BadRequest;
                ErrorResponse.error.Message = $"Data Error [Bad Request]{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                if (exception.Details != null)
                {
                    ErrorResponse.error.Details = exception.Details;
                }
            }
            else if (ex is NotFoundException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.NotFound;
                ErrorResponse.error.Message = $"Data Error [Not Found]{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
            }
            else if (ex is AlreadyExistException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.Conflict;
                ErrorResponse.error.Message = $"Data Error [Already Exists]{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
            }
            else
            {
                ErrorResponse.Status = (int)HttpStatusCode.InternalServerError;
                ErrorResponse.error.Message = "Internal Server Error: " + ex.Message;
            }
            context.Response.ContentType = _jsonContentType;
            context.Response.StatusCode = ErrorResponse.Status;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponse));
        }
    }
}

