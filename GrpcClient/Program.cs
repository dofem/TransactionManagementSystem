using System;
using Microsoft.Extensions.Configuration;
using Grpc.Net.Client;
using GrpcClient;
using GrpcClient.Protos;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var grpcSetting = configuration.GetSection("GprsSetting").Get<GrpcSetting>();

        var serviceUrl = grpcSetting.ServerUrl;

        //// Create the gRPC channel
        var channel = GrpcChannel.ForAddress(serviceUrl);

        //Create the gRPC client
        var client = new CustomerProfilingService.CustomerProfilingServiceClient(channel);


        Console.Write("Enter the Customer ID to check profiling status: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            var request = new CustomerProfileRequest
            {
                CustomerId = customerId
            };

            try
            {

                var response = client.GetCustomerProfile(request);
                if (response != null)
                {

                    Console.WriteLine($"Customer ID: {response.CustomerId}");
                    Console.WriteLine($"Customer Category: {response.CustomerClass}");
                    Console.WriteLine($"Average Purchase Overtime: {response.AveragePurchaseAmount}");
                }
                else
                {
                    Console.WriteLine("Customer not found or an error occurred.");
                }
            }
            catch (Grpc.Core.RpcException ex)
            {
                Console.WriteLine($"gRPC Error: {ex.Status.StatusCode} - {ex.Status.Detail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric Customer ID.");
        }
    }
}

