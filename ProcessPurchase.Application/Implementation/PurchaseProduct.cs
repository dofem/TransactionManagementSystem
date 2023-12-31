﻿using Microsoft.EntityFrameworkCore;
using ProcessPurchase.Application.AppManager;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Request;
using ProcessPurchase.Application.Response;
using ProcessPurchase.Domain.Enums;
using ProcessPurchase.Domain.Model;
using ProcessPurchase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Implementation
{
    public class PurchaseProduct : IPurchaseAProduct
    {
        private readonly ApplicationDbContext _context;

        public PurchaseProduct(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<ProductResponse> PurchaseAProduct(PurchaseProductRequest request)
        {
            var response = new ProductResponse();
            Product product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                response.status = false; 
                response.message = "Product not found.";
            }
            decimal amount = product.Price * request.Quantity;
            CustomerInformation customer = await _context.CustomerInformation.FirstOrDefaultAsync(c => c.walletId == request.WalletId);
            if (customer == null)
            {
                response.status = false; 
                response.message = "Customer not found.";
            }

            // Verify the customer's password using the stored hash and salt
            if (!ApplicationManager.VerifyPassword(request.Password, customer.PasswordHash, customer.PasswordSalt))
            {
                response.status = false; 
                response.message = "Incorrect walletId or password.";
            }

            // Check if the customer has enough balance for the purchase
            if (customer.Balance < amount)
            {
                response.status = false; 
                response.message = "Insufficient balance.";
            }

            Transaction newTransaction = new Transaction
            {
                TransactionReference = Guid.NewGuid().ToString(), // Generate a unique transaction reference
                TransactionStatus = TransactionStatus.PENDING,
                ProductId = request.ProductId,
                Amount = amount,
                TransactionTime = DateTime.UtcNow,
                TransactionType = TransactionType.PURCHASE,
                WalletId = customer.walletId // Assuming the WalletNumber is the unique identifier for the customer's wallet
            };

            // Check if the requested quantity is available in the product stock
            if (product.QuantityAvailable < request.Quantity)
            {
                response.status = false; 
                response.message = "Insufficient product quantity.";
            }

            // Perform the purchase and update the customer's balance and product quantity
            customer.Balance -= amount;
            product.QuantityAvailable -= request.Quantity;
            customer.NumberOfPurchase += 1;
            customer.TotalPurchaseAmount += amount;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            response.status = true; 
            response.message = "Purchase Initiated successful.";

            return response;
        }


    }
        
}
