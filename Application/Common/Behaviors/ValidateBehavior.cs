using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidateBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
: IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
      public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
      {
            if (validators.Any())
            {
                  var context = new ValidationContext<TRequest>(request);

                  var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                  var errors = validationResults
                        .SelectMany(r => r.Errors)
                        .Where(f => f != null)
                        .GroupBy(f => f.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

                  if (errors.Count != 0)
                  {
                        throw new Domain.Exceptions.ValidationException(errors);
                  }
            }

            return await next();
      }
}

