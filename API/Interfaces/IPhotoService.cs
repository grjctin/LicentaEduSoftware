using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        //IFormFile Represents a file sent with the HttpRequest
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}