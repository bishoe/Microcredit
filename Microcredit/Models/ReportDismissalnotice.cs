using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class ReportDismissalnotice
    {
        [Key]
        public int DismissalnoticeId { get; set; }
        public int MasterOFSToresId { get; set; }
        public int ProdouctsID { get; set; }

        public int quantityProduct { get; set; }

        public DateTime DateAdd { get; set; }
        public int UserID { get; set; }
        public string ProdouctName { get; set; }

        public string BarCodeText { get; set; }

        public string NameMasterOFSTores { get; set; }
    }
}
