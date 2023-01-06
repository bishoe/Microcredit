using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
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
