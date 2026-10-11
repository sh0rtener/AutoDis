using AutoDis.Persistense.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoDis.Persistense.EntityFramework.Configurations;

public sealed class CarBillNoteEntityConfiguration : IEntityTypeConfiguration<CarBillNoteEntity>
{
    public void Configure(EntityTypeBuilder<CarBillNoteEntity> builder)
    {
        builder.ToTable("car_bill_notes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.Name).HasColumnName("name");

        builder.Property(x => x.Cost).HasColumnName("cost").HasColumnType("numeric");

        builder.Property(x => x.Available).HasColumnName("available");

        builder.Property(x => x.CarBillId).HasColumnName("car_bill_id");
    }
}
