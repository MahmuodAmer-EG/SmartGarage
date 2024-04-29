namespace SmartGarage.API.Models
{
    public class User_QrCode
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string QrCodeContent { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RequestType RequestType { get; set; }
    }
}