﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekti1.M2MRelations.Data;

#nullable disable

namespace Projekti1.Migrations.Dieta_UshqimiDb
{
    [DbContext(typeof(Dieta_UshqimiDbContext))]
    partial class Dieta_UshqimiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("DietaUshqimi");
                });
#pragma warning restore 612, 618
        }
    }
}
