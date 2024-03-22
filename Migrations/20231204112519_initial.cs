using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRexErpAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBillings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBillings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIndustries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIndustries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemImageGalleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemImageGalleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalePrice = table.Column<double>(type: "float", nullable: true),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvWorkFlowStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Menufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FactoryInformationOnly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HP_AMP_KW_KVA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voltage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voltage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LengthInches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WidthInches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightInches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightLBS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstiamtedPackaginWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedTotalWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnDetials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemAddedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemUpdateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemTax = table.Column<double>(type: "float", nullable: true),
                    NewReplacementCostId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagingCostData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerItemCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemMaintenanceCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedTypeId = table.Column<int>(type: "int", nullable: true),
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RelatedCategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RelatedIndustrId = table.Column<int>(type: "int", nullable: true),
                    IndustryName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeactivate = table.Column<bool>(type: "bit", nullable: false),
                    GuaranteeOrAsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAccept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManuFacturers_Specs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWeightFt3 = table.Column<double>(type: "float", nullable: true),
                    ShippingClass = table.Column<double>(type: "float", nullable: true),
                    ExamplesforClassRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightAdditionalOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISLocalPickupOnly = table.Column<bool>(type: "bit", nullable: true),
                    IsSpecializedFreightTransit = table.Column<bool>(type: "bit", nullable: true),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoAccept = table.Column<double>(type: "float", nullable: false),
                    AutoReject = table.Column<double>(type: "float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderTotal = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTax = table.Column<int>(type: "int", nullable: false),
                    TotalDiscount = table.Column<int>(type: "int", nullable: false),
                    ShipmentCharges = table.Column<int>(type: "int", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCharges = table.Column<int>(type: "int", nullable: false),
                    PaymentMathod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripePaymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPalPaymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireTransferRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentPerences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardLastDigits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentPerences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblShippingInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShippingInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CustomerKeyQB = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QbRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVisitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVisitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVisitorMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryQBID = table.Column<int>(type: "int", nullable: false),
                    TypeQBID = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalZip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowCanWeReply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVisitorMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblallowItemOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblallowItemOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblallowItemOffers_tblItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "tblItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Examet = table.Column<bool>(type: "bit", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerKeyQB = table.Column<int>(type: "int", nullable: true),
                    CustomerNameQB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerQBId = table.Column<int>(type: "int", nullable: true),
                    OtpHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtpExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUser_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSaveLater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSaveLater", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSaveLater_tblItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "tblItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSaveLater_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAddresses_UserId",
                table: "tblAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblallowItemOffers_Attempts",
                table: "tblallowItemOffers",
                column: "Attempts");

            migrationBuilder.CreateIndex(
                name: "IX_tblallowItemOffers_IPAddress",
                table: "tblallowItemOffers",
                column: "IPAddress");

            migrationBuilder.CreateIndex(
                name: "IX_tblallowItemOffers_ItemId",
                table: "tblallowItemOffers",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillings_UserId",
                table: "tblBillings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_ItemCount",
                table: "tblCategories",
                column: "ItemCount");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_Name",
                table: "tblCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_tblIndustries_ItemCount",
                table: "tblIndustries",
                column: "ItemCount");

            migrationBuilder.CreateIndex(
                name: "IX_tblIndustries_Name",
                table: "tblIndustries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_Area",
                table: "tblItems",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_BrandName",
                table: "tblItems",
                column: "BrandName");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_CategoryName",
                table: "tblItems",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_Description",
                table: "tblItems",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_IndustryName",
                table: "tblItems",
                column: "IndustryName");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_IsDeactivate",
                table: "tblItems",
                column: "IsDeactivate");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_Location",
                table: "tblItems",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_QbRecordId",
                table: "tblItems",
                column: "QbRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_Quantity",
                table: "tblItems",
                column: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_RelatedCategoryId",
                table: "tblItems",
                column: "RelatedCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_RelatedIndustrId",
                table: "tblItems",
                column: "RelatedIndustrId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_RelatedTypeId",
                table: "tblItems",
                column: "RelatedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_SalePrice",
                table: "tblItems",
                column: "SalePrice");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_Title",
                table: "tblItems",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_TypeName",
                table: "tblItems",
                column: "TypeName");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentPerences_UserId",
                table: "tblPaymentPerences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSaveLater_ItemId",
                table: "tblSaveLater",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSaveLater_UserId",
                table: "tblSaveLater",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingInformations_UserId",
                table: "tblShippingInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTypes_CategoryId",
                table: "tblTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTypes_ItemCount",
                table: "tblTypes",
                column: "ItemCount");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_Email",
                table: "tblUser",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_UserTypeId",
                table: "tblUser",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserContacts_UserId",
                table: "tblUserContacts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAddresses");

            migrationBuilder.DropTable(
                name: "tblallowItemOffers");

            migrationBuilder.DropTable(
                name: "tblBillings");

            migrationBuilder.DropTable(
                name: "tblCarts");

            migrationBuilder.DropTable(
                name: "tblCategories");

            migrationBuilder.DropTable(
                name: "tblIndustries");

            migrationBuilder.DropTable(
                name: "tblItemImageGalleries");

            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblPaymentPerences");

            migrationBuilder.DropTable(
                name: "tblSaveLater");

            migrationBuilder.DropTable(
                name: "tblShippingInformations");

            migrationBuilder.DropTable(
                name: "tblTypes");

            migrationBuilder.DropTable(
                name: "tblUserContacts");

            migrationBuilder.DropTable(
                name: "tblVisitor");

            migrationBuilder.DropTable(
                name: "tblVisitorMessages");

            migrationBuilder.DropTable(
                name: "tblItems");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
