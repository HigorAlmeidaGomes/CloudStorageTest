using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
public interface IUploadProfilePhotoUseCase
{
    public string Execute(IFormFile file);
}
