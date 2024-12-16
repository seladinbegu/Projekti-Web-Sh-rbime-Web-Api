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
    [Migration("20241215155031_init090")]
    partial class init090
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekti1.M2MRelations.DietaUshqimi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DietaId")
                        .HasColumnType("int");

                    b.Property<int>("UshqimiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DietaId");

                    b.HasIndex("UshqimiId");

                    b.ToTable("DietaUshqimi");
                });

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

                    b.Property<string>("Pershkrimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dieta");
                });

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

                    b.Property<string>("Origjina")
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

            modelBuilder.Entity("Projekti1.M2MRelations.DietaUshqimi", b =>
                {
                    b.HasOne("Projekti1.Models.Dieta", "Dieta")
                        .WithMany("DietaUshqimi")
                        .HasForeignKey("DietaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekti1.Models.Ushqimi", "Ushqimi")
                        .WithMany("DietaUshqimi")
                        .HasForeignKey("UshqimiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dieta");

                    b.Navigation("Ushqimi");
                });

            modelBuilder.Entity("Projekti1.Models.Dieta", b =>
                {
                    b.Navigation("DietaUshqimi");
                });

            modelBuilder.Entity("Projekti1.Models.Ushqimi", b =>
                {
                    b.Navigation("DietaUshqimi");
                });
#pragma warning restore 612, 618
        }
    }
}
