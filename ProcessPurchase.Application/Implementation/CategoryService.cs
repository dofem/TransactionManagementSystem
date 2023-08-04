using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Response;
using ProcessPurchase.Domain.Model;
using ProcessPurchase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Implementation
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateCategoryResponse>  CreateACategory(int id, string categoryName)
        {
            var response = new CreateCategoryResponse();
            var category = _context.Categories.Where(c=>c.Id == id).FirstOrDefault();   
            if (category != null) 
            {
                response.status = false;
                response.message = $"Category with id {category.Id} already exist";
            }
            else 
            { 
                category.Id = id;
                category.CategoryName = categoryName;
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            response.status = true; 
            response.message = $"category Name {category.CategoryName} created successfully";
            return response;
        }
    }
}
