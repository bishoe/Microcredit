using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microcredit.GETErr
{
    public class MyBadRequestObjectResult : BadRequestObjectResult, IClientErrorActionResult
    {
        public MyBadRequestObjectResult() : base((object)null)
        {
        }

        public MyBadRequestObjectResult(object error) : base(error)
        {
        }
    }
}