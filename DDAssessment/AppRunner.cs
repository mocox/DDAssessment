using DDAssessment.Sorters;
using Serilog;

namespace DDAssessment;

public class AppRunner(INameSorter sorter) : IAppRunner
{
    public async Task RunAsync(string filePath)
    {
        Log.Information("Starting application");

        var lines = await sorter!.SortNamesAsync(filePath);

        await sorter.SaveSortedNamesAsync(lines);

        Console.WriteLine("Sorted File Contents");
        Console.WriteLine("====================");
        var sortedLines = await sorter.GetSortedNamesAsync();

        foreach (var line in sortedLines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine("====================");

        Log.Information("Application finished");
    }
}