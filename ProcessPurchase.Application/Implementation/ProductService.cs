using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Request;
using ProcessPurchase.Application.Response;
using ProcessPurchase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<ProductResponse> AddAProduct(AddProduct addProduct)
        {
            var response = new ProductResponse();
          var product = _context.Products.Where(u => u.Name.ToUpper() == addProduct.Name.ToUpper()).FirstOrDefault();
        if(product != null) 
        {
                response.status = false; 
                response.message = $"{product.Name} is already part of our collection";
        }
        else
        {
            product.Name = addProduct.Name;
            product.Price = addProduct.Price;
            product.CategoryId = addProduct.CategoryId;
            product.QuantityAvailable = 0;
        }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            response.status = true; 
            response.message = $"{product.Name} added successfully";
            return response;
        }



        public async Task<ProductResponse> AddProductQuantity(int quantity, int id)
        {
            var response = new ProductResponse();
            var product = _context.Products.Where(u => u.Id == id).FirstOrDefault();
            if(product == null) 
            {
                return new ProductResponse { status = false , message = $"Product with id {id} doesnt exist in our collection" };
            }
            else
            {
                product.QuantityAvailable += quantity;
            }
            await _context.SaveChangesAsync();
            response.status = true;
            response.message = $"{quantity} quantity of {product.Name} added successfully";
            return response;
        }
    }
}
