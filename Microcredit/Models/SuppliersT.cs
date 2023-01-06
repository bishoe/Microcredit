using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace InternalShop.Models
{
    public class SuppliersT
    {
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuppliersID { get; set; }
        [Required]
        [MaxLength(40)]
        public string SuplierName { get; set; }
        [MaxLength(11)]
        public string SuplierPhone { get; set; }
        [Required]
        [MaxLength(250)]
        public string SuplierAddress { get; set; }
         public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }
        public int UsersID { get; set; }
    }
    public class SupplyRepresentativeT
    {
       [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuppliersID { get; set; }
        [ForeignKey("SuppliersID")]
        public int SupplierRID { get; set; }
        [Required]
        [MaxLength(30)]
        public string SupplierRName { get; set; }
        [Required]
        [MaxLength(11)]
        public string SupplierRPhone { get; set; }
        [Required]
        [MaxLength(250)]
        public string SupplierRAddress { get; set; }
         public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }

        [Required]
        [MaxLength(250)]
        public string SupplierRNotes { get; set; }
        [Required]
        public int UsersID { get; set; }





    }}