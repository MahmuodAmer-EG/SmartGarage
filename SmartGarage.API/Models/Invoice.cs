namespace SmartGarage.API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Total { get; set; }
    }
}
