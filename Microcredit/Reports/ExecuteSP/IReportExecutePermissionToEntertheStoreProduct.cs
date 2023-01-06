using Microcredit.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
namespace Microcredit.Reports.ExecuteSP
{
    public interface IReportExecutePermissionToEntertheStoreProduct
    {
        public IEnumerable<ReportPermissionToEntertheStoreProduct> ExecuteSPPermissionToEntertheStoreProduct(string SPName, [Optional] SqlParameter ParamValue);
        public string GetHTMLString([Optional] SqlParameter ParamValue);

        public string GetHTMLStringWithoutParam();

    }

}
