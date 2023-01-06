//using DataBaseService;
//using InternalShop.Models;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace InternalShop.ClassProject.MasterSellProductsSVC
//{


//    public class MasterSellProductsSVC : IMasterSellProducts
//    {

//        private readonly ApplicationDbContext _db;

//        public MasterSellProductsSVC(
//         ApplicationDbContext db)
//        {
//            _db = db;
//        }
//        public async Task<ResponseObject> CreateMasterSellProducts(SalesinvoiceT SellProductsMasterT)
//        {


//            // Will hold all the errors related to registration
//            //var errorList = new List<string>();
//            ResponseObject responseObject = new ResponseObject();
//            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
//            try
//            {

//                var addSelesInvoiceMaster = new SalesinvoiceT
//                {
//                    CustomerID = SellProductsMasterT.CustomerID,
//                    UsersID = 1
//                };
           
//             var result = await _db.SalesInvoicesMaster.AddAsync(SellProductsMasterT);
//            await _db.SaveChangesAsync();
//            await  dbContextTransaction.CommitAsync();
//            responseObject.IsValid = true;
//            responseObject.Message = "Added successfully";
//            responseObject.Data = DateTime.Now.ToString();

// }
//            catch (Exception ex)
//            {

//                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
//                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

//                await dbContextTransaction.RollbackAsync();

//            }
//            return responseObject;


//        }
//    } }

