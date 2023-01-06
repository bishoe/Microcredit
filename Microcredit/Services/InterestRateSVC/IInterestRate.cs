using Microcredit.ClassProject;
using Microcredit.ModelService;

namespace Microcredit.Services.InterestRateSVC
{
    public interface IInterestRate
    {
        IEnumerable<InterestRateModel> GetAllInterestAsync(string SPName);

        Task<ResponseObject> CreateInterestRateAsync( InterestRateModel interestRate);

        Task<bool> UpdateInterestRate(int InterestRateId, InterestRateModel interestRate);
    }
}
