using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class ConvertofStoresT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConvertofStoresId { get; set; }
        
        [MaxLength(250)]
        public string Notes { get; set; }
         [Required]
        //public int MasterOFSToresId { get; set; }
        public int MasterOFSToresIdFrom { get; set; }
        [Required]
        public int MasterOFSToresIdTo { get; set; }

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
