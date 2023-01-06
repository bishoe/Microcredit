using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class QuantityProductT
    {
        [Key]
   //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      //  public int ID { get; set; }
        //[Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; } ////Q !=q
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int MasterOFSToresID { get; set; } //MasterOFSToresID   ID STORE
        [Required]
        public int BranchCode { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

    }
}
