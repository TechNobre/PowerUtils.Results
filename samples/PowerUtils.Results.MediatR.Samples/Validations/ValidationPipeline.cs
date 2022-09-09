using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PowerUtils.Results.MediatR.Samples.Validations;

public interface IValidation
{
    List<IError> Validate();
}

/*
 * With
 *   where TRequest : IRequest<TResponse>, IValidation
 *   where TResponse : IResult
 * 
 * you guarantee that validable requests with response IResult invoke the pipeline
 */

internal sealed class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

        return (dynamic)errors;
    }
}
