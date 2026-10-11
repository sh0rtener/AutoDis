using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public class CarEntityConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.ToTable("car");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityByDefaultColumn();

        builder.Property(x => x.Model).HasColumnName("model").IsRequired();

        builder.Property(x => x.ManufacturerId).HasColumnName("manufacturer_id");

        builder.Property(x => x.Year).HasColumnName("year");

        builder.Property(x => x.ColorType).HasColumnName("color_type");

        builder.Property(x => x.BodyType).HasColumnName("body_type");

        builder.Property(x => x.FuelType).HasColumnName("fuel_type");

        builder.Property(x => x.TransmissionType).HasColumnName("transmission_type");

        builder.Property(x => x.DriveTrain).HasColumnName("drive_train");

        builder.Property(x => x.Power).HasColumnName("power");

        builder.Property(x => x.Doors).HasColumnName("doors");

        builder.Property(x => x.Seats).HasColumnName("seats");

        builder.Property(x => x.Mileage).HasColumnName("mileage").HasColumnType("real");

        builder
            .HasOne(x => x.Manufacturer)
            .WithMany(m => m.Cars)
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
