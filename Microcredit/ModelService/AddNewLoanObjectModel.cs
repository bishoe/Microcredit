 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class AddNewLoanObjectModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int LonaId { get; set; }
        public int ProdcutId { get; set; }
        public int CustomeId { get; set; }

        public int InterestRateid { get; set; }

        public int MonthNumber { get; set; }

        public DateTime StartDateLona { get; set; }

        public DateTime EndDateLona { get; set; }

        public DateTime DateAdd { get; set; }


        public decimal AmountBeforeAddInterest { get; set; }

        public decimal AmountAfterAddInterest { get; set; }

        public bool StatusLona { get; set; }



        //--------
        public int LonaDetailsId { get; set; }
         public int LonaGuarantorFirst { get; set; }
        public int LonaGuarantorSecond { get; set; }
        public int LonaGuarantorThird { get; set; }
        public int LonaGuarantorFourth { get; set; }
        public string UserID { get; set; }
        public int IstalmentsNo { get; set; }
        public int Nocolumn { get; set; }

    }

    public class AddNewLonaMasterModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LonaId { get; set; }
        public int ProdcutId { get; set; }
        public int CustomeId { get; set; }

        public int InterestRateid { get; set; }

        public int MonthNumber { get; set; }

        public DateTime StartDateLona { get; set; }

        public DateTime EndDateLona { get; set; }
        public decimal AmountBeforeAddInterest { get; set; }

        public decimal AmountAfterAddInterest { get; set; }

        public DateTime DateAdd { get; set; }
        public bool StatusLona { get; set; }
        public string UserID { get; set; }

    }

    public class AddnewLonaDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LonaDetailsId { get; set; }
        public int LonaId { get; set; }
        public int LonaGuarantorFirst { get; set; }
        public int LonaGuarantorSecond { get; set; }
        public int LonaGuarantorThird { get; set; }
        public int LonaGuarantorFourth { get; set; }
        public int IstalmentsNo { get; set; }
        public DateTime DateAdd { get; set; }


    }
}
