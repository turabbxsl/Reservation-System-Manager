using FluentValidation;
using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errors = failures.Select(f => f.ErrorMessage).ToList();

                    if (typeof(TResponse).IsGenericType &&
                        typeof(TResponse).GetGenericTypeDefinition() == typeof(ResponseDto<>))
                    {
                        var genericType = typeof(ResponseDto<>).MakeGenericType(typeof(TResponse).GetGenericArguments()[0]);
                        var method = genericType.GetMethod(nameof(ResponseDto<object>.ErrorResponse), new[] { typeof(List<string>), typeof(int) });

                        return (TResponse)method.Invoke(null, new object[] { errors, 200 });
                    }

                    return default;
                }
            }

            return await next();
        }
    }
}
