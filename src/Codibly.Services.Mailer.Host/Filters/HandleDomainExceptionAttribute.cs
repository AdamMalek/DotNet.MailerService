using System.Collections.Generic;
using Codibly.Services.Mailer.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Codibly.Services.Mailer.Host.Filters
{
    public class HandleDomainExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled && context.Exception is DomainException de)
            {
                if (de is InvalidEmailMessageException)
                {
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.Result = new BadRequestObjectResult(ApiErrorResponse.ForErrors(de.Message));
                }
            }
        }
    }

    public class ApiErrorResponse
    {
        [JsonProperty("errors")] public IEnumerable<string> Errors { get; protected set; }

        public static ApiErrorResponse ForErrors(params string[] errors)
        {
            return new ApiErrorResponse()
            {
                Errors = errors
            };
        }

        public static ApiErrorResponse ForErrors(IEnumerable<string> errors)
        {
            return new ApiErrorResponse()
            {
                Errors = errors
            };
        }
    }
}