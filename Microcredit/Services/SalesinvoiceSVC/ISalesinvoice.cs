using Microcredit.Models;

namespace Microcredit.ClassProject.SalesinvoiceSVC
{
    public interface ISalesinvoice
    {
        // public   Task<ResponseObject> CreateSalesinvoiceAsync(SalesinvoiceMasterT salesinvoiceMaster  ,
        //        SalesinvoiceT salesinvoice,SalesinvoiceObject salesinvoiceObject);
        //}
        public Task<ResponseObject> CreateSalesinvoiceAsync(SalesinvoiceObject salesinvoiceObject);

        IEnumerable<SalesinvoiceObjectReport> GetAllsalesinvoice(string SPName);
        /// <summary>
        /// Get new Invoice Report
        /// </summary>
        //public bool GetInvoiceReportByID(int SellingMasterID);
    }


}
