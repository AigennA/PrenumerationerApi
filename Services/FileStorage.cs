namespace PrenumerationerApi.Services;

public class FileStorage
{
    public const string RequestPath = "/uploads";

    public string FolderPath { get; }

    public FileStorage(IWebHostEnvironment environment)
    {
        FolderPath = Path.Combine(environment.ContentRootPath, "uploads");
        Directory.CreateDirectory(FolderPath);
    }

    public async Task<string> SaveAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(FolderPath, fileName);

        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);

        return $"{RequestPath}/{fileName}";
    }
}
