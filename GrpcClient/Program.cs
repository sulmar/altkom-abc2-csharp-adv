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

            await Task.Delay(TimeSpan.FromSeconds(2));

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Virus.Api.VirusService.VirusServiceClient(channel);

            while (true)
            {
                AddInfectedRequest request = new AddInfectedRequest
                {
                    Confirmed = 1,
                    Country = "Poland",
                    Latitude = 52.45f,
                    Longitude = 28.43f
                };

                var response = await client.AddInfectedAsync(request);

                Console.WriteLine($"Is confirmed = {response.IsConfirmed}");

                // await Task.Delay(TimeSpan.FromSeconds(1));
            }

            

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
