using Microcredit.ClassProject;
using Microcredit.ModelService;

namespace Microcredit.Services.PaymentOfistallmentsSVC
{
    public interface IPaymentOfistallments
    {

        IEnumerable<PaymentOfistallmentsModel> GetAllPaymentOfistallmentsAsync(string SPName);

        Task<ResponseObject> CreatePaymentOfistallmentsAsync(PaymentOfistallmentsModel paymentOfistallmentsModel);

        Task<bool> UpdatePaymentOfistallments(int PaymentId, PaymentOfistallmentsModel paymentOfistallmentsModel);

    }
}
