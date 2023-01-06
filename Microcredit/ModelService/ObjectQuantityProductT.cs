using System.ComponentModel.DataAnnotations;

namespace Microcredit.Models
{
    public class ObjectQuantityProductT
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ProdouctsID { get; set; }
        [Required]
        public int CurrentQTProduct { get; set; } ////Q !=q
        [Required]
        public int NewQtProduct { get; set; }
    }
}
