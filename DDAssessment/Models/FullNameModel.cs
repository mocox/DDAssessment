namespace DDAssessment.Models;

public record FullNameModel(string FirstName, string SecondName, string ThirdName, string LastName)
{
    public override string ToString()
    {
        if (!string.IsNullOrEmpty(ThirdName))
        {
            return $"{FirstName} {SecondName} {ThirdName} {LastName}";
        }

        return !string.IsNullOrEmpty(SecondName) ? $"{FirstName} {SecondName} {LastName}" : $"{FirstName} {LastName}";
    }
}