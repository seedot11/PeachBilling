using System.Linq;
using System.Collections.Generic;
using Billing.Core.Models;

namespace Billing.Core.Services
{
    /// <summary>
    /// Add the Implementation of the Cost Calculation Service
    /// </summary>
    public class CostCalculationService : ICostCalculationService
    {
        private readonly BillingContext context;

        public CostCalculationService(BillingContext context)
        {
            this.context = context;
        }

        public IEnumerable<Purchase> CalculateCost(IEnumerable<string> productSkus)
        {
            if (productSkus == null || !productSkus.Any())
                return new List<Purchase>();
            
            var purchases = new List<Purchase>();
            var discountedPurchases = new List<Purchase>();

            foreach (var sku in productSkus)
            {
                var product = context.Products.First(p => p.SKU == sku);
                var discounts = Discounts(sku);
                if (discounts.Any())
                {
                    discountedPurchases.Add(new Purchase
                    {
                        Product = product,
                        PossibleDiscounts = discounts
                    });
                }
                else
                {
                    purchases.Add(new Purchase
                    {
                        Product = product
                    });
                }
            }

            ApplyDiscount(discountedPurchases);
            purchases.AddRange(discountedPurchases);
            return purchases;
        }

        private IEnumerable<Discount> Discounts(string sku)
        {
            return context.Discounts.Where(d => d.DiscountedProducts.Any(a => a.Product.SKU == sku)).ToList();
        }

        private void ApplyDiscount(IEnumerable<Purchase> discountedPurchases)
        {
            var discountedSkus = new List<string>();
            var maxDiscounted = 0;
            var uniqueDiscounted = discountedPurchases.Select(x => x.Product.SKU).Distinct().Count();
            foreach (var purchase in discountedPurchases)
            {
                // Don't discount duplicates.
                if (discountedSkus.Any(x => x == purchase.Product.SKU))
                    continue;

                // Get the most applicable discount to apply.
                var bestDiscount = purchase.PossibleDiscounts
                    .Where(d => d.MinProductsRequired <= uniqueDiscounted)
                    .OrderByDescending(o => o.MinProductsRequired).FirstOrDefault();

                // If there aren't enough for a discount, then we're done here.
                if (bestDiscount == null)
                    break;

                // Set the maximum number which can be discounted.
                if (maxDiscounted < bestDiscount.MinProductsRequired)
                    maxDiscounted = bestDiscount.MinProductsRequired;

                // Finally, set the discount as the best discount applicable.
                purchase.Discount = bestDiscount.Percent;

                // Record this SKU as a discounted one so we don't discount duplicates.
                discountedSkus.Add(purchase.Product.SKU);

                // If we've hit the maximum that can be discounted, then we're done.
                if (discountedSkus.Count() >= maxDiscounted)
                    break;
            }
        }
    }
}
