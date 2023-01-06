using Microcredit.Models;

namespace Microcredit.ClassProject.DismissalnoticeSVC
{
    public interface IDismissalnotice
    {
        IEnumerable<DismissalnoticeT> GetAllDismissalnoticeAsync(string SPName);

        Task<DismissalnoticeT> GetDismissalnoticeByidAsync(int DismissalnoticeId);

        Task<ResponseObject> CreateDismissalnoticeAsync(DismissalnoticeT dismissalnotice);

        Task<bool> UpdateDismissalnoticeAsync(int IdDismissalnotice, DismissalnoticeT dismissalnotice);

        Task<bool> DeleteDismissalnoticeAsync(int DismissalnoticeId);

    }
}
