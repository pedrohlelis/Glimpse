using Glimpse.Models;


namespace Glimpse.Helpers;

public class FileHandlingHelper
{
    public static bool CopyFileToPath(IFormFile file, string filePath)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }

    public static string VerifyUploadFolder(string folderName, IWebHostEnvironment hostingEnvironment)
    {
        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folderName);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        return uploadsFolder;
    }

    public static string UploadFile(IFormFile file, string folderName, IWebHostEnvironment hostingEnvironment) // returns the path of where the file was stored
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        string uploadsFolder = VerifyUploadFolder(folderName, hostingEnvironment);

        // Get the file name
        string fileName = Path.GetFileName(file.FileName);

        // Combine the file name with a unique identifier to ensure a unique file name
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        // Get the path where the file will be stored
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Copy the file to the target path
        CopyFileToPath(file, filePath);

        return Path.Combine("/", folderName, uniqueFileName).Replace("\\", "/");
    }
}
