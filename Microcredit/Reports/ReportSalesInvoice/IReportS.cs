using System.Runtime.InteropServices;

namespace Microcredit.Reports.ReportSalesInvoice
{
    public interface IReportS
    {
        //public IEnumerable<ReportSalesInvoiceById> CreateReportSalesInvoice(string SPName, Microsoft.Data.SqlClient.SqlParameter ParamValue);
        public string GetHTMLString([Optional] int ParamValue);
    }
}
