namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class WorkTimeField
{
    public int Open { get; set; }

    public int Close { get; set; }

    public string OpenHours{ get; set; }

    public string CloseHours{ get; set; }

    public WorkTimeField(int open , int close, string openHours, string closeHours)
    {
        Open = open;
        Close = close;
        OpenHours = openHours;
        CloseHours = closeHours;
    }

}
