namespace PKS.Models.DBModels
{
    public class Ticket
    {
        public int idTicket { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SeatNumber { get; set; }
        public int idBus { get; set; } 
        public int idRoute { get; set; }
        public int idPassenger { get; set; }

        public virtual Passenger NavigationPassenger { get; set; }
        public virtual Route NavigationRoute { get; set; }
        public virtual Bus NavigationBus { get; set; }

    }
}
