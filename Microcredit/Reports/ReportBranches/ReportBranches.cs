//using InternalShop.Models;
//using InternalShop.Reports.ExecuteSP;
//using InternalShop.Reports.ReportSalesInvoice;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InternalShop.Reports.ReportBranches
//{
//    public class ReportBranches : IReportS
//    {
//        private readonly IExecuteBranches _executeSp;



//        public string GetHTMLString(int ParamValue)
//            {

//                var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

//                var BranchesObject = _executeSp.ExecuteSPBranches("dbo.SP_CreateReportBranchesBYCode @BranchCode", sqlParms);

//                var sb = new StringBuilder();
//                sb.Append(@"
//                        <html>
//                            <head>
   

//                            </ head>
//                            <body>
//<img src='' alt='Girl in a jacket' width='' height=''>
//                                <table align='center'>
//                                    <tr>
//                                         <th>BranchID</th>
//                                        <th>BranchCode</th>
//                                        <th>BranchName</th>
//                                        <th>BranchAddress</th>
//                                         <th>BranchPhone</th>
//                                         <th>BranchMobile</th>
//                                         <th>WearhouseBranche</th>


 
//                                    </tr>");
//                foreach (var _BranchesObject in BranchesObject)
//                {
//                    sb.AppendFormat(@"<tr>
//                                    <td>{0}</td>
//                                    <td>{1}</td>
//                                    <td>{2}</td>
//                                    <td>{3}</td>
//                                    <td>{4}</td>
//                                    <td>{5}</td>
//                                    <td>{6}</td>
//                                    </tr>
//",
//     _BranchesObject.BranchID,
//     _BranchesObject.BranchCode,
//                                 _BranchesObject.BranchName,
//                                _BranchesObject.BranchAddress,
// _BranchesObject.BranchPhone,
//  _BranchesObject.BranchMobile);
//                }
//                sb.Append(@"</table></body></html>");


//                return sb.ToString();
//            }
//        }
//    }
 