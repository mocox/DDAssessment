// See https://aka.ms/new-console-template for more information

using DDAssessment.FileStore;
using DDAssessment.Sorters;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

Console.WriteLine("Dye & Durham Assessment");

string filePath = "unsorted-names-list.txt";
#if !DEBUG

    if (args.Length == 0) throw new ArgumentException("Please provide a file path");
    filepath = args[0];

#endif

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddScoped<IFileWrapper, FileWrapper>()
    .AddScoped<IFileHandler, FileHandler>()
    .AddScoped<INameSorter, NameSorter>()
    .BuildServiceProvider();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo
    .Console()
    .CreateLogger();

Log.Information("Starting application");

var nameSorter = serviceProvider.GetService<INameSorter>();
var lines = await nameSorter!.SortNamesAsync(filePath);

await nameSorter.SaveSortedNamesAsync(lines);

Console.WriteLine("Sorted File Contents");
Console.WriteLine("====================");
var sortedLines = await nameSorter.GetSortedNamesAsync();

foreach (var line in sortedLines)
{
    Console.WriteLine(line);
}
Console.WriteLine("====================");

Log.Information("Application finished");


Console.ReadKey();
