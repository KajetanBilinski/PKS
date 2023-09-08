namespace PKS.Models.DBModels
{
    public class Bus
    {
        public int idBus { get; set; }
        public int Capacity { get; set; }
        public string Registration { get; set; }
        public int idBusType { get; set; }
        public int idBusSchema { get; set; }

        public virtual BusSchema NavigationBusSchema { get; set; }
        public virtual BusType NavigationBusType { get; set; }

        public virtual ICollection<Ticket> NavigationTickets { get; set; }

    }
}
