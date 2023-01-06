using Microcredit.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExcuteSpSalesInvoice : IExecuteSPSalesInvoice
    {
        private readonly ApplicationDbContext _db;
        public ExcuteSpSalesInvoice(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ReportSalesInvoiceById> ExecuteSPSalesInvoice(string SPName, SqlParameter ParamValue)
        {
            //var sqlParms = new Microsoft.Data.SqlClient.SqlParameter { ParameterName = "ParamName", Value = ParamValue };
            //var result = _db.reportSalesInvoiceByIds.FromSqlRaw("'" + SPName + "'"  + "@"+ParamName , sqlParms).ToList();
            var result = _db.reportSalesInvoiceByIds.FromSqlRaw(SPName, ParamValue).ToList();

            return (result);



        }


    }
}
