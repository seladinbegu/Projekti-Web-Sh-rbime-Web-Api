﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Projekti1.Migrations.Receta_UshqimiDb
{
    [DbContext(typeof(Receta_UshqimiDbContext))]
    [Migration("20241220195700_RecetaUshqimiMigration")]
    partial class RecetaUshqimiMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.ToTable("RecetaUshqimi");
                });
#pragma warning restore 612, 618
        }
    }
}
