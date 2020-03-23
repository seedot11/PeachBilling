using Billing.Core.Models;
using System.Collections.Generic;

namespace Billing.Core.Services
{
    public interface IBookService
    {
        IEnumerable<Product> GetAllBooks();
    }
}
