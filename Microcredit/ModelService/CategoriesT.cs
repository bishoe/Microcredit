using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Microcredit.Models
{
    //TODO Edit Migration DB
    //*** Edit Migration in visual studio add Memory Table and Date Time and Add new name of Tables
    //**

    public class CategoriesT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryProductId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string? CategoryName { get; set; }
        public DateTime DateAdd { get; set; }
        [Required]
        public int UsersID { get; set; }


    }
    public class ProductsT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdouctsID { get; set; }
        [Required]
        [ForeignKey("CategoryProductId")]
        public int CategoryProductId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string? ProdouctName { get; set; }
        public DateTime DateAdd { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string? Notes { get; set; }
        [Required]
        public int BarCodeText { get; set; }
        public int UsersID { get; set; }

        public ICollection<PermissionToEntertheStoreProductT>? PermissionToEntertheStoreProduct { get; set; }


    }
}