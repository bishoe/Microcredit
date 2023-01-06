using System.ComponentModel.DataAnnotations;

namespace Microcredit.Models
{
    public class ReportProductsWarehouse
    {
        [Key]
        public int ManageStoreID { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateAdd { get; set; }
        public int Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalBDiscount { get; set; }
        public decimal AMountDicount { get; set; }
        public string Notes { get; set; }
        public int UsersID { get; set; }
        public int StoreId { get; set; }
        public string CategoryName { get; set; }
        public int SuppliersID { get; set; }
        public int Tax { get; set; }

        public int Billno { get; set; }
        public int PermissionToEntertheStoreProductId { get; set; }
        public int CategoryProductId { get; set; }

        public int ProdouctsID { get; set; }
        public string ProdouctName { get; set; }

        public decimal PurchasingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime Productiondate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int SizeProducts { get; set; }
        public int UnitesId { get; set; }
        public decimal TotalAmountRow { get; set; }
        public int QuntityProduct { get; set; }
        public int QtStartPeriod { get; set; }
        public DateTime Dateofregistration { get; set; }
        public bool Anexpiredproduct { get; set; }
    }
}
