using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class BranchesT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchID { get; set; }
        [Required]
        //[MaxLength(8)]
        public int BranchCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string BranchName { get; set; }
        [Required]
        [MaxLength(250)]
        public string BranchAddress { get; set; }
        [Required]
        [MaxLength(9)]
        public string BranchPhone { get; set; }
        [Required]
        public int MasterOFSToresID { get; set; }
        [Required]
        [MaxLength(11)]
        public string BranchMobile { get; set; }
         public DateTime DateAdd { get; set; }
        public DateTime DateEdit  { get; set; }
        public int USerID { get; set; }
    }
}
