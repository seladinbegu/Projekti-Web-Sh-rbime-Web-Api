﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Projekti1.Migrations.Receta_UserDb
{
    [DbContext(typeof(Receta_UserDbContext))]
    [Migration("20241220212724_InitialMigration")]
    partial class InitialMigration
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Kalcium")
                        .HasColumnType("float");

                    b.Property<double>("Kalori")
                        .HasColumnType("float");

                    b.Property<double>("Karbohidrate")
                        .HasColumnType("float");

                    b.Property<string>("Kategoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origjina")
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

            modelBuilder.Entity("Projekti1.Receta.Receta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Udhezimet")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Receta");
                });

            modelBuilder.Entity("Projekti1.User.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RecetaUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RecetaId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RecetaId");

                    b.HasIndex("UserId");

                    b.ToTable("RecetaUser");
                });

            modelBuilder.Entity("RecetaUshqimi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RecetaId")
                        .HasColumnType("int");

                    b.Property<int>("UshqimiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecetaId");

                    b.HasIndex("UshqimiId");

                    b.ToTable("RecetaUshqimi");
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

            modelBuilder.Entity("RecetaUser", b =>
                {
                    b.HasOne("Projekti1.Receta.Receta", "Receta")
                        .WithMany()
                        .HasForeignKey("RecetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekti1.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receta");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecetaUshqimi", b =>
                {
                    b.HasOne("Projekti1.Receta.Receta", "Receta")
                        .WithMany("RecetaUshqimi")
                        .HasForeignKey("RecetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekti1.Models.Ushqimi", "Ushqimi")
                        .WithMany("RecetaUshqimi")
                        .HasForeignKey("UshqimiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receta");

                    b.Navigation("Ushqimi");
                });

            modelBuilder.Entity("Projekti1.Models.Dieta", b =>
                {
                    b.Navigation("DietaUshqimi");
                });

            modelBuilder.Entity("Projekti1.Models.Ushqimi", b =>
                {
                    b.Navigation("DietaUshqimi");

                    b.Navigation("RecetaUshqimi");
                });

            modelBuilder.Entity("Projekti1.Receta.Receta", b =>
                {
                    b.Navigation("RecetaUshqimi");
                });
#pragma warning restore 612, 618
        }
    }
}
