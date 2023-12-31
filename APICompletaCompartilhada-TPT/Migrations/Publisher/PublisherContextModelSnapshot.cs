﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Univali.Api.DbContexts;

#nullable disable

namespace Univali.Api.Migrations.Publisher
{
    [DbContext(typeof(PublisherContext))]
    partial class PublisherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Univali.Api.Entities.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuthorId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            FirstName = "Grace",
                            LastName = "Hopper"
                        },
                        new
                        {
                            AuthorId = 2,
                            FirstName = "John",
                            LastName = "Backus"
                        },
                        new
                        {
                            AuthorId = 3,
                            FirstName = "Bill",
                            LastName = "Gates"
                        },
                        new
                        {
                            AuthorId = 4,
                            FirstName = "Jim",
                            LastName = " Berners-Lee"
                        },
                        new
                        {
                            AuthorId = 5,
                            FirstName = "Linus",
                            LastName = "Torvalds"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.AuthorCourse", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("AuthorId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("PublishersCourses", (string)null);

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            CourseId = 1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AuthorId = 1,
                            CourseId = 3,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AuthorId = 2,
                            CourseId = 1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AuthorId = 2,
                            CourseId = 2,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AuthorId = 4,
                            CourseId = 1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AuthorId = 5,
                            CourseId = 3,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("CourseId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            Category = "Backend",
                            Description = "In this course, you'll learn how to build an API with ASP.NET Core that connects to a database via Entity Framework Core from scratch.",
                            Price = 97.00m,
                            PublisherId = 1,
                            Title = "ASP.NET Core Web Api"
                        },
                        new
                        {
                            CourseId = 2,
                            Category = "Backend",
                            Description = "In this course, Entity Framework Core 6 Fundamentals, you’ll learn to work with data in your .NET applications.",
                            Price = 197.00m,
                            PublisherId = 1,
                            Title = "Entity Framework Fundamentals"
                        },
                        new
                        {
                            CourseId = 3,
                            Category = "Operating Systems",
                            Description = "You've heard that Linux is the future of enterprise computing and you're looking for a way in.",
                            Price = 47.00m,
                            PublisherId = 2,
                            Title = "Getting Started with Linux"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.Publisher", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PublisherId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PublisherType")
                        .HasColumnType("integer");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers", (string)null);

                    b.HasDiscriminator<int>("PublisherType").HasValue(1);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Univali.Api.Entities.LegalPublisher", b =>
                {
                    b.HasBaseType("Univali.Api.Entities.Publisher");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.HasDiscriminator().HasValue(3);

                    b.HasData(
                        new
                        {
                            PublisherId = 1,
                            Name = "LegalPublisher1",
                            CNPJ = "64167199000120"
                        },
                        new
                        {
                            PublisherId = 2,
                            Name = "LegalPublisher2",
                            CNPJ = "94333690000144"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.NaturalPublisher", b =>
                {
                    b.HasBaseType("Univali.Api.Entities.Publisher");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasDiscriminator().HasValue(2);

                    b.HasData(
                        new
                        {
                            PublisherId = 3,
                            Name = "NaturalPublisher1",
                            CPF = "07382817946"
                        },
                        new
                        {
                            PublisherId = 4,
                            Name = "NaturalPublisher2",
                            CPF = "12345678912"
                        });
                });

            modelBuilder.Entity("Univali.Api.Entities.AuthorCourse", b =>
                {
                    b.HasOne("Univali.Api.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Univali.Api.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Univali.Api.Entities.Course", b =>
                {
                    b.HasOne("Univali.Api.Entities.Publisher", "Publisher")
                        .WithMany("Courses")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Univali.Api.Entities.Publisher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
