//using DataBaseService;
//using InternalShop.Models;
//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace InternalShop.ClassProject.Searchproducts
//{
//    public class SearchproductsSVC : IproductsSearch
//    {
//        private readonly ApplicationDbContext _db;

//        public IList<ProductsT> GetProductbyBarcode(int Barcode)
//        {
//            //var GetProductbyBarcode = (ProductsT)null;
//            //try
//            //{
//            //if (Barcode != 0)
//            //{ //}

//            string query = Convert.ToString(100);

//            //var GetProductbyBarcode  =  await _db.Products.Where(x => EF.Functions.Like(Convert.ToString(x.BarCodeText), "%" + query + "%")).ToListAsync();
//            var GetProductbyBarcode = from c in _db.Products.Where(x => EF.Functions.Like(Convert.ToString(x.BarCodeText), "%" + query + "%")) select c;


//            return (IList<ProductsT>)GetProductbyBarcode;





//            //}

//            //catch (Exception ex)
//            //{

//            //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
//            //                          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
//            //}



//        }
//        public async Task<List<ProductsT>> GetProductsAsync()
//        {

//            List<ProductsT> _productsModel = new List<ProductsT>();
//            try
//            {
//                _productsModel = await _db.Products.OrderBy(x => x.ProdouctName).ToListAsync();

//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
//                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

//            }
//            return _productsModel;



//        }
//        public Task<ProductsT> GetProductbyName(string ProdouctName)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
