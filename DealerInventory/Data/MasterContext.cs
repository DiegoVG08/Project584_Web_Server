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

