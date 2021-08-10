// <copyright file="ValidatorBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ProductMaintenanceService.Common.Exceptions;

namespace TweetApplication.Behaviors
{
    /// <summary>
    /// Validator Behavior
    /// </summary>
    /// <typeparam name="TRequest">TRequest values</typeparam>
    /// <typeparam name="TResponse">TResponse values</typeparam>
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The validators.
        /// </summary>
        private readonly IValidator<TRequest>[] validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">validators value</param>
        public ValidatorBehavior(IValidator<TRequest>[] validators) => this.validators = validators;

        /// <summary>
        /// Handle response
        /// </summary>
        /// <param name="request">request value</param>
        /// <param name="cancellationToken">cancellation Token value</param>
        /// <param name="next">TResponse value</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = this.validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                throw new TweetApplicationException(
                    $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}
