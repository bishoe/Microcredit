using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class CustomersT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2 )]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(11)]
        public string CustomerPhone { get; set; }
        [Required]
        [StringLength(250)]
        public string CustomerAddress { get; set; }
        
         public DateTime DateAdd { get; set; }

        public DateTime DateEdit { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
        public string Notes { get; set; }
        public int UsersID { get; set; }
    }
}
