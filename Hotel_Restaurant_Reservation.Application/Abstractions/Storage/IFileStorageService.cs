using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Storage
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(UploadRestaurantImagesRequest file, string subfolder);
    }
}
