using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using Virus.Api;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Virus.Api.VirusService.VirusServiceClient(channel);

            AddInfectedRequest request = new AddInfectedRequest
            {
                Confirmed = 1,
                Country = "Poland",
                Latitude = 52.45f,
                Longitude = 28.43f
            };

            await client.AddInfectedAsync(request);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
