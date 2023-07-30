using ProcessPurchase.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProcessPurchase.Domain.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionReference { get; set; }
        public TransactionStatus TransactionStatus { get; set; } //pending,completed,processing
        public string ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public TransactionType TransactionType { get; set; } // purchase,refund,transfer
        public string WalletId { get; set; }
    }
}
