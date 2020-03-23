using Billing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Billing.Core.Database
{
    public class SampleDatabaseService : IDatabaseService
    {
        private BillingContext context;

        public SampleDatabaseService(BillingContext context)
        {
            this.context = context;
        }

        public void SetUpDatabase()
        {
            // Don't re-setup a database
            if (context.Products.Any())
                return;

            // Products 
            var p1 = new Product()
            {
                Id = 1,
                Name = "Harry Potter Book 1",
                Price = 10,
                SKU = "potter1"
            };
            var p2 = new Product()
            {
                Id = 2,
                Name = "Harry Potter Book 2",
                Price = 10,
                SKU = "potter2"
            };
            var p3 = new Product()
            {
                Id = 3,
                Name = "Harry Potter Book 3",
                Price = 10,
                SKU = "potter3"
            };
            var p4 = new Product()
            {
                Id = 4,
                Name = "Harry Potter Book 4",
                Price = 10,
                SKU = "potter4"
            };
            var p5 = new Product()
            {
                Id = 5,
                Name = "Harry Potter Book 5",
                Price = 10,
                SKU = "potter5"
            };
            var p6 = new Product()
            {
                Id = 6,
                Name = "Harry Potter Book 6",
                Price = 10,
                SKU = "potter6"
            };
            var p7 = new Product()
            {
                Id = 7,
                Name = "Harry Potter Book 7",
                Price = 10,
                SKU = "potter7"
            };            

            var potterBooks = new List<Product> { p1, p2, p3, p4, p5, p6, p7 };
            
            context.AddRange(potterBooks);

            var p8 = new Product()
            {
                Id = 8,
                Name = "Animal Farm",
                Price = 10,
                SKU = "animalfarm"
            };
            var p9 = new Product()
            {
                Id = 9,
                Name = "Nineteen Eighty-Four",
                Price = 12,
                SKU = "ninteeneightyfour"
            };
            var p10 = new Product()
            {
                Id = 10,
                Name = "Burmese Days",
                Price = 15,
                SKU = "burmesedays"
            };
            var otherBooks = new List<Product> { p8, p9, p10 };
            context.AddRange(otherBooks);

            // Discounts
            var d1 = new Discount()
            {
                Id = 1,
                MinProductsRequired = 2,
                Percent = 0.05
            };
            AddDiscountProduct(d1, potterBooks);

            var d2 = new Discount()
            {
                Id = 2,
                MinProductsRequired = 3,
                Percent = 0.10,
            };
            AddDiscountProduct(d2, potterBooks);

            var d3 = new Discount()
            {
                Id = 3,
                MinProductsRequired = 4,
                Percent = 0.20
            };
            AddDiscountProduct(d3, potterBooks);

            var d4 = new Discount()
            {
                Id = 4,
                MinProductsRequired = 5,
                Percent = 0.25
            };
            AddDiscountProduct(d4, potterBooks);

            context.Add(d1);
            context.Add(d2);
            context.Add(d3);
            context.Add(d4);

            context.SaveChanges();
        }

        private void AddDiscountProduct(Discount discount, Product product)
        {
            discount.DiscountedProducts.Add(new DiscountProduct()
            {
                Discount = discount,
                Product = product
            });
        }

        private void AddDiscountProduct(Discount discount, IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                AddDiscountProduct(discount, product);
            }
        }
    }
}
