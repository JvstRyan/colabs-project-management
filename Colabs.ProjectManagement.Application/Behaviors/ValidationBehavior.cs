﻿using FluentValidation;
using MediatR;

namespace Colabs.ProjectManagement.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>  : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
            
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            
                var failures = validationResults
                    .Where(e => e.Errors != null)
                    .SelectMany(e => e.Errors)
                    .ToList();
            
                if (failures.Count > 0)
                    throw new ValidationException(failures);
                
            }
            return await next();
        }
    }
}
