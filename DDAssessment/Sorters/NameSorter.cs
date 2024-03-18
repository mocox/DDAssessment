using DDAssessment.FileStore;
using DDAssessment.Models;
using Serilog;


namespace DDAssessment.Sorters;

public class NameSorter(IFileHandler fileHandler): INameSorter
{
    const string SortedNamesFilePath = "sorted-names-list.txt";

    public async Task<IEnumerable<string>> SortNamesAsync(string filePath)
    {
        var names = await fileHandler.GetFileAsync(filePath);
        List<FullNameModel> nameList = [];
        foreach (var name in names)
        {
            var model = await BuildModel(name);
            Log.Information($"Adding {model} to the list");
            nameList.Add(model);
        }

        nameList = [.. nameList.OrderBy(x=>x.LastName)
            .ThenBy(x=>x.FirstName)
            .ThenBy(x=>x.SecondName)
            .ThenBy(x=>x.ThirdName)];

        var items = nameList.Select(x=>x.ToString());
        return items;
    }

    public async Task<IEnumerable<string>> GetSortedNamesAsync()
    {
        return await fileHandler.GetFileAsync(SortedNamesFilePath);
    }

    public Task<bool> SaveSortedNamesAsync(IEnumerable<string> content)
    {
        var fileContent = string.Join(Environment.NewLine, content);

        return fileHandler.SaveFileAsync(SortedNamesFilePath, fileContent);
    }

    private static Task<FullNameModel> BuildModel(string name)
    {
        var nameArray = name.Split(" ");
        var firstNames = nameArray[..^1];
        var first = firstNames[0];
        var second = firstNames.Length > 1 ? firstNames[1]: string.Empty;
        var third = firstNames.Length > 2 ? firstNames[2]: string.Empty;
        var lastName = nameArray[^1];

        return Task.FromResult(new FullNameModel(first, second, third, lastName));
    }
}