﻿// <auto-generated />
using DealerInventory.Data;
using DealerInventory.Data.DealerModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DealerInventory.Migrations
{
    [DbContext(typeof(MasterContext))]
    [Migration("20230405183122_initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DealerInventory.DealerModel.CarDealership", b =>
                {
                    b.Property<int>("DealershipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DealershipID"));

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<int>("VehicleTypeID")
                        .HasColumnType("int");

                    b.HasKey("DealershipID");

                    b.HasIndex("VehicleTypeID");

                    b.ToTable("dealerShips");
                });

            modelBuilder.Entity("DealerInventory.DealerModel.VehicleType", b =>
                {
                    b.Property<int>("VehicleTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleTypeID"));

                    b.Property<int>("DealershipID")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("VehicleTypeID");

                    b.ToTable("vehicles");
                });

            modelBuilder.Entity("DealerInventory.DealerModel.CarDealership", b =>
                {
                    b.HasOne("DealerInventory.DealerModel.VehicleType", "VehicleType")
                        .WithMany("dealers")
                        .HasForeignKey("VehicleTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("DealerInventory.DealerModel.VehicleType", b =>
                {
                    b.Navigation("dealers");
                });
#pragma warning restore 612, 618
        }
    }
}
