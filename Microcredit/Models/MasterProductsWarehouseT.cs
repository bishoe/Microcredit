using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternalShop.Models
{


    public class MasterProductsWarehouseT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MasterStoreID { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public DateTime DateAdd { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        [Range(2,15)]
        public decimal TotalPrice { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalBDiscount { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal AMountDicount { get; set; }
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Notes { get; set; }

        public int UsersID { get; set; }
        public  int Tax { get; set; }
    }
    
    
}
