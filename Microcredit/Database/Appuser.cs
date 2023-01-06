using Microsoft.AspNetCore.Identity;

namespace Microcredit
{
    public class Appuser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
