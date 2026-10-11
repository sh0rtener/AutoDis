using System.Reflection;
using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework;

public sealed class AppDbContext : DbContext
{
    public DbSet<CarEntity> Cars => Set<CarEntity>();
    public DbSet<CarBillEntity> CarBills => Set<CarBillEntity>();
    public DbSet<CarBillNoteEntity> CarBillNotes => Set<CarBillNoteEntity>();
    public DbSet<CarBillImageEntity> CarBillImages => Set<CarBillImageEntity>();
    public DbSet<CarCountryEntity> CarCountries => Set<CarCountryEntity>();
    public DbSet<CarManufacturerEntity> CarManufacturers => Set<CarManufacturerEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
