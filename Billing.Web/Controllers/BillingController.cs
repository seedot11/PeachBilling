using System;
using System.Collections.Generic;
using System.Linq;
using Billing.Core.Models;
using Billing.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Billing.Core.Utils;
using Newtonsoft.Json;
using Billing.Core.Database;

namespace Billing.API.Controllers
{
    /// <summary>
    /// API Controller
    /// </summary>
    [Route("api/billing")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        public BillingController(ICostCalculationService costCalculationService
            , IDatabaseService databaseService
            , IBookService bookService)
        {
            CostCalculationService = costCalculationService;
            DatabaseService = databaseService;
            BookService = bookService;

            DatabaseService.SetUpDatabase();
        }

        public ICostCalculationService CostCalculationService { get; }
        public IDatabaseService DatabaseService { get; }
        public IBookService BookService { get; }

        // GET api/billing/cost/
        [HttpGet("cost")]
        public IEnumerable<PurchaseViewModel> Cost(string[] skus)
        {
            var results = CostCalculationService.CalculateCost(skus).ToPurchaseViewModel();
            return results;
        }

        // GET api/billing/basket/
        [HttpGet("basket")]
        public BasketViewModel Total(string[] skus)
        {
            var results = CostCalculationService.CalculateCost(skus).ToBasketViewModel();
            return results;
        }

        // GET api/billing/books
        [HttpGet("books")]
        public IEnumerable<ProductViewModel> Books()
        {
            var results = BookService.GetAllBooks().ToProductViewModel();
            return results;
        }
    }
}