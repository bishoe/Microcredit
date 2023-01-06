
using System.ComponentModel.DataAnnotations;

namespace Microcredit.ModelService
{
    public class PaymentOfistallmentsModel
    {
        [Key]
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public decimal IstalmentsAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }
        public decimal LonaAmount { get; set; }
        public DateTime DateAdd { get; set; }
        public string UserId { get; set; }

    }
}
