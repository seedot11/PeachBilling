using System.Collections.Generic;

namespace Billing.Core.Models
{
    public class BasketViewModel
    {
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public int Quantity { get; set; }
        public List<string> Skus { get; set; }
    }
}
