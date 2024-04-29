namespace SmartGarage.Models;

public class User_QrCode
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string QrCodeContent { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public RequestType RequestType { get; set; }
}