using PKS.Models.DTO.Ticket;

namespace PKS.Models.DTO.Passenger
{
    public class PassengerSelectDTO
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<TicketSelectDTO> Tickets { get; set; }

    }
}
