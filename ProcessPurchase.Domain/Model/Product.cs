using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Domain.Model
{
     public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
