using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.QuantityProductSVC
{
    public class QuantityProductSVC : IQuantityProduct
    {

        private readonly ApplicationDbContext _db;
        public QuantityProductSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        // Add qt = 0 when add new product
        public async Task<ResponseObject> AddQtProduct(int ProdouctsID)
        {
            ResponseObject responseObject = new ResponseObject();

            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {

                var AddQTProducts = new QuantityProductT
                {
                    quantityProduct = 0,
                    ProdouctsID = ProdouctsID
                };
                var result = await _db.QuantityProducts.AddAsync(AddQTProducts);
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                //TODO GET Method insert new row in quntity product
                var CurrentidafterinsertNewRow = AddQTProducts.ProdouctsID;

                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {


                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                await dbContextTransaction.RollbackAsync();


            }
            GC.Collect();

            return responseObject;


        }

        public IEnumerable<ReportQuantityProductT> GetAllquantityProducts(string SPName)
        {

            GC.Collect();

            return _db.reportQuantityProduct.FromSqlRaw("select * from " + SPName).ToList();


        }

        public async Task<QuantityProductT> GetQuantityProductBYIDandManageStoreIdAsync(int manageStoreID, int ProdouctsID)
        {
            var Result = (QuantityProductT)null;
            try
            {
                if (ProdouctsID != 0 || manageStoreID != 0) Result = await _db.QuantityProducts.Where(o => o.ProdouctsID == ProdouctsID)
             .Where(o => o.manageStoreID == manageStoreID)
             .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return Result;


        }
        public async Task<QuantityProductT> GetQuantityProductBYIDAsync(int ProdouctsID)
        {
            var Result = (QuantityProductT)null;
            try
            {
                var checkexistsId = true;

                if (ProdouctsID != 0) checkexistsId = _db.QuantityProducts.Any(x => x.ProdouctsID == ProdouctsID);
                if (checkexistsId is true)
                {
                    Result = await _db.QuantityProducts.Where(o => o.ProdouctsID == ProdouctsID)
                      .FirstOrDefaultAsync();

                }

                return Result;


            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return Result;


        }

        //public async Task<bool> UpdateQTafterSelling(int ProductId, ObjectQuantityProductT _ObjectQuantityProduct)
        public async Task<ResponseObject> UpdateQTafterSelling(int ProductId, ObjectQuantityProductT _ObjectQuantityProduct)

        {
            ResponseObject responseObject = new();
            QuantityProductT quantityProduct = new();
            // Get one row and Update quantityProduct only
            var quantityProductresult = _db.QuantityProducts.First(x => x.ProdouctsID == ProductId);
            quantityProductresult.quantityProduct = _ObjectQuantityProduct.NewQtProduct;

            //var CalcNewQTProduct = _ObjectQuantityProduct.CurrentQTProduct - _ObjectQuantityProduct.NewQtProduct;




            if (!ProdouctsIDExists(ProductId)) responseObject.Message = "Error objectQuantity is Empty";


            _db.Entry(quantityProductresult).State = EntityState.Modified;

            try
            {
                if (quantityProduct is null)
                {
                    responseObject.Message = "Error object Quantity is Empty";

                }

                await _db.SaveChangesAsync();

                responseObject.IsValid = true;
                responseObject.Message = "Update successfully";
                responseObject.Data = DateTime.Now.ToString();
                return responseObject;
                //return true;
            }
            catch (Exception ex)
            {

                if (!ProdouctsIDExists(ProductId))

                    Log.Error("Error while Update Quantity Prodouct {Error} {StackTrace} {InnerException} {Source}",
     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();

            }
            GC.Collect();
            // return false;

            return responseObject;

        }
        private bool ProdouctsIDExists(int ProdouctsID)
        {
            return _db.QuantityProducts.Any(e => e.ProdouctsID == ProdouctsID);
        }
    }

}