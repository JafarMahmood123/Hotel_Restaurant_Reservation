using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;
using Microsoft.AspNetCore.Hosting;

namespace Hotel_Restaurant_Reservation.Infrastructure.Storage;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> SaveFileAsync(UploadImageRequest file, string subfolder)
    {
        const string IMAGES_FOLDER = "images";

        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, IMAGES_FOLDER, subfolder);

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.Content.CopyToAsync(stream);
        }

        return $"/{IMAGES_FOLDER}/{subfolder}/{fileName}";
    }
}
