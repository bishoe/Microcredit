using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
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
