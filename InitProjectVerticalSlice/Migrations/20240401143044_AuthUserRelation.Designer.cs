﻿// <auto-generated />
using System;
using EventEchosAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InitProjectVerticalSlice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240401143044_AuthUserRelation")]
    partial class AuthUserRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventEchosAPI.Entities.Events.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.EventCoodrinator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoordinatorId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("EventId");

                    b.ToTable("EventCoodrinator");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.EventGuestImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("ImageDataId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GuestId");

                    b.HasIndex("ImageDataId");

                    b.ToTable("EventGuestImages");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.ImageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BitmapImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageMetaData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ImageDatas");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DiscountLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoordinatorId")
                        .HasColumnType("int");

                    b.Property<int?>("CouponId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("CouponId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductVersionId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductVersionId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsAddon")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Products.ProductVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVersions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Coordinator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Coordinator");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ImageCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.Auth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Auths");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PermissionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.UserRolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserRolePermissions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.EventCoodrinator", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Roles.Coordinator", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Events.Event", "Event")
                        .WithMany("EventCoodrinators")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinator");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.EventGuestImage", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Events.Event", "Event")
                        .WithMany("EventGuestImages")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Roles.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Events.ImageData", "ImageData")
                        .WithMany("EventGuestImages")
                        .HasForeignKey("ImageDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Guest");

                    b.Navigation("ImageData");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Order", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Roles.Coordinator", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Orders.Coupon", "Coupon")
                        .WithMany()
                        .HasForeignKey("CouponId");

                    b.Navigation("Coordinator");

                    b.Navigation("Coupon");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.OrderDetail", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Orders.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Products.ProductVersion", "ProductVersion")
                        .WithMany()
                        .HasForeignKey("ProductVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductVersion");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Transaction", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Orders.Order", "Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Products.ProductVersion", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Products.Product", "Product")
                        .WithMany("ProductVersions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Admin", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.User", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Coordinator", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.User", "User")
                        .WithMany("Coordinators")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Roles.Guest", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.User", "User")
                        .WithMany("Guests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.Auth", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.User", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.UserRolePermission", b =>
                {
                    b.HasOne("EventEchosAPI.Entities.Users.Permission", "Permission")
                        .WithMany("UserRolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventEchosAPI.Entities.Users.UserRole", "UserRole")
                        .WithMany("UserRolePermissions")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.Event", b =>
                {
                    b.Navigation("EventCoodrinators");

                    b.Navigation("EventGuestImages");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Events.ImageData", b =>
                {
                    b.Navigation("EventGuestImages");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Products.Product", b =>
                {
                    b.Navigation("ProductVersions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.Permission", b =>
                {
                    b.Navigation("UserRolePermissions");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Coordinators");

                    b.Navigation("Guests");
                });

            modelBuilder.Entity("EventEchosAPI.Entities.Users.UserRole", b =>
                {
                    b.Navigation("UserRolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
