using Microcredit.ClassProject.QuantityProductSVC;

using Microcredit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class QuantityProductController : ControllerBase
    {
        private readonly IQuantityProduct _quantityProduct;
        private readonly ApplicationDbContext _db;
        private int GetQTFromQuantityProduct;
        private IDistributedCache _cache;
        private const string QuantityProductListCacheKey = "QuantityProductList";
        private ILogger<QuantityProductController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public QuantityProductController(IQuantityProduct quantityProduct, ApplicationDbContext db, IDistributedCache cache, ILogger<QuantityProductController> logger
)
        {
            _db = db;
            _quantityProduct = quantityProduct;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));



        }

        // GET All QuantityProduct
        [HttpGet("GetQuantityProductBYIDandmanageStoreIDAsync/{manageStoreID}/{ProdouctsID}")]
        //[HttpGet]
        public IActionResult GetQuantityProductBYIDandmanageStoreIDAsync(int manageStoreID, int ProdouctsID)
        {
            if (ProdouctsID is 0 || manageStoreID is 0) return NotFound();
            var checkexistsId = true;
            checkexistsId = _db.QuantityProducts.Any(x => x.ProdouctsID == ProdouctsID || x.manageStoreID == manageStoreID);
            if (checkexistsId == false) return BadRequest("Cannot Find Prodouct Or Store");
            GetQTFromQuantityProduct = _db.QuantityProducts.Where(o => o.ProdouctsID == ProdouctsID)
             .Where(o => o.manageStoreID == manageStoreID)
             .FirstOrDefault().quantityProduct;

            GC.Collect();

            return Ok(GetQTFromQuantityProduct);
        }
        [HttpGet("GetAllquantityProducts")]

        public async Task<IActionResult> GetAllquantityProducts()
        {

            if (_cache.TryGetValue(QuantityProductListCacheKey, out IEnumerable<ReportQuantityProductT>? reportQuantityProduct))
            {
                _logger.Log(LogLevel.Information, "QuantityProduct list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("QuantityProductlist", out reportQuantityProduct))
                    {
                        _logger.Log(LogLevel.Information, "QuantityProduct list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "QuantityProduct list not found in cache. Fetching from database.");
                        reportQuantityProduct = _quantityProduct.GetAllquantityProducts("dbo.view_CreateReportQuantityProduct");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(QuantityProductListCacheKey, reportQuantityProduct, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(reportQuantityProduct);
        }


        [HttpGet("GetQTProdouct/{ProductId}")]
        public IActionResult GetQTProdouct(int ProductId)
        {
            var checkexistsId = true;
            checkexistsId = _db.QuantityProducts.Any(x => x.ProdouctsID == ProductId);
            if (checkexistsId == false) return BadRequest("Cannot Find ProdouctID");
            GetQTFromQuantityProduct = _db.QuantityProducts.Where(x => x.ProdouctsID == ProductId).FirstOrDefault().quantityProduct;
            GC.Collect();
            return Ok(GetQTFromQuantityProduct);
        }
        [HttpGet("GetProdouctQT/{ProductId}/{ManageStoreId}")]
        public IActionResult GetProdouctQT(int ProductId, int ManageStoreId)
        {
            var checkexistsId = true;
            checkexistsId = _db.QuantityProducts.Any(x => x.ProdouctsID == ProductId && x.manageStoreID == ManageStoreId);
            if (checkexistsId == false) return BadRequest("Cannot Find ProdouctID Or cannot find this warehouse  to the branch");

            var GetQT = _db.QuantityProducts.Where(x => x.ProdouctsID == ProductId).FirstOrDefault().quantityProduct;

            GC.Collect();

            return Ok(GetQT);
        }
        [HttpPost]
        public async Task<IActionResult> AddQTProduct(int ProdouctsID)
        {
            var result = await _quantityProduct.AddQtProduct(ProdouctsID);
            if (result.IsValid)
            {
                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("UpdateQTafterSelling/{ProductId}")]
        public async Task<IActionResult> UpdateQTafterSelling(int ProductId, [FromBody] ObjectQuantityProductT quantityProduct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _quantityProduct.UpdateQTafterSelling(ProductId, quantityProduct);

            if (result.IsValid)
            {

                return Ok(new { Message = "Update successfully" });
            }

            return NoContent();

        }





    }
}
