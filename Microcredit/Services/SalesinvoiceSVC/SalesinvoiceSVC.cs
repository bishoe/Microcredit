using Microcredit.Models;
using Microcredit.Reports.ReportSalesInvoice;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.SalesinvoiceSVC
{
    public class SalesinvoiceSVC : ISalesinvoice
    {

        private readonly ApplicationDbContext _db;
        IReportS _ReportSalesInvoice;
        public SalesinvoiceSVC(ApplicationDbContext db,
             IReportS reportSalesInvoice
           )
        {
            _db = db;
            _ReportSalesInvoice = reportSalesInvoice;
        }
        public SalesinvoiceMasterT _SalesinvoiceMaster;


        public async Task<ResponseObject> CreateSalesinvoiceAsync(SalesinvoiceObject salesinvoiceObject)
        {

            ResponseObject responseObject = new();
            _db.Database.CloseConnection();

            var countrow = _db.SalesInvoicesMaster.Count();
            if (countrow == 0)
            {
                await AddFirstRowINDB(_SalesinvoiceMaster);
            }

            await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            {


                for (int i = 0; i < salesinvoiceObject.Nocolumn; i++)
                {
                    try
                    {
                        var GetSalesMasterID = _db.SalesInvoicesMaster.Max(x => x.SellingMasterID);
                        var NewSalesMasterID = GetSalesMasterID + 1;

                        var AddSalesInvoicesMasterModel = new SalesinvoiceMasterT
                        {
                            CustomerID = salesinvoiceObject.CustomerID = 1,
                            EmployeeId = salesinvoiceObject.EmployeeId = 1,
                            UsersID = salesinvoiceObject.UsersID = 1,
                            AMountDicount = salesinvoiceObject.AMountDicount,
                            Discount = salesinvoiceObject.Discount,
                            TotalBDiscount = salesinvoiceObject.TotalBDiscount,
                            TotalPrice = salesinvoiceObject.TotalPrice,
                            Tax = salesinvoiceObject.Tax,
                            DateAdd = DateTime.Now.ToUniversalTime(),
                            AmountPaid = salesinvoiceObject.AmountPaid,
                            RemainingAmount = salesinvoiceObject.RemainingAmount,
                            SellingMasterID = salesinvoiceObject.SellingMasterID = NewSalesMasterID

                        };
                        var resultSalesInvoicesMasterModel = await _db.SalesInvoicesMaster.AddAsync(AddSalesInvoicesMasterModel);

                        var AddsalesinvoiceModel = new SalesinvoiceT
                        {
                            ProdouctsID = salesinvoiceObject.ProdouctsID,
                            Quntity_Product = salesinvoiceObject.Quntity_Product,
                            SellingPrice = salesinvoiceObject.SellingPrice,
                            TotalAmountRow = salesinvoiceObject.TotalAmountRow,
                            SellingMasterID = salesinvoiceObject.SellingMasterID = NewSalesMasterID,
                        };

                        var resultsalesinvoiceModel = await _db.SalesInvoices.AddAsync(AddsalesinvoiceModel);
                        await _db.SaveChangesAsync();
                        await dbContextTransaction.CommitAsync();
                        //_ReportSalesInvoice.GetHTMLString(NewSalesMasterID);

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
                    }

                    #endregion
                 ;
                }

            }
            //responseObject.IsValid = true;
            if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;
        }

        /// <summary>
        /// add new row in db when not any data in db
        /// </summary>
        /// <param name="SalesinvoiceMaster"></param>
        /// <returns></returns>
        public async Task<ResponseObject> AddFirstRowINDB(SalesinvoiceMasterT salesinvoiceMaster)
        {
            await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            {
                ResponseObject responseObject = new();
                try
                {
                    //var GetSalesMasterID = _db.SalesInvoicesMaster.Max(x => x.SellingMasterID);

                    var AddSalesInvoicesMasterModel = new SalesinvoiceMasterT
                    {
                        CustomerID = 1,
                        UsersID = 1,
                        AMountDicount = 1,
                        Discount = 1,
                        TotalBDiscount = 1,
                        TotalPrice = 1,
                        Tax = 1,
                        DateAdd = DateTime.Now.ToUniversalTime(),
                        SellingMasterID = 1

                    };
                    var resultSalesInvoicesMasterModel = await _db.SalesInvoicesMaster.AddAsync(AddSalesInvoicesMasterModel);

                    await _db.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    //Myid =masterProductsWarehouseT.ManageStoreID;
                    //dbContextTransaction.Dispose();
                    //   _ReportSalesInvoice.CreateReportSalesInvoice(NewSalesMasterID);

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
                }
                //responseObject.IsValid = true;
                if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
                return responseObject;

            }
            #endregion





        }

        public IEnumerable<SalesinvoiceObjectReport> GetAllsalesinvoice(string SPName)
        {

            return _db.SalesinvoiceObjectReport.FromSqlRaw("select * from " + SPName).ToList();


        }
    }
}

