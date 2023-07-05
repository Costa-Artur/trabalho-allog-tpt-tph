﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Univali.Api.DbContexts;

#nullable disable

namespace Univali.Api.Migrations.Customer
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20230704173701_AddCustomerContext")]
    partial class AddCustomerContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Univali.Api.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            City = "Elvira",
                            CustomerId = 1,
                            Street = "Verão do Cometa"
                        },
                        new
                        {
                            AddressId = 2,
                            City = "Perobia",
                            CustomerId = 1,
                            Street = "Borboletas Psicodélicas"
                        },
                        new
                        {
                            AddressId = 3,
                            City = "Salandra",
                            CustomerId = 2,
                            Street = "Canção Excêntrica"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Univali.Api.Entities.LegalCustomer", b =>
                {
                    b.HasBaseType("Univali.Api.Entities.Customer");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character(14)")
                        .IsFixedLength();

                    b.ToTable("LegalCustomers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Name = "Linus Torvalds",
                            CNPJ = "14698277000144"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.NaturalCustomer", b =>
                {
                    b.HasBaseType("Univali.Api.Entities.Customer");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character(11)")
                        .IsFixedLength();

                    b.ToTable("NaturalCustomers");

                    b.HasData(
                        new
                        {
                            CustomerId = 2,
                            Name = "Bill Gates",
                            CPF = "95395994076"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.Address", b =>
                {
                    b.HasOne("Univali.Api.Entities.Customer", "customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("Univali.Api.Entities.LegalCustomer", b =>
                {
                    b.HasOne("Univali.Api.Entities.Customer", null)
                        .WithOne()
                        .HasForeignKey("Univali.Api.Entities.LegalCustomer", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Univali.Api.Entities.NaturalCustomer", b =>
                {
                    b.HasOne("Univali.Api.Entities.Customer", null)
                        .WithOne()
                        .HasForeignKey("Univali.Api.Entities.NaturalCustomer", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Univali.Api.Entities.Customer", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
