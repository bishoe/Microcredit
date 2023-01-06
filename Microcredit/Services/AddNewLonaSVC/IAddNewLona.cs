using Microcredit.ClassProject;
using Microcredit.Models;
using Microcredit.ModelService;

namespace Microcredit.Services.AddNewLonaSVC
{
    public interface IAddNewLona
    {

        IEnumerable<AddNewLoanObjectModel> GetAllLonaAsync(string SPName);

        Task<AddNewLoanObjectModel> GetLonaByidAsync(int LonaId);

        Task<ResponseObject> CreateNewLona(AddNewLonaMasterModel addNewLonaMasterModel, AddnewLonaDetailsModel addnewLonaDetailsModel, AddNewLoanObjectModel addNewLoanObjectModel);

        Task<bool> UpdateLonaAsync(int LonaId, AddNewLoanObjectModel addNewLoanObjectModel);

        Task<bool> DeleteLonaAsync(int LonaId);
    }
}
