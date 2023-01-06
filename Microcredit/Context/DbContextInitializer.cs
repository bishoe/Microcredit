using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Context
{
    public class DbContextInitializer
    {
        public static async Task Initialize(ApplicationDbContext applicationDbContext)
        {
            await applicationDbContext.Database.EnsureCreatedAsync();
             
        }
    }
}
