﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebRexErpAPI.Data;

#nullable disable

namespace WebRexErpAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204112519_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.AllowItemOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Attempts");

                    b.HasIndex("IPAddress");

                    b.HasIndex("ItemId");

                    b.ToTable("tblallowItemOffers");
                });

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.SaveLater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("tblSaveLater");
                });

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.UserContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerKeyQB")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblUserContacts");
                });

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypeId"), 1L, 1);

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes", (string)null);
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Billing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Zip_PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblBillings");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblCarts");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ItemCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemCount");

                    b.HasIndex("Name");

                    b.ToTable("tblCategories");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Industry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ItemCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemCount");

                    b.HasIndex("Name");

                    b.ToTable("tblIndustries");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Amps")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AutoAccept")
                        .HasColumnType("float");

                    b.Property<double>("AutoReject")
                        .HasColumnType("float");

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstiamtedPackaginWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimatedTotalWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamplesforClassRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactoryInformationOnly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreightAdditionalOptions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuaranteeOrAsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HP_AMP_KW_KVA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeightInches")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ISLocalPickupOnly")
                        .HasColumnType("bit");

                    b.Property<string>("IndustryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InvWorkFlowStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeactivate")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsSpecializedFreightTransit")
                        .HasColumnType("bit");

                    b.Property<string>("ItemAddedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemMaintenanceCost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ItemTax")
                        .HasColumnType("float");

                    b.Property<string>("ItemUpdateDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LengthInches")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ManuFacturers_Specs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewReplacementCostId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackagingCostData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentAccept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PerItemCost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedIndustrId")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ReturnDetials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SalePrice")
                        .HasColumnType("float");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ShippingClass")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("TotalWeightFt3")
                        .HasColumnType("float");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Voltage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voltage2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeightLBS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WidthInches")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Area");

                    b.HasIndex("BrandName");

                    b.HasIndex("CategoryName");

                    b.HasIndex("Description");

                    b.HasIndex("IndustryName");

                    b.HasIndex("IsDeactivate");

                    b.HasIndex("Location");

                    b.HasIndex("QbRecordId");

                    b.HasIndex("Quantity");

                    b.HasIndex("RelatedCategoryId");

                    b.HasIndex("RelatedIndustrId");

                    b.HasIndex("RelatedTypeId");

                    b.HasIndex("SalePrice");

                    b.HasIndex("Title");

                    b.HasIndex("TypeName");

                    b.ToTable("tblItems");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.ItemImageGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblItemImageGalleries");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderTotal")
                        .HasColumnType("int");

                    b.Property<string>("PayPalPaymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMathod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShipmentCharges")
                        .HasColumnType("int");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingCharges")
                        .HasColumnType("int");

                    b.Property<string>("ShippingProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StripePaymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalDiscount")
                        .HasColumnType("int");

                    b.Property<int>("TotalTax")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WireTransferRef")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblOrders");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.PaymentPreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CardLastDigits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpireDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethodId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblPaymentPerences");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.ShoppingInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Zip_PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblShippingInformations");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ItemCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QbRecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ItemCount");

                    b.ToTable("tblTypes");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerKeyQB")
                        .HasColumnType("int");

                    b.Property<string>("CustomerNameQB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerQBId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Examet")
                        .HasColumnType("bit");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OtpExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("OtpHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("UserTypeId");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Zip_PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblAddresses");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblVisitor");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.VisitorMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowCanWeReply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IndustryQBID")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalZip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeQBID")
                        .HasColumnType("int");

                    b.Property<int?>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblVisitorMessages");
                });

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.AllowItemOffer", b =>
                {
                    b.HasOne("WebRexErpAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("WebRexErpAPI.DataAccess.Models.SaveLater", b =>
                {
                    b.HasOne("WebRexErpAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebRexErpAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebRexErpAPI.Models.User", b =>
                {
                    b.HasOne("WebRexErpAPI.DataAccess.Models.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });
#pragma warning restore 612, 618
        }
    }
}
