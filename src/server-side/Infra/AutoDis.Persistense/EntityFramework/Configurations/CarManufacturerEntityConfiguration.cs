using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public sealed class CarManufacturerEntityConfiguration
    : IEntityTypeConfiguration<CarManufacturerEntity>
{
    public void Configure(EntityTypeBuilder<CarManufacturerEntity> builder)
    {
        builder.ToTable("car_manufacturer");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityByDefaultColumn();

        builder.Property(x => x.Name).HasColumnName("name");

        builder.Property(x => x.CountryId).HasColumnName("country_id");

        builder
            .HasOne(x => x.Country)
            .WithMany(c => c.Manufacturers)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
