using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Request
{
    public class PurchaseProductRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string WalletId { get; set; } 
        public string Password { get; set; }
    }
}
