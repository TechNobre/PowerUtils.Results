using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PowerUtils.Results.MediatR.Samples.Services;

public record PingCommand(string Message) : IRequest<string> //, IValidation
{
    /*
     * Just to show the importance of
     *   where TRequest : IRequest<TResponse>, IValidation
     *   where TResponse : IResult
     * in `ValidationPipeline<TRequest, TResponse>`
     * 
     */

    //public List<IError> Validate()
    //{
    //    if(string.IsNullOrWhiteSpace(Message))
    //    {
    //        throw new ArgumentException($"The {nameof(Message)} cannot be null or empty", nameof(Message));
    //    }
    //    return null;
    //}

    public class Handler(ILogger<Handler> logger) : IRequestHandler<PingCommand, string>
    {
        private readonly ILogger<Handler> _logger = logger;


        public Task<string> Handle(PingCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[PING COMMAND] {command.Message}");

            return Task.FromResult($"PONG: {command.Message}");
        }
    }
}
