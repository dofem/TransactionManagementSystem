using FurtherProcessingActionService.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProcessPurchase.Infrastructure;

namespace FurtherProcessingActionService.Services
{
    public class CustomerProfileService : CustomerProfilingService.CustomerProfilingServiceBase
    {
        private readonly ILogger<CustomerProfileService> _logger;
        private readonly ApplicationDbContext _context;

        public CustomerProfileService(ILogger<CustomerProfileService> logger,ApplicationDbContext Context)
        {
            _logger = logger;
            _context = Context;
        }

        public override async Task<CustomerProfileResponse> GetCustomerProfile(CustomerProfileRequest request, ServerCallContext context)
        {
            var customer = await _context.CustomerInformation
                .Where(c => c.Id == request.CustomerId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Customer not found."));
            }

            int numberOfPurchases = customer.NumberOfPurchase;
            decimal totalPurchase = customer.TotalPurchaseAmount;

            decimal averagePurchaseAmount = totalPurchase / numberOfPurchases;

            string customerCategory;
            if (averagePurchaseAmount > 1000000)
            {
                customerCategory = "Premium Customer";
            }
            else if (averagePurchaseAmount >= 500000)
            {
                customerCategory = "Gold Customer";
            }
            else if (averagePurchaseAmount >= 100000)
            {
                customerCategory = "Silver Customer";
            }
            else
            {
                customerCategory = "Normal Customer";
            }

            var response = new CustomerProfileResponse
            {
                CustomerId = request.CustomerId,
                CustomerClass = customerCategory,
                AveragePurchaseAmount = (double)averagePurchaseAmount
            };

            return response;
        }


    }
}
