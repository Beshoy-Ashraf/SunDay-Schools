using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

// Logs the request name and duration only; request bodies may contain passwords or tokens.
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
: IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
      private const int SlowRequestThresholdMs = 500;

      public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
      {
            var requestName = typeof(TRequest).Name;
            logger.LogInformation("Handling {RequestName}", requestName);

            var stopwatch = Stopwatch.StartNew();
            try
            {
                  var response = await next();
                  stopwatch.Stop();

                  if (stopwatch.ElapsedMilliseconds > SlowRequestThresholdMs)
                        logger.LogWarning("Slow request {RequestName} took {ElapsedMs} ms", requestName, stopwatch.ElapsedMilliseconds);
                  else
                        logger.LogInformation("Handled {RequestName} in {ElapsedMs} ms", requestName, stopwatch.ElapsedMilliseconds);

                  return response;
            }
            catch
            {
                  // The exception itself is logged once by ExceptionHandlingMiddleware.
                  logger.LogInformation("{RequestName} failed after {ElapsedMs} ms", requestName, stopwatch.ElapsedMilliseconds);
                  throw;
            }
      }
}
