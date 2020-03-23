using Billing.Core.Models;
using System.Collections.Generic;

namespace Billing.Core.Services
{
    public class BookService : IBookService
    {
        private readonly BillingContext context;

        public BookService(BillingContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllBooks()
        {
            return context.Products;
        }
    }
}
