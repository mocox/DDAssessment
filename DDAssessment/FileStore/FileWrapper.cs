namespace DDAssessment.FileStore;

public class FileWrapper : IFileWrapper
{
    public async Task<IEnumerable<string>> ReadAllLinesAsync(string filePath)
    {
        return await File.ReadAllLinesAsync(filePath);
    }

    public async Task WriteAllTextAsync(string filePath, string fileContent)
    {
        await File.WriteAllTextAsync(filePath, fileContent);
    }
}