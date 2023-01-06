using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class DismissalnoticeT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DismissalnoticeId { get; set; }
        [Required]

        public int MasterOFSToresId { get; set; }
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
