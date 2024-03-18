
namespace DDAssessment.FileStore;

public class FileHandler(IFileWrapper file) : IFileHandler
{
    public async Task<bool> SaveFileAsync(string filePath, string fileContent)
    {
        try
        {
            await file.WriteAllTextAsync(filePath, fileContent);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<string>> GetFileAsync(string filePath)
    {
        try
        {
            return await file.ReadAllLinesAsync(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

