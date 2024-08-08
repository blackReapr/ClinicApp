namespace WebApplication2.Dtos.DepartmentDtos;

public class DepartmentReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Limit { get; set; }
    public string Image { get; set; }
    public List<DoctorInDepartmentReturnDto> Doctors { get; set; }
}

public class DoctorInDepartmentReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
}
