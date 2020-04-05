﻿// <auto-generated />
using System;
using CoreDuiWebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreDuiWebApi.Data.Migrations
{
    [DbContext(typeof(DbLabCalcContext))]
    [Migration("20200404204141_RoleToUserUniqueConstraint")]
    partial class RoleToUserUniqueConstraint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreDuiWebApi.Authentication.DbUserEf.DbUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AccountEnabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("ConfirmEmailToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ConfirmEmailTokenExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RefreshToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RefreshTokenExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ResetPasswordToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ResetPasswordTokenExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DbUsers");
                });

            modelBuilder.Entity("CoreDuiWebApi.Authentication.DbUserRoleEf.DbUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DbUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DbUserId");

                    b.HasIndex("Role", "DbUserId")
                        .IsUnique()
                        .HasFilter("[Role] IS NOT NULL");

                    b.ToTable("DbUserRoles");
                });

            modelBuilder.Entity("CoreDuiWebApi.Email.DbEmailEf.DbEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RetryCount")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DbEmails");
                });

            modelBuilder.Entity("CoreDuiWebApi.Authentication.DbUserRoleEf.DbUserRole", b =>
                {
                    b.HasOne("CoreDuiWebApi.Authentication.DbUserEf.DbUser", null)
                        .WithMany("Roles")
                        .HasForeignKey("DbUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}