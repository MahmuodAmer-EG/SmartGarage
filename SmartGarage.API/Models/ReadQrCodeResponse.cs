namespace SmartGarage.API.Models
{
    public class ReadQrCodeResponse
    {
        public bool Result { get; set; }
        public string? Message { get; set; }
        public string? Request { get; set; }
        public bool IsCharge { get; set; }
        public int Floor { get; set; }
    }
}