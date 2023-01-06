using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class ReportSalesInvoiceById
    {       

        [Key]
        public int SellingMasterID { get; set; }
        public int ProdouctsID { get; set; }
        public string ProdouctName { get; set; }
        public int Quntity_Product { get; set; }

        public decimal SellingPrice { get; set; }
        public decimal TotalAmountRow { get; set; }

        public decimal AMountDicount { get; set; }

        public int Discount { get; set; }
        public decimal Tax { get; set; }

        public decimal TotalBDiscount { get; set; }

        public decimal TotalPrice { get; set; }
         public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal AmountPaid { get; set; }
 
        public decimal RemainingAmount { get; set; }


    }
}
