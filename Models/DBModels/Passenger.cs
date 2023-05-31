using System.Text.Json.Serialization;

namespace PKS.Models.DBModels
{
    public class Passenger
    {
        public int idPassenger { get; set; }
        public string Firstname{ get; set; }
        public string LastName{ get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Ticket> NavigationTickets { get; set; }

    }
}
