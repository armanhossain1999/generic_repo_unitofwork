﻿// <auto-generated />
using System;
using Masters_Details_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Masters_Details_CRUD.Migrations
{
    [DbContext(typeof(ApplicantDbContext))]
    partial class ApplicantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Masters_Details_CRUD.Models.Applicant", b =>
                {
                    b.Property<int>("ApplicantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicantId"));

                    b.Property<string>("ApplicantName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("AppliedFor")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsReadyToWork")
                        .HasColumnType("bit");

                    b.HasKey("ApplicantId");

                    b.ToTable("Applicants");

                    b.HasData(
                        new
                        {
                            ApplicantId = 1,
                            ApplicantName = "A Applicant",
                            AppliedFor = "Peon",
                            BirthDate = new DateTime(2005, 1, 2, 0, 59, 35, 441, DateTimeKind.Local).AddTicks(3854),
                            Gender = 2,
                            IsReadyToWork = false
                        });
                });

            modelBuilder.Entity("Masters_Details_CRUD.Models.Qualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QualificationId"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Institute")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("PassingYear")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("QualificationId");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Qualifications");

                    b.HasData(
                        new
                        {
                            QualificationId = 1,
                            ApplicantId = 1,
                            Degree = "SSC",
                            Institute = "KPS",
                            PassingYear = 2019,
                            Result = "4.5"
                        },
                        new
                        {
                            QualificationId = 2,
                            ApplicantId = 1,
                            Degree = "HSC",
                            Institute = "KPC",
                            PassingYear = 2021,
                            Result = "5.0"
                        });
                });

            modelBuilder.Entity("Masters_Details_CRUD.Models.Qualification", b =>
                {
                    b.HasOne("Masters_Details_CRUD.Models.Applicant", "Applicant")
                        .WithMany("Qualifications")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Masters_Details_CRUD.Models.Applicant", b =>
                {
                    b.Navigation("Qualifications");
                });
#pragma warning restore 612, 618
        }
    }
}
