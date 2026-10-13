using FluentValidation;

namespace Application.Common.Validation;

public static class ValidationExtensions
{
      private const string UtcMessage = "{PropertyName} must be a UTC time (ending with 'Z').";

      public static IRuleBuilderOptions<T, DateTime> MustBeUtc<T>(this IRuleBuilder<T, DateTime> rule) =>
            rule.Must(d => d.Kind == DateTimeKind.Utc).WithMessage(UtcMessage);

      public static IRuleBuilderOptions<T, DateTime?> MustBeUtc<T>(this IRuleBuilder<T, DateTime?> rule) =>
            rule.Must(d => d is null || d.Value.Kind == DateTimeKind.Utc).WithMessage(UtcMessage);
}
