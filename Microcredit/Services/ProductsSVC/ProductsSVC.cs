using Microcredit.ClassProject.QuantityProductSVC;
using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.ProductsSVC
{
    public class ProductsSVC : IProducts
    {
        private readonly ApplicationDbContext _db;
        private readonly IQuantityProduct _quantityProduct;

        public ProductsSVC(ApplicationDbContext db, IQuantityProduct quantityProduct)
        {
            _db = db;
            _quantityProduct = quantityProduct;
        }


        public async Task<ResponseObject> CreateProductsAsync(ProductsT products)
        {
            ResponseObject responseObject = new();

            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddProducts = new ProductsT
                {
                    CategoryProductId = products.CategoryProductId,
                    ProdouctName = products.ProdouctName,
                    Notes = products.Notes,
                    BarCodeText = products.BarCodeText,
                    UsersID = 1
                };
                var result = await _db.Products.AddAsync(AddProducts);
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                var CurrentidafterinsertNewRow = AddProducts.ProdouctsID;

                //TODO GET Method insert new row in quntity product
                var resultQT = await _quantityProduct.AddQtProduct(AddProducts.ProdouctsID);


                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {


                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                await dbContextTransaction.RollbackAsync();


            }
            GC.Collect();

            return responseObject;

        }

        public async Task<bool> DeleteProductsAsync(int ProdouctsID)
        {
            var GETProdouctsID = await _db.Products.FindAsync(ProdouctsID);
            ResponseObject responseObject = new();
            if (GETProdouctsID == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.Products.Remove(GETProdouctsID);
            _db.SaveChanges();
            GC.Collect();

            return true;

        }

        public async Task<ProductsT> GetProductbyBarcode(int Barcode)
        {
            var Result = (ProductsT)null;
            try
            {
                var checkexistsBarcode = true;
                if (Barcode != 0) checkexistsBarcode = _db.Products.Any(x => x.BarCodeText == Barcode);
                if (checkexistsBarcode is true)
                {
                    Result = await _db.Products.Where(o => o.BarCodeText == Barcode)
                      .FirstOrDefaultAsync();

                }

                return Result;
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating product {Error} {StackTrace} {InnerException} {Source}",
                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();

            return Result;
        }




        public IEnumerable<ProductsT> GetProductsAsync(string SPName)
        {

            //List<ProductsT> _productsModel = new List<ProductsT>();
            //try
            //{
            //    _productsModel = await _db.Products.OrderBy(x => x.ProdouctName).ToListAsync();

            //}
            //catch (Exception ex)
            //{

            //    Log.Error("Error while creating product {Error} {StackTrace} {InnerException} {Source}",
            //          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            //}

            GC.Collect();

            return _db.Products.FromSqlRaw("select * from " + SPName).ToList();



        }


        public async Task<ProductsT> GetProductsByIdAsync(int ProdouctsID)
        {
            var GETProductsBaseID = (ProductsT)null;
            try
            {
                if (ProdouctsID != 0) GETProductsBaseID = await _db.Products.FindAsync(ProdouctsID);



            }
            catch (Exception ex)
            {

                Log.Error("Error while creating Product {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            GC.Collect();


            return GETProductsBaseID;
        }
        public async Task<bool> UpdateProductsAsync(int ProdouctsID, ProductsT productsT)
        {

            ResponseObject responseObject = new();

            if (productsT.ProdouctsID == ProdouctsID)
            {


                _db.Entry(productsT).State = EntityState.Modified;

            }
            try
            {
                if (productsT == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                GC.Collect();

                return true;

            }
            catch (Exception ex)
            {

                Log.Error("Error while Update Product {Error} {StackTrace} {InnerException} {Source}",
 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);



                return false;
            }
        }

        private bool PoductsExists(int ProductsID)
        {
            return _db.Products.Any(x => x.ProdouctsID == ProductsID);
        }


    }

}

