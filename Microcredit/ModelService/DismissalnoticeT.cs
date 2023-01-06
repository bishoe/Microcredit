using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class DismissalnoticeT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DismissalnoticeId { get; set; }
        [Required]

        public int ManageStoreId { get; set; }
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
}
