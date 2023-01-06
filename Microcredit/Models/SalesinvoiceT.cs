using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class SalesinvoiceMasterT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellingMasterID { get; set; }
        public DateTime DateAdd { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public int Discount { get; set; }
        [Required]
        public decimal Tax { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalBDiscount { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal AMountDicount { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalPrice { get; set; }
        public int EmployeeId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public int UsersID { get; set; }
        public bool IsDelete { get; set; }
        //Use relationship one - many
        //public List<SalesinvoiceT> Salesinvoice { get; set; }
        //public List<EmployeesT> Employee { get; set; }

    }


    public class SalesinvoiceT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellingId { get; set; }
        [ForeignKey("SellingMasterID")]
        public int SellingMasterID { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        //[Required]
        //public int UnitesId { get; set; }
        [Required]
        public int Quntity_Product { get; set; }
        [Required]
        [Range(2,15)]
        public decimal SellingPrice { get; set; }
        //[Required]
        //public int Billno { get; set; }
        //[Required]
        //public int CategoryProductId { get; set; }
        [Required]
         [Range(2, 15)]
        public decimal TotalAmountRow { get; set; }
        public int UsersID { get; set; }
      //public   SalesinvoiceMasterT SalesinvoiceMaster { get; set; }
        //public List<ProductsT> Products { get; set; }

    }
}
