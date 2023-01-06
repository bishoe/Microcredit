using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class ProductsWarehouseT
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StoreId { get; set; }
        [Required]
            public int SuppliersID { get; set; }
        //[Required]
        //public int SupplierRID { get; set; }
        //[ForeignKey("MasterStoreID")]
        //[Required]
        //public int MasterStoreID { get; set; }
        [Required]
        public int Billno { get; set; }
        [Required]
            public int ReceivingpermissionId { get; set; }
        [Required]
        public int MasterStoreID { get; set; }
        //public int BranchCode { get; set; }
        [Required]
            public int CategoryProductId { get; set; }
            [Required]
            public int ProdouctsID { get; set; }
            [Required]
            //[Range(2, 15)]
            public decimal PurchasingPrice { get; set; }
            [Required]
            //[Range(2, 15)]
            public decimal SellingPrice { get; set; }
            [Required]
            public DateTime Productiondate { get; set; }
            [Required]
            public DateTime ExpireDate { get; set; }
            [Required]
            public int SizeProducts { get; set; }
            [Required]
            public int UnitesId { get; set; }
            [Required]
            //[Range(2, 15)]
            public decimal TotalAmountRow { get; set; }
            //[Required]
            //[Range(2, 15)]
            //public decimal TotalSize { get; set; }
            [Required]
            public int QuntityProduct { get; set; }
            [Required]
            public int QtStartPeriod { get; set; }
            [Required]
            public DateTime Dateofregistration { get; set; }
            [Required]
            public bool Anexpiredproduct { get; set; }
        public int Nocolumn { get; set; }


    }
    }

