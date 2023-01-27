using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MockBank.Application.Common.Exceptions;
// using System.ComponentModel.DataAnnotations;

namespace MockBank.WebApi.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new
            {
                error = new
                {
                    code = "transitory_failure",
                    message =
                        "Bad Gateway: Sorry, due to technical difficulties we are unable to process your request at this time. Please try again later.",
                    tracking_code = Guid.NewGuid().ToString()
                }
            };


            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status502BadGateway
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "-------------https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails
            {
                Type = "-----https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.------",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
        // {
        //     "error": {
        //         "code": "already_activated",
        //         "message": "Card already activated"
        //     }
        // }
        // {
        //     "error": {
        //         "code": "unauthorized",
        //         "message": "Unauthorized to access this resource"
        //     }
        // }
        // {
        //     "error": {
        //         "code": "transitory_failure",
        //         "message": "Bad Gateway: Sorry, due to technical difficulties we are unable to process your request at this time. Please try again later.",
        //         "tracking_code": "299b11f6-ea48-49de-83a0-61ad46150d85"
        //     }
        // }
    }
}