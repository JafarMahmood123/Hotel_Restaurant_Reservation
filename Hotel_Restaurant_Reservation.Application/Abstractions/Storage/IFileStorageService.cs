using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Storage
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(UploadImageRequest file, string subfolder);
    }
}
