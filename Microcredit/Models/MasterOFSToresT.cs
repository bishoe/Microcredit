using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class MasterOFSToresT    /// Stores   MasterOFSToresID
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MasterOFSToresID { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameMasterOFSTores { get; set; }
        [Required]

        public DateTime DateAdd { get; set; }
        [Required]

        public int UserID { get; set; }
    }
}
