using Microcredit.Reports.ExecuteSP;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Microcredit.Reports.ReportSalesInvoice
{
    public class ReportSalesInvoiceSVC : IReportS
    {
        private readonly IExecuteSPSalesInvoice _executeSp;

        public ReportSalesInvoiceSVC(IExecuteSPSalesInvoice executeSp)
        {
            _executeSp = executeSp;
        }




        /// <summary>
        /// Genrate Report From Screen Salesinvoice 
        /// 1. run sp  in CreateReportSalesInvoice
        /// 2. run GetHTMLString
        /// 3.GO TO Controller
        /// </summary>
        /// <param name="SellingMasterID"></param>
        /// <returns></returns>



        /// <summary>
        /// Generate Report
        /// </summary>
        /// <param name="SellingMasterID"></param>
        /// <returns></returns>
        public string GetHTMLString(int ParamValue)
        {
            var sqlParms = new SqlParameter { ParameterName = "@SellingMasterID", Value = ParamValue };
            var SalesInvoiceObject = _executeSp.ExecuteSPSalesInvoice("dbo.SP_CreateReportSalesInvoiceById @SellingMasterID", sqlParms);
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
                                         <th>اسم المنتج</th>
                                        <th>كمية المنتج</th>
                                        <th>سعر البيع</th>
                                        <th>اجمالى الصف</th>
                                          <th>الخصم</th>
                                         <th>مبلغ الخصم</th>
                                         <th>الضريبه</th>
 
                                    </tr>");
            foreach (var _SalesInvoiceObject in SalesInvoiceObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                     </tr>
",
 _SalesInvoiceObject.ProdouctName,
 _SalesInvoiceObject.Quntity_Product,
                            _SalesInvoiceObject.SellingPrice,
                            _SalesInvoiceObject.TotalAmountRow,
                             _SalesInvoiceObject.Discount,
                            _SalesInvoiceObject.AMountDicount,
                            _SalesInvoiceObject.Tax,
                         sb.Append("المبلغ المدفوع"), sb.Append(_SalesInvoiceObject.AmountPaid),
                         sb.Append("المبلغ المتبقى"), sb.Append(_SalesInvoiceObject.RemainingAmount));
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }
    }
}
