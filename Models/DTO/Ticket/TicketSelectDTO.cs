namespace PKS.Models.DTO.Ticket
{
    public class TicketSelectDTO
    {
        public decimal Cost { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool Validated { get; set; }
        public string SeatNumber { get; set; }
        public string RouteName { get; set; }
        public decimal Distance { get; set; }
        public string DiscountName { get; set; }
    }
}
