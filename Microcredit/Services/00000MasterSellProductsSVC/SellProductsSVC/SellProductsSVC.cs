//using DataBaseService;
//using InternalShop.Models;
//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace InternalShop.ClassProject.MasterSellProductsSVC.SellProductsSVC
//{
//    public class SellProductsSVC : ISellProducts
//    {
//        private readonly ApplicationDbContext _db;
//        public SellProductsSVC(ApplicationDbContext db)
//        {
//            _db = db;

//        }

//        public async Task<ResponseObject> CreateSellProducts(SalesinvoiceT SellProductsMaster, SellProductsT sellProducts)
//        {
//            // Will hold all the errors related to registration
//            //var errorList = new List<string>();
//            ResponseObject responseObject = new ResponseObject();
//            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

//            try
//            {
//                var addSelesInvoiceMaster = new SalesinvoiceT
//                {
//                    CustomerID = SellProductsMaster.CustomerID,
//                    UsersID = 1
//                };

//                var resultSellProductsMaster = await _db.SalesInvoicesMaster.AddAsync(SellProductsMaster);
//                await _db.SaveChangesAsync();
//                await dbContextTransaction.CommitAsync();
//                var AddBIllSelling = new SellProductsT
//                {

//                    EmployeeId = sellProducts.EmployeeId,
//                   // CategoryProductId = sellProducts.CategoryProductId,
//                    //Billno = sellProducts.Billno,
//                    ProdouctsID = sellProducts.ProdouctsID,
//                    //UnitesId = sellProducts.UnitesId,
//                    Quntity_Product = sellProducts.Quntity_Product,
//                    TotalAmountRow = sellProducts.TotalAmountRow,
//                    SellingPrice = sellProducts.SellingPrice,
//                    Discount = sellProducts.Discount,
//                    AMountDicount = sellProducts.AMountDicount,
//                    TotalPrice = sellProducts.TotalPrice,
//                    TotalBDiscount = sellProducts.TotalBDiscount,
//                    SellingMasterID = sellProducts.SellingMasterID,
//                    UsersID = 1


//                };

//                var result = _db.SellProducts.Add(sellProducts);
//                await _db.SaveChangesAsync();
//                await dbContextTransaction.CommitAsync();
//                responseObject.IsValid = true;
//                responseObject.Message = "Added successfully";
//                responseObject.Data = DateTime.Now.ToString();

//            }
//            catch (Exception ex)
//            {
//                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
//                 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
//                await dbContextTransaction.RollbackAsync();
//            }
//            return responseObject;
//        }

//        public async Task<List<SellProductsT>> GETAllSellProducts()
//        {
//            List<SellProductsT> sellProductss = new List<SellProductsT>();
//            sellProductss = await _db.SellProducts.OrderBy(x => x.SellingMasterID).ToListAsync();

//            return sellProductss;
//        }
//        public async Task<SellProductsT> GETAllSellProductsByDateTime(DateTime SellingMasterDateTime)
//        {
//            var GETBiiByDateTime = (SellProductsT)null;
//            try
//            {
//                GETBiiByDateTime = await _db.SellProducts.FindAsync(SellingMasterDateTime);

//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
//                                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
//            }

//            return GETBiiByDateTime;
//        }
//        public async Task<SellProductsT> GETAllSellProductsById(int SellProductsID)
//        {
//            var GETBiiBySellingMasterID = (SellProductsT)null;
//            try
//            {
//                GETBiiBySellingMasterID = await _db.SellProducts.FindAsync(SellProductsID);

//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
//                                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
//            }

//            return GETBiiBySellingMasterID;
//        }


//    }
//}
