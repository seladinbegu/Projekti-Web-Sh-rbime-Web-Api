﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekti1.Data;

#nullable disable

namespace Projekti1.Migrations.DietaDb
{
    [DbContext(typeof(DietaDbContext))]
    [Migration("20241128142329_AddDietaTable")]
    partial class AddDietaTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekti1.Models.Dieta", b =>
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

                    b.Property<string>("Lloji")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Pershkrimi")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Dieta");
                });
#pragma warning restore 612, 618
        }
    }
}
