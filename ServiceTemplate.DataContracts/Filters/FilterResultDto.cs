namespace ServiceTemplate.DataContracts.Filters;

public class FilterResultDto<TResult>
{
    public required IEnumerable<TResult> Results { get; set; }
    public required long Count { get; set; }
}