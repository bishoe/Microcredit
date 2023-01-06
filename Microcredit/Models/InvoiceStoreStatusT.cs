using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class InvoiceStoreStatusT
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int MasterStoreID { get; set; }
        [Required]
        public int Billno { get; set; }
        [Required]
[Range(2,15)]
        public decimal PAIDAMOUNT { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal RemainingAMOUNT { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

        public int UserID { get; set; }



    }
}
