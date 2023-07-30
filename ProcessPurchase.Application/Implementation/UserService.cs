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
            var user = _context.CustomerInformation.Where(c=>c.UserName.ToUpper() == createUser.UserName.ToLower()).FirstOrDefault();
            
            if (user != null) 
            {
                return new CreateUserResponse { status = false, message = $"{user.UserName} already exist as a user" };
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

                return new CreateUserResponse { status = true, message = "User created successfully" };
            }
        }
    }
}
