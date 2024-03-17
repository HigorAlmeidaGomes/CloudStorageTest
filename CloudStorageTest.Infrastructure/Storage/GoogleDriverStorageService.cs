using CloudStorageTest.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Infrastructure.Storage;
public class GoogleDriverStorageService : IStorageService
{
    private readonly GoogleAuthorizationCodeFlow _authorization;

    public GoogleDriverStorageService(GoogleAuthorizationCodeFlow authorization)
    {
            _authorization = authorization;
    }

    public string Upload(IFormFile file, Domain.Entities.User user)
    {
        try
        {
            var credential = new UserCredential(_authorization, user.Email, new Google.Apis.Auth.OAuth2.Responses.TokenResponse 
            {
                AccessToken = user.AccesToken,
                RefreshToken = user.RefreshToken
            });
            
            var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer {
                ApplicationName = "GoogleDriveTest",
                HttpClientInitializer = credential
            });

            var driveFile = new Google.Apis.Drive.v3.Data.File { Name = file.FileName, MimeType = file.ContentType };

            var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
            command.Fields = "id";

            var response = command.Upload();

            if(response.Status is not UploadStatus.Completed)
            {
                throw new Exception(response.Exception.ToString());
            }
            return response.Status.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
            throw;
        }
    }
}
