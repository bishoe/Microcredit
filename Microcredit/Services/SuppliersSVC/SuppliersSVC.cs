using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.SuppliersSVC
{
    public class SuppliersSVC : ISuppliers
    {
        private readonly ApplicationDbContext _db;

        public SuppliersSVC(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseObject> CreateSuppliers(SuppliersT suppliersModel)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var addSuppliers = new SuppliersT
                {

                    SuplierName = suppliersModel.SuplierName,
                    SuplierAddress = suppliersModel.SuplierAddress,
                    SuplierPhone = suppliersModel.SuplierPhone,
                    Notes = suppliersModel.Notes,
                    UsersID = suppliersModel.UsersID = 1
                };
                var result = await _db.Suppliers.AddAsync(suppliersModel);
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
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

        public async Task<List<SuppliersT>> GETALLSuppliersASYNC()
        {
            List<SuppliersT> _SuppliersModel = new();
            try
            {
                _SuppliersModel = await _db.Suppliers.OrderBy(x => x.SuplierName).ToListAsync();

            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            }
            GC.Collect();

            return _SuppliersModel;
        }

        public async Task<SuppliersT> GETSupplierByidASYNC(int SuppliersID)
        {

            var GETSupplierBYID = (SuppliersT)null;
            try
            {
                if (SuppliersID != 0) GETSupplierBYID = await _db.Suppliers.FindAsync(SuppliersID);



            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();


            return GETSupplierBYID;
        }

        public async Task<bool> UpdateSuppliers(int SuppliersID, SuppliersT suppliersT)
        {


            ResponseObject responseObject = new();

            if (suppliersT.SuppliersID == SuppliersID)
            {


                _db.Entry(suppliersT).State = EntityState.Modified;

            }
            try
            {
                if (suppliersT == null)
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
    }
}
