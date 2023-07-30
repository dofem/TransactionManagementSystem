using ProcessPurchase.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Contract
{
    public interface ICategoryService
    {
        Task<CreateCategoryResponse> CreateACategory(int id,string categoryName);
    }
}
