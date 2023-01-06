using Microcredit.Models;

namespace Microcredit.ClassProject.MasterOFSToresSVC
{
    public interface IManageStore
    {

        // insert products in stores 
        //ManageStoreT   MasterOFstores 
        IEnumerable<ManageStoreT> GetAllManageStoreAsync(string SPName);

        Task<ManageStoreT> GetManageStoreByidAsync(int ManageStoreId);

        Task<ResponseObject> CreateManageStoreAsync(ManageStoreT ManageStoreT);

        Task<bool> UpdateManageStoreAsync(int ManageStoreId, ManageStoreT ManageStoreT);

        Task<bool> DeleteManageStoreAsync(int ManageStoreId);

    }
}
