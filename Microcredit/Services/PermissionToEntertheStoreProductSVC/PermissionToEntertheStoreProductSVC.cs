using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.PermissionToEntertheStoreProductSVC
{
    public class PermissionToEntertheStoreProductSVC : IPermissionToEntertheStoreProduct
    {
        private readonly ApplicationDbContext _db;

        public PermissionToEntertheStoreProductSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreatePermissionToEntertheStoreProductAsync(PermissionToEntertheStoreProductT PermissionToEntertheStoreProductT)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddPermissionToEntertheStoreProduct = new PermissionToEntertheStoreProductT
                {
                    ManageStoreId = PermissionToEntertheStoreProductT.ManageStoreId,
                    ProdouctsID = PermissionToEntertheStoreProductT.ProdouctsID,
                    quantityProduct = PermissionToEntertheStoreProductT.quantityProduct,
                    DateAdd = PermissionToEntertheStoreProductT.DateAdd,
                    //UserID = 1

                };
                var result = await _db.PermissionToEntertheStoreProduct.AddAsync(AddPermissionToEntertheStoreProduct);
                await _db.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
                var GetCurrentId = AddPermissionToEntertheStoreProduct.PermissionToEntertheStoreProductId;

                responseObject.IsValid = true;
                responseObject.Message = ">" + GetCurrentId;
                responseObject.Data = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                await dbContextTransaction.RollbackAsync();

                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();
            }
            GC.Collect();

            return responseObject;
        }

        public async Task<bool> DeletePermissionToEntertheStoreProductAsync(int PermissionToEntertheStoreProductId)
        {
            var GETPermissionToEntertheStoreProductId = await _db.PermissionToEntertheStoreProduct.FindAsync(PermissionToEntertheStoreProductId);
            ResponseObject responseObject = new();
            if (GETPermissionToEntertheStoreProductId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.PermissionToEntertheStoreProduct.Remove(GETPermissionToEntertheStoreProductId);
            _db.SaveChanges();
            GC.Collect();

            return true;
        }

        public IEnumerable<ReportPermissionToEntertheStoreProduct> GetAllPermissionToEntertheStoreProductAsync(string SPName)
        {

            return _db.reportPermissionToEntertheStoreProducts.FromSqlRaw("select * from " + SPName).ToList();




        }

        //public IEnumerable<object> ExecuteSP(string SPName)
        //{
        //    var result = _db.reportPermissionToEntertheStoreProducts.FromSqlRaw(SPName).ToList();
        //    return result;
        //}
        //public async Task<List<PermissionToEntertheStoreProductT>> GetAllPermissionToEntertheStoreProductAsync()
        //{

        //    Task<List<PermissionToEntertheStoreProductT>> PermissionToEntertheStoreProduct;
        //    try
        //    {
        //        PermissionToEntertheStoreProduct = await _db.PermissionToEntertheStoreProduct.OrderBy(x => x.PermissionToEntertheStoreProductId).ToListAsync();
        //        ReportPermissionToEntertheStoreProduct


        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
        //                             ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
        //    }
        //    GC.Collect();

        //    return await Task.FromResult(_db.PermissionToEntertheStoreProduct.Include
        //         (Per => Per.Products).
        //         ThenInclude(s => s.PermissionToEntertheStoreProduct).ThenInclude(MStore => MStore.ManageStore)
        //         .OrderBy(p => p.Products.ProdouctName).ToList())
        //    ;
        //}

        public async Task<PermissionToEntertheStoreProductT> GetPermissionToEntertheStoreProductByidAsync(int PermissionToEntertheStoreProductId)
        {
            var GETIdPermissionToEntertheStoreProduct = (PermissionToEntertheStoreProductT)null;

            try
            {
                if (PermissionToEntertheStoreProductId != 0) GETIdPermissionToEntertheStoreProduct = await _db.PermissionToEntertheStoreProduct.FindAsync(PermissionToEntertheStoreProductId);
            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GETIdPermissionToEntertheStoreProduct;
        }

        public async Task<bool> UpdatePermissionToEntertheStoreProductAsync(int IdPermissionToEntertheStoreProduct, PermissionToEntertheStoreProductT PermissionToEntertheStoreProduct)
        {
            ResponseObject responseObject = new();

            if (IdPermissionToEntertheStoreProduct == PermissionToEntertheStoreProduct.ManageStoreId)
            {
                _db.Entry(PermissionToEntertheStoreProduct).State = EntityState.Modified;

            }
            try
            {
                if (PermissionToEntertheStoreProduct == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {

                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
            ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                GC.Collect();

                return false;
            }
        }
        private bool PermissionToEntertheStoreProductExists(int IdPermissionToEntertheStoreProduct)
        {

            return _db.PermissionToEntertheStoreProduct.Any(x => x.PermissionToEntertheStoreProductId == IdPermissionToEntertheStoreProduct);
        }

    }
}
