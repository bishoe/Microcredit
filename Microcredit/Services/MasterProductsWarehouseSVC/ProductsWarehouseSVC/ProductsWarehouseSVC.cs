using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.MasterProductsWarehouseSVC.ProductsWarehouseSVC
{
    public class ProductsWarehouseSVC : IProductsWarehouse
    {

        private readonly ApplicationDbContext _db;

        public ProductsWarehouseSVC(ApplicationDbContext db)
        {
            _db = db;
        }

        public MasterProductsWarehouseT _masterProductsWarehouse;
        //  public MasterProductsWarehouseT _AddMasterProductsWarehouse = new ();

        public async Task<ResponseObject> CreateProductsWarehouse(MasterProductsWarehouseT masterProductsWarehouse, ProductsWarehouseT productsWarehouse, ProductsWarehouseObjectT ProductsWarehouseModel)
        {
            ResponseObject responseObject = new();
            _db.Database.CloseConnection();
            var countrow = _db.MasterProductsWarehouse.Count();
            if (countrow == 0)
            {
                await AddFirstRowINDB(masterProductsWarehouse);
            }
            //_db.MasterProductsWarehouse.Add()
            await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            {

                for (int i = 0; i < ProductsWarehouseModel.Nocolumn; i++)
                {
                    try
                    {
                        var GetManageStoreID = _db.MasterProductsWarehouse.Max(x => x.ManageStoreID);
                        var NewManageStoreID = GetManageStoreID + 1;

                        var AddMasterProductsWarehouse = new MasterProductsWarehouseT
                        {
                            EmployeeId = masterProductsWarehouse.EmployeeId = 1,
                            UsersID = masterProductsWarehouse.UsersID = 1,
                            AMountDicount = ProductsWarehouseModel.AMountDicount = ProductsWarehouseModel.AMountDicount,
                            Discount = ProductsWarehouseModel.Discount,
                            TotalBDiscount = ProductsWarehouseModel.TotalBDiscount,
                            TotalPrice = ProductsWarehouseModel.TotalPrice,
                            Notes = ProductsWarehouseModel.Notes,
                            DateAdd = masterProductsWarehouse.DateAdd = DateTime.Now.ToUniversalTime(),
                            ManageStoreID = masterProductsWarehouse.ManageStoreID = NewManageStoreID
                        };
                        var resultMasterProductsWarehouse = await _db.MasterProductsWarehouse.AddAsync(AddMasterProductsWarehouse);

                        var AddProducts = new ProductsWarehouseT
                        {
                            SuppliersID = ProductsWarehouseModel.SuppliersID,
                            CategoryProductId = ProductsWarehouseModel.CategoryProductId,
                            ProdouctsID = ProductsWarehouseModel.ProdouctsID,
                            QuntityProduct = ProductsWarehouseModel.QuntityProduct,
                            SizeProducts = ProductsWarehouseModel.SizeProducts,
                            PurchasingPrice = ProductsWarehouseModel.PurchasingPrice,
                            Productiondate = ProductsWarehouseModel.Productiondate,
                            ExpireDate = ProductsWarehouseModel.ExpireDate,
                            Dateofregistration = ProductsWarehouseModel.Dateofregistration,
                            Anexpiredproduct = ProductsWarehouseModel.Anexpiredproduct,
                            //QtStartPeriod = ProductsWarehouseModel.QtStartPeriod,
                            SellingPrice = ProductsWarehouseModel.SellingPrice,
                            TotalAmountRow = ProductsWarehouseModel.TotalAmountRow,
                            PermissionToEntertheStoreProductId = ProductsWarehouseModel.PermissionToEntertheStoreProductId,
                        };
                        var resultProductsWarehouseModel = await _db.ProductsWarehouse.AddAsync(AddProducts);
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
        /// <summary>
        /// add new row in db when not any data in db
        /// </summary>
        /// <param name="masterProductsWarehouse"></param>
        /// <returns></returns>
        public async Task<ResponseObject> AddFirstRowINDB(MasterProductsWarehouseT masterProductsWarehouse)
        {
            await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            {
                ResponseObject responseObject = new();
                try
                {
                    //var GetManageStoreID = _db.MasterProductsWarehouse.Max(x => x.ManageStoreID);
                    var NewManageStoreID = 1;
                    //await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
                    {
                        var AddMasterProductsWarehouse = new MasterProductsWarehouseT

                        {
                            EmployeeId = masterProductsWarehouse.EmployeeId = 1,
                            UsersID = masterProductsWarehouse.UsersID = 1,
                            AMountDicount = masterProductsWarehouse.AMountDicount = 1,
                            Discount = masterProductsWarehouse.Discount = 1,
                            TotalBDiscount = masterProductsWarehouse.TotalBDiscount = 0,
                            TotalPrice = masterProductsWarehouse.TotalPrice = 1,
                            Notes = masterProductsWarehouse.Notes,
                            DateAdd = masterProductsWarehouse.DateAdd = DateTime.Now.ToUniversalTime(),
                            ManageStoreID = masterProductsWarehouse.ManageStoreID = NewManageStoreID,
                            Tax = masterProductsWarehouse.Tax = 1
                        };
                        var resultMasterProductsWarehouse = await _db.MasterProductsWarehouse.AddAsync(AddMasterProductsWarehouse);
                        await _db.SaveChangesAsync();

                        await dbContextTransaction.CommitAsync();
                        GC.Collect();


                    }
                    responseObject.IsValid = true;
                    responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
                    GC.Collect();

                }
                catch (Exception ex)
                {

                    Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                }

                if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
                return responseObject;

            }
        }
        public async Task<ProductsWarehouseT> GetProductsWarehouseBYBillnoAsync(int Billno)
        {
            //GET all invoic from Warehouse BY Billno 

            var GetwarehouseStore = (ProductsWarehouseT)null;

            try
            {
                if (Billno != 0)
                {
                    GetwarehouseStore = await _db.ProductsWarehouse.FindAsync(Billno);

                }
            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetwarehouseStore;

        }

        public async Task<ProductsWarehouseT> GetProductsWarehouseBYIDAsync(int ManageStoreID)
        {
            //GET all invoic from Warehouse BY storeId 

            var GetwarehouseStore = (ProductsWarehouseT)null;

            try
            {
                if (ManageStoreID != 0) GetwarehouseStore = await _db.ProductsWarehouse.FindAsync(ManageStoreID);


            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return GetwarehouseStore;

        }


        Task<ResponseObject> IProductsWarehouse.GetNoColumn(int NoColumn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductsWarehouseObjectT> GetAllProductsWarehouse(string SPName)
        {
            return _db.productsWarehouseObjectTs.FromSqlRaw("select * from " + SPName).ToList();
        }
    }
}
