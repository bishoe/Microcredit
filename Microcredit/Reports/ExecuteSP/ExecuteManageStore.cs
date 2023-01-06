
using Microcredit.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteManageStore : IExecuteManageStore
    {
        private readonly ApplicationDbContext _db;
        public ExecuteManageStore(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<ManageStoreT> ExecuteSPManageStore(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.ManageStore.FromSqlRaw(SPName, ParamValue).ToList();
            _db.Dispose();
            return (result);
        }

        public string GetHTMLString(SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

            var ManageStoreObject
                =
ExecuteSPManageStore("dbo.SP_CreateReportmanageStoreById @ManageStoreID", ParamValue);

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center' style='margin: 0 0 40px 0;
    width: 100%;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    display: table;'>
                                    <tr>
                                         <th>رقم المخزن</th>
                                        <th>اسم المخزن</th>
                                     


 
                                    </tr>");
            foreach (var _ManageStoreObject in ManageStoreObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    
                                   
                                    </tr>
",
 _ManageStoreObject.ManageStoreID,
 _ManageStoreObject.ManageStorename);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }

        public string GetHTMLStringWithoutParam()
        {

            var ManageStoreObject
                =
ExecuteSPManageStore("dbo.view_CreateReportManageStore");

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center'>
                                    <tr>
                                         <th>رقم المخزن</th>
                                        <th>اسم المخزن</th>
                                     


 
                                    </tr>");
            foreach (var _ManageStoreObject in ManageStoreObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    
                                   
                                    </tr>
",
 _ManageStoreObject.ManageStoreID,
 _ManageStoreObject.ManageStorename);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }
    }
}
