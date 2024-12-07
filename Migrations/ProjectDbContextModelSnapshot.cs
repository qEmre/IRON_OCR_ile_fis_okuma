﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OCRproject.DataLayer;

#nullable disable

namespace OCRproject.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    partial class ProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OCRproject.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FisNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("KDV")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ResimAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResimUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Toplam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VergiNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("documentTable");
                });

            modelBuilder.Entity("OCRproject.Models.documentContent", b =>
                {
                    b.Property<int>("DocumentConentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentConentId"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<string>("UrunBilgisi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UrunFiyati")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DocumentConentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("documentContentTable");
                });

            modelBuilder.Entity("OCRproject.Models.documentContent", b =>
                {
                    b.HasOne("OCRproject.Models.Document", "Document")
                        .WithMany("DocumentContents")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("OCRproject.Models.Document", b =>
                {
                    b.Navigation("DocumentContents");
                });
#pragma warning restore 612, 618
        }
    }
}
