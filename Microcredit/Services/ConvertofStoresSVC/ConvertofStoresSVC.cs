
using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.ConvertofStoresSVC
{
    public class ConvertofStoresSVC : IConvertofStores
    {

        private readonly ApplicationDbContext _db;
        public ConvertofStoresSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreateConvertofStoresAsync(ConvertofStoresT ViewModelconvertofStores)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var addConvertofStores = new ConvertofStoresT
                {
                    ManageStoreIdFrom = ViewModelconvertofStores.ManageStoreIdFrom,
                    ManageStoreIdTo = ViewModelconvertofStores.ManageStoreIdTo,
                    ProdouctsID = ViewModelconvertofStores.ProdouctsID,
                    quantityProduct = ViewModelconvertofStores.quantityProduct,
                    Notes = ViewModelconvertofStores.Notes,
                    DateAdd = ViewModelconvertofStores.DateAdd,
                    UserID = ViewModelconvertofStores.UserID = 1

                };
                var result = await _db.ConvertofStores.AddAsync(ViewModelconvertofStores);
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

        //public async Task<List<ConvertofStoresT>> GetAllConvertofStoresAsync()
        //{
        //    List<ConvertofStoresT> convertofStores = new();
        //    try
        //    {
        //        await _db.ConvertofStores.OrderBy(x => x.ConvertofStoresId).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
        //              ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

        //    }
        //    GC.Collect();

        //    return convertofStores;
        //}

        public async Task<ConvertofStoresT> GetConvertofStoresByidAsync(int ConvertofStoresId)
        {
            var GetIdConvertofStores = (ConvertofStoresT)null;
            try
            {
                if (ConvertofStoresId != 0)
                {
                    GetIdConvertofStores = await _db.ConvertofStores.FindAsync(ConvertofStoresId);

                }

            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetIdConvertofStores;

        }

        public async Task<bool> UpdateConvertofStoresAsync(int IdConvertofStores, ConvertofStoresT convertofStoresT)
        {
            ResponseObject responseObject = new();

            if (IdConvertofStores == convertofStoresT.ConvertofStoresId)
            {
                _db.Entry(convertofStoresT).State = EntityState.Modified;

            }
            try
            {
                if (convertofStoresT == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                if (!ConvertofStoresExists(IdConvertofStores)) return false;

                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
            ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                GC.Collect();

                return false;
            }

        }

        //   public async Task<object> GetQt(int ProdouctsID, int BranchCode);
        public async Task<bool> DeleteConvertofStoresAsync(int IdConvertofStores)
        {
            var GETIdConvertofStores = await _db.ConvertofStores.FindAsync(IdConvertofStores);
            ResponseObject responseObject = new();

            if (GETIdConvertofStores == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.ConvertofStores.Remove(GETIdConvertofStores);
            _db.SaveChanges();
            GC.Collect();

            return true;
        }
        private bool ConvertofStoresExists(int IdConvertofStores)
        {
            GC.Collect();

            return _db.ConvertofStores.Any(x => x.ConvertofStoresId == IdConvertofStores);
        }

        public IEnumerable<ConvertofStoresT> GetAllConvertofStoresAsync(string SPName)
        {
            return _db.ConvertofStores.FromSqlRaw("select * from " + SPName).ToList();

            //return result;
        }

        //public IEnumerable<ConvertofStoresT> GetAllConvertofStores()
        //{
        //    GC.Collect();
        // return _db.ConvertofStores.ToList();
        //}
    }
}
