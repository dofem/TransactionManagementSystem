using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Domain.Enums
{
    public enum TransactionStatus
    {
        SUCCESSFUL,
        PENDING,
        FAILED
    }

    public enum TransactionType
    {
        PURCHASE,
        REFUND,
        TRANSFER
    }
}
