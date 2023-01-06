using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class InterestRateModel
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestRateId { get; set; }
        public string InterestRateName { get; set; }
        public string InterestRateValue { get; set; }
        public DateTime DateAdd { get; set; }

    }
}
