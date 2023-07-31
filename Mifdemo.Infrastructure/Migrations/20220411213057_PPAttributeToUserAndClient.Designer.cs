﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220411213057_PPAttributeToUserAndClient")]
    partial class PPAttributeToUserAndClient
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AccountModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountNo")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("AprovedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountNo")
                        .IsUnique();

                    b.HasIndex("ClientId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AccountsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .HasColumnType("longtext");

                    b.Property<string>("AccountType")
                        .HasColumnType("longtext");

                    b.Property<string>("AccountUsage")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("GlCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ManualEntriesAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Parent")
                        .HasColumnType("longtext");

                    b.Property<string>("Tag")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("longtext");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("longtext");

                    b.Property<string>("AddressLine3")
                        .HasColumnType("longtext");

                    b.Property<string>("AddressType")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("StateProvince")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ApplicationUserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.BranchModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ClientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Activation_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ActiveLoans")
                        .HasColumnType("int");

                    b.Property<float>("ActiveSavings")
                        .HasColumnType("float");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("LastLoanAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.Property<int>("NoOfLoans")
                        .HasColumnType("int");

                    b.Property<int>("PassBookNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("PassBookNumber")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.DocumentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentType")
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ObjectType")
                        .HasColumnType("int");

                    b.Property<int>("Reference")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.FamilyMembersModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Profession")
                        .HasColumnType("longtext");

                    b.Property<string>("Qualification")
                        .HasColumnType("longtext");

                    b.Property<string>("RelationShip")
                        .HasColumnType("longtext");

                    b.Property<bool>("isDepandant")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.IdentifierModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentType")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Identifiers");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.PaymentScheduleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Due")
                        .HasColumnType("double");

                    b.Property<double>("Interest")
                        .HasColumnType("double");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("LoanBalance")
                        .HasColumnType("double");

                    b.Property<double>("Paid")
                        .HasColumnType("double");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("PayingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Penality")
                        .HasColumnType("double");

                    b.Property<double>("PricipalDue")
                        .HasColumnType("double");

                    b.Property<int>("PurchasedProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PurchasedProductId");

                    b.ToTable("PaymentSchedules");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("LoanBalance")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<float>("OriginalLoan")
                        .HasColumnType("float");

                    b.Property<string>("ProductType")
                        .HasColumnType("longtext");

                    b.Property<string>("ShortName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ProductPurchasedProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PurchasedProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PurchasedProductId");

                    b.HasIndex("PurchasedProductId");

                    b.ToTable("ProductPurchasedProducts");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.PurchasedProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<float>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("LoanBalance")
                        .HasColumnType("float");

                    b.Property<float>("OriginalLoan")
                        .HasColumnType("float");

                    b.Property<int>("PaymentContrat")
                        .HasColumnType("int");

                    b.Property<float>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("PurchasedProducts");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.TokenManagerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Used")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RefreshToken")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("TokenManagers");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.VoucherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastOpration")
                        .HasColumnType("longtext");

                    b.Property<int>("PurchasedProductId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Reference")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("VoucherType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ClientId");

                    b.HasIndex("PurchasedProductId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ApplicationUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ApplicationUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mifdemo.Domain.Models.ApplicationUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ApplicationUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AccountModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Clients")
                        .WithMany("Account")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AddressModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ApplicationUserModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.BranchModel", "Branch")
                        .WithMany("Users")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ClientModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.BranchModel", "Branch")
                        .WithMany("Clients")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.FamilyMembersModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Client")
                        .WithMany("Families")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.IdentifierModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Client")
                        .WithMany("Identifiers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.PaymentScheduleModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Client")
                        .WithMany("PaymentSchedules")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mifdemo.Domain.Models.PurchasedProductModel", "PurchasedProduct")
                        .WithMany("PaymentSchedules")
                        .HasForeignKey("PurchasedProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PurchasedProduct");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ProductPurchasedProduct", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ProductModel", "Product")
                        .WithMany("PurchasedProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mifdemo.Domain.Models.PurchasedProductModel", "PurchasedProduct")
                        .WithMany("Products")
                        .HasForeignKey("PurchasedProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("PurchasedProduct");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.PurchasedProductModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.AccountModel", "Account")
                        .WithMany("PurchasedProducts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.TokenManagerModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.ApplicationUserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.VoucherModel", b =>
                {
                    b.HasOne("Mifdemo.Domain.Models.BranchModel", "Branch")
                        .WithMany("Vouchers")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mifdemo.Domain.Models.ClientModel", "Client")
                        .WithMany("Vouchers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mifdemo.Domain.Models.PurchasedProductModel", "PurchasedProduct")
                        .WithMany("Vouchers")
                        .HasForeignKey("PurchasedProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Client");

                    b.Navigation("PurchasedProduct");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.AccountModel", b =>
                {
                    b.Navigation("PurchasedProducts");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.BranchModel", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Users");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ClientModel", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Addresses");

                    b.Navigation("Families");

                    b.Navigation("Identifiers");

                    b.Navigation("PaymentSchedules");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.ProductModel", b =>
                {
                    b.Navigation("PurchasedProducts");
                });

            modelBuilder.Entity("Mifdemo.Domain.Models.PurchasedProductModel", b =>
                {
                    b.Navigation("PaymentSchedules");

                    b.Navigation("Products");

                    b.Navigation("Vouchers");
                });
#pragma warning restore 612, 618
        }
    }
}
