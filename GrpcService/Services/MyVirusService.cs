using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Logging;
using Microsoft.Extensions.Logging;
using Virus.Api;

namespace GrpcService.Services
{
    public class MyVirusService : Virus.Api.VirusService.VirusServiceBase
    {
        private readonly ILogger<MyVirusService> logger;

        public MyVirusService(ILogger<MyVirusService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddInfectedResponse> AddInfected(AddInfectedRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.Country} {request.Confirmed} {request.Latitude} {request.Longitude}");

            var response = new AddInfectedResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }
    }
}
