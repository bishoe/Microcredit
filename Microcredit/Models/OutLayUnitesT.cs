using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class OutLayUnitesT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OutLayID { get; set; }
        [Required]
         [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string QName { get; set; }


    }

    public class Qutaly
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QutalyID { get; set; }
        [ForeignKey("OutLayID")]
        [Required]
        public int OutLayUnID { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Notes { get; set; }
        public DateTime DateAdd { get; set; }
        public int UsersID { get; set; }
    }
}
