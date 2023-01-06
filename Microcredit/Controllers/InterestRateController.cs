using DinkToPdf.Contracts;
using Microcredit.ClassProject.BranchesSVC;
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP;
using Microcredit.Services.InterestRateSVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestRateController : ControllerBase
    {
        private readonly IInterestRate _interestRatee;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string interestRateeListCacheKey = "interestRateeList";
        private ILogger<InterestRateController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public InterestRateController(IInterestRate interestRatee, IConverter converter, IDistributedCache cache, ILogger<InterestRateController> logger)
        {
            _interestRatee = interestRatee;
            _converter = converter;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GetAllInterestAsync()
        {
            if (_cache.TryGetValue(interestRateeListCacheKey, out IEnumerable<InterestRateModel>? interestRates))
            {
                _logger.Log(LogLevel.Information, "interestRate list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("interestRatelist", out interestRates))
                    {
                        _logger.Log(LogLevel.Information, "interestRate list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "interestRate list not found in cache. Fetching from database.");
                        interestRates = _interestRatee.GetAllInterestAsync("dbo.view_CreateReportBranches");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(interestRateeListCacheKey, interestRates, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(interestRates);

        }

        [HttpPost]
        public async Task<IActionResult> CreateInterestRateAsync([FromBody] InterestRateModel  interestRate)
        {

            // Will hold all the errors related to registration
            if (interestRate is null )
            {
                return BadRequest("interestRate is null");

            }
            var result = await _interestRatee.CreateInterestRateAsync(interestRate);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            _cache.Remove(interestRateeListCacheKey);
            return BadRequest("Cannot Save");


        }

        [HttpPut("{InterestRateId}")]
        public async Task<IActionResult> UpdateBranches(int InterestRateId, [FromBody] InterestRateModel interestRate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _interestRatee.UpdateInterestRate(InterestRateId, interestRate);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }

}
 