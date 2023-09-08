namespace PKS.Models.DBModels
{
    public class Ticket
    {
        public int idTicket { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool Validated { get; set; }
        public string SeatNumber { get; set; }
        public int idBus { get; set; }
        public int idRoute { get; set; }
        public int idPassenger { get; set; }
        public int idDiscount { get; set; }

        public virtual Passenger NavigationPassenger { get; set; }
        public virtual Route NavigationRoute { get; set; }
        public virtual Bus NavigationBus { get; set; }
        public virtual Discount NavigationDiscount { get; set; }

    }
}
