using Microcredit.ClassProject;
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Services.InterestRateSVC;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Stimulsoft.Blockly.Blocks.Maths;

namespace Microcredit.Services.AddNewLonaSVC
{
    public class AddNewLonaSVC : IAddNewLona
    {
        private readonly ApplicationDbContext _db;
        public AddNewLonaSVC(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseObject> CreateNewLona(AddNewLonaMasterModel addNewLonaMasterModel, AddnewLonaDetailsModel addnewLonaDetailsModel, AddNewLoanObjectModel addNewLoanObjectModel)
        {
           
                ResponseObject responseObject = new();
                _db.Database.CloseConnection();
                var countrow = _db.addNewLonaMasters.Count();
                //if (countrow == 0)
                //{
                //    await AddFirstRowINDB(addNewLonaMasterModel);
                //}
                //_db.MasterProductsWarehouse.Add()
                await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
                {

                    for (int i = 0; i < addNewLoanObjectModel.Nocolumn; i++)
                    {
                        try
                        {
                        var GetManageStoreID = _db.MasterProductsWarehouse.Max(x => x.ManageStoreID);
                        var NewManageStoreID = GetManageStoreID + 1;
                        var AddNewLonaMasterModel = new AddNewLonaMasterModel
                        {
                            ProdcutId = addNewLoanObjectModel.ProdcutId,
                            CustomeId = addNewLoanObjectModel.CustomeId,
                            InterestRateid = addNewLoanObjectModel.InterestRateid,
                            MonthNumber = addNewLoanObjectModel.MonthNumber,
                            StartDateLona = addNewLoanObjectModel.StartDateLona,
                            EndDateLona = addNewLoanObjectModel.EndDateLona,
                            AmountBeforeAddInterest = addNewLoanObjectModel.AmountBeforeAddInterest,
                            AmountAfterAddInterest = addNewLoanObjectModel.AmountAfterAddInterest,
                            StatusLona = addNewLoanObjectModel.StatusLona,
                            UserID = addNewLoanObjectModel.UserID,
                            //UserID = 1

                        };
                        var resultMasterProductsWarehouse = await _db.addNewLonaMasters.AddAsync(AddNewLonaMasterModel);

                            var AddNewLoan = new AddnewLonaDetailsModel
                            {
                                 IstalmentsNo = addNewLoanObjectModel.IstalmentsNo,
                                 LonaDetailsId = addNewLoanObjectModel.LonaDetailsId,
                                 LonaGuarantorFirst= addNewLoanObjectModel.LonaGuarantorFirst,
                                 LonaGuarantorSecond= addNewLoanObjectModel.LonaGuarantorSecond,
                                 LonaGuarantorThird = addNewLoanObjectModel.LonaGuarantorThird,
                                 LonaGuarantorFourth= addNewLoanObjectModel.LonaGuarantorFourth,
                                 

                            };
                            var resultAddaddNewLoanModel = await _db.addnewLonaDetails.AddAsync(AddNewLoan);
                            await _db.SaveChangesAsync();
                            await dbContextTransaction.CommitAsync();
                            //Myid =masterProductsWarehouseT.ManageStoreID;
                            //dbContextTransaction.Dispose();

                            responseObject.IsValid = true;
                            responseObject.Message = "Added successfully";
                            responseObject.Data = DateTime.Now.ToString();
                            GC.Collect();

                        }


                        #region catch
                        catch (Exception ex)
                        {

                            Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                            await dbContextTransaction.RollbackAsync();
                            responseObject.IsValid = false;
                            responseObject.Message = "failed";
                            responseObject.Data = DateTime.Now.ToString();
                            GC.Collect();

                        }

                        #endregion






                    }
                    //responseObject.IsValid = true;
                    if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
                    return responseObject;
                }
            }

        public Task<bool> DeleteLonaAsync(int LonaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AddNewLoanObjectModel> GetAllLonaAsync(string SPName)
        {
             
                return _db.addNewLoanObject.FromSqlRaw("select * from " + SPName).ToList();
            }


        public Task<AddNewLoanObjectModel> GetLonaByidAsync(int LonaId)
        {
            //GET all invoic from Warehouse BY storeId 

            var GetwarehouseStore = (AddnewLonaDetailsModel)null;

            try
            {
                if ( LonaId != 0) GetwarehouseStore = await _db.addnewLonaDetails.FindAsync(LonaId);


            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetwarehouseStore;

        }


        public Task<bool> UpdateLonaAsync(int LonaId, AddNewLoanObjectModel addNewLoanObjectModel)
        {
            throw new NotImplementedException();
        }
    }
}
