 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Emit;
 using InternalShop.Models;

namespace DataBaseService
{
    public class ApplicationDbContext : IdentityDbContext<Appuser,IdentityRole ,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


    
        public DbSet<CustomersT> Customers { get; set; }

        public DbSet<EmployeesT> Employees { get; set; }


        public DbSet<CategoriesT> Categories { get; set; }

        public DbSet<ProductsT> Products { get; set; }

        public DbSet<InvoiceStoreStatusT> InvoiceStoreStatus { get; set; }


        public DbSet<OutLayUnitesT> OutLayUnites { get; set; }


        public DbSet<SalesinvoiceMasterT> SalesInvoicesMaster { get; set; }
        public DbSet<SalesinvoiceT> SalesInvoices { get; set; }

        //public DbSet<SellProductsT> SellProducts { get; set; }

        public DbSet<SuppliersT> Suppliers { get; set; }


        public DbSet<SupplyRepresentativeT> SupplyRepresentatives { get; set; }


        public DbSet<UnitesT> Unites { get; set; }

        public DbSet<MasterProductsWarehouseT> MasterProductsWarehouse { get; set; }


        public DbSet<ProductsWarehouseT> ProductsWarehouse { get; set; }
        public DbSet<QuantityProductT> QuantityProducts { get; set; }
        public DbSet<ConvertofStoresT> ConvertofStores { get; set; }
        public DbSet<ReportConvertofStoresT> reportConvertofStoresTs { get; set; }
        public DbSet<DismissalnoticeT> Dismissalnotice { get; set; }

        public DbSet<PermissionToEntertheStoreProductT> PermissionToEntertheStoreProduct { get; set; }

        public DbSet<ManageStoreT> ManageStore{ get; set; }

        public DbSet<BranchesT> Branches { get; set; }

        public DbSet<ProductsWarehouseObjectT> productsWarehouseObjectTs { get; set; }
 

        public DbSet<ReportSalesInvoiceById> reportSalesInvoiceByIds { get; set; }

        public DbSet<ReportDismissalnotice> reportDismissalnotices { get; set; }
        public DbSet<ReportProductsWarehouse> reportProductsWarehouses { get; set; }

        public DbSet<ReportPermissionToEntertheStoreProduct> reportPermissionToEntertheStoreProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region model
            base.OnModelCreating(modelBuilder);
            #endregion

            #region default Create AdminUser
            modelBuilder.Entity<IdentityRole>().HasData(
                 new { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR", RoleName = "Administrator", Handle = "administrator", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true },
                 new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER", RoleName = "customer", Handle = "customer", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true }
             );
            #endregion
            //modelBuilder.Types().Configure(t => t.MapToStoredProcedures());

            modelBuilder.Entity<PermissionToEntertheStoreProductT>().HasOne(p => p.Products).WithMany(PerMEnter => PerMEnter.PermissionToEntertheStoreProduct).HasForeignKey(PID => PID.ProdouctsID);

            modelBuilder.Entity<PermissionToEntertheStoreProductT>().HasOne(MStore => MStore.ManageStore).WithMany(PerMEnter => PerMEnter.PermissionToEntertheStoreProduct).HasForeignKey(ManageSID => ManageSID.ManageStoreId);



        }
    }

}
