using System;
using System.Collections.Generic;
using Billing.API.Models;

namespace Billing.API.Services
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
            // Add your implementation
            throw new NotImplementedException();
        }
    }
}
