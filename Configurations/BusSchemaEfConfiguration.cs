using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class BusSchemaEfConfiguration : IEntityTypeConfiguration<BusSchema>
    {
        public void Configure(EntityTypeBuilder<BusSchema> builder)
        {
            builder.HasKey(e => e.idBusSchema).HasName("BusSchema_pk");
            builder.Property(e => e.Filename).HasColumnType("string");

        }
    }
}
