
using Microcredit.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.MasterOFSToresSVC
{
    [Route("api/[controller]")]


    public class ManageStoreSVC : IManageStore
    {
        private readonly ApplicationDbContext _db;
        public ManageStoreSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreateManageStoreAsync(ManageStoreT manageStore)
        {

            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddmanageStore = new ManageStoreT
                {
                    ManageStoreID = manageStore.ManageStoreID,
                    ManageStorename = manageStore.ManageStorename,
                    UserID = 1

                };
                var result = await _db.ManageStore.AddAsync(AddmanageStore);

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

        public async Task<bool> DeleteManageStoreAsync(int ManageStoreId)
        {
            var GETmanageStoreId = await _db.ManageStore.FindAsync(ManageStoreId);
            ResponseObject responseObject = new();
            if (GETmanageStoreId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.ManageStore.Remove(GETmanageStoreId);
            _db.SaveChanges();
            GC.Collect();

            return true;
        }

        public IEnumerable<ManageStoreT> GetAllManageStoreAsync(string SPName)
        {
            //List<ManageStoreT> ManageStore = new();
            //try
            //{
            //    ManageStore = await _db.ManageStore.OrderBy(x => x.ManageStorename).ToListAsync();

            //}
            //catch (Exception ex)
            //{
            //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
            //                         ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            //}
            GC.Collect();

            return _db.ManageStore.FromSqlRaw("select * from " + SPName).ToList();
        }

        public async Task<ManageStoreT> GetManageStoreByidAsync(int ManageStoreId)
        {

            var GetManageStoreID = (ManageStoreT)null;

            try
            {
                if (ManageStoreId != 0)
                {
                    GetManageStoreID = await _db.ManageStore.FindAsync(ManageStoreId);
                }

            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetManageStoreID;
        }



        public async Task<bool> UpdateManageStoreAsync(int ManageStoreID, ManageStoreT masterOFSTores)
        {

            if (ManageStoreID == masterOFSTores.ManageStoreID)
            {
                _db.Entry(masterOFSTores).State = EntityState.Modified;

            }
            try
            {
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
        private bool MasterOFSToresExists(int ManageStoreId)
        {

            return _db.ManageStore.Any(x => x.ManageStoreID == ManageStoreId);
        }

    }
}
