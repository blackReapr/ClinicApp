namespace WebApplication2.Dtos;

public class PaginationDto<T>
{
    public int PageCount { get; set; }
    public List<T> items  { get; set; }
}
