namespace SmartGarage.API.Models
{
    /// <summary>
    /// Define the Requests that the user can make 
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// Open the Garage to enter the car into it
        /// </summary>
        OpenGarageDoor,
        /// <summary>
        /// The car is inside and start to store it into it's floor
        /// </summary>
        StartStoringCar,
        /// <summary>
        /// Get the car back to the client
        /// </summary>
        RetrieveCar
    }

    public enum CarState
    {
        None,
        WaitItToStore,
        StoringInProcess,
        Stored,
        RequestedToOut,
    }
}
