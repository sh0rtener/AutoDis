using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public sealed class CarBillEntityConfiguration : IEntityTypeConfiguration<CarBillEntity>
{
    public void Configure(EntityTypeBuilder<CarBillEntity> builder)
    {
        builder.ToTable("car_bill");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityByDefaultColumn();

        builder.Property(x => x.Cost).HasColumnName("cost").HasColumnType("numeric");

        builder.Property(x => x.IsArchieve).HasColumnName("is_archieve");

        builder.Property(x => x.CarId).HasColumnName("car_id");

        builder
            .HasOne(x => x.Car)
            .WithMany()
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Notes)
            .WithOne(n => n.CarBill)
            .HasForeignKey(n => n.CarBillId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Images)
            .WithOne(i => i.CarBill)
            .HasForeignKey(i => i.CarBillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
