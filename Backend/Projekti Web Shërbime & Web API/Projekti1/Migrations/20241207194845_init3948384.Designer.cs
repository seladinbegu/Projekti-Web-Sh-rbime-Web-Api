﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekti1.Data;

#nullable disable

namespace Projekti1.Migrations
{
    [DbContext(typeof(UshqimiDbContext))]
    [Migration("20241207194845_init3948384")]
    partial class init3948384
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekti1.Models.Ushqimi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Fibrat")
                        .HasColumnType("float");

                    b.Property<double>("Hekur")
                        .HasColumnType("float");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Kalcium")
                        .HasColumnType("float");

                    b.Property<double>("Kalori")
                        .HasColumnType("float");

                    b.Property<double>("Karbohidrate")
                        .HasColumnType("float");

                    b.Property<string>("Kategoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pershkrimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Proteina")
                        .HasColumnType("float");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.Property<double>("VitaminA")
                        .HasColumnType("float");

                    b.Property<double>("VitaminC")
                        .HasColumnType("float");

                    b.Property<double>("Yndyrna")
                        .HasColumnType("float");

                    b.Property<bool>("kaBulmet")
                        .HasColumnType("bit");

                    b.Property<bool>("kaGluten")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Ushqimi");
                });
#pragma warning restore 612, 618
        }
    }
}
