using MediatR;

namespace IncomeTaxCalculator.Server.Features.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // This is where you would implement validation
            // For example, if using FluentValidation:
            // var validators = _serviceProvider.GetServices<IValidator<TRequest>>().ToList();

            // if (validators.Any())
            // {
            //     var context = new ValidationContext<TRequest>(request);
            //     var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            //     var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            //     if (failures.Count != 0)
            //         throw new ValidationException(failures);
            // }

            _logger.LogInformation("Validation passed for {RequestType}", typeof(TRequest).Name);
            return await next();
        }
    }
}
