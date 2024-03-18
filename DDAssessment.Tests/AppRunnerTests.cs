using DDAssessment.Sorters;
using NSubstitute;
using Shouldly;
namespace DDAssessment.Tests;

public class AppRunnerTests
{
    [Fact]
    public async Task RunAsync_ShouldSortAndPrintNames()
    {
        // Arrange
        var filePath = "testFilePath";
        var sorter = Substitute.For<INameSorter>();
        var appRunner = new AppRunner(sorter);

        var sortedNames = new List<string> { "Alice Clements", "Bob Alan", "Terry Alan", "Charlie Brown" };
        sorter.SortNamesAsync(filePath)
            .Returns(sortedNames);
        sorter.GetSortedNamesAsync()
            .Returns(sortedNames);

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await appRunner.RunAsync(filePath);

        // Assert
        consoleOutput.ToString().ShouldContain("Sorted File Contents");
        consoleOutput.ToString().ShouldContain("====================");
        consoleOutput.ToString().ShouldContain("Alice Clements");
        consoleOutput.ToString().ShouldContain("Bob Alan");
        consoleOutput.ToString().ShouldContain("Charlie Brown");
        consoleOutput.ToString().ShouldContain("Terry Alan");
        consoleOutput.ToString().ShouldContain("====================");

        await sorter.Received(1).SortNamesAsync(filePath);
        await sorter.Received(1).SaveSortedNamesAsync(sortedNames);
        await sorter.Received(1).GetSortedNamesAsync();
    }
}