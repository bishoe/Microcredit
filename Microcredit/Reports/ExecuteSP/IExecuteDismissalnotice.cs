using Microcredit.Models;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Microcredit.Reports.ExecuteSP
{
    public interface IExecuteDismissalnotice
    {
        public IEnumerable<ReportDismissalnotice> ExecuteSPDismissalnotice(string SPName, [Optional] SqlParameter ParamValue);
        public string GetHTMLString([Optional] SqlParameter ParamValue);
        public string GetHTMLStringWithoutParam();
    }
}
