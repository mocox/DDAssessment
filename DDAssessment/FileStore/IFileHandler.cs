namespace DDAssessment.FileStore;

public interface IFileHandler
{
    Task<bool> SaveFileAsync(string filePath, string fileContent);
    Task<IEnumerable<string>> GetFileAsync(string filePath);
}