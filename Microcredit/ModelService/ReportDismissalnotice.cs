using System.ComponentModel.DataAnnotations;

namespace Microcredit.Models
{
    public class ReportDismissalnotice
    {
        [Key]
        public int DismissalnoticeId { get; set; }
        public int ManageStoreID { get; set; }
        public int ProdouctsID { get; set; }

        public int quantityProduct { get; set; }

        public DateTime DateAdd { get; set; }
        public int UserID { get; set; }
        public string ProdouctName { get; set; }

        public string BarCodeText { get; set; }

        public string ManageStorename { get; set; }
    }
}
