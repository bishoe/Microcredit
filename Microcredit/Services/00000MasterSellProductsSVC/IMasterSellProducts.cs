using InternalShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.ClassProject.MasterSellProductsSVC
{
    interface IMasterSellProducts

    {
        Task<ResponseObject> CreateMasterSellProducts(SalesinvoiceT SellProductsMasterT);

    }
}
