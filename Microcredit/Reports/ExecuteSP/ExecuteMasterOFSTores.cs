using DataBaseService;
using InternalShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InternalShop.Reports.ExecuteSP
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

            var MasterOFSToresObject
                =
ExecuteSPManageStore("dbo.SP_CreateReportMasterOFSToresById @MasterOFSToresID", ParamValue);

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
                                 <table align='center' style='margin: 0 0 40px 0;
    width: 100%;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    display: table;'>
                                    <tr>
                                         <th>رقم المخزن</th>
                                        <th>اسم المخزن</th>
                                     


 
                                    </tr>");
            foreach (var _MasterOFSToresObject in MasterOFSToresObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    
                                   
                                    </tr>
",
 _MasterOFSToresObject.ManageStoreID,
 _MasterOFSToresObject.ManageStorename );
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }

        public string GetHTMLString()
        {

            var MasterOFSToresObjectObject
                =
ExecuteSPManageStore("dbo.SP_CreateReportMasterOFSTores");

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
            foreach (var _MasterOFSToresObjectObject in MasterOFSToresObjectObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    
                                   
                                    </tr>
",
 _MasterOFSToresObjectObject.ManageStoreID,
 _MasterOFSToresObjectObject.ManageStorename);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }
    }
}
