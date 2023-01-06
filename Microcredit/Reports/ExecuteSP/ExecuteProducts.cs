
using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteProducts : IExecuteProducts
    {
        private readonly ApplicationDbContext _db;
        public ExecuteProducts(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<ProductsT> ExecuteSPProducts(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.Products.FromSqlRaw(SPName, ParamValue).ToList();
            _db.Dispose();
            return (result);
        }

        public string GetHTMLString(SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

            var ProductsObject
                =
ExecuteSPProducts("dbo.SP_CreateReportProductsById @ProdouctsID", ParamValue);

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
                                          <th>رقم المنتج</th>
                                    <th>اسم المنتج</th>


 
                                    </tr>");
            foreach (var _ProductsObject in ProductsObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    
                                    </tr>
",
 _ProductsObject.ProdouctsID
 , _ProductsObject.ProdouctName);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }


        public string GetHTMLStringWithoutParam()
        {

            var ProductsObject
                =
ExecuteSPProducts("dbo.view_CreateReportProducts");

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
                                 <th>رقم المنتج</th>
                                    <th>اسم المنتج</th>

                                    </tr>");
            foreach (var _ProductsObject in ProductsObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                   
                                   
                                    </tr>
",
  _ProductsObject.ProdouctsID
 , _ProductsObject.ProdouctName
          );
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }

    }
}
