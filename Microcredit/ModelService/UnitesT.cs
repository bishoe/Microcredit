using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class UnitesT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitesId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UnitesName { get; set; }

        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

        [Required]
        public int UnitesConvert { get; set; }



    }
}
