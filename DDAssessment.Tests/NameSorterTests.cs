using DDAssessment.FileStore;
using DDAssessment.Sorters;
using NSubstitute;
using Shouldly;

namespace DDAssessment.Tests
{
    public class NameSorterTests
    {
        private readonly IFileHandler _fileHandler;
        private readonly NameSorter _nameSorter;

        public NameSorterTests()
        {
            _fileHandler = Substitute.For<IFileHandler>();
            _nameSorter = new NameSorter(_fileHandler);
        }

        [Fact]
        public async Task SortNamesAsync_ShouldReturnSortedNames()
        {
            // Arrange
            var filePath = "test-file.txt";
            var names = await GetMockedContent();

            _fileHandler.GetFileAsync(filePath)
                .Returns(names);

            // Act
            var result = await _nameSorter.SortNamesAsync(filePath);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<IEnumerable<string>>();
            result.ShouldContain("Alice Smith");
            result.ShouldContain("Bob Johnson");
            result.ShouldContain("John Doe");
        }

        [Fact]
        public async Task GetSortedNamesAsync_ShouldReturnSortedNames()
        {
            // Arrange
            var sortedNames = await GetMockedContent();

            _fileHandler.GetFileAsync("sorted-names-list.txt")
                .Returns(sortedNames);

            // Act
            var result = await _nameSorter.GetSortedNamesAsync();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<IEnumerable<string>>();
            result.ShouldContain("Alice Smith");
            result.ShouldContain("Bob Johnson");
            result.ShouldContain("John Doe");

            await _fileHandler.Received(1).GetFileAsync("sorted-names-list.txt");
        }

        [Fact]
        public async Task SaveSortedNamesAsync_ShouldSaveSortedNamesToFile()
        {
            // Arrange
            var sortedNames = await GetMockedContent();
            var expectedContent = string.Join(Environment.NewLine, sortedNames);

            _fileHandler.SaveFileAsync("sorted-names-list.txt", expectedContent)
                .Returns(true);

            // Act
            var result = await _nameSorter.SaveSortedNamesAsync(sortedNames);

            // Assert
            result.ShouldBeTrue();
            await _fileHandler.Received(1).SaveFileAsync("sorted-names-list.txt", expectedContent);
        }

        private static Task<List<string>> GetMockedContent()
        {
            var list = new List<string> { "Alice Smith", "Bob Johnson", "John Doe", "John Ray Doe", "John Ray Mee Doe" };
            return Task.FromResult(list);
        }
    }
}
