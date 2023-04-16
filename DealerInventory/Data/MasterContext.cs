using System;
using System.Collections.Generic;
using DealerInventory.Data.DealerModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DealerInventory.Data;

public partial class MasterContext : IdentityDbContext<ApplicationUser>
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarDealership> carDealership { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Integrated Security=true");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //gets the data from the Car Dealerships stored in database 
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CarDealership>(entity =>
        {
            //update to the current fields in database

            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Location).IsFixedLength();
            entity.Property(e => e.ContactInfo).IsFixedLength();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

/*
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<CarDealership>().ToTable("CarDealership");
    modelBuilder.Entity<CarDealership>()
        .HasKey(x => x.DealershipID);
    modelBuilder.Entity<CarDealership>()
        .Property(x => x.DealershipID).IsRequired();
    modelBuilder.Entity<CarDealership>()
        .Property(x => x.Name).HasColumnType("VARCHAR(50)");
    modelBuilder.Entity<CarDealership>()
        .Property(x => x.Location).HasColumnType("VARCHAR(100)");
        modelBuilder.Entity<CarDealership>()
           .Property(x => x.ContactInfo).HasColumnType("VARCHAR(100)");


        modelBuilder.Entity<VehicleType>().ToTable("VehicleType");
    modelBuilder.Entity<VehicleType>()
        .HasKey(x => x.DealershipID);
    modelBuilder.Entity<VehicleType>()
        .Property(x => x.DealershipID).IsRequired();
    modelBuilder.Entity<CarDealership>()
        .HasOne(x => x.VehicleType)
        .WithMany(y => y.carDealership)
        .HasForeignKey(x => x.VehicleTypeID);

    // add the EntityTypeConfiguration classes
    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(MasterContext).Assembly
        );
}

public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
public DbSet<CarDealership> carDealership => Set<CarDealership>();

}*/