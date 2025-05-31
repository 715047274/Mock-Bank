using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MockBank.Application.Configurations.Common.Behaviours
{
    /*
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        // private readonly ICurrentUserService _currentUserService;
        // private readonly IIdentityService _identityService;
    
        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
           
        }
    
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
           _logger.LogInformation("Mock Architecture Request: {@Request}", requestName,request);
        }
        
    }
    */
    // TODO use the Mediator Pipeline 
    // public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    // {
    //     private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    //     public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    //     {
    //         _logger = logger;
    //     }
    //     public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //     {
    //         //Request
    //         _logger.LogInformation($"Handling {typeof(TRequest).Name}");
    //         Type myType = request.GetType();
    //         IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
    //         foreach (PropertyInfo prop in props)
    //         {
    //             object propValue = prop.GetValue(request, null);
    //             _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
    //         }
    //         var response = await next();
    //         //Response
    //         _logger.LogInformation($"Handled {typeof(TResponse).Name}");
    //         return response;
    //     }
    //}
    
}