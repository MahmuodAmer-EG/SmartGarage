namespace SmartGarage.Models.MobileClient.Responses;

public class RetrieveCarResponse
{
    /// <summary>
    /// If found place for the car or not
    /// </summary>
    public bool Result { get; set; }
    /// <summary>
    /// If not found place or other error happens , it will return error message here to the mobile app.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// This will contain the qr code cotent if there place found.else it will return empty string value
    /// </summary>
    public string QrCode { get; set; }
}