namespace SmartGarage.MobileClient.Models
{
    public class CarReserveResponse
    {
        public bool Result  { get; set; }
        public string Message { get; set; }
        public string QrCode { get; set; }
        public int Floor { get; set; } = 1;
    }
}
