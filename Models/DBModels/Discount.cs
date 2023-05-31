namespace PKS.Models.DBModels
{
    public class Discount
    {
        public int IdDiscount { get; set; }
        public decimal DiscountValue { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> NavigationTickets { get; set;}
    }
}
