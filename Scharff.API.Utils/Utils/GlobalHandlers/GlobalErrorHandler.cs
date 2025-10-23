using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Scharff.API.Utils.Utils.Models;
using Scharff.Domain.Utils.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Scharff.API.Utils.Utils.GlobalHandlers
{
    public class GlobalErrorHandler
    {
        private readonly ILogger<GlobalErrorHandler> _logger;
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error en el manejo global de errores: {ErrorMessage}", error.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                CustomResponse<string> errorResponse = new();

                switch (error)
                {
                    case BadRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Error?.Add(error.Message);
                        errorResponse.Message = error.Message;
                        break;
                    case System.ComponentModel.DataAnnotations.ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Error?.Add(error.Message);
                        errorResponse.Message = error.Message;
                        break;

                    case AccessDeniedException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Error?.Add("Usted no puede acceder a esta opción.");
                        break;
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        errorResponse.Error?.Add(error.Message);
                        errorResponse.Message = error.Message;
                        break;
                    case FluentValidation.ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Error?.Add(error.Message);
                        errorResponse.Message = error.Message;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Error?.Add("Ocurrio un error en el sistema");
                        break;
                }

                var result = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
