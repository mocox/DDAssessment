namespace DDAssessment.FileStore;

public interface IFileWrapper
{
    Task<IEnumerable<string>> ReadAllLinesAsync(string filePath);
    Task WriteAllTextAsync(string filePath, string fileContent);
}