using Microcredit.Models;

namespace Microcredit.ClassProject.QuantityProductSVC
{
    public interface IQuantityProduct
    {
        Task<QuantityProductT> GetQuantityProductBYIDandManageStoreIdAsync(int manageStoreID, int ProdouctsID);
        //  public async Task<object> GetQt(int ProdouctsID, int BranchCode);
        //  Task<bool> UpdateQTafterSelling(int ProductId, QuantityProductT _ObjectQuantityProductT);
        Task<ResponseObject> AddQtProduct(int ProdouctsID);
        IEnumerable<ReportQuantityProductT> GetAllquantityProducts(string SPName);

        Task<ResponseObject> UpdateQTafterSelling(int ProductId, ObjectQuantityProductT _ObjectQuantityProduct);


        //IEnumerable<ReportQuantityProductT> GetAlLQuantity(string SPName);

    }
}
