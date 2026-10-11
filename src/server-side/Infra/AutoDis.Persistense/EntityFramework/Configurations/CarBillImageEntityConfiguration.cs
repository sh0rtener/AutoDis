using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public sealed class CarBillImageEntityConfiguration : IEntityTypeConfiguration<CarBillImageEntity>
{
    public void Configure(EntityTypeBuilder<CarBillImageEntity> builder)
    {
        builder.ToTable("car_bill_images");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityByDefaultColumn();

        builder.Property(x => x.CarBillId).HasColumnName("car_bill_id");

        builder.Property(x => x.ImagePath).HasColumnName("image_path");
    }
}
