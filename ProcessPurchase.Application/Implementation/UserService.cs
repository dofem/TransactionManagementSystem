using ProcessPurchase.Application.AppManager;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Request;
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
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<CreateUserResponse> AddNewCustomer(CreateUser createUser)
        {
            var response = new CreateUserResponse();
            var user = _context.CustomerInformation.Where(c=>c.UserName.ToUpper() == createUser.UserName.ToLower()).FirstOrDefault();
            
            if (user != null) 
            {
                response.status = false; 
                response.message = $"{user.UserName} already exist as a user";
            }
            else
            {
                CustomerInformation customerInformation = new CustomerInformation();
                customerInformation.UserName = createUser.UserName;
                customerInformation.Email = createUser.Email;
                customerInformation.walletId = ApplicationManager.GenerateWalletId();
                customerInformation.Balance = 0;

                ApplicationManager.HashPassword(createUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                customerInformation.PasswordHash = passwordHash;
                customerInformation.PasswordSalt = passwordSalt;

                // Add the new customer to the database
                _context.CustomerInformation.Add(customerInformation);
                await _context.SaveChangesAsync();

                response.status = true; 
                response.message= "User created successfully";
            }
            return response;
        }

        public async Task<ProductResponse> TopUpWallet(TopUp topUpUser)
        {
            var response = new ProductResponse();
            var customer = _context.CustomerInformation.Where(c=>c.walletId == topUpUser.WalletId).FirstOrDefault();
            if(customer != null)
            {
                if (!ApplicationManager.VerifyPassword(topUpUser.Password, customer.PasswordHash, customer.PasswordSalt))
                {
                    response.status = false; 
                    response.message = "Incorrect walletId or password.";
                }
                else
                {
                    customer.Balance += topUpUser.Amount;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                response.status = false; 
                response.message = "Customer doesnt exist";
            }
            return response;
        }
    }
}
