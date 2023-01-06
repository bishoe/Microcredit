using Microcredit.Models;

namespace Microcredit.ClassProject.PermissionToEntertheStoreProductSVC
{
    public interface IPermissionToEntertheStoreProduct
    {

        //Task<List<PermissionToEntertheStoreProductT>> GetAllPermissionToEntertheStoreProductAsync();

        Task<PermissionToEntertheStoreProductT> GetPermissionToEntertheStoreProductByidAsync(int PermissionToEntertheStoreProductId);

        Task<ResponseObject> CreatePermissionToEntertheStoreProductAsync(PermissionToEntertheStoreProductT PermissionToEntertheStoreProduct);

        Task<bool> UpdatePermissionToEntertheStoreProductAsync(int PermissionToEntertheStoreProductId, PermissionToEntertheStoreProductT PermissionToEntertheStoreProduct);

        Task<bool> DeletePermissionToEntertheStoreProductAsync(int PermissionToEntertheStoreProductId);
        public IEnumerable<ReportPermissionToEntertheStoreProduct> GetAllPermissionToEntertheStoreProductAsync(string SPName);


    }
}
