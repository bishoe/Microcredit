//using Microcredit.ClassProject.MasterProductsWarehouseSVC;
//using Microcredit;
//using Microcredit.Models;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Microcredit.ClassProject.MasterStoreSVC
//{
//    public class MasterProductsWarehouseSVC : IMasterProductsWarehouse
//    {
//        private readonly ApplicationDbContext _db;

//        //public MasterProductsWarehouseSVC(ApplicationDbContext db)
//        //{
//        //    _db = db;
//        //}

//        public Task<ResponseObject> CreateMasterProductsWarehouse(MasterProductsWarehouseT masterProductsWarehouseT)
//        {
//            throw new NotImplementedException();
//        }

//        //        public async Task<ResponseObject> CreateMasterProductsWarehouse(MasterProductsWarehouseT  masterProductsWarehouseT)
//        //        {

//        //            ResponseObject responseObject = new ResponseObject();

//        //            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

//        //            try
//        //            {
//        //                var AddProducts = new MasterProductsWarehouseT
//        //                {
//        //                    EmployeeId = masterProductsWarehouseT.EmployeeId,
//        //                    UsersID = 1
//        //                };
//        //                var result = await _db.MasterProductsWarehouse.AddAsync(masterProductsWarehouseT);
//        //                await _db.SaveChangesAsync();
//        //                await dbContextTransaction.CommitAsync();
//        //                responseObject.IsValid = true;
//        //                responseObject.Message = "Added";
//        //                responseObject.Data = DateTime.Now.ToString();
//        //            }
//        //            catch (Exception ex)
//        //            {


//        //                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
//        //        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

//        //                await dbContextTransaction.RollbackAsync();


//        //            }
//        //            return responseObject;

//        //        }
//        //    }
//    }
//}