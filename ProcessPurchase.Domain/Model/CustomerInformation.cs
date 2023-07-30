using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Domain.Model
{
    public class CustomerInformation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string walletId { get; set; }
        public decimal Balance { get; set; }
        public int NumberOfPurchase { get; set; }
        public decimal TotalPurchaseAmount { get; set; }
    }
}
