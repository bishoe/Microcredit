using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject
{
    public class CategoriesSVC : ICategories
    {
        private readonly ApplicationDbContext _db;


        public CategoriesSVC(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }
        public IEnumerable<CategoriesT> GeTCategoriesAsync(string SPName)
        {
            //List<CategoriesT> CategoriesModel = new ();
            //try
            //{


            //}
            //catch (Exception ex)
            //{

            //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
            //                          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            //}
            GC.Collect();
            return _db.Categories.FromSqlRaw("select * from " + SPName).ToList();


        }
        public async Task<CategoriesT> GeTCategoriesByIdAsync(int CategoryProductId)
        {

            var categories = (CategoriesT)null;
            try
            {
                //await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
                //categoriesViewModels = await _db.categories.Where(x => x.CategoryProductId == CategoryProductId).ToListAsync();
                if (CategoryProductId != 0)
                {
                    categories = await _db.Categories.FindAsync(CategoryProductId);
                }


            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return categories;




        }



        public async Task<ResponseObject> CreateCategoriesAsync(CategoriesT categoriesViewModel)
        {

            // Will hold all the errors related to registration
            //var errorList = new List<string>();
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var addCategories = new CategoriesT
                {
                    CategoryName = categoriesViewModel.CategoryName,
                    UsersID = categoriesViewModel.UsersID = 1
                };
                var result = await _db.Categories.AddAsync(categoriesViewModel);
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                await dbContextTransaction.RollbackAsync();
                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();

            }
            GC.Collect();

            return responseObject;




        }
        public async Task<ResponseObject> UpdateCategoriesAsync(int CategoryProductId, CategoriesT categories)
        {
            ResponseObject responseObject = new();

            if (categories.CategoryProductId == CategoryProductId)
            {

                _db.Entry(categories).State = EntityState.Modified;
            }

            try
            {
                if (categories == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                responseObject.IsValid = true;
                responseObject.Message = "Success";
                responseObject.Data = DateTime.Now.ToString();
                //return responseObject;
                //return true;
            }
            catch (Exception ex)
            {
                if (!CategoryExists(CategoryProductId))

                    Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();

            }
            GC.Collect();

            return responseObject;

        }


        private bool CategoryExists(int id)
        {
            return _db.Categories.Any(e => e.CategoryProductId == id);
        }

        public async Task<bool> DeleteCategoriesAsync(int CategoryProductId)
        {

            var GETCategoryProductId = await _db.Categories.FindAsync(CategoryProductId);

            ResponseObject responseObject = new();

            if (GETCategoryProductId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.Categories.Remove(GETCategoryProductId);
            _db.SaveChanges();
            GC.Collect();

            return true;
        }
    }
}

