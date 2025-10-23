using FluentValidation;
using Grpc.Core;
using MediatR;
using SixLabors.ImageSharp.ColorSpaces;
using System;

namespace Scharff.API.Utils.PipelineBehaviour
{
    public class ValidationBehaviour<TRequest, TResponse> :
                 IPipelineBehavior<TRequest, TResponse>
                 where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors)
                                                .Where(f => f != null)
                                                .Select(f => f.ErrorMessage) 
                                                .ToList();

                if (failures.Count != 0)
                {
                    var errorMessage = string.Join(", ", failures);
                    throw new ValidationException(errorMessage);
                }
            }
            return await next();
        }

    }
}
