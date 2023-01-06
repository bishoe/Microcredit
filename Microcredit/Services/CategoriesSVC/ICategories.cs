using Microcredit.Models;

namespace Microcredit.ClassProject
{
    public interface ICategories
    {
        IEnumerable<CategoriesT> GeTCategoriesAsync(string SPName);
        Task<CategoriesT> GeTCategoriesByIdAsync(int CategoryProductId);

        Task<ResponseObject> CreateCategoriesAsync(CategoriesT categoriesViewModel);

        Task<ResponseObject> UpdateCategoriesAsync(int CategoryProductId, CategoriesT categories);

        Task<bool> DeleteCategoriesAsync(int CategoryProductId);

    }
}
