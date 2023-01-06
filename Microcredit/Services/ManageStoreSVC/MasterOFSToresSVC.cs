using DataBaseService;
using InternalShop.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.ClassProject.MasterOFSToresSVC
{
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
                    ManageStorename = manageStore.ManageStorename,
                    DateAdd = manageStore.DateAdd,
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

            return true;
        }

        public async Task<List<ManageStoreT>> GetAllManageStoreAsync()
        {
            List<ManageStoreT> ManageStore = new();
            try
            {
                ManageStore = await _db.ManageStore.OrderBy(x => x.ManageStorename).ToListAsync();

            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return ManageStore;
        }

        public async Task<ManageStoreT> GetManageStoreByidAsync(int ManageStoreId)
        {

            var GETIdMasterOFSTores = (ManageStoreT)null;

            try
            {
                if (ManageStoreId != 0)
                {
                    GETIdMasterOFSTores = await _db.ManageStore.FindAsync(ManageStoreId);
                }

            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return GETIdMasterOFSTores;
        }

      

        public async Task<bool> UpdateMasterOFSToresAsync(int ManageStoreID, ManageStoreT masterOFSTores)
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
                return false;
            } }
            private bool MasterOFSToresExists(int MasterOFSToresId)
            {

                return _db.ManageStore.Any(x => x.ManageStoreID == MasterOFSToresId);
            }
       
    }
}
