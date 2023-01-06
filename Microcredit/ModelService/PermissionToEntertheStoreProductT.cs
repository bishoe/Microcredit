using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class PermissionToEntertheStoreProductT
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionToEntertheStoreProductId { get; set; }
        [Required]
        public int ManageStoreId { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        //public DateTime DateEdit { get; set; }
        [Required]
        //public int UserID { get; set; }
        public ProductsT? Products { get; set; }
        public ManageStoreT? ManageStore { get; set; }

    }
    public class ReportPermissionToEntertheStoreProduct
    {
        [Key]
        public int PermissionToEntertheStoreProductId { get; set; }
        [Required]
        public int ManageStoreId { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        [Required]
        public virtual string? ProdouctName { get; set; }
        //[OptionalField]
        //public int   UserID { get; set; }
        public virtual string? ManageStorename { get; set; }
    }
}
