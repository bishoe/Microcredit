using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class ManageStoreT    /// Stores   ManageStoreId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManageStoreID { get; set; }
        [Required]
        [MaxLength(50)]
        public string? ManageStorename { get; set; }
        [Required]

        public DateTime DateAdd { get; set; }
        [Required]

        public int UserID { get; set; }

        public ICollection<PermissionToEntertheStoreProductT>? PermissionToEntertheStoreProduct { get; set; }

    }
}
