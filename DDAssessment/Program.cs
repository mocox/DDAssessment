// See https://aka.ms/new-console-template for more information

using DDAssessment;
using DDAssessment.FileStore;
using DDAssessment.Sorters;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

Console.WriteLine("Dye & Durham Assessment");

string filePath = "unsorted-names-list.txt";
#if !DEBUG

    if (args.Length == 0) {
        Console.WriteLine("File path missing. Please provide a file path.");
        return;
    }
    filePath = args[0];

#endif

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddScoped<IFileWrapper, FileWrapper>()
    .AddScoped<IFileHandler, FileHandler>()
    .AddScoped<INameSorter, NameSorter>()
    .AddScoped<IAppRunner,AppRunner>()
    .BuildServiceProvider();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo
    .Console()
    .CreateLogger();


var app = serviceProvider.GetService<IAppRunner>();
await app!.RunAsync(filePath);


Console.ReadKey();
