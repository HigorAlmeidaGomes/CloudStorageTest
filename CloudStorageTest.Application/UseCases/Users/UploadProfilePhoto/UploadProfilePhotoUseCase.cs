using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService;
    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
            _storageService = storageService;
    }
    public string Execute(IFormFile file)
    {
        try
        {
            var fileStream = file.OpenReadStream();

            if (fileStream.Is<JointPhotographicExpertsGroup>() == false)
            {
                throw new Exception("The file is not an image");
            }
            else
            {
                var retorno = _storageService.Upload(file, GetFromUserDataBase());
                return retorno;
            }
        }
        catch (Exception ex )
        {
            return ex.Message.ToString();
            throw;
        }
    }
    private User GetFromUserDataBase()
    {
        return new User { 
            Id = 1, 
            Email = "higorgto@hotmail.com", 
            Name = "Higor almeida gomes",
            RefreshToken = "1//04ABvB1_E7uAKCgYIARAAGAQSNwF-L9Ird72KWG40Ofa-MZytgND1O3WLC7HQPy6amFRmm5hCO8m90iZpJ_QLgBIxdmRIjnB4DG8",
            AccesToken = "ya29.a0Ad52N39jguTsV9OzeRVGvWhDGZToNnH5vk0ZDMWSXWy2W6E3iMn3mq0ZSWLCwhksQUpOzNcKLSHjmO9JndABpnKnyN_5QaBIQW8S-hDxGV7Y5b92Enw6MTtmLGX3b2MwomxY_BQKNwVfndXKNk8oQ1zXH5yPcCzV-5ibaCgYKATcSARASFQHGX2MiICdvReiOxFCt72BMzJQv4A0171"
        };
    }
}
