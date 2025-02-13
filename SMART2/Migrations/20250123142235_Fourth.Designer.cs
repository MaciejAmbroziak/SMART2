﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMART2.Domain;

#nullable disable

namespace SMART2.Migrations
{
    [DbContext(typeof(DomainDbContext))]
    [Migration("20250123142235_Fourth")]
    partial class Fourth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProcessEquipmentEquipmentContracts", b =>
                {
                    b.Property<int>("EquipmentContractsId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessEquipmentsId")
                        .HasColumnType("int");

                    b.HasKey("EquipmentContractsId", "ProcessEquipmentsId");

                    b.HasIndex("ProcessEquipmentsId");

                    b.ToTable("ProcessEquipmentEquipmentContracts");
                });

            modelBuilder.Entity("SMART2.Domain.EquipmentContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TotalEquipmentUnits")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EquipmentContracts");
                });

            modelBuilder.Entity("SMART2.Domain.ProcessEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("ProcessEquipments");
                });

            modelBuilder.Entity("SMART2.Domain.ProductionFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EquipmentContractId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Occupied")
                        .HasColumnType("bit");

                    b.Property<double>("StandardArea")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("EquipmentContractId");

                    b.ToTable("ProductionFacilities");
                });

            modelBuilder.Entity("ProcessEquipmentEquipmentContracts", b =>
                {
                    b.HasOne("SMART2.Domain.EquipmentContract", null)
                        .WithMany()
                        .HasForeignKey("EquipmentContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SMART2.Domain.ProcessEquipment", null)
                        .WithMany()
                        .HasForeignKey("ProcessEquipmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SMART2.Domain.ProductionFacility", b =>
                {
                    b.HasOne("SMART2.Domain.EquipmentContract", "EquipmentContract")
                        .WithMany("ProductionFacilities")
                        .HasForeignKey("EquipmentContractId");

                    b.Navigation("EquipmentContract");
                });

            modelBuilder.Entity("SMART2.Domain.EquipmentContract", b =>
                {
                    b.Navigation("ProductionFacilities");
                });
#pragma warning restore 612, 618
        }
    }
}
