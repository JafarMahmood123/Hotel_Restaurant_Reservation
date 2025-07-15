namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;

public abstract class UploadImageRequest
{
    public Stream Content { get; set; }

    public string FileName { get; set; }
    public string ContentType { get; set; }
}
