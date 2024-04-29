namespace SmartGarage.Models
{
    /// <summary>
    /// Define the Requests that the user can make 
    /// </summary>
    public enum RequestType
    {
        StoreCar = 0,
        RetrieveCar = 1
    }

    public enum CarState
    {
        InGarage = 0,
        OutOfGarage = 1,
    }
}
