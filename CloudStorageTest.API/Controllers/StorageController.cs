using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorageTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : Controller
    {
        private readonly IUploadProfilePhotoUseCase _uploadProfilePhotoUseCase;
        public StorageController(IUploadProfilePhotoUseCase uploadProfilePhotoUseCase)
        {
            _uploadProfilePhotoUseCase = uploadProfilePhotoUseCase;
        }
        
        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            var sucess = _uploadProfilePhotoUseCase.Execute(file);
            if(sucess != "Completed") 
            {
                return BadRequest(sucess);
            }
            return Ok("Upload sucess");
        }
    }
}
