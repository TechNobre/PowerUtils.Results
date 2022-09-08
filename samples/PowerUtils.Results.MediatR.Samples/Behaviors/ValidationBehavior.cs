using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PowerUtils.Results.MediatR.Samples.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IValidation
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // Fast validations
        var errors = command.Validate();

        if(errors is null || !errors.Any())
        {
            return await next();
        }

        Result result = errors;
        return (dynamic)result;
    }
}
