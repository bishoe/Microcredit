using Microcredit.ClassProject;
using Microcredit.ModelService;
using Microcredit.Services.InterestRateSVC;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.Services.PaymentOfistallmentsSVC
{
    public class PaymentOfistallmentsSVC : IPaymentOfistallments
    {
        private readonly ApplicationDbContext _db;
        public PaymentOfistallmentsSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreatePaymentOfistallmentsAsync(PaymentOfistallmentsModel paymentOfistallmentsModel)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddpaymentOfistallmentsModel = new PaymentOfistallmentsModel
                {
                     CustomerId = paymentOfistallmentsModel.CustomerId,
                     AmountPaid =  paymentOfistallmentsModel.AmountPaid,
                     AmountRemaining = paymentOfistallmentsModel.AmountRemaining,
                      IstalmentsAmount= paymentOfistallmentsModel.IstalmentsAmount,
                      LonaAmount = paymentOfistallmentsModel.LonaAmount ,
                      UserId = paymentOfistallmentsModel.UserId ,
                      
                       
                      
                };
                var result = await _db.paymentOfistallments.AddAsync(paymentOfistallmentsModel);
                await _db.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
                var GetCurrentId = paymentOfistallmentsModel.PaymentId;

                responseObject.IsValid = true;
                responseObject.Message = ">" + GetCurrentId;
                responseObject.Data = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                await dbContextTransaction.RollbackAsync();

                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();
            }
            GC.Collect();

            return responseObject;
        }

        public IEnumerable<PaymentOfistallmentsModel> GetAllPaymentOfistallmentsAsync(string SPName)
        {
            return _db.paymentOfistallments.FromSqlRaw("select * from " + SPName).ToList();

        }

        public async Task<bool> UpdatePaymentOfistallments(int PaymentId, PaymentOfistallmentsModel paymentOfistallmentsModel)
        {

            ResponseObject responseObject = new();

            if (PaymentId == paymentOfistallmentsModel.PaymentId)
            {
                _db.Entry(PaymentId).State = EntityState.Modified;

            }
            try
            {
                if (PaymentId == 0 )
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {

                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
            ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                GC.Collect();

                return false;
            }


        }
    }
}
