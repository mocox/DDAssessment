namespace DDAssessment.Sorters;

public interface INameSorter
{
    Task<IEnumerable<string>> SortNamesAsync(string filePath);
    Task<bool> SaveSortedNamesAsync(IEnumerable<string> content);
    Task<IEnumerable<string>> GetSortedNamesAsync();
}