
using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject;
using Microcredit.ClassProject.BranchesSVC;
using Microcredit.ClassProject.ConvertofStoresSVC;
using Microcredit.ClassProject.CustomersSVC;
using Microcredit.ClassProject.DismissalnoticeSVC;
using Microcredit.ClassProject.EmployeeSVC;
using Microcredit.ClassProject.MasterOFSToresSVC;
using Microcredit.ClassProject.MasterProductsWarehouseSVC.ProductsWarehouseSVC;
using Microcredit.ClassProject.PermissionToEntertheStoreProductSVC;
using Microcredit.ClassProject.ProductsSVC;
using Microcredit.ClassProject.QuantityProductSVC;
using Microcredit.ClassProject.SalesinvoiceSVC;
//using Microcredit.ClassProject.Searchproducts;
using Microcredit.ClassProject.SuppliersSVC;
using Microcredit.GETErr;
using Microcredit.Reports.ExecuteSP;
//using Microcredit.Reports.ReportBranches;
//using Microcredit.Reports.ReportCategories;
using Microcredit.Reports.ReportSalesInvoice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModelService;
using System.Text;

namespace Microcredit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            #region CONNECTION MIGRATION
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("MicrocreditTOCon"), x => x.MigrationsAssembly("Microcredit")));

            services.AddIdentity<Appuser, IdentityRole>(opt => { }).AddEntityFrameworkStores<ApplicationDbContext>();


            //     services.AddDbContext<DataProtectionKeysContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DataProtectionKeysContextCon"), x => x.MigrationsAssembly("Microcredit")));
            #endregion

            #region Services
            services.AddTransient<ICategories, CategoriesSVC>();
            services.AddTransient<IProducts, ProductsSVC>();
            services.AddTransient<ICustomers, CustomersSVC>();
            services.AddTransient<IConvertofStores, ConvertofStoresSVC>();
            services.AddTransient<IDismissalnotice, DismissalnoticeSVC>();
            services.AddTransient<IManageStore, ManageStoreSVC>();
            services.AddTransient<ISalesinvoice, SalesinvoiceSVC>();
            services.AddTransient<IPermissionToEntertheStoreProduct, PermissionToEntertheStoreProductSVC>();
            services.AddTransient<IEmployee, EmployeeSVC>();
            services.AddTransient<IQuantityProduct, QuantityProductSVC>();
            services.AddTransient<IBranches, BranchesSVC>();
            services.TryAddSingleton<IClientErrorFactory, ProblemDetailsErrorFactory>();
            services.AddTransient<ISuppliers, SuppliersSVC>();
            services.AddTransient<IProductsWarehouse, ProductsWarehouseSVC>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IReportS, ReportSalesInvoiceSVC>();
            services.AddTransient<IExecuteSPSalesInvoice, ExcuteSpSalesInvoice>();
            services.AddTransient<IExecuteCategories, ExecuteCategories>();
            services.AddTransient<IExecuteConvertofStores, ExecuteConvertofStores>();
            services.AddTransient<IExecuteDismissalnotice, ExecuteDismissalnotice>();
            services.AddTransient<IExecuteManageStore, ExecuteManageStore>();
            services.AddTransient<IExecuteProducts, ExecuteProducts>();
            services.AddTransient<IExecuteProductsWarehouse, ExecuteProductsWarehouse>();
            services.AddTransient<IExecuteBranches, ExecuteBranches>();

            services.AddTransient<IReportExecutePermissionToEntertheStoreProduct, ExecutePermissionToEntertheStoreProductReport>();

            //services.AddTransient<IGetAllPermissionToEntertheStoreProduct, ExecuteGetAllPermissionToEntertheStoreProduct>();

            services.AddTransient<IPermissionToEntertheStoreProduct, PermissionToEntertheStoreProductSVC>();

            #endregion

            /*                              DEFAULT IDENTITY OPTIONS                                             */
            /*---------------------------------------------------------------------------------------------------*/
            //var identityDefaultOptionsConfiguration = Configuration.GetSection("IdentityDefaultOptions");
            //services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfiguration);
            //var identityDefaultOptions = identityDefaultOptionsConfiguration.Get<IdentityDefaultOptions>();

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
            //    options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
            //    options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
            //    options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
            //    options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
            //    options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
            //    options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
            //    options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

            //    // User settings
            //    options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

            //    // email confirmation require
            //    options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;

            //}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            /*---------------------------------------------------------------------------------------------------*/


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Key"]);
                var issuer = Configuration["JWTConfig:Issuer"];
                var audience = Configuration["JWTConfig:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience

                };
            });


            services.AddApiVersioning(
         options =>
         {
             options.ReportApiVersions = true;
             options.AssumeDefaultVersionWhenUnspecified = true;
             options.DefaultApiVersion = new ApiVersion(1, 0);
         });
            /*---------------------------------------------------------------------------------------------------*/
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {


                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));



            services.AddCors(); // Make sure you call this previous to AddMvc

            services.AddMvc();
            services.AddSession();
            services.AddMvc().AddControllersAsServices().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microcredit", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microcredit v1"));
            }
            // Make sure you call this before calling app.UseMvc()
            //app.UseCors(
            //    options => options.WithOrigins("http://localhost:4200/").AllowAnyMethod()
            //);
            app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
            //app.UseMvc();
            app.UseSession();  // Before UseMvc()

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
