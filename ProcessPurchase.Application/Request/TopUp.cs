using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.Request
{
    public class TopUp
    {
            public string WalletId { get; set; }
            public string Password { get; set; }
            public decimal Amount { get; set; }
        
    }
}
