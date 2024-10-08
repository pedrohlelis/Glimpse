using GLIMPSE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace GLIMPSE.Domain.Helpers;

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
        catch (Exception)
        {
            return false;
        }
    }
    /*
    public static string VerifyUploadFolder(string folderName, IWebHostEnvironment hostingEnvironment)
    {
        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folderName);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        return uploadsFolder;
    }

    public static string? UploadFile(IFormFile file, string folderName, IWebHostEnvironment hostingEnvironment) // returns the path of where the file was stored
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        string uploadsFolder = VerifyUploadFolder(folderName, hostingEnvironment);

        string fileName = Path.GetFileName(file.FileName);

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        CopyFileToPath(file, filePath);

        return Path.Combine("/", folderName, uniqueFileName).Replace("\\", "/");
    }*/

    public static void DeleteFile(string folderPath, string fileName)
    {
        try
        {
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                Console.WriteLine($"File '{fileName}' does not exist in the specified folder.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
        }
    }
}
