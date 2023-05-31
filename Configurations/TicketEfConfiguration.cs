using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class TicketEfConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.idTicket).HasName("Ticket_pk");
            builder.Property(e => e.ValidFrom).HasColumnType("datetime");
            builder.Property(e => e.ValidTo).HasColumnType("datetime");
            builder.Property(e => e.Validated).HasColumnType("bool");
            builder.Property(e => e.SeatNumber).HasColumnType("string");
            builder.HasOne(e => e.NavigationPassenger)
                .WithMany(e => e.NavigationTickets)
                .HasForeignKey(e => e.idPassenger);
            builder.HasOne(e => e.NavigationRoute)
                .WithMany(e => e.NavigationTickets)
                .HasForeignKey(e => e.idRoute);
            builder.HasOne(e => e.NavigationDiscount)
                .WithMany(e => e.NavigationTickets)
                .HasForeignKey(e => e.idDiscount);
            builder.HasOne(e => e.NavigationBus)
               .WithMany(e => e.NavigationTickets)
               .HasForeignKey(e => e.idBus);
        }
    }
}
