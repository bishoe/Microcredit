using Microcredit.ClassProject;
using Microcredit.ModelService;

namespace Microcredit.Services.PaymentOfistallmentsSVC
{
    public class PaymentOfistallmentsSVC : IPaymentOfistallments
    {
        public Task<ResponseObject> CreatePaymentOfistallmentsAsync(PaymentOfistallmentsModel paymentOfistallmentsModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentOfistallmentsModel> GetAllPaymentOfistallmentsAsync(string SPName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePaymentOfistallments(int PaymentId, PaymentOfistallmentsModel paymentOfistallmentsModel)
        {
            throw new NotImplementedException();
        }
    }
}
