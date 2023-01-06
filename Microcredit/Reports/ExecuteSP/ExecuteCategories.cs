using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteCategories : IExecuteCategories
    {
        private readonly ApplicationDbContext _db;
        public ExecuteCategories(ApplicationDbContext db)

        {
            _db = db;

        }
        public IEnumerable<CategoriesT> ExecuteSPCategories(string SPName)
        {
            var result = _db.Categories.FromSqlRaw(SPName).ToList();

            return (result);
        }


        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "@BranchCode", Value = ParamValue };

            var CategoriesObject
                =
ExecuteSPCategories("dbo.SP_CreateReportCategories");

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center' style='margin: 0 0 40px 0;color:blue;
    width: 100%;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    display: table; direction: rtl;'>
                                    <tr>
                                         <th>اسم القسم</th>
                                        <th>تاريخ الاضافة</th>
                                      


 
                                    </tr>");
            foreach (var _CategoriesObject in CategoriesObject)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                  
                                   
                                    </tr>
",
 _CategoriesObject.CategoryName,
 _CategoriesObject.DateAdd);
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }


    }
}

