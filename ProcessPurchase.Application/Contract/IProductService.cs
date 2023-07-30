using ProcessPurchase.Application.Request;
using ProcessPurchase.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Contract
{
    public interface IProductService
    {
        Task<ProductResponse> AddAProduct(AddProduct addProduct);
        Task<ProductResponse> AddProductQuantity(int quantity, int id);
    }
}
