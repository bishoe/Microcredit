using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteDismissalnotice : IExecuteDismissalnotice
    {
        private readonly ApplicationDbContext _db;
        public ExecuteDismissalnotice(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<ReportDismissalnotice> ExecuteSPDismissalnotice(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.reportDismissalnotices.FromSqlRaw("select * from " + SPName, ParamValue).ToList();

            _db.Dispose();
            return (result);
        }

        public string GetHTMLString(SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

            var DismissalnoticeObject
                =
ExecuteSPDismissalnotice("dbo.SP_CreateReportDismissalnoticeById @DismissalnoticeId", ParamValue);
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
                                 <table align='center' style='margin: 0 0 40px 0;color:blue;
    width: 100%;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    display: table; direction: rtl;'>
                                    <tr>
                                         <th>رقم اذن الصرف</th>
                                        <th>رقم المخزن</th>
                                        <th>اسم المنتج</th>
                                        <th>الباركود</th>
                                         <th>الكميه</th>
                                         <th>التاريخ</th>
 

 
                                    </tr>");
            foreach (var _DismissalnoticeObject in DismissalnoticeObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    </tr>",
 _DismissalnoticeObject.DismissalnoticeId,
 _DismissalnoticeObject.ManageStoreID,
 _DismissalnoticeObject.ProdouctName,
 _DismissalnoticeObject.BarCodeText,
_DismissalnoticeObject.quantityProduct,
_DismissalnoticeObject.DateAdd);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }



        public string GetHTMLStringWithoutParam()
        {


            var DismissalnoticeObject
                =
ExecuteSPDismissalnotice("dbo.view_CreateReportDismissalnotice");

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center'>
                                    <tr>
                                       <th>رقم اذن الصرف</th>
                                        <th>رقم المخزن</th>
                                        <th>اسم المنتج</th>
                                        <th>الباركود</th>
                                         <th>الكميه</th>
                                         <th>التاريخ</th>
 

 
                                    </tr>");
            foreach (var _DismissalnoticeObject in DismissalnoticeObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    </tr>",
 _DismissalnoticeObject.DismissalnoticeId,
 _DismissalnoticeObject.ManageStoreID,
 _DismissalnoticeObject.ProdouctName,
 _DismissalnoticeObject.BarCodeText,
_DismissalnoticeObject.quantityProduct,
_DismissalnoticeObject.DateAdd);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }


    }
}

