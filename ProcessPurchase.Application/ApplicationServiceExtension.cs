using Microsoft.Extensions.DependencyInjection;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPurchaseAProduct, PurchaseProduct>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
