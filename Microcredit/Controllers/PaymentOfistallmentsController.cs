using DinkToPdf.Contracts;
using Microcredit.ModelService;
using Microcredit.Services.InterestRateSVC;
using Microcredit.Services.PaymentOfistallmentsSVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOfistallmentsController : ControllerBase
    {
        private readonly IPaymentOfistallments _paymentOfistallments ;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string paymentOfistallmentsListCacheKey = "paymentOfistallmentsList";
        private ILogger<PaymentOfistallmentsController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public PaymentOfistallmentsController(IPaymentOfistallments paymentOfistallments, IConverter converter, IDistributedCache cache, ILogger<PaymentOfistallmentsController> logger)
        {
            _paymentOfistallments = paymentOfistallments;
            _converter = converter;
            _cache = cache;
            _logger = logger;
        }
        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GetAllPaymentOfistallmentsAsync()
        {
            if (_cache.TryGetValue(paymentOfistallmentsListCacheKey, out IEnumerable<PaymentOfistallmentsModel> paymentOfistallmentsModel))
            {
                _logger.Log(LogLevel.Information, "paymentOfistallmentsList list found in cache.");
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("paymentOfistallmentsList", out paymentOfistallmentsModel))
                    {
                        _logger.Log(LogLevel.Information, "paymentOfistallments list found in cache.");
                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, "paymentOfistallments list not found in cache. Fetching from database.");
                        paymentOfistallmentsModel = _paymentOfistallments.GetAllPaymentOfistallmentsAsync("xxx");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(paymentOfistallmentsListCacheKey, paymentOfistallmentsModel, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(paymentOfistallmentsModel);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentOfistallmentsAsync([FromBody] PaymentOfistallmentsModel paymentOfistallmentsModel)
        {

            // Will hold all the errors related to registration
            if (paymentOfistallmentsModel is null)
            {
                return BadRequest("interestRate is null");

            }
            var result = await _paymentOfistallments.CreatePaymentOfistallmentsAsync(paymentOfistallmentsModel);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            _cache.Remove(paymentOfistallmentsListCacheKey);
            return BadRequest("Cannot Save");


        }

 


    }
}
