using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public sealed class CarCountryEntityConfiguration : IEntityTypeConfiguration<CarCountryEntity>
{
    public void Configure(EntityTypeBuilder<CarCountryEntity> builder)
    {
        builder.ToTable("car_country");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityByDefaultColumn();

        builder.Property(x => x.Name).HasColumnName("name");

        builder.Property(x => x.IsoCode).HasColumnName("iso_code");
    }
}
