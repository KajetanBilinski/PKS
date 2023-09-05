namespace PKS.Models.DTO.Ticket
{
    public class TicketAddDTO
    {
        public decimal Cost { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool Validated { get; set; }
        public string SeatNumber { get; set; }
        public string RouteName { get; set; }
        public decimal Distance { get; set; }
        public string DiscountName { get; set; }
        public int idBus { get; set; }
        public int idRoute { get; set; }
        public int idPassenger { get; set; }
        public int idDiscount { get; set; }
    }
}
