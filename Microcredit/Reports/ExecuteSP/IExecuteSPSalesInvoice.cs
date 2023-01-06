using Microcredit.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
namespace Microcredit.Reports.ExecuteSP
{
    public interface IExecuteSPSalesInvoice
    {
        public IEnumerable<ReportSalesInvoiceById> ExecuteSPSalesInvoice(string SPName, [Optional] SqlParameter ParamValue);


    }
}
