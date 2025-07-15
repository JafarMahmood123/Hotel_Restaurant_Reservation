namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;

public class UploadRestaurantImagesRequest
{
    public Stream Content { get; set; }

    public string FileName { get; set; }
    public string ContentType { get; set; }
}
