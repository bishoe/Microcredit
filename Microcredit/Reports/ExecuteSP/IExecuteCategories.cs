using Microcredit.Models;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Microcredit.Reports.ExecuteSP
{
    public interface IExecuteCategories
    {

        public IEnumerable<CategoriesT> ExecuteSPCategories([Optional] string SPName
                  );
        public string GetHTMLString([Optional] SqlParameter ParamValue);
        //public string GetHTMLString();

    }
}
