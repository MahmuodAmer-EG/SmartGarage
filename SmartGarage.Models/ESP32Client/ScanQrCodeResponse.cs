namespace SmartGarage.Models.ESP32Client;

public class ScanQrCodeResponse
{
    public bool Result { get; set; }
    public string? Message { get; set; }
    public string? Request { get; set; }
    public int? Floor{ get; set; }
}