using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Pipelines
{
    public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

        public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string requestName = typeof(TRequest).Name;            

            _logger.LogInformation("Executing {0}", requestName);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();

            _logger.LogInformation("Executed {0} in {1} ms", requestName, timer.ElapsedMilliseconds);

            return response;
        }
    }
}
