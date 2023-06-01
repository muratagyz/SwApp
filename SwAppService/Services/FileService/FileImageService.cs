using Microsoft.AspNetCore.Http;

namespace SwAppService.Services.FileService;

public interface IFileImageService
{
    string GetImagePath(IFormFile formFile);
}

public class FileImageService : IFileImageService
{
    public string GetImagePath(IFormFile formFile)
    {
        if (formFile != null)
        {
            var extent = Path.GetExtension(formFile.FileName);
            var randomName = $"{Guid.NewGuid()}{extent}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", randomName);
            var dbpath = "/wwwroot/images/" + randomName;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyToAsync(stream);
            }

            return dbpath;
        }

        return null;
    }
}