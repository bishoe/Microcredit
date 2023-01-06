using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class ConvertofStoresT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConvertofStoresId { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }
        [Required]
        //public int ManageStoreId { get; set; }
        public int ManageStoreIdFrom { get; set; }
        [Required]
        public int ManageStoreIdTo { get; set; }

        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        [Required]
        public DateTime DateEdit { get; set; }
        [Required]

        public int UserID { get; set; }
    }

    public class ReportConvertofStoresT
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ConvertofStoresId { get; set; }

        [MaxLength(250)]
        public string? Notes { get; set; }
        [Required]
        //public int ManageStoreId { get; set; }
        public int ManageStoreIdFrom { get; set; }
        //[Required]
        //public int ManageStoreIdTo { get; set; }
        public string? ProdouctName { get; set; }
        //[Required]
        //public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        [Required]
        //public DateTime DateEdit { get; set; }
        public string? ManageStorename { get; set; }
        //[Required]  
        //public virtual int UserID { get; set; }
    }
}
