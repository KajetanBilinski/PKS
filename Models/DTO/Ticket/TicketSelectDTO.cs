namespace PKS.Models.DTO.Ticket
{
    public class TicketSelectDTO
    {
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SeatNumber { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Email { get; set;}
    }
}
