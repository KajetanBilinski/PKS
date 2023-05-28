using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class PassengerEfConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(e => e.idPassenger).HasName("Passenger_pk");
            builder.Property(e => e.Firstname).HasColumnType("string");
            builder.Property(e => e.LastName).HasColumnType("string");
            builder.Property(e => e.Age).HasColumnType("integer");
            builder.Property(e => e.PhoneNumber).HasColumnType("string");
            builder.Property(e => e.Email).HasColumnType("string");
        }
    }
}
