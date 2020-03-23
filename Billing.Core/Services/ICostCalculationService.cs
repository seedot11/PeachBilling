using System.Collections.Generic;
using Billing.Core.Models;

namespace Billing.Core.Services
{
    public interface ICostCalculationService
    {
        IEnumerable<Purchase> CalculateCost(IEnumerable<string> productSkus);
    }
}
