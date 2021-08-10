using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TweetApplication.Models;

namespace TweetApplication
{
    public class HttpGlobalExceptionFilter<T> : IExceptionFilter, IFilterMetadata
    {
        /// <summary>
        /// On exception event
        /// </summary>
        /// <param name="context">Context  parameter value</param>
        public void OnException(ExceptionContext context)
        {
            IEnumerable<string> data = null;
            if (context.Exception.InnerException != null && context.Exception.InnerException.GetType() == typeof(ValidationException))
            {
                data = ((ValidationException)context.Exception.InnerException).Errors.Select(x => x.ErrorMessage).Distinct();
            }

            var json = new JsonErrorResponse
            {
                Messages = data != null && data.Any() ? data.ToList() : new List<string>() { context.Exception.Message },
            };

            if (context.Exception.GetType() == typeof(T))
            {
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                json.Messages = new[] { "An error occurred. Try it again." };
                json.DeveloperMessage = context.Exception.ToString();

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}