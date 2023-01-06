
using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.BranchesSVC
{
    public class BranchesSVC : IBranches
    {
        private readonly ApplicationDbContext _db;

        public BranchesSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreateBranches(BranchesT branchesViewModel)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var addBranches = new BranchesT
                {
                    BranchName = branchesViewModel.BranchName,
                    BranchCode = branchesViewModel.BranchCode,
                    BranchAddress = branchesViewModel.BranchAddress,
                    BranchPhone = branchesViewModel.BranchPhone,
                    BranchMobile = branchesViewModel.BranchMobile,
                    manageStoreID = branchesViewModel.manageStoreID,
                    USerID = branchesViewModel.USerID = 1
                };
                var result = await _db.Branches.AddAsync(branchesViewModel);
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

        public IEnumerable<BranchesReportT> GETALLBRANCHESASYNC(string SPName)
        {
            //try
            //{

            //}
            //catch (Exception ex)
            //{

            //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
            //          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            //}
            GC.Collect();

            //return _branchesModel;
            return _db.BranchesReport.FromSqlRaw("select * from " + SPName).ToList();


        }

        public async Task<BranchesT> GETBRANCHByidASYNC(int BranchCode)
        {
            var GETBranchCodeBYID = (BranchesT)null;
            try
            {
                if (BranchCode != 0) GETBranchCodeBYID = await _db.Branches.FindAsync(BranchCode);



            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GETBranchCodeBYID;
        }
        //TODO Check update Name and mobil not updateAll column
        public async Task<bool> UpdateBranches(int BranchID, BranchesT branches)
        {
            ResponseObject responseObject = new();
            if (branches.BranchID == BranchID)
            {
                _db.Entry(branches).State = EntityState.Modified;
            }
            try
            {
                if (branches == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";
                }
                await _db.SaveChangesAsync();
                GC.Collect();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return false;
            }
        }
    }
}
