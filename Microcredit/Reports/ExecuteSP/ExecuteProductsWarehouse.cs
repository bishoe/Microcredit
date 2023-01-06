
using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteProductsWarehouse : IExecuteProductsWarehouse
    {
        private readonly ApplicationDbContext _db;
        public ExecuteProductsWarehouse(ApplicationDbContext db)
        {
            _db = db;

        }
        //public IEnumerable<ReportProductsWarehouse> ExecuteSPProductsWarehouse(string SPName, [Optional] SqlParameter ParamValue)
        //{
        //    var result = _db.Products.FromSqlRaw(SPName, ParamValue).ToList();
        //     return (result);

        //}
        public IEnumerable<ReportProductsWarehouse> ExecuteSPProductsWarehouse(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.reportDismissalnotices.FromSqlRaw("select * from " + SPName, ParamValue).ToList();

            _db.Dispose();
            return ((IEnumerable<ReportProductsWarehouse>)result);
        }

        public string GetHTMLString(SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

            var ProductsWarehousebject
                =
ExecuteSPProductsWarehouse("dbo.SP_CreateReportProductsWarehouseBYManageStoreID @ManageStoreID", ParamValue);
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
                                          <th>رقم فاتورة المورد</th>
                                          <th>اسم المورد</th>
                                        <th>اذن الاستلام</th>
                                         <th>القسم</th>
                                         <th>المنتج</th>
                                         <th>سعر الشراء</th>
                                         <th>سعر البيع</th>
                                        <th>تاريخ الانتاج</th>
                                        <th>تاريخ الانتهاء</th>
                                        <th>حجم المنتج</th>
                                        <th>اجمالى الصف</th>
                                        <th>كمية المنتج</th>
                                        <th>الكميه اول المده</th>
                                        <th>التاريخ</th>
                                        <th>اسم الموظف</th>
                                    </tr>");
            foreach (var _ProductsWarehousebject in ProductsWarehousebject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                    <td>{8}</td>
                                    <td>{9}</td>
                                    <td>{10}</td>
                                    <td>{11}</td>
                                    <td>{12}</td>
                                    <td>{13}</td>
  
                                    </tr>",
 _ProductsWarehousebject.Billno,
 _ProductsWarehousebject.PermissionToEntertheStoreProductId,
 _ProductsWarehousebject.CategoryName,
 _ProductsWarehousebject.ProdouctName,
_ProductsWarehousebject.PurchasingPrice,
_ProductsWarehousebject.SellingPrice
, _ProductsWarehousebject.Productiondate
, _ProductsWarehousebject.ExpireDate
, _ProductsWarehousebject.SizeProducts
, _ProductsWarehousebject.TotalAmountRow
, _ProductsWarehousebject.QuntityProduct
, _ProductsWarehousebject.QtStartPeriod
, _ProductsWarehousebject.Dateofregistration

, _ProductsWarehousebject.EmployeeName



);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }



        public string GetHTMLStringWithoutParam()
        {


            var ProductsWarehousebject
                =
ExecuteSPProductsWarehouse("dbo.view_CreateReportProductsWarehouse");

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
                                            <th>رقم فاتورة المورد</th>
                                          <th>اسم المورد</th>
                                        <th>اذن الاستلام</th>
                                         <th>القسم</th>
                                         <th>المنتج</th>
                                         <th>سعر الشراء</th>
                                         <th>سعر البيع</th>
                                        <th>تاريخ الانتاج</th>
                                        <th>تاريخ الانتهاء</th>
                                        <th>حجم المنتج</th>
                                        <th>اجمالى الصف</th>
                                        <th>كمية المنتج</th>
                                        <th>الكميه اول المده</th>
                                        <th>التاريخ</th>
                                        <th>اسم الموظف</th>
                                    </tr>");
            foreach (var _ProductsWarehousebject in ProductsWarehousebject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                    <td>{8}</td>
                                    <td>{9}</td>
                                    <td>{10}</td>
                                    <td>{11}</td>
                                    <td>{12}</td>
                                    <td>{13}</td>
   
                                    </tr>",
 _ProductsWarehousebject.Billno,
 _ProductsWarehousebject.PermissionToEntertheStoreProductId,
 _ProductsWarehousebject.CategoryName,
 _ProductsWarehousebject.ProdouctName,
_ProductsWarehousebject.PurchasingPrice,
_ProductsWarehousebject.SellingPrice
, _ProductsWarehousebject.Productiondate
, _ProductsWarehousebject.ExpireDate
, _ProductsWarehousebject.SizeProducts
, _ProductsWarehousebject.TotalAmountRow
, _ProductsWarehousebject.QuntityProduct
, _ProductsWarehousebject.QtStartPeriod
, _ProductsWarehousebject.Dateofregistration
 , _ProductsWarehousebject.EmployeeName



);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }
    }
}
