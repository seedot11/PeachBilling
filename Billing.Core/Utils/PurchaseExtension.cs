using Billing.Core.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Billing.Core.Utils
{
    public static class BillingExtension
    {
        public static IEnumerable<PurchaseViewModel> ToPurchaseViewModel(this IEnumerable<Purchase> purchases)
        {
            var output = new List<PurchaseViewModel>();
            foreach (var purchase in purchases)
            {
                output.Add(new PurchaseViewModel
                {
                    Discount = purchase.Discount,
                    SKU = purchase.Product.SKU,
                    FinalPrice = purchase.FinalPrice,
                    OriginalPrice = purchase.Product.Price
                });
            }
            return output;
        }

        public static BasketViewModel ToBasketViewModel(this IEnumerable<Purchase> purchases)
        {
            return new BasketViewModel
            {
                Total = purchases.Select(x => x.FinalPrice).Sum(),
                Quantity = purchases.Count(),
                Skus = purchases.Select(x => x.Product.SKU).ToList(),
                TotalDiscount = purchases.Select(x => x.Discount).Sum() * 10
            };
        }

        public static IEnumerable<ProductViewModel> ToProductViewModel(this IEnumerable<Product> products)
        {
            var output = new List<ProductViewModel>();
            foreach (var product in products)
            {
                output.Add(new ProductViewModel
                {
                    Name = product.Name,
                    SKU = product.SKU,
                    Price = product.Price
                });
            }
            return output;
        }
    }
}
