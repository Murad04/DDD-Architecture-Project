using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class RequestLogger<TRequest>:IRequestPreProcessor<TRequest>
    {
        private ILogger _logger;
        private ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userID = _currentUserService.UserID;

            _logger.LogInformation("Request: {Name}, UserID{@userID},{@request}", requestName, userID, request);

            await Task.CompletedTask;
        }
    }
}
