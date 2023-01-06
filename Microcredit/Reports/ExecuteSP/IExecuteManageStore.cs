using Microcredit.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Microcredit.Reports.ExecuteSP
{
    public interface IExecuteManageStore
    {
        public IEnumerable<ManageStoreT> ExecuteSPManageStore(string SPName, [Optional] SqlParameter ParamValue);
        public string GetHTMLString([Optional] SqlParameter ParamValue);
        //public string GetHTMLString();

        public string GetHTMLStringWithoutParam();

    }
}
