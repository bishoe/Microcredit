using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microcredit.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "#BranchesReportT",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BranchPhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    manageStoreID = table.Column<int>(type: "int", nullable: false),
                    ManageStorename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "addNewLoanObject",
                columns: table => new
                {
                    LonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdcutId = table.Column<int>(type: "int", nullable: false),
                    CustomeId = table.Column<int>(type: "int", nullable: false),
                    InterestRateid = table.Column<int>(type: "int", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    StartDateLona = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateLona = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountBeforeAddInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountAfterAddInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LonaDetailsId = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorFirst = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorSecond = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorThird = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorFourth = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addNewLoanObject", x => x.LonaId);
                });

            migrationBuilder.CreateTable(
                name: "addnewLonaDetails",
                columns: table => new
                {
                    LonaDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LonaId = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorFirst = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorSecond = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorThird = table.Column<int>(type: "int", nullable: false),
                    LonaGuarantorFourth = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addnewLonaDetails", x => x.LonaDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "addNewLonaMasters",
                columns: table => new
                {
                    LonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdcutId = table.Column<int>(type: "int", nullable: false),
                    CustomeId = table.Column<int>(type: "int", nullable: false),
                    InterestRateid = table.Column<int>(type: "int", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    StartDateLona = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateLona = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountBeforeAddInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountAfterAddInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addNewLonaMasters", x => x.LonaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BranchPhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    manageStoreID = table.Column<int>(type: "int", nullable: false),
                    BranchMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryProductId);
                });

            migrationBuilder.CreateTable(
                name: "ConvertofStores",
                columns: table => new
                {
                    ConvertofStoresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ManageStoreIdFrom = table.Column<int>(type: "int", nullable: false),
                    ManageStoreIdTo = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertofStores", x => x.ConvertofStoresId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Dismissalnotice",
                columns: table => new
                {
                    DismissalnoticeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStoreId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dismissalnotice", x => x.DismissalnoticeId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EmployeeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "interestRateModels",
                columns: table => new
                {
                    InterestRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestRateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterestRateValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interestRateModels", x => x.InterestRateId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStoreStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStoreID = table.Column<int>(type: "int", nullable: false),
                    Billno = table.Column<int>(type: "int", nullable: false),
                    PAIDAMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStoreStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ManageStore",
                columns: table => new
                {
                    ManageStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStorename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManageStore", x => x.ManageStoreID);
                });

            migrationBuilder.CreateTable(
                name: "MasterProductsWarehouse",
                columns: table => new
                {
                    ManageStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProductsWarehouse", x => x.ManageStoreID);
                });

            migrationBuilder.CreateTable(
                name: "OutLayUnites",
                columns: table => new
                {
                    OutLayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutLayUnites", x => x.OutLayID);
                });

            migrationBuilder.CreateTable(
                name: "paymentOfistallments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IstalmentsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LonaAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentOfistallments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProdouctsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryProductId = table.Column<int>(type: "int", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BarCodeText = table.Column<int>(type: "int", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProdouctsID);
                });

            migrationBuilder.CreateTable(
                name: "ProductsWarehouse",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuppliersID = table.Column<int>(type: "int", nullable: false),
                    Billno = table.Column<int>(type: "int", nullable: false),
                    PermissionToEntertheStoreProductId = table.Column<int>(type: "int", nullable: false),
                    ManageStoreID = table.Column<int>(type: "int", nullable: false),
                    CategoryProductId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    PurchasingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Productiondate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeProducts = table.Column<int>(type: "int", nullable: false),
                    UnitesId = table.Column<int>(type: "int", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuntityProduct = table.Column<int>(type: "int", nullable: false),
                    Dateofregistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anexpiredproduct = table.Column<bool>(type: "bit", nullable: false),
                    Nocolumn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsWarehouse", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "productsWarehouseObjectTs",
                columns: table => new
                {
                    ManageStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SuppliersID = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false),
                    Billno = table.Column<int>(type: "int", nullable: false),
                    PermissionToEntertheStoreProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryProductId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    PurchasingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Productiondate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeProducts = table.Column<int>(type: "int", nullable: false),
                    UnitesId = table.Column<int>(type: "int", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuntityProduct = table.Column<int>(type: "int", nullable: false),
                    Dateofregistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anexpiredproduct = table.Column<bool>(type: "bit", nullable: false),
                    Nocolumn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsWarehouseObjectTs", x => x.ManageStoreID);
                });

            migrationBuilder.CreateTable(
                name: "QuantityProducts",
                columns: table => new
                {
                    QTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    manageStoreID = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityProducts", x => x.QTID);
                });

            migrationBuilder.CreateTable(
                name: "reportConvertofStoresTs",
                columns: table => new
                {
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ManageStoreIdFrom = table.Column<int>(type: "int", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManageStorename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportConvertofStoresTs", x => x.Notes);
                });

            migrationBuilder.CreateTable(
                name: "reportDismissalnotices",
                columns: table => new
                {
                    DismissalnoticeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStoreID = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCodeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManageStorename = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportDismissalnotices", x => x.DismissalnoticeId);
                });

            migrationBuilder.CreateTable(
                name: "reportPermissionToEntertheStoreProducts",
                columns: table => new
                {
                    PermissionToEntertheStoreProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStoreId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManageStorename = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportPermissionToEntertheStoreProducts", x => x.PermissionToEntertheStoreProductId);
                });

            migrationBuilder.CreateTable(
                name: "reportProductsWarehouses",
                columns: table => new
                {
                    ManageStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuppliersID = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false),
                    Billno = table.Column<int>(type: "int", nullable: false),
                    PermissionToEntertheStoreProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryProductId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Productiondate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeProducts = table.Column<int>(type: "int", nullable: false),
                    UnitesId = table.Column<int>(type: "int", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuntityProduct = table.Column<int>(type: "int", nullable: false),
                    QtStartPeriod = table.Column<int>(type: "int", nullable: false),
                    Dateofregistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anexpiredproduct = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportProductsWarehouses", x => x.ManageStoreID);
                });

            migrationBuilder.CreateTable(
                name: "reportQuantityProduct",
                columns: table => new
                {
                    QTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    manageStoreID = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportQuantityProduct", x => x.QTID);
                });

            migrationBuilder.CreateTable(
                name: "reportSalesInvoiceByIds",
                columns: table => new
                {
                    SellingMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quntity_Product = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportSalesInvoiceByIds", x => x.SellingMasterID);
                });

            migrationBuilder.CreateTable(
                name: "SalesinvoiceObjectReport",
                columns: table => new
                {
                    SellingMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellingId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    Quntity_Product = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nocolumn = table.Column<int>(type: "int", nullable: true),
                    ProdouctName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesinvoiceObjectReport", x => x.SellingMasterID);
                });

            migrationBuilder.CreateTable(
                name: "salesinvoiceObjects",
                columns: table => new
                {
                    SellingMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    SellingId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    Quntity_Product = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nocolumn = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salesinvoiceObjects", x => x.SellingMasterID);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    SellingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellingMasterID = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    Quntity_Product = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountRow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoices", x => x.SellingId);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoicesMaster",
                columns: table => new
                {
                    SellingMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AMountDicount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoicesMaster", x => x.SellingMasterID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SuppliersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuplierName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SuplierPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SuplierAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SuppliersID);
                });

            migrationBuilder.CreateTable(
                name: "SupplyRepresentatives",
                columns: table => new
                {
                    SuppliersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierRID = table.Column<int>(type: "int", nullable: false),
                    SupplierRName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SupplierRPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    SupplierRAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierRNotes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyRepresentatives", x => x.SuppliersID);
                });

            migrationBuilder.CreateTable(
                name: "Unites",
                columns: table => new
                {
                    UnitesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitesName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitesConvert = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unites", x => x.UnitesId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionToEntertheStoreProduct",
                columns: table => new
                {
                    PermissionToEntertheStoreProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageStoreId = table.Column<int>(type: "int", nullable: false),
                    ProdouctsID = table.Column<int>(type: "int", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionToEntertheStoreProduct", x => x.PermissionToEntertheStoreProductId);
                    table.ForeignKey(
                        name: "FK_PermissionToEntertheStoreProduct_ManageStore_ManageStoreId",
                        column: x => x.ManageStoreId,
                        principalTable: "ManageStore",
                        principalColumn: "ManageStoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionToEntertheStoreProduct_Products_ProdouctsID",
                        column: x => x.ProdouctsID,
                        principalTable: "Products",
                        principalColumn: "ProdouctsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", null, "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionToEntertheStoreProduct_ManageStoreId",
                table: "PermissionToEntertheStoreProduct",
                column: "ManageStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionToEntertheStoreProduct_ProdouctsID",
                table: "PermissionToEntertheStoreProduct",
                column: "ProdouctsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "#BranchesReportT");

            migrationBuilder.DropTable(
                name: "addNewLoanObject");

            migrationBuilder.DropTable(
                name: "addnewLonaDetails");

            migrationBuilder.DropTable(
                name: "addNewLonaMasters");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ConvertofStores");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Dismissalnotice");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "interestRateModels");

            migrationBuilder.DropTable(
                name: "InvoiceStoreStatus");

            migrationBuilder.DropTable(
                name: "MasterProductsWarehouse");

            migrationBuilder.DropTable(
                name: "OutLayUnites");

            migrationBuilder.DropTable(
                name: "paymentOfistallments");

            migrationBuilder.DropTable(
                name: "PermissionToEntertheStoreProduct");

            migrationBuilder.DropTable(
                name: "ProductsWarehouse");

            migrationBuilder.DropTable(
                name: "productsWarehouseObjectTs");

            migrationBuilder.DropTable(
                name: "QuantityProducts");

            migrationBuilder.DropTable(
                name: "reportConvertofStoresTs");

            migrationBuilder.DropTable(
                name: "reportDismissalnotices");

            migrationBuilder.DropTable(
                name: "reportPermissionToEntertheStoreProducts");

            migrationBuilder.DropTable(
                name: "reportProductsWarehouses");

            migrationBuilder.DropTable(
                name: "reportQuantityProduct");

            migrationBuilder.DropTable(
                name: "reportSalesInvoiceByIds");

            migrationBuilder.DropTable(
                name: "SalesinvoiceObjectReport");

            migrationBuilder.DropTable(
                name: "salesinvoiceObjects");

            migrationBuilder.DropTable(
                name: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "SalesInvoicesMaster");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "SupplyRepresentatives");

            migrationBuilder.DropTable(
                name: "Unites");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ManageStore");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
