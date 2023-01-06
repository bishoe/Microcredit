using System.ComponentModel.DataAnnotations;

namespace Microcredit.Models
{
    public class SalesinvoiceObject
    {
        [Key]
        public int SellingMasterID { get; set; }
        public DateTime DateAdd { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public int UsersID { get; set; }
        public int SellingId { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int Quntity_Product { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal SellingPrice { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalPrice { get; set; }
        [Required]
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
        public decimal TotalAmountRow { get; set; }
        [Range(2, 15)]
        public decimal AmountPaid { get; set; }
        [Range(2, 15)]
        public decimal RemainingAmount { get; set; }
        public virtual int? Nocolumn { get; set; }
        

    }
}

public class SalesinvoiceObjectReport { 




    [Key]
    public int SellingMasterID { get; set; }
    public DateTime DateAdd { get; set; }
     public int SellingId { get; set; }
     public int ProdouctsID { get; set; }
     public int Quntity_Product { get; set; }
     public decimal SellingPrice { get; set; }

    public decimal TotalPrice { get; set; }
    
     public virtual int? Nocolumn { get; set; }
    public string ProdouctName { get; set; }

}
