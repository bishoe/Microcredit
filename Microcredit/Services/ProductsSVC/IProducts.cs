using Microcredit.Models;

namespace Microcredit.ClassProject
{
    public interface IProducts
    {
        IEnumerable<ProductsT> GetProductsAsync(string SPName);

        Task<ProductsT> GetProductsByIdAsync(int ProdouctsID);
        Task<ProductsT> GetProductbyBarcode(int Barcode);

        Task<ResponseObject> CreateProductsAsync(ProductsT products);

        Task<bool> UpdateProductsAsync(int ProdouctsID, ProductsT productsT);

        Task<bool> DeleteProductsAsync(int ProdouctsID);

    }
}
