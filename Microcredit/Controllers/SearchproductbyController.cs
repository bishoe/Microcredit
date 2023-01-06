//using Microcredit.ClassProject;
//using Microcredit.ClassProject.Searchproducts;
//
//using Microcredit.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Microcredit.Controllers
//{
//    [Route("api/[controller]")]
//    //[ApiController]
//    public class SearchproductbyController : ControllerBase
//    {
//        List<ProductsT> products = new();

//        private readonly IproductsSearch  _productsSearch;
//        private readonly IProducts _products;

//        private readonly ApplicationDbContext _db;
//        public SearchproductbyController(IproductsSearch IproductsSearch, ApplicationDbContext db)
//        {
//            _productsSearch = IproductsSearch;
//            _db = db;
//        }


//        [HttpGet("{Barcode}")]
//        public async Task <IActionResult> GetProductbyBarcode(int Barcode)
//        {
//            var GetProductbyName =   _productsSearch.GetProductbyBarcode(Barcode);

//            return Ok(GetProductbyName);

//        }

//    }
//}
