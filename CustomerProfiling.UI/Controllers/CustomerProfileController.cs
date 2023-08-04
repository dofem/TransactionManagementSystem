using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CustomerProfiling.UI.Controllers
{
    public class CustomerProfileController : Controller
    {
        private readonly CustomerProfilingService.CustomerProfileService.CustomerProfileServiceClient _client;
        private readonly GprsSetting _options;

        public CustomerProfileController(IOptions<GprsSetting> options, CustomerProfiling..CustomerProfileServiceClient client)
        {
            _client = client;
            _options = options.Value;
        }
        public IActionResult Index(int customerId)
        {
            var request = new CustomerProfilingService.CustomerProfileRequest
            {
                CustomerId = customerId
            };

            try
            {
                var response = client.GetCustomerProfile(request);
                return View(response); // Pass the customer profile response to the view.
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
