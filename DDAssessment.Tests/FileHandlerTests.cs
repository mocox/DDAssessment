using DDAssessment.FileStore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;

namespace DDAssessment.Tests;

public class FileHandlerTests
{
    private readonly IFileHandler _fileHandler;
    private readonly string _filePath = "test.txt";
    private readonly IFileWrapper _file;

    public FileHandlerTests()
    {
        _file = Substitute.For<IFileWrapper>();
        _fileHandler = new FileHandler(_file);
    }

    [Fact]
    public async Task GetFileAsync_WhenFileExists_ReturnsFileContent()
    {
        // Arrange
        var fileContent = await GetMockedContent();

        _file.ReadAllLinesAsync(_filePath)
            .Returns(fileContent);

        // Act
        var result = await _fileHandler.GetFileAsync(_filePath);

        // Assert
        result.ShouldBe(fileContent);

        await _file.Received(1).ReadAllLinesAsync(_filePath);
    }

    [Fact]
    public async Task GetFileAsync_WhenFileDoesNotExist_ThrowsException()
    {
        // Arrange
        _file.ReadAllLinesAsync(_filePath)
            .Throws(new Exception("File not found"));

        // Act & Assert
        await Should.ThrowAsync<Exception>(async () => await _fileHandler.GetFileAsync(_filePath));
    }

    [Fact]
    public async Task SaveFileAsync_WhenFileContentIsSaved_ReturnsTrue()
    {
        // Arrange
        var fileContent = string.Join(Environment.NewLine, GetMockedContent());

        _file.WriteAllTextAsync(_filePath, fileContent)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _fileHandler.SaveFileAsync(_filePath, fileContent);

        // Assert
        result.ShouldBeTrue();

        await _file.Received(1).WriteAllTextAsync(_filePath, fileContent);
    }

    [Fact]
    public async Task SaveFileAsync_WhenFileContentIsNotSaved_ReturnsFalse()
    {
        // Arrange
        var fileContent = string.Join(Environment.NewLine, GetMockedContent());

        _file.When(x => x.WriteAllTextAsync(_filePath, fileContent))
            .Do(x => throw new Exception("Failed to save file"));

        // Act
        var result = await _fileHandler.SaveFileAsync(_filePath, fileContent);

        // Assert
        result.ShouldBeFalse();
    }


    private static Task<List<string>> GetMockedContent()
    {
        var list = new List<string>
        {
            "Tom One",
            "Tom Two",
            "Tom Three"
        };
        return Task.FromResult(list);
    }
}