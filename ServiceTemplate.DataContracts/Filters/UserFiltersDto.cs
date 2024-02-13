namespace ServiceTemplate.DataContracts.Filters;

public class UserFiltersDto
{
    public string? UserName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}