using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ArenaPro.Domain.Validations;
using ArenaPro.Application.Exceptions;

namespace ArenaPro.Api.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;   
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException exception)
        {
            _logger.LogError(exception.Message);
            var result = new ObjectResult(new { Errors = exception.Message })
            {
                StatusCode = 400
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
        else if (context.Exception is RepositoryException)
        {
            _logger.LogError(context.Exception.Message);
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = 400
            };
            context.ExceptionHandled = true;
        }
        else 
        {
            _logger.LogError(context.Exception.Message);
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            context.ExceptionHandled = false;
        }
    }
}