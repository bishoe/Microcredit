
using Microcredit.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteConvertofStores : IExecuteConvertofStores
    {
        private readonly ApplicationDbContext _db;
        public ExecuteConvertofStores(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ReportConvertofStoresT> ExecuteSPConvertofStores(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.Branches.FromSqlRaw("select * from " + SPName, ParamValue).ToList();

            _db.Dispose();
            return ((IEnumerable<ReportConvertofStoresT>)result);
        }




        public string GetHTMLString(SqlParameter ParamValue)
        {
            var ConvertofStoresObject
               =
ExecuteSPConvertofStores("dbo.SP_CreateReportConvertofStoresBYProdouctsID @ProdouctsID", ParamValue);

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   <link rel='Stylesheet' href='StyleSheet.css'>

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
 <table align='center'   style='margin: 0 0 40px 0;color:blue;
    width: 100%;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    display: table; direction: rtl;'>
 <tr>
                                        <th style='text-align: center;'>كمية المنتج</th>
                                         <th style='text-align: center;'>اسم المنتج</th>
                                         <th style='text-align: center;'>اسم المخزن</th>
 

 
                                    </tr>");
            foreach (var _ConvertofStoresObject in ConvertofStoresObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td style='text-align: center;'>{0}</td>
                                    <td style='text-align: center;'>{1}</td>
                                    <td style='text-align: center;'>{2}</td>
 
                                    </tr>",
                            _ConvertofStoresObject.quantityProduct,
_ConvertofStoresObject.ProdouctName,
_ConvertofStoresObject.ManageStorename
//,_BranchesObject.ManageStorename

);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }

        public string GetHTMLStringWithoutParam()
        {

            var ConvertofStoresObject
                =
ExecuteSPConvertofStores("dbo.View_GetAllConvertOfStores");

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center'>
                                    <tr>
                                        <th style='text-align: center;'>كمية المنتج</th>
                                         <th style='text-align: center;'>اسم المنتج</th>
                                         <th style='text-align: center;'>اسم المخزن</th>


 
                                    </tr>");
            foreach (var _ConvertofStoresObject in ConvertofStoresObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td style='text-align: center;'>{0}</td>
                                    <td style='text-align: center;'>{1}</td>
                                    <td style='text-align: center;'>{2}</td>
                                   
                                    </tr>
",
   _ConvertofStoresObject.quantityProduct,
_ConvertofStoresObject.ProdouctName,
_ConvertofStoresObject.ProdouctName);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }


    }
}

