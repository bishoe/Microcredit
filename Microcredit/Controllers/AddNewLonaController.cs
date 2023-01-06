using DinkToPdf.Contracts; 
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Services.AddNewLonaSVC;
using Microcredit.Services.InterestRateSVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddNewLonaController : ControllerBase
    {
        private readonly IAddNewLona  _IaddNewLona;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string addNewLonaListCacheKey = "addNewLonaList";
        private ILogger<AddNewLonaController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public AddNewLonaController(IAddNewLona iaddNewLona, IConverter converter, IDistributedCache cache, ILogger<AddNewLonaController> logger)
        {
            _IaddNewLona = iaddNewLona;
            _converter = converter;
            _cache = cache;
            _logger = logger;
        }

        //TODO check all name view in all contrroller
        [HttpGet]
       public async Task<IActionResult> GetAllLona()
        {

            if (_cache.TryGetValue(addNewLonaListCacheKey, out IEnumerable<AddNewLoanObjectModel> addNewLoanObjectModel))
            {
                _logger.Log(LogLevel.Information, "Lona list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("Lonalist", out addNewLoanObjectModel))
                    {
                        _logger.Log(LogLevel.Information, "Lona list found in cache.");
                    }
                    else
                    {

                        _logger.Log(LogLevel.Information, "Lona list not found in cache. Fetching from database.");
                        addNewLoanObjectModel = _IaddNewLona.GetAllLonaAsync("xxxx");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(addNewLonaListCacheKey, addNewLoanObjectModel, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(addNewLoanObjectModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewLona([FromBody] AddNewLoanObjectModel addNewLoanObjectModel)
        {
             
             AddNewLonaMasterModel addNewLonaMaster  = new();
             AddnewLonaDetailsModel addnewLonaDetailsModel = new();
 
            var addNewLoanObjectModelresult = await _IaddNewLona.CreateNewLona(addNewLonaMaster, addnewLonaDetailsModel, addNewLoanObjectModel);
             
            if (addNewLoanObjectModelresult.IsValid)
            {
                return Ok(new { Message = "Added successfully", addNewLonaMaster.LonaId });
              
            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("{LonaId}")]
        public async Task<IActionResult> UpdateLona(int LonaId, [FromBody] AddNewLoanObjectModel addNewLoanObjectModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _IaddNewLona.UpdateLonaAsync(LonaId, addNewLoanObjectModel);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }


    }
}
