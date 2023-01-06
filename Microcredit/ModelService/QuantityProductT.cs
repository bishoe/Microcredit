using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class QuantityProductT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QTID { get; set; }
        //[Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; } ////Q !=q
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int manageStoreID { get; set; } //manageStoreID   ID STORE
        [Required]
        public int BranchCode { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

    }



    public class ReportQuantityProductT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QTID { get; set; }
        //[Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; } ////Q !=q
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int manageStoreID { get; set; } //manageStoreID   ID STORE
        [Required]
        public int BranchCode { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

        //public string ManageStorename { get; set; }
        public string BranchName { get; set; }
        public string ProdouctName { get; set; }
    }
}
