using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.DismissalnoticeSVC
{
    public class DismissalnoticeSVC : IDismissalnotice
    {

        private readonly ApplicationDbContext _db;
        public DismissalnoticeSVC(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<DismissalnoticeT> GetAllDismissalnoticeAsync(string SPName)
        {
            List<DismissalnoticeT> dismissalnotice = new();

            //try
            //{
            //    dismissalnotice =await _db.Dismissalnotice.OrderBy(x => x.DismissalnoticeId).ToListAsync();
            //}
            //catch (Exception ex)
            //{

            //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
            //          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            //}


            GC.Collect();

            return _db.Dismissalnotice.FromSqlRaw("select * from " + SPName).ToList();

        }

        public async Task<DismissalnoticeT> GetDismissalnoticeByidAsync(int DismissalnoticeId)
        {
            var GetIdDismissalnotice = (DismissalnoticeT)null;

            try
            {
                if (DismissalnoticeId != 0)
                {
                    GetIdDismissalnotice = await _db.Dismissalnotice.FindAsync(DismissalnoticeId);


                }
            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetIdDismissalnotice;
        }
        public async Task<ResponseObject> CreateDismissalnoticeAsync(DismissalnoticeT dismissalnotice)
        {



            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {

                var Adddismissalnotice = new DismissalnoticeT
                {
                    ManageStoreId = dismissalnotice.ManageStoreId,
                    ProdouctsID = dismissalnotice.ProdouctsID,
                    quantityProduct = dismissalnotice.quantityProduct,
                    DateAdd = dismissalnotice.DateAdd,
                    UserID = 1

                };
                var result = await _db.Dismissalnotice.AddAsync(Adddismissalnotice);
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
                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();
            }
            GC.Collect();

            return responseObject;
        }
        public async Task<bool> UpdateDismissalnoticeAsync(int IdDismissalnotice, DismissalnoticeT dismissalnotice)
        {
            ResponseObject responseObject = new();

            if (IdDismissalnotice == dismissalnotice.DismissalnoticeId)
            {
                _db.Entry(dismissalnotice).State = EntityState.Modified;
            }
            try
            {
                if (dismissalnotice == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                if (!dismissalnoticeExists(IdDismissalnotice))

                    Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                GC.Collect();

                return false;
            }
        }

        private bool dismissalnoticeExists(int IdDismissalnotice)
        {

            return _db.Dismissalnotice.Any(x => x.DismissalnoticeId == IdDismissalnotice);

        }

        public async Task<bool> DeleteDismissalnoticeAsync(int DismissalnoticeId)
        {
            var GETDismissalnoticeId = await _db.Dismissalnotice.FindAsync(DismissalnoticeId);
            ResponseObject responseObject = new();
            if (GETDismissalnoticeId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.Dismissalnotice.Remove(GETDismissalnoticeId);
            _db.SaveChanges();
            GC.Collect();

            return true;
        }
    }




}

