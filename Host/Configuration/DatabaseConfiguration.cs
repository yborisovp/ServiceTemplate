namespace ServiceTemplate.Configuration;

public class DatabaseConfiguration
{
    public string DbUserName { get; set; } = string.Empty;
    public string DbPassword { get; set; } = string.Empty;
    public string DbConnection { get; set; } = string.Empty;

    public string FullConnectionString
        => MergeWithDelimiter(MergeWithDelimiter(DbConnection, $"Username={DbUserName}", ';'),
            $"Password={DbPassword}", ';');

    private string MergeWithDelimiter(string one, string another, char delimiter)
    {
        if (string.IsNullOrEmpty(one))
        {
            return another;
        }
        if (string.IsNullOrEmpty(another))
        {
            return one;
        }
        return $"{one.TrimEnd(delimiter)}{delimiter}{another.TrimStart(delimiter)}";
    }
}
